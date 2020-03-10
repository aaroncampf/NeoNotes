Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial


<PropertyChanged.ImplementPropertyChanged>
Partial Public Class Quote
	Public Sub New()
		QuoteLines = New HashSet(Of QuoteLine)()
	End Sub

	Public Property ID As Integer

	Public Property [Date] As Date

	<Required>
	Public Property Name As String

	Public Property Company_ID As Integer

	Public Overridable Property Company As Company

	Public Overridable Property QuoteLines As ICollection(Of QuoteLine)

	Public Function AsXML() As XElement
		Return <Quote ID=<%= Me.ID %> Date=<%= Me.Date %> Name=<%= Me.Name %>>
				   <%= From Line In Me.QuoteLines Select Line.ToXML %>
			   </Quote>
	End Function
End Class
