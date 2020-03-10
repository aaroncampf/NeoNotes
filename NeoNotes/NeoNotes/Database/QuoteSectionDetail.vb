Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class QuoteSectionDetail
    Public Property ID As Integer

    Public Property Display As Integer

    <Required>
    Public Property UNIT As String

    Public Property COST As Decimal

    <Required>
    Public Property DESC As String

    Public Property IsCentered As Boolean

    Public Property QuoteSection_ID As Integer

    Public Overridable Property QuoteSection As QuoteSection
End Class
