Public Class frmCreateEmail

#Region "       Properties	>>>"

	Private Canceled As Boolean = True

#End Region

#Region "       Public		>>>"

	Public Class frmCreateEmail_Results
		''' <summary>Did the user cancel the email?</summary>
		Public Property Canceled As Boolean = True
		''' <summary>The subject of the email</summary>
		Public Property Subject As String
		''' <summary>The body of the email</summary>
		Public Property Body As String
		''' <summary>The contacts that where added in the TO of the email</summary>
		Public Property Contacts As New List(Of Contact)
	End Class

	''' <summary>
	''' Opens the form and returns the user input
	''' </summary>
	''' <param name="DefaultContact">This contact will be added to the TO list</param>
	''' <returns></returns>
	Public Shared Function Open(DefaultContact As Contact) As frmCreateEmail_Results
		Dim Form As New frmCreateEmail 'With {.Company = DefaultContact.Company}
		Form.lbxEmails.Items.Add(DefaultContact)

		For Each Item In DefaultContact.Company.Contacts
			Form.cbxContacts.Items.Add(Item)
		Next

		Form.ShowDialog()

		Dim Results As New frmCreateEmail_Results
		Results.Body = Form.txtBody.Text
		Results.Subject = Form.txtSubject.Text
		Results.Canceled = Form.Canceled
		Results.Contacts.AddRange(Form.lbxEmails.Items.Cast(Of Contact)) '.Where(Function(x) x IsNot Nothing))

		Return Results
	End Function

#End Region

#Region "       Private		>>>"

	Private Sub New()
		InitializeComponent()
	End Sub

	Private Sub lbxEmails_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lbxEmails.SelectionChanged
		lbxEmails.Items.Remove(lbxEmails.SelectedItem)
	End Sub

	Private Sub cbxContacts_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbxContacts.SelectionChanged
		If cbxContacts.SelectedItem IsNot Nothing And Not lbxEmails.Items.Contains(cbxContacts.SelectedItem) Then
			lbxEmails.Items.Add(cbxContacts.SelectedItem)
		End If
	End Sub

	Private Sub btnSend_Click(sender As Object, e As RoutedEventArgs) Handles btnSend.Click
		Canceled = False
		Me.Close()
	End Sub

	Private Sub cbxContacts_DropDownClosed(sender As Object, e As EventArgs) Handles cbxContacts.DropDownClosed
		cbxContacts.SelectedItem = Nothing
	End Sub

#End Region

End Class
