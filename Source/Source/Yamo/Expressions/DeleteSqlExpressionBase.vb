﻿Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ''' <summary>
  ''' Base class for SQL DELETE statements.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public MustInherit Class DeleteSqlExpressionBase
    Inherits SqlExpressionBase

    ''' <summary>
    ''' Gets context.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    Protected ReadOnly DbContext As DbContext

    ''' <summary>
    ''' Gets builder.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Protected Property Builder As DeleteSqlExpressionBuilder

    ''' <summary>
    ''' Gets query executor.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <returns></returns>
    Protected Property Executor As QueryExecutor

    Friend Sub New(context As DbContext, builder As DeleteSqlExpressionBuilder, executor As QueryExecutor)
      Me.DbContext = context
      Me.Builder = builder
      Me.Executor = executor
    End Sub

  End Class
End Namespace