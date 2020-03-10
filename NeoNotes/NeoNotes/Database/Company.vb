Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial


<PropertyChanged.ImplementPropertyChanged>
Partial Public Class Company
	Public Sub New()
		Contacts = New HashSet(Of Contact)()
		Quotes = New HashSet(Of Quote)()
		Locations = New HashSet(Of Location)()
	End Sub

	Public Property ID As Integer

	<Required>
	Public Property Name As String

	Public Property Address As String

	Public Property City As String

	Public Property Phone As String

	Public Property Zip As String

	Public Property Misc As String

	Public Overridable Property Contacts As ICollection(Of Contact)

	Public Overridable Property Quotes As ICollection(Of Quote)

	Public Overridable Property Locations As ICollection(Of Location)

	<CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification:="Is Actually Used in XML Literal>")>
	Public Function ToXML() As XElement
		Return <Company ID=<%= Me.ID %> Name=<%= Me.Name %> Address=<%= Me.Address %> City=<%= Me.City %> Misc=<%= Me.Misc %>>
				   <%= From Contact In Me.Contacts Select Contact.AsXML %>

				   <Quotes>
					   <%= From Quote In Me.Quotes Select Quote.AsXML %>
				   </Quotes>
			   </Company>
	End Function
End Class
