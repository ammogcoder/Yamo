﻿Imports System.Linq.Expressions
Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query
Imports Yamo.Internal.Query.Metadata

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
    Inherits SelectSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="SelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)"/>.
    ''' </summary>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(builder, executor)
    End Sub

    Public Function Join(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.Inner, predicate, Nothing)
    End Function

    Public Function LeftJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.LeftOuter, predicate, Nothing)
    End Function

    Public Function RightJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.RightOuter, predicate, Nothing)
    End Function

    Public Function FullJoin(Of TJoined)(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoined), Boolean))) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.FullOuter, predicate, Nothing)
    End Function

    Public Function CrossJoin(Of TJoined)() As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoined)
      Return InternalJoin(Of TJoined)(JoinType.CrossJoin, Nothing, Nothing)
    End Function

    Private Function InternalJoin(Of TJoined)(joinType As JoinType, predicate As Expression, entityIndexHints As Int32()) As JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoined)
      Me.Builder.AddJoin(Of TJoined)(joinType, predicate, entityIndexHints)
      Return New JoinedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TJoined)(Me.Builder, Me.Executor)
    End Function

    Public Function Where(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12), Boolean))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
      Return InternalWhere(predicate, Nothing)
    End Function

    Public Function Where(predicate As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12), FormattableString))) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
      Return InternalWhere(predicate, Nothing)
    End Function

    Public Function Where(predicate As String) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
      Me.Builder.AddWhere(predicate)
      Return New FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)(Me.Builder, Me.Executor)
    End Function

    Public Function Where() As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
      Return New FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)(Me.Builder, Me.Executor)
    End Function

    Private Function InternalWhere(predicate As Expression, entityIndexHints As Int32()) As FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
      Me.Builder.AddWhere(predicate, entityIndexHints)
      Return New FilteredSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)(Me.Builder, Me.Executor)
    End Function

    Public Function GroupBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12), TKey))) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
      Return InternalGroupBy(Of TKey)(keySelector, Nothing)
    End Function

    Private Function InternalGroupBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32()) As GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
      Me.Builder.AddGroupBy(keySelector, entityIndexHints)
      Return New GroupedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)(Me.Builder, Me.Executor)
    End Function

    Public Function OrderBy(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, True)
    End Function

    Public Function OrderByDescending(Of TKey)(keySelector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12), TKey))) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
      Return InternalOrderBy(Of TKey)(keySelector, Nothing, False)
    End Function

    Private Function InternalOrderBy(Of TKey)(keySelector As Expression, entityIndexHints As Int32(), ascending As Boolean) As OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
      Me.Builder.AddOrderBy(keySelector, entityIndexHints, ascending)
      Return New OrderedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectAll() As SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)
      Me.Builder.AddSelectAll(GetType(T1), GetType(T2), GetType(T3), GetType(T4), GetType(T5), GetType(T6), GetType(T7), GetType(T8), GetType(T9), GetType(T10), GetType(T11), GetType(T12))
      Return New SelectedSelectSqlExpression(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)(Me.Builder, Me.Executor)
    End Function

    Public Function SelectCount() As Int32
      Me.Builder.AddSelectCount()
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.QueryFirstOrDefault(Of Int32)(query)
    End Function

    Public Function [Select](Of TResult)(selector As Expression(Of Func(Of Join(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12), TResult))) As CustomSelectSqlExpression(Of TResult)
      Return InternalSelect(Of TResult)(selector, Nothing)
    End Function

    Private Function InternalSelect(Of TResult)(selector As Expression, entityIndexHints As Int32()) As CustomSelectSqlExpression(Of TResult)
      Me.Builder.AddSelect(selector, entityIndexHints)
      Return New CustomSelectSqlExpression(Of TResult)(Me.Builder, Me.Executor)
    End Function

  End Class
End Namespace
