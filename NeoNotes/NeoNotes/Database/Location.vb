Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class Location
    Public Property Id As Integer

    <Required>
    Public Property Name As String

    <Required>
    Public Property Address As String

    Public Property Companies_ID As Integer

    Public Overridable Property Company As Company
End Class
