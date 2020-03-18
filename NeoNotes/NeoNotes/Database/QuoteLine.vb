Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<PropertyChanged.ImplementPropertyChanged>
Partial Public Class QuoteLine
	Public Property ID As Integer

	Public Property Display As Integer

	<Required(AllowEmptyStrings:=True)>
	Public Property UNIT As String

	Public Property COST As Decimal

	<Required>
	Public Property DESC As String

	Public Property IsCentered As Boolean

	Public Property Quote_ID As Integer

	Public Overridable Property Quote As Quote

	<CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification:="Is Actually Used in XML Literal>")>
	Public Function ToXML() As XElement
		Return <Detail ID=<%= Me.ID %> Display=<%= Me.Display %> DESC=<%= Me.DESC %> UNIT=<%= Me.UNIT %> Cost=<%= Me.COST %>/>
	End Function
End Class
