﻿Namespace Generator

  Public Class SelectSqlExpressionCodeGenerator
    Inherits CodeGenerator

    Public Sub New(indentation As String, maxEntityCount As Int32, outputFolder As String)
      MyBase.New(indentation, maxEntityCount, outputFolder)
    End Sub

    Protected Overrides Function GetClassName() As String
      Return "SelectSqlExpression"
    End Function

    Protected Overrides Sub Generate(builder As CodeBuilder, entityCount As Int32)
      builder.Indent().AppendLine("Imports System.Linq.Expressions")
      builder.Indent().AppendLine("Imports Yamo.Expressions.Builders")
      builder.Indent().AppendLine("Imports Yamo.Internal.Query")
      builder.Indent().AppendLine("Imports Yamo.Internal.Query.Metadata")
      builder.AppendLine()
      builder.Indent().AppendLine("Namespace Expressions").PushIndent()
      builder.AppendLine()

      Dim comment As String
      Dim typeParams As String()

      If entityCount = 1 Then
        comment = "Represents SQL SELECT statement from one table (entity)."
        typeParams = GetGenericNames(entityCount)
      Else
        comment = $"Represents SQL SELECT statement from {entityCount.ToInvariantString()} tables (entities)."
        typeParams = GetGenericNames(entityCount)

      End If
      AddComment(builder, comment, typeParams:=typeParams)

      builder.Indent().AppendLine($"Public Class {GetFullClassName(entityCount)}").PushIndent()
      builder.Indent().AppendLine("Inherits SelectSqlExpressionBase")
      builder.AppendLine()
      GenerateConstructor(builder, entityCount)
      builder.AppendLine()

      If Not entityCount = Me.MaxEntityCount Then
        GenerateJoin(builder, entityCount)
        builder.AppendLine()
      End If

      GenerateWhere(builder, entityCount)
      builder.AppendLine()

      GenerateGroupBy(builder, entityCount)
      builder.AppendLine()

      GenerateOrderBy(builder, entityCount)
      builder.AppendLine()

      GenerateLimit(builder, entityCount)
      builder.AppendLine()

      GenerateSelect(builder, entityCount)
      builder.AppendLine()

      GenerateIf(builder, entityCount)
      builder.AppendLine()

      builder.PopIndent()
      builder.Indent().AppendLine($"End Class").PopIndent()
      builder.Indent().AppendLine("End Namespace")
    End Sub

    Private Sub GenerateConstructor(builder As CodeBuilder, entityCount As Int32)
      If entityCount = 1 Then
        Dim comment = $"Creates new instance of <see cref=""{GetFullClassName(entityCount)}""/>."
        Dim params = {"context"}
        AddComment(builder, comment, params:=params)

        builder.Indent().AppendLine("Friend Sub New(context As DbContext)").PushIndent()
        builder.Indent().AppendLine("MyBase.New(New SelectSqlExpressionBuilder(context), New QueryExecutor(context))")
        builder.Indent().AppendLine("Me.Builder.SetMainTable(Of T)()").PopIndent()
        builder.Indent().AppendLine("End Sub")
      Else
        Dim comment = $"Creates new instance of <see cref=""{GetFullClassName(entityCount)}""/>."
        Dim params = {"builder", "executor"}
        AddComment(builder, comment, params:=params)

        builder.Indent().AppendLine("Friend Sub New(builder As SelectSqlExpressionBuilder, executor As QueryExecutor)").PushIndent()
        builder.Indent().AppendLine("MyBase.New(builder, executor)").PopIndent()
        builder.Indent().AppendLine("End Sub")
      End If
    End Sub

  End Class
End Namespace