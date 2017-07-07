'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel

Partial Public Class Company
	Implements ComponentModel.INotifyPropertyChanged

	Public Property ID As Integer
	Public Property Name As String
	Public Property Address As String
	Public Property City As String
	Public Property Phone As String
	Public Property Zip As String
	Public Property Misc As String

	Public Overridable Property Contacts As ICollection(Of Contact) = New HashSet(Of Contact)
	Public Overridable Property Quotes As ICollection(Of Quote) = New HashSet(Of Quote)

	Public Property LastUpdated As Date = DateTime.Now.AddYears(-1)
	Public Property LastChanged As Date = DateTime.Now.AddYears(-1)

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Private Sub Company_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
		If e.PropertyName <> NameOf(LastUpdated) And e.PropertyName <> NameOf(LastChanged) Then
			LastChanged = Now
		End If
	End Sub
End Class
