﻿Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Represents SQL SELECT statement from 7 tables (entities).
  ''' </summary>
  ''' <typeparam name="T1"></typeparam>
  ''' <typeparam name="T2"></typeparam>
  ''' <typeparam name="T3"></typeparam>
  ''' <typeparam name="T4"></typeparam>
  ''' <typeparam name="T5"></typeparam>
  ''' <typeparam name="T6"></typeparam>
  ''' <typeparam name="T7"></typeparam>
  Public Class JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
    Inherits SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)

    ''' <summary>
    ''' Creates new instance of <see cref="JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of T1, TProperty))) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalAs(relationship)
    End Function

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of T2, TProperty))) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalAs(relationship)
    End Function

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of T3, TProperty))) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalAs(relationship)
    End Function

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of T4, TProperty))) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalAs(relationship)
    End Function

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of T5, TProperty))) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalAs(relationship)
    End Function

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of T6, TProperty))) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalAs(relationship)
    End Function

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <typeparam name="TProperty"></typeparam>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Public Function [As](Of TProperty)(relationship As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6), TProperty))) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Return InternalAs(relationship)
    End Function

    ''' <summary>
    ''' Specifies last joined entity.
    ''' </summary>
    ''' <param name="relationship"></param>
    ''' <returns></returns>
    Private Function InternalAs(relationship As Expression) As SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)
      Me.Builder.SetLastJoinRelationship(relationship)
      Return New SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7)(Me.Builder, Me.Executor)
    End Function

    ''' <summary>
    ''' Conditionally builds the expression.
    ''' </summary>
    ''' <typeparam name="TResult"></typeparam>
    ''' <param name="condition"></param>
    ''' <param name="[then]"></param>
    ''' <param name="otherwise"></param>
    ''' <returns></returns>
    Public Overloads Function [If](Of TResult)(condition As Boolean, [then] As Func(Of JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7), TResult), Optional otherwise As Func(Of JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7), TResult) = Nothing) As TResult
      Dim result As TResult

      If condition Then
        result = [then].Invoke(Me)
      ElseIf otherwise Is Nothing Then
        Me.Builder.StartConditionalIgnoreMode()
        result = [then].Invoke(Me)
        Me.Builder.EndConditionalIgnoreMode()
      Else
        result = otherwise.Invoke(Me)
      End If

      Return result
    End Function

  End Class
End Namespace
