Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Public Class Location
	Public Property Id As Integer

	<Required(AllowEmptyStrings:=True)>
	Public Property Name As String

	<Required(AllowEmptyStrings:=True)>
	Public Property Address As String

	'<Required(AllowEmptyStrings:=True)>
	Public Property Details As String

	Public Property Companies_ID As Integer

	Public Overridable Property Company As Company

End Class
