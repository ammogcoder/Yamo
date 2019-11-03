﻿Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SET statement in UPDATE SQL expression.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  Public Class SetUpdateSqlExpression(Of T)
    Inherits UpdateSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="SetUpdateSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(context As DbContext, builder As UpdateSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(context, builder, executor)
    End Sub

    ''' <summary>
    ''' Adds SET statement.
    ''' </summary>
    ''' <param name="action"></param>
    ''' <returns></returns>
    Public Function [Set](action As Expression(Of Action(Of T))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(action)
      Return Me
    End Function

    ''' <summary>
    ''' Adds SET statement.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function [Set](Of TProperty)(keySelector As Expression(Of Func(Of T, TProperty)), value As TProperty) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(keySelector, value)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SET statement.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <param name="valueSelector"></param>
    ''' <returns></returns>
    Public Function [Set](Of TProperty)(keySelector As Expression(Of Func(Of T, TProperty)), valueSelector As Expression(Of Func(Of T, TProperty))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(keySelector, valueSelector)
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds SET statement.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [Set](predicate As Expression(Of Func(Of T, FormattableString))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(predicate)
      Return Me
    End Function

    ''' <summary>
    ''' Adds SET statement.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function [Set](predicate As String) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(predicate)
      Return Me
    End Function

    ''' <summary>
    ''' Adds SET null statement.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="keySelector"></param>
    ''' <returns></returns>
    Public Function SetNull(Of TProperty)(keySelector As Expression(Of Func(Of T, TProperty))) As SetUpdateSqlExpression(Of T)
      Me.Builder.AddSet(keySelector, DirectCast(Nothing, Object))
      Return New SetUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE statement.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T, Boolean))) As FilteredUpdateSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE statement.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As Expression(Of Func(Of T, FormattableString))) As FilteredUpdateSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Adds WHERE statement.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Where(predicate As String) As FilteredUpdateSqlExpression(Of T)
      Me.Builder.AddWhere(predicate)
      Return New FilteredUpdateSqlExpression(Of T)(Me.DbContext, Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Executes an UPDATE statement.
    ''' </summary>
    ''' <returns></returns>
    Public Function Execute() As Int32
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.Execute(query)
    End Function

  End Class
End Namespace