﻿Imports Yamo.Expressions.Builders
Imports Yamo.Internal.Query

Namespace Expressions

  ' TODO: SIP - add documentation to this class.
  Public Class FilteredDeleteSqlExpression(Of T)
    Inherits DeleteSqlExpressionBase

    ''' <summary>
    ''' Creates new instance of <see cref="FilteredDeleteSqlExpression(Of T)"/>.
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="builder"></param>
    ''' <param name="executor"></param>
    Friend Sub New(context As DbContext, builder As DeleteSqlExpressionBuilder, executor As QueryExecutor)
      MyBase.New(context, builder, executor)
    End Sub

    Public Function Execute() As Int32
      Dim query = Me.Builder.CreateQuery()
      Return Me.Executor.Execute(query)
    End Function

  End Class
End Namespace