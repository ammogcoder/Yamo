﻿Imports System.Data
Imports System.Linq.Expressions
Imports System.Reflection
Imports Yamo.Internal
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata
Imports Yamo.Metadata

Namespace Infrastructure

  ''' <summary>
  ''' Custom result reader factory.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class CustomResultReaderFactory
    Inherits ReaderFactoryBase

    ''' <summary>
    ''' Creates result factory.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="customEntities"></param>
    ''' <returns></returns>
    Public Shared Function CreateResultFactory(node As Expression, customEntities As CustomSqlEntity()) As Object
      If node.NodeType = ExpressionType.New Then
        ' ValueTuple or anonymous type
        Return CreateResultFactory(customEntities, DirectCast(node, NewExpression).Constructor)
      Else
        ' single value or entity
        Return CreateResultFactory(customEntities, Nothing)
      End If
    End Function

    ''' <summary>
    ''' Creates result factory.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="resultType"></param>
    ''' <returns></returns>
    Public Shared Function CreateResultFactory(resultType As Type) As Object
      Dim resultConstructorInfo As ConstructorInfo = Nothing
      Dim nullableValueConstructorInfo As ConstructorInfo = Nothing
      Dim customEntities As CustomSqlEntity()

      ' NOTE: right now this should only be called from Query/QueryFirstOrDefault and only following types are supported:
      ' - nullable and non-nullable ValueTuples: with basic value-types or model entities as a field value
      ' - model entities

      Dim underlyingNullableType = Nullable.GetUnderlyingType(resultType)

      If underlyingNullableType Is Nothing Then
        If resultType.IsGenericType Then
          ' ValueTuple
          Dim args = resultType.GetGenericArguments()
          resultConstructorInfo = resultType.GetConstructor(args)
          customEntities = args.Select(AddressOf CreateCustomSqlEntity).ToArray()
        Else
          ' entity model
          customEntities = {CreateCustomSqlEntity(resultType, 0)}
        End If

      Else
        resultConstructorInfo = resultType.GetConstructor(resultType.GetGenericArguments())

        If underlyingNullableType.IsGenericType Then
          ' nullable ValueTuple
          Dim args = underlyingNullableType.GetGenericArguments()
          nullableValueConstructorInfo = underlyingNullableType.GetConstructor(args)
          customEntities = args.Select(AddressOf CreateCustomSqlEntity).ToArray()
        Else
          Throw New NotSupportedException($"Type '{resultType}' is not supported.")
        End If
      End If

      Return CreateResultFactory(customEntities, resultConstructorInfo, nullableValueConstructorInfo)
    End Function

    ''' <summary>
    ''' Creates instance of <see cref="CustomSqlEntity"/>.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Private Shared Function CreateCustomSqlEntity(type As Type, index As Int32) As CustomSqlEntity
      If Helpers.Types.IsProbablyModel(type) Then
        ' If type is not defined in model, there will be an exception thrown later from
        ' CustomEntityReadInfo.CreateForGenericType() or CustomEntityReadInfo.CreateForModelType().
        ' We could check it also here, but it would be slower ;)
        ' Also, we don't need (and don't know) entityIndex, so it's set to -1 in this scenario.
        Return New CustomSqlEntity(index, -1, type)
      Else
        Return New CustomSqlEntity(index, type)
      End If
    End Function

    ''' <summary>
    ''' Creates result factory.
    ''' </summary>
    ''' <param name="customEntities"></param>
    ''' <param name="resultConstructorInfo"></param>
    ''' <param name="nullableValueConstructorInfo"></param>
    ''' <returns></returns>
    Private Shared Function CreateResultFactory(customEntities As CustomSqlEntity(), Optional resultConstructorInfo As ConstructorInfo = Nothing, Optional nullableValueConstructorInfo As ConstructorInfo = Nothing) As Object
      Dim readerParam = Expression.Parameter(GetType(IDataReader), "reader") ' this has to be IDataRecord, otherwise Expression.Call() cannot find the method
      Dim customEntityInfosParam = Expression.Parameter(GetType(CustomEntityReadInfo()), "customEntityInfos")
      Dim parameters = {readerParam, customEntityInfosParam}

      Dim variables = New List(Of ParameterExpression)
      Dim expressions = New List(Of Expression)

      Dim arguments = New List(Of Expression)

      For i = 0 To customEntities.Length - 1
        Dim customEntity = customEntities(i)
        Dim type = customEntity.Type

        Dim valueVar = Expression.Variable(type, $"value{i.ToString(Globalization.CultureInfo.InvariantCulture)}")
        variables.Add(valueVar)

        Dim customEntityInfoVar = Expression.Variable(GetType(CustomEntityReadInfo), $"readInfo{i.ToString(Globalization.CultureInfo.InvariantCulture)}")
        variables.Add(customEntityInfoVar)

        Dim customEntityInfoIndex = Expression.ArrayIndex(customEntityInfosParam, Expression.Constant(i))
        expressions.Add(Expression.Assign(customEntityInfoVar, customEntityInfoIndex))

        Dim readerIndexProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.ReaderIndex))

        If customEntity.IsEntity Then
          Dim entityProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.Entity))
          Dim includedColumnsProp = Expression.Property(entityProp, NameOf(SqlEntity.IncludedColumns))
          Dim pkOffsetsProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.PKOffsets))

          Dim containsPKReaderProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.ContainsPKReader))
          Dim containsPKReaderType = GetType(Func(Of, , ,)).MakeGenericType(GetType(IDataReader), GetType(Int32), GetType(Int32()), GetType(Boolean))
          Dim containsPKReaderInvokeMethodInfo = containsPKReaderType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance)
          Dim containsPKReaderCall = Expression.Call(containsPKReaderProp, containsPKReaderInvokeMethodInfo, readerParam, readerIndexProp, pkOffsetsProp)

          Dim entityReaderProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.EntityReader))
          Dim entityReaderType = GetType(Func(Of, , ,)).MakeGenericType(GetType(IDataReader), GetType(Int32), GetType(Boolean()), GetType(Object))
          Dim entityReaderInvokeMethodInfo = entityReaderType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance)
          Dim entityReaderCall = Expression.Call(entityReaderProp, entityReaderInvokeMethodInfo, readerParam, readerIndexProp, includedColumnsProp)
          Dim entityReaderCallCast = Expression.Convert(entityReaderCall, type)

          Dim valueAssign = Expression.Assign(valueVar, entityReaderCallCast)
          Dim valueAssignNull = Expression.Assign(valueVar, Expression.Default(type))

          Dim valueAssignBlock As Expression

          Dim ihpmtType = GetType(IHasDbPropertyModifiedTracking)
          If ihpmtType.IsAssignableFrom(type) Then
            Dim rpmtMethodInfo = ihpmtType.GetMethod(NameOf(IHasDbPropertyModifiedTracking.ResetDbPropertyModifiedTracking))
            Dim rpmtCast = Expression.Convert(valueVar, ihpmtType)
            Dim rpmtCall = Expression.Call(rpmtCast, rpmtMethodInfo)

            valueAssignBlock = Expression.Block(valueAssign, rpmtCall)
          Else
            valueAssignBlock = valueAssign
          End If

          expressions.Add(Expression.IfThenElse(containsPKReaderCall, valueAssignBlock, valueAssignNull))
        Else
          Dim valueTypeReaderProp = Expression.Property(customEntityInfoVar, NameOf(CustomEntityReadInfo.ValueTypeReader))
          Dim valueTypeReaderType = GetType(Func(Of, , )).MakeGenericType(GetType(IDataReader), GetType(Int32), type)
          Dim valueTypeReaderCast = Expression.Convert(valueTypeReaderProp, valueTypeReaderType)
          Dim valueTypeReaderInvokeMethodInfo = valueTypeReaderType.GetMethod("Invoke", BindingFlags.Public Or BindingFlags.Instance)
          Dim valueTypeReaderCall = Expression.Call(valueTypeReaderCast, valueTypeReaderInvokeMethodInfo, readerParam, readerIndexProp)

          Dim valueAssign = Expression.Assign(valueVar, valueTypeReaderCall)
          expressions.Add(valueAssign)
        End If

        arguments.Add(valueVar)
      Next

      If resultConstructorInfo Is Nothing Then
        ' single value or entity
        expressions.Add(variables(0))
      Else
        If nullableValueConstructorInfo Is Nothing Then
          ' ValueTuple or anonymous type
          Dim newExp = Expression.[New](resultConstructorInfo, arguments)
          expressions.Add(newExp)
        Else
          ' nullable ValueTuple
          Dim newExp = Expression.[New](resultConstructorInfo, Expression.[New](nullableValueConstructorInfo, arguments))
          expressions.Add(newExp)
        End If
      End If

      Dim body = Expression.Block(variables, expressions)

      Dim reader = Expression.Lambda(body, parameters)
      Return reader.Compile()
    End Function

  End Class
End Namespace