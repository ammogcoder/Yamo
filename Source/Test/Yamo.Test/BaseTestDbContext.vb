﻿Imports Yamo.Test.Model

Public Class BaseTestDbContext
  Inherits DbContext

  Public Property UserId As Int32

  Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
    CreateArticleModel(modelBuilder)
    CreateArticleCategoryModel(modelBuilder)
    CreateArticlePartModel(modelBuilder)
    CreateArticleSubstitutionModel(modelBuilder)
    CreateCategoryModel(modelBuilder)
    CreateItemWithAllSupportedValuesModel(modelBuilder)
    CreateItemWithAuditFieldsModel(modelBuilder)
    CreateItemWithDefaultValueIdModel(modelBuilder)
    CreateItemWithIdentityIdModel(modelBuilder)
    CreateItemWithIdentityIdAndDefaultValuesModel(modelBuilder)
    CreateItemWithPropertyModifiedTrackingModel(modelBuilder)
    CreateLabelModel(modelBuilder)
    CreateLinkedItemModel(modelBuilder)
    CreateLinkedItemWithShuffledPropertiesModel(modelBuilder)
    CreateLinkedItemChildModel(modelBuilder)
  End Sub

  Private Sub CreateArticleModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of Article)()

    modelBuilder.Entity(Of Article).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of Article).Property(Function(x) x.Price)

    modelBuilder.Entity(Of Article).HasOne(Function(x) x.Label)
    modelBuilder.Entity(Of Article).HasMany(Function(x) x.Parts)
    modelBuilder.Entity(Of Article).HasMany(Function(x) x.Categories)
  End Sub

  Private Sub CreateArticleCategoryModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ArticleCategory)()

    modelBuilder.Entity(Of ArticleCategory).Property(Function(x) x.ArticleId).IsKey()
    modelBuilder.Entity(Of ArticleCategory).Property(Function(x) x.CategoryId).IsKey()
  End Sub

  Private Sub CreateArticlePartModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ArticlePart)()

    modelBuilder.Entity(Of ArticlePart).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of ArticlePart).Property(Function(x) x.ArticleId)
    modelBuilder.Entity(Of ArticlePart).Property(Function(x) x.Price)

    modelBuilder.Entity(Of ArticlePart).HasOne(Function(x) x.Label)
  End Sub

  Private Sub CreateArticleSubstitutionModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ArticleSubstitution)()

    modelBuilder.Entity(Of ArticleSubstitution).Property(Function(x) x.OriginalArticleId).IsKey()
    modelBuilder.Entity(Of ArticleSubstitution).Property(Function(x) x.SubstitutionArticleId).IsKey()

    modelBuilder.Entity(Of ArticleSubstitution).HasOne(Function(x) x.Original)
    modelBuilder.Entity(Of ArticleSubstitution).HasOne(Function(x) x.Substitution)
  End Sub

  Private Sub CreateCategoryModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of Category)()

    modelBuilder.Entity(Of Category).Property(Function(x) x.Id).IsKey()

    modelBuilder.Entity(Of Category).HasOne(Function(x) x.Label)
  End Sub

  Private Sub CreateItemWithAllSupportedValuesModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithAllSupportedValues)()

    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.UniqueidentifierColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.UniqueidentifierColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Nvarchar50Column).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Nvarchar50ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.NvarcharMaxColumn).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.NvarcharMaxColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.BitColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.BitColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.SmallintColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.SmallintColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.IntColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.IntColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.BigintColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.BigintColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.RealColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.RealColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.FloatColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.FloatColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Numeric10and3Column)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Numeric10and3ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Numeric15and0Column)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Numeric15and0ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.DatetimeColumn)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.DatetimeColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Varbinary50Column).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.Varbinary50ColumnNull)
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.VarbinaryMaxColumn).IsRequired()
    modelBuilder.Entity(Of ItemWithAllSupportedValues).Property(Function(x) x.VarbinaryMaxColumnNull)
  End Sub

  Private Sub CreateItemWithAuditFieldsModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithAuditFields)()

    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.Created).SetOnInsertTo(Function() Helpers.Calendar.Now)
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.CreatedUserId).SetOnInsertTo(Function(c As BaseTestDbContext) c.UserId)
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.Modified).SetOnUpdateTo(Function() Helpers.Calendar.Now)
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.ModifiedUserId).SetOnUpdateTo(Function(c As BaseTestDbContext) c.UserId)
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.Deleted).SetOnDeleteTo(Function() Helpers.Calendar.Now)
    modelBuilder.Entity(Of ItemWithAuditFields).Property(Function(x) x.DeletedUserId).SetOnDeleteTo(Function(c As BaseTestDbContext) c.UserId)
  End Sub

  Private Sub CreateItemWithDefaultValueIdModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithDefaultValueId)()

    modelBuilder.Entity(Of ItemWithDefaultValueId).Property(Function(x) x.Id).IsKey().HasDefaultValue()
    modelBuilder.Entity(Of ItemWithDefaultValueId).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateItemWithIdentityIdModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithIdentityId)()

    modelBuilder.Entity(Of ItemWithIdentityId).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithIdentityId).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateItemWithIdentityIdAndDefaultValuesModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValues)()

    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValues).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValues).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValues).Property(Function(x) x.UniqueidentifierValue).HasDefaultValue()
    modelBuilder.Entity(Of ItemWithIdentityIdAndDefaultValues).Property(Function(x) x.IntValue).HasDefaultValue()
  End Sub

  Private Sub CreateItemWithPropertyModifiedTrackingModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of ItemWithPropertyModifiedTracking)()

    modelBuilder.Entity(Of ItemWithPropertyModifiedTracking).Property(Function(x) x.Id).IsKey().IsIdentity()
    modelBuilder.Entity(Of ItemWithPropertyModifiedTracking).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of ItemWithPropertyModifiedTracking).Property(Function(x) x.IntValue)
  End Sub

  Private Sub CreateLabelModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of Label)()

    modelBuilder.Entity(Of Label).Property(Function(x) x.TableId).IsKey().IsRequired()
    modelBuilder.Entity(Of Label).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of Label).Property(Function(x) x.Language).IsKey().IsRequired()
    modelBuilder.Entity(Of Label).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateLinkedItemModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of LinkedItem)()

    modelBuilder.Entity(Of LinkedItem).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of LinkedItem).Property(Function(x) x.PreviousId)
    modelBuilder.Entity(Of LinkedItem).Property(Function(x) x.Description).IsRequired()
  End Sub

  Private Sub CreateLinkedItemWithShuffledPropertiesModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of LinkedItemWithShuffledProperties).ToTable("LinkedItem")

    ' properties are shuffled and PK property is defined last
    modelBuilder.Entity(Of LinkedItemWithShuffledProperties).Property(Function(x) x.Description).IsRequired()
    modelBuilder.Entity(Of LinkedItemWithShuffledProperties).Property(Function(x) x.PreviousId)
    modelBuilder.Entity(Of LinkedItemWithShuffledProperties).Property(Function(x) x.Id).IsKey()
  End Sub

  Private Sub CreateLinkedItemChildModel(modelBuilder As ModelBuilder)
    modelBuilder.Entity(Of LinkedItemChild)()

    modelBuilder.Entity(Of LinkedItemChild).Property(Function(x) x.Id).IsKey()
    modelBuilder.Entity(Of LinkedItemChild).Property(Function(x) x.LinkedItemId)
    modelBuilder.Entity(Of LinkedItemChild).Property(Function(x) x.Description).IsRequired()
  End Sub

End Class
