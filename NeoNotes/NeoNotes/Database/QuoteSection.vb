Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class QuoteSection
    Public Sub New()
        QuoteSectionDetails = New HashSet(Of QuoteSectionDetail)()
    End Sub

    Public Property ID As Integer

    <Required>
    Public Property Name As String

    Public Overridable Property QuoteSectionDetails As ICollection(Of QuoteSectionDetail)
End Class
