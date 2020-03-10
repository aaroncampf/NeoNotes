Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<PropertyChanged.ImplementPropertyChanged>
Partial Public Class Contact
	Public Sub New()
		Notes = New HashSet(Of Note)()
	End Sub

	Public Property ID As Integer

	<Required>
	Public Property Name As String

	Public Property Phone As String

	Public Property Email As String

	Public Property Position As String

	Public Property Company_ID As Integer

	Public Overridable Property Company As Company

	Public Overridable Property Notes As ICollection(Of Note)

	<CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification:="Is Actually Used in XML Literal>")>
	Public Function AsXML() As XElement
		Return <Contact ID=<%= Me.ID %> Name=<%= Me.Name %> Email=<%= Me.Email %> Phone=<%= Me.Phone %> Position=<%= Me.Position %>>
				   <%= From Note In Me.Notes Select Note.ToXML %>
			   </Contact>
	End Function
End Class
