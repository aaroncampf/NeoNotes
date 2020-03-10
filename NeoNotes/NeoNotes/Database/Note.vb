Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<PropertyChanged.ImplementPropertyChanged>
Partial Public Class Note
	Public Property ID As Integer

	Public Property [Date] As Date

	<Required>
	Public Property Title As String

	Public Property Text As String

	Public Property Contact_ID As Integer

	Public Overridable Property Contact As Contact

	<CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification:="Is Actually Used in XML Literal>")>
	Public Function ToXML() As XElement
		Return <Note ID=<%= Me.ID %> Title=<%= Me.Title %> Text=<%= Me.Text %>/>
	End Function
End Class
