﻿Imports System.Data
Imports Yamo.Infrastructure
Imports Yamo.Metadata

Namespace Internal

  Public Class ValueTypeReaderCache

    Private Shared m_Instances As Dictionary(Of Int32, ValueTypeReaderCache)

    ' Func(Of IDataReader, Int32, T)
    Private m_Readers As Dictionary(Of Type, Object)

    Shared Sub New()
      m_Instances = New Dictionary(Of Int32, ValueTypeReaderCache)
    End Sub

    Private Sub New()
      m_Readers = New Dictionary(Of Type, Object)
    End Sub

    Public Shared Function GetReader(Of T)(dialectProvider As SqlDialectProvider, model As Model) As Func(Of IDataReader, Int32, T)
      Return DirectCast(GetInstance(dialectProvider, model).GetOrCreateReader(dialectProvider, GetType(T)), Func(Of IDataReader, Integer, T))
    End Function

    Public Shared Function GetReader(dialectProvider As SqlDialectProvider, model As Model, type As Type) As Object
      Return GetInstance(dialectProvider, model).GetOrCreateReader(dialectProvider, type)
    End Function

    Private Shared Function GetInstance(dialectProvider As SqlDialectProvider, model As Model) As ValueTypeReaderCache
      Dim instance As ValueTypeReaderCache

      ' TODO: use System.HashCode instead (when available in .NET)
      Dim key = (dialectProvider, model).GetHashCode()

      If m_Instances Is Nothing Then
        SyncLock m_Instances
          instance = New ValueTypeReaderCache
          m_Instances = New Dictionary(Of Int32, ValueTypeReaderCache)
          m_Instances.Add(key, instance)
        End SyncLock
      Else
        SyncLock m_Instances
          If m_Instances.ContainsKey(key) Then
            instance = m_Instances(key)
          Else
            instance = New ValueTypeReaderCache
            m_Instances.Add(key, instance)
          End If
        End SyncLock
      End If

      Return instance
    End Function

    Private Function GetOrCreateReader(dialectProvider As SqlDialectProvider, type As Type) As Object
      Dim reader As Object = Nothing

      SyncLock m_Readers
        If m_Readers.ContainsKey(type) Then
          reader = m_Readers(type)
        End If
      End SyncLock

      If reader Is Nothing Then
        reader = dialectProvider.ValueTypeReaderFactory.CreateReader(type)
      Else
        Return reader
      End If

      SyncLock m_Readers
        m_Readers(type) = reader
      End SyncLock

      Return reader
    End Function

  End Class
End Namespace