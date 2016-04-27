﻿Imports Aaron.Reports
Imports AppLimit.CloudComputing.SharpBox
Imports AppLimit.CloudComputing.SharpBox.StorageProvider.API
Imports M = System.Net.Mail

Class MainWindow
	Dim db As New DatabaseContainer

	Private Sub window_Loaded(sender As Object, e As RoutedEventArgs) Handles window.Loaded
		AppDomain.CurrentDomain.SetData("DataDirectory", IO.Directory.GetCurrentDirectory())
		Data.Entity.Database.SetInitializer(Of DatabaseContainer)(New DatabaseDbInitializer)

		db.Database.CreateIfNotExists()
		db.Database.Delete()
		db.Database.CreateIfNotExists()
		db.Database.Initialize(True)

		colQuoteDetailDescription.ItemsSource = {"A", "B", "C"}
		cbxQuoteLineDescription.ItemsSource = {"AAtest", "AAABBB", "AAVBBHJ"}


		Application.Current.MainWindow.WindowState = WindowState.Maximized
		For Each Item In db.Companies
			cbxCompanies.Items.Add(Item)
		Next

		For Each Item In db.Contacts
			cbxSearchContacts.Items.Add(Item)
		Next


		dgQuoteDetails.Items.SortDescriptions.Add(New ComponentModel.SortDescription With {.PropertyName = "Display", .Direction = ComponentModel.ListSortDirection.Ascending})
	End Sub

	Private Sub btnPrintCompanies_Click(sender As Object, e As RoutedEventArgs) Handles btnPrintCompanies.Click
		Dim Report As New Basic("Companies")
		Dim Table As New Sections.Table(
			New TableColumn With {.Tag = "Name"},
			New TableColumn With {.Tag = "Address"},
			New TableColumn With {.Tag = "City", .Width = New GridLength(100, GridUnitType.Pixel)},
			New TableColumn With {.Tag = "Zip", .Width = New GridLength(75, GridUnitType.Pixel)},
			New TableColumn With {.Tag = "Phone", .Width = New GridLength(100, GridUnitType.Pixel)})

		For Each Item In db.Companies.OrderBy(Function(x) x.Name)
			Table.Table.AddRow(0, TextAlignment.Left, Item.Name, Item.Address, Item.City, Item.Zip, Item.Phone)
		Next
		Report.Sections.Add(Table)
		Report.Show()
	End Sub

	Private Sub btnAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnAdd.Click
		Dim Contact As New Contact With {.Name = "New Contact"}
		CType(cbxCompanies.SelectedItem, Company).Contacts.Add(Contact)

		lbxContacts.SelectedItem = Contact
		lbxContacts.Items.Refresh()
	End Sub


	Private Sub lbxContacts_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lbxContacts.SelectionChanged
		If lbxContacts.SelectedItem Is Nothing Then Exit Sub
		'dgNotes.Items.Clear()

		'For Each Item In CType(lbxContacts.SelectedItem, Contact).Notes
		'	dgNotes.Items.Add(Item)
		'Next
	End Sub

	Private Sub cbxSearchContacts_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbxSearchContacts.SelectionChanged
		Dim Contact As Contact = cbxSearchContacts.SelectedItem
		cbxCompanies.SelectedItem = Contact.Company
		lbxContacts.SelectedItem = Contact
	End Sub

	Private Sub btnContactRemove_Click(sender As Object, e As RoutedEventArgs) Handles btnContactRemove.Click
		If MsgBox($"Are you sure you want to remove the contact {txtContactName.Text}?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
			CType(cbxCompanies.SelectedItem, Company).Contacts.Remove(lbxContacts.SelectedItem)
			lbxContacts.Items.Refresh()
		End If
	End Sub

	Private Sub btnQuoteAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnQuoteAdd.Click
		Dim Quote As New Quote With {.Name = "New Quote"}
		CType(cbxCompanies.SelectedItem, Company).Quotes.Add(Quote)

		lbxQuotes.SelectedItem = Quote
		lbxQuotes.Items.Refresh()
	End Sub

	Private Sub btnQuoteDetailAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnQuoteDetailAdd.Click
		Dim Quote As Quote = lbxQuotes.SelectedItem
		Dim QuoteLine As New QuoteLine With {.DESC = "Text", .Quote = Quote}
		Quote.Lines.Add(QuoteLine)

		dgQuoteDetails.SelectedItem = QuoteLine
		dgQuoteDetails.Items.Refresh()
	End Sub

	Private Sub btnQuoteLineUp_Click(sender As Object, e As RoutedEventArgs) 'Handles btnQuoteLineUp.Click
		Quote_ShiftDetail(LogicalDirection.Forward)
	End Sub

	Private Sub btnQuoteLineDown_Click(sender As Object, e As RoutedEventArgs) 'Handles btnQuoteLineUp.Click
		Quote_ShiftDetail(LogicalDirection.Backward)
	End Sub


	''' <summary>
	'''
	''' </summary>
	''' <param name="Direction">The Direction you want to Move the Detail; -1 = No movement.</param>
	''' <remarks></remarks>
	''' <stepthrough></stepthrough>
	Private Sub Quote_ShiftDetail(Direction As LogicalDirection)
		If dgQuoteDetails.SelectedItem Is Nothing Then Exit Sub
		Dim CurrentLine As QuoteLine = CType(dgQuoteDetails.SelectedItem, QuoteLine)
		Dim Quote As Quote = CurrentLine.Quote


		Dim Sorter = Function(x As QuoteLine, y As QuoteLine) If(x.Display = y.Display, x.ID.CompareTo(y.ID), x.Display).CompareTo(y.Display)

		Dim Data = Quote.Lines.ToList
		Data.Sort(Sorter)

		Dim List As New LinkedList(Of QuoteLine)
		For I As Integer = 0 To Data.Count - 1
			Data(I).Display = I
			List.AddLast(Data(I))
		Next

		If Direction = -1 Then
		ElseIf Direction = LogicalDirection.Backward Then
			CurrentLine.Display = CurrentLine.Display + 1

			If List.Find(CurrentLine).Next IsNot Nothing Then
				List.Find(CurrentLine).Next.Value.Display = List.Find(CurrentLine).Next.Value.Display - 1
			End If

		ElseIf Val(CurrentLine.Display) > 0 Then
			CurrentLine.Display = CurrentLine.Display - 1

			If List.Find(CurrentLine).Previous IsNot Nothing Then
				List.Find(CurrentLine).Previous.Value.Display = List.Find(CurrentLine).Previous.Value.Display + 1
			End If
		End If

		dgQuoteDetails.Items.SortDescriptions.Add(New ComponentModel.SortDescription With {.PropertyName = "Display", .Direction = ComponentModel.ListSortDirection.Ascending})
		dgQuoteDetails.Items.Refresh()
	End Sub

	Private Sub btnCompanyPrintContacts_Click(sender As Object, e As RoutedEventArgs) Handles btnCompanyPrintContacts.Click
		Dim Company As Company = cbxCompanies.SelectedItem

		Dim ContactReport As New Basic()
		Dim Info As New Sections.Table("Contacts For " & Company.Name)
		Info.Header.TextAlignment = TextAlignment.Center

		Info.Table.Columns.Add(New TableColumn With {.Tag = "Name"})
		Info.Table.Columns.Add(New TableColumn With {.Tag = "Position"})
		Info.Table.Columns.Add(New TableColumn With {.Tag = "Phone"})
		Info.Table.Columns.Add(New TableColumn With {.Tag = "Email"})

		For Each Item In Company.Contacts.OrderBy(Function(x) x.Name)
			Info.Table.AddRow(0, TextAlignment.Left, Item.Name, Item.Position, Item.Phone, Item.Email)
		Next

		ContactReport.Sections.Add(Info)
		ContactReport.Show()
	End Sub

	Private Sub btnContactEmail_Click(sender As Object, e As RoutedEventArgs) Handles btnContactEmail.Click
		Process.Start($"https://mail.google.com/mail/?view=cm&fs=1&tf=1&to={CType(lbxContacts.SelectedItem, Contact).Email}")
	End Sub

	Private Sub btnQuoteEmail_Click(sender As Object, e As RoutedEventArgs) Handles btnQuoteEmail.Click
		Dim Contact As Contact = lbxContacts.SelectedItem
		If Contact.Email Is Nothing Then
			MsgBox("Contact has no email")
			Exit Sub
		End If

		Dim Quote As Quote = lbxQuotes.SelectedItem
		If Quote Is Nothing Then
			MsgBox("No Quote has been selected")
			Exit Sub
		End If

		Dim Settings = New DatabaseContainer().Settings.First
		If String.IsNullOrWhiteSpace(Settings.Gmail) Or String.IsNullOrWhiteSpace(Settings.GmailPassword) Then
			MsgBox("Please Setup the GMail Email And Password Settings")
		Else
			If My.User.Name.Contains("Aaron Campf") Then
				If MsgBox("Are you Sure you want to send then Email to: " & Contact.Email, MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
			End If

			Dim GetBody = frmMsgBuilder.ShowDialog(True, False, "Send Email to " & Contact.Name)
			If GetBody IsNot Nothing AndAlso GetBody.Item1 = Forms.DialogResult.OK Then
				Dim Body As XElement
				Body = XElement.Parse(GetBody.Item2.Item2).<BODY>(0)
				Body.Name = XName.Get("P")
				Dim Attachments As New Dictionary(Of String, String) From {{"Quote.pdf", Create_Quote_Printout().AsPDF}}

				SendMail(Settings.Name, Settings.Gmail, Settings.GmailPassword, GetBody.Item2.Item1, Body, {Contact.Email}, Attachments)
			End If
		End If
	End Sub



	Private Function Create_Quote_Printout() As Basic
		Dim Company As Company = cbxCompanies.SelectedItem
		Dim Contact As Contact = lbxContacts.SelectedItem
		Dim Quote As Quote = lbxQuotes.SelectedItem
		Dim Settings = New DatabaseContainer().Settings.First

		Dim Items As New Sections.Table(New TableColumn With {.Tag = "UNIT", .Width = New GridLength(150)},
										New TableColumn With {.Tag = "Description"},
										New TableColumn With {.Tag = "COST", .Width = New GridLength(100)})

		Items.Table.CellSpacing = 0 '<-- I don't think this is having any affect due to the CustomXAML

		For Each Detail In Quote.Lines.OrderBy(Function(x) Val(x.Display))
			If Val(Detail.COST) > 0.0 Then
				Items.Table.AddRow(0, TextAlignment.Center, Detail.UNIT, Detail.DESC, FormatCurrency(Val(Detail.COST), 2))
			Else
				Items.Table.AddRow(0, TextAlignment.Center, Detail.UNIT, Detail.DESC, "")
			End If

			Items.Table.RowGroups(0).Rows.Last.Cells(1).Blocks(0).TextAlignment = TextAlignment.Left

			If Detail.IsCentered Then
				Items.Table.RowGroups(0).Rows.Last.Cells(1).Blocks(0).TextAlignment = TextAlignment.Center
			End If
		Next

		'TODO: Inline this Soon!

		'{"Quote_Title", If(Quote.Title Is Nothing, "Quote", Quote.Title)},
		Dim Temp_Holder As New Dictionary(Of String, Object) From {
			{"Company", Company.Name},
			{"Address", Company.Address},
			{"Contact", Contact.Name},
			{"CityZip", Company.City & " " & Company.Zip},
			{"Phone", Company.Phone},
			{"Quote_Title", Quote.Name},
			{"User_Cell", Settings.Phone},
			{"User_Address", Settings.Address},
			{"Salesperson", Settings.Name},
			{"Email", Settings.Email}
		}

		Dim CustomXAML As String = My.Resources.Hand_Made_Quote.Replace("<!--{0}-->", Items.Table.RowGroups(0).ToXML.ToString)
		If Company Is Nothing Then CustomXAML = CustomXAML.Replace("To:", "")

		Dim XAML = CustomXAML.Replace("xmlns:xrd=""clr-namespace:CodeReason.Reports.Document;assembly=CodeReason.Reports""",
									  "xmlns:xrd=""clr-namespace:Aaron.Xaml;assembly=Aaron.Xaml""")

		Dim Test_Report As New Basic()
		Test_Report.CustomXAML = XAML
		Test_Report.DocumentValues = Temp_Holder

		Return Test_Report
	End Function


	Private Sub SendMail(DisplayName As String, Email As String, Password As String, Subject As String, Body As String, SendTo As IEnumerable(Of String), Attachments As Dictionary(Of String, String))
		Dim Mail As New M.MailMessage()

		Dim SmtpServer As New M.SmtpClient("smtp.gmail.com", 587) With {
				.EnableSsl = True, .UseDefaultCredentials = False, .Credentials = New Net.NetworkCredential(Email, Password)}

		'From Text.Encoding.UTF8 --> Text.Encoding.Default --> Text.Encoding.UTF8
		Mail = New M.MailMessage() With {.Subject = Subject, .Body = Body, .From = New M.MailAddress(Email, DisplayName, Text.Encoding.UTF8)}

		Try
			For Each Item In SendTo
				Mail.To.Add(Item)
			Next

			For Each Item In Attachments
				Mail.Attachments.Add(New M.Attachment(Item.Value) With {.Name = Item.Key})
			Next

			Mail.IsBodyHtml = True
			Mail.DeliveryNotificationOptions = M.DeliveryNotificationOptions.OnFailure
			AddHandler SmtpServer.SendCompleted,
				Sub()
					MsgBox("Email Sent")
					SmtpServer.Dispose()
				End Sub

			SmtpServer.SendAsync(Mail, "")
		Catch ex As M.SmtpException
			SmtpServer.Dispose()
			Select Case ex.StatusCode
				Case M.SmtpStatusCode.MustIssueStartTlsFirst
					MsgBox("Error when Sending Message" & vbCrLf & vbCrLf & "Check your Email and Password", MsgBoxStyle.Critical)
				Case Else
					MsgBox(ex.ToString)
			End Select
		End Try
	End Sub

	Private Sub window_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles window.Closing
		Try
			Me.db.SaveChanges()

			Dim Settings = New DatabaseContainer().Settings.First

			'Dim MyNotes = XElement.Load(N.frmNotes.File) '.Save(SaveFile1.SelectedPath & "\Notes_To_Droid.txt")



			Dim MyNotes =
			<Customers>
				<%= From Company In db.Companies.ToArray Select Company.ToXML %>
			</Customers>

			'Do not use [.ForEach()] for this or[Visual Studio] Will Crash
			For Each Node In MyNotes...<Quote>
				Node.@Date = If(Node.@Date = "", Date.MinValue.ToShortDateString, CDate(Node.@Date).ToShortDateString)
			Next

			For Each Node In MyNotes...<Note>
				Node.@Date = If(Node.@Date = "", Date.MinValue.ToShortDateString, CDate(Node.@Date).ToShortDateString)
			Next

			MyNotes.Save(IO.Path.GetTempPath & "\Notes.xml")


			'The example totally works!!!
			Dim dropBoxStorage As New CloudStorage()
			Dim dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox)
			Dim accessToken As ICloudStorageAccessToken

			'load a valid security token from file
			Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(My.Resources.DropBox_Token)
			accessToken = dropBoxStorage.DeserializeSecurityTokenFromBase64(Convert.ToBase64String(byt))


			'open the connection
			Dim storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken)

			dropBoxStorage.UploadFile(IO.Path.GetTempPath & "\Notes.xml", "/Users/" & Settings.Name)
		Catch ex As Exception
			MsgBox(ex.ToString, MsgBoxStyle.OkOnly, ex.GetType.Name)
			e.Cancel = True
		End Try
	End Sub

End Class
