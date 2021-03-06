Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class Setting
    Public Property ID As Integer

    <Required>
    Public Property Name As String

    <Required>
    Public Property Gmail As String

    <Required>
    Public Property GmailPassword As String

    <Required>
    Public Property Email As String

    <Required>
    Public Property Address As String

    <Required>
    Public Property Phone As String
End Class
