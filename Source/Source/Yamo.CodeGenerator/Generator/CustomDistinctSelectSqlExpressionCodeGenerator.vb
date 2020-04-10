﻿Namespace Generator

  Public Class CustomDistinctSelectSqlExpressionCodeGenerator
    Inherits CodeGenerator

    Public Sub New(indentation As String, outputFolder As String)
      MyBase.New(indentation, 1, outputFolder)
    End Sub

    Protected Overrides Function GetFilename(entityCount As Int32) As String
      Return $"CustomDistinctSelectSqlExpression{GetGenericsSuffixForFilename(entityCount)}.vb"
    End Function

    Protected Overrides Sub Generate(builder As CodeBuilder, entityCount As Int32)
      builder.Indent().AppendLine("Imports System.Linq.Expressions")
      builder.Indent().AppendLine("Imports Yamo.Expressions.Builders")
      builder.Indent().AppendLine("Imports Yamo.Internal.Query")
      builder.AppendLine()
      builder.Indent().AppendLine("Namespace Expressions").PushIndent()
      builder.AppendLine()

      Dim comment = "Represents DISTINCT clause in SQL SELECT statement."
      Dim typeParams = GetGenericNames(entityCount)
      AddComment(builder, comment, typeParams:=typeParams)

      builder.Indent().AppendLine($"Public Class CustomDistinctSelectSqlExpression{GetGenericOfDefinition(entityCount)}").PushIndent()
      builder.Indent().AppendLine("Inherits SelectSqlExpressionBase")
      builder.AppendLine()
      GenerateConstructor(builder, entityCount)
      builder.AppendLine()

      GenerateCustomToList(builder, entityCount)
      builder.AppendLine()

      GenerateCustomFirstOrDefault(builder, entityCount)
      builder.AppendLine()

      builder.PopIndent()
      builder.Indent().AppendLine($"End Class").PopIndent()
      builder.Indent().AppendLine("End Namespace")
    End Sub

    Private Sub GenerateConstructor(builder As CodeBuilder, entityCount As Int32)
      Dim comment = $"Creates new instance of <see cref=""CustomDistinctSelectSqlExpression{GetGenericOfDefinition(entityCount)}""/>."
      Dim params = {"builder", "executor"}
      AddComment(builder, comment, params:=params)

      builder.Indent().AppendLine("Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)").PushIndent()
      builder.Indent().AppendLine("MyBase.New(builder, executor)").PopIndent()
      builder.Indent().AppendLine("End Sub")
    End Sub

  End Class
End Namespace