﻿Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  Public Class FilteredUpdateSqlExpression(Of T)
    Inherits UpdateSqlExpressionBase

    Friend Sub New(context As DbContext, builder As UpdateSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(context, builder, executor)
    End Sub

    Public Function Execute() As Int32
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.ExecuteNonQuery(query)
    End Function

  End Class
End Namespace