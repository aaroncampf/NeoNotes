Imports Aaron.Reports
Imports M = System.Net.Mail

Class MainWindow
	Dim db As New NeoNotesContainer
	Dim UploadData_Lock As New Object
	WithEvents dispatcherTimer As New Threading.DispatcherTimer() With {.Interval = New TimeSpan(0, 1, 15)}

	Private Sub window_Loaded(sender As Object, e As RoutedEventArgs) Handles window.Loaded
		Dim DataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory")
		If Not IO.Directory.Exists(DataDirectory) Then IO.Directory.CreateDirectory(DataDirectory)
		dispatcherTimer.Start()

		If Not db.Settings.Any Then
			MsgBox("Please configure your settings with the settings button")
		End If

		cbxQuoteLineDescription.ItemsSource = Get_Item_Names()
		Application.Current.MainWindow.WindowState = WindowState.Maximized

		For Each Item In db.Companies.OrderBy(Function(x) x.Name)
			cbxCompanies.Items.Add(Item)
		Next

		For Each Item In db.Contacts.OrderBy(Function(x) x.Name)
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
		Contact.Company = cbxCompanies.SelectedItem

		lbxContacts.SelectedItem = Contact
		lbxContacts.Items.Refresh()
		txtContactName.Focus()
		txtContactName.SelectAll()
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
		If cbxSearchContacts.SelectedItem IsNot Nothing Then
			cbxCompanies.SelectedItem = Contact.Company
			lbxContacts.SelectedItem = Contact
		End If
	End Sub

	Private Sub btnContactRemove_Click(sender As Object, e As RoutedEventArgs) Handles btnContactRemove.Click
		If MsgBox($"Are you sure you want to remove the contact {txtContactName.Text}?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
			CType(cbxCompanies.SelectedItem, Company).Contacts.Remove(lbxContacts.SelectedItem)
			lbxContacts.Items.Refresh()
		End If
	End Sub

	Private Sub btnQuoteAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnQuoteAdd.Click
		Dim Quote As New Quote With {.Name = "New Quote", .Date = Now}
		CType(cbxCompanies.SelectedItem, Company).Quotes.Add(Quote)
		Quote.Lines.Add(New QuoteLine With {.Display = 0, .DESC = "NO MINIMUM ORDER", .IsCentered = True})
		Quote.Lines.Add(New QuoteLine With {.Display = 0, .DESC = "NO DELIVERY CHARGE", .IsCentered = True})

		lbxQuotes.SelectedItem = Quote
		lbxQuotes.Items.Refresh()

		txtQuoteTitle.Focus()
		txtQuoteTitle.SelectAll()
	End Sub

	Private Sub btnQuoteRemove_Click(sender As Object, e As RoutedEventArgs) Handles btnQuoteRemove.Click
		Dim Quote As Quote = lbxQuotes.SelectedItem

		CType(cbxCompanies.SelectedItem, Company).Quotes.Remove(Quote)
		lbxQuotes.Items.Refresh()
	End Sub

	Private Sub btnQuoteDetailAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnQuoteDetailAdd.Click
		cbxQuoteLineDescription.Focus() 'Hack for ensuring that all controls have there data saved
		txtQuoteLineCost.Focus()        'Hack for ensuring that all controls have there data saved

		Dim Quote As Quote = lbxQuotes.SelectedItem

		Dim Display = If(Quote.Lines.Any, Quote.Lines.Max(Function(x) x.Display), 0) + 1
		Dim QuoteLine As New QuoteLine With {.DESC = "", .Quote = Quote, .Display = Display, .COST = Nothing}
		Quote.Lines.Add(QuoteLine)

		dgQuoteDetails.SelectedItem = QuoteLine
		dgQuoteDetails.Items.Refresh()

		cbxQuoteLineDescription.Focus()
	End Sub


	Private Sub btnRemoveQuoteLine_Click(sender As Object, e As RoutedEventArgs) Handles btnRemoveQuoteLine.Click
		Dim Quote As Quote = lbxQuotes.SelectedItem
		Quote.Lines.Remove(dgQuoteDetails.SelectedItem)
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

	Private Sub btnCompanyPrintCompany_Click(sender As Object, e As RoutedEventArgs) Handles btnCompanyPrintCompany.Click
		Dim Company As Company = cbxCompanies.SelectedItem
		Dim Report As New Basic(Company.Name)

		Dim CompanyData As New Sections.Table("Details")
		CompanyData.Table.Columns.Add(New TableColumn)
		CompanyData.Table.Columns.Add(New TableColumn)

		CompanyData.Table.AddRow(0, TextAlignment.Left, "Name", Company.Name)
		CompanyData.Table.AddRow(0, TextAlignment.Left, "Address", Company.Address)
		CompanyData.Table.AddRow(0, TextAlignment.Left, "City", Company.City)
		CompanyData.Table.AddRow(0, TextAlignment.Left, "Zip", Company.Zip)
		CompanyData.Table.AddRow(0, TextAlignment.Left, "Phone", Company.Phone)
		Report.Sections.Add(CompanyData)

		Dim Contacts As New Sections.Table("Contacts")
		Contacts.Header.TextAlignment = TextAlignment.Center

		Contacts.Table.Columns.Add(New TableColumn With {.Tag = "Name"})
		Contacts.Table.Columns.Add(New TableColumn With {.Tag = "Position"})
		Contacts.Table.Columns.Add(New TableColumn With {.Tag = "Phone"})
		Contacts.Table.Columns.Add(New TableColumn With {.Tag = "Email"})

		For Each Item In Company.Contacts.OrderBy(Function(x) x.Name)
			Contacts.Table.AddRow(0, TextAlignment.Left, Item.Name, Item.Position, Item.Phone, Item.Email)
		Next

		Report.Sections.Add(Contacts)
		Report.Show()
	End Sub

	Private Sub btnContactEmail_Click(sender As Object, e As RoutedEventArgs) Handles btnContactEmail.Click
		Process.Start($"https://mail.google.com/mail/?view=cm&fs=1&tf=1&to={CType(lbxContacts.SelectedItem, Contact).Email}")
	End Sub

	Private Function Create_Quote_Printout(Contact As Contact) As Basic
		Dim Company As Company = cbxCompanies.SelectedItem
		'Dim Contact As Contact = lbxContacts.SelectedItem
		Dim Quote As Quote = lbxQuotes.SelectedItem
		Dim Settings = db.Settings.First

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

		Dim CustomXAML As String = My.Resources.Hand_Made_Quote.Replace("<!--{0}-->", Items.Table.RowGroups(0).ToXML.ToString)
		'If Company Is Nothing Then CustomXAML = CustomXAML.Replace("To:", "")

		Dim XAML = CustomXAML.Replace("xmlns:xrd=""clr-namespace:CodeReason.Reports.Document;assembly=CodeReason.Reports""",
									  "xmlns:xrd=""clr-namespace:Aaron.Xaml;assembly=Aaron.Xaml""")

		Dim Test_Report As New Basic()
		Test_Report.CustomXAML = XAML

		Test_Report.DocumentValues = New Dictionary(Of String, Object) From {
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

		Return Test_Report
	End Function

	Private Sub SendMail(DisplayName As String, Email As String, Password As String, Subject As String, Body As String, SendTo As String, Attachments As Dictionary(Of String, String))
		Dim Mail As New M.MailMessage()

		Dim SmtpServer As New M.SmtpClient("smtp.gmail.com", 587) With {
				.EnableSsl = True, .UseDefaultCredentials = False, .Credentials = New Net.NetworkCredential(Email, Password)}

		'From Text.Encoding.UTF8 --> Text.Encoding.Default --> Text.Encoding.UTF8

		Try
			Mail = New M.MailMessage() With {.Subject = Subject, .Body = Body, .From = New M.MailAddress(Email, DisplayName, Text.Encoding.UTF8)}
		Catch ex As Exception
			MsgBox(ex.Message, MsgBoxStyle.Critical & MsgBoxStyle.OkOnly, $"Email for {DisplayName} Failed")
			Exit Sub
		End Try

		Try
			Mail.To.Add(SendTo)
		Catch ex As Exception
			MsgBox(ex.Message, MsgBoxStyle.Critical & MsgBoxStyle.OkOnly, $"SendTo email was invalid")
			Exit Sub
		End Try

		Try

			For Each Item In Attachments
				Mail.Attachments.Add(New M.Attachment(Item.Value) With {.Name = Item.Key})
			Next

			Mail.IsBodyHtml = True
			Mail.DeliveryNotificationOptions = M.DeliveryNotificationOptions.OnFailure

			AddHandler SmtpServer.SendCompleted,
				Sub(sender As Object, e As ComponentModel.AsyncCompletedEventArgs)
					If e.Error Is Nothing Then
						MsgBox("Email Sent")
					Else
						MsgBox(e.Error.Message, MsgBoxStyle.Critical & vbOK, "Quote Email Failed")
					End If

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
		cbxCompanies.Focus()
		UploadData(True)
	End Sub

	Private Sub btnCompany_Add_Click(sender As Object, e As RoutedEventArgs) Handles btnCompany_Add.Click
		Dim Company As New Company With {.Name = "New Name"}
		db.Companies.Add(Company)
		cbxCompanies.Items.Add(Company)
		cbxCompanies.SelectedItem = Company

		txtCompanyName.Focus()
		txtCompanyName.SelectAll()
	End Sub

	Private Sub btnAddNote_Click(sender As Object, e As RoutedEventArgs) Handles btnAddNote.Click
		Dim Contact As Contact = lbxContacts.SelectedItem
		Dim Note = New Note With {.Contact = Contact, .Date = Now}
		Contact.Notes.Add(Note)

		dgNotes.Items.Refresh()
		dgNotes.SelectedItem = Note
		txtNoteName.Focus()
	End Sub

	Private Sub btnQuotePrint_Click(sender As Object, e As RoutedEventArgs) Handles btnQuotePrint.Click
		If lbxContacts.SelectedItem Is Nothing Then
			MsgBox("Select a Contact")
			Exit Sub
		End If

		Dim Quote As Quote = lbxQuotes.SelectedItem
		If Quote.Lines.Any Then
			Create_Quote_Printout(lbxContacts.SelectedItem).Show()
		Else
			MsgBox("You cannot print an empty quote")
		End If
	End Sub

	Private Sub btnPrintNotes_Click(sender As Object, e As RoutedEventArgs) Handles btnPrintNotes.Click
		Dim Contact As Contact = lbxContacts.SelectedItem

		Dim NotePrintout As New Basic($"Notes For {Contact.Company.Name} - {Contact.Name}")
		'Dim Info As New Sections.Table(
		'	New TableColumn With {.Tag = "Company"},
		'	New TableColumn With {.Tag = "Contact"},
		'	New TableColumn With {.Tag = "Position"},
		'	New TableColumn With {.Tag = "Phone"}
		')

		'Info.Table.AddRow(0, TextAlignment.Center, Contact.Company.Name, Contact.Name, Contact.Position, Contact.Phone)
		'NotePrintout.Sections.Add(Info)

		For Each Note In Contact.Notes.OrderByDescending(Function(x) x.Date)
			Dim Header As New Sections.Table(
				New TableColumn With {.Tag = "Date", .Width = New GridLength(110)},
				New TableColumn With {.Tag = "Title"}
			)

			Header.Table.AddRow(0, TextAlignment.Center, Note.Date.ToShortDateString, Note.Title)
			NotePrintout.Sections.Add(Header)

			NotePrintout.Sections.Add(New Sections.Basic(Nothing, Note.Text))
		Next

		NotePrintout.Show()
	End Sub

	Private Sub btnRemoveCompany_Click(sender As Object, e As RoutedEventArgs) Handles btnRemoveCompany.Click
		If MsgBox($"Are you sure you want to remove the company {txtCompanyName.Text}?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
			Dim Company As Company = cbxCompanies.SelectedItem
			cbxCompanies.Items.Remove(Company)
			db.Companies.Remove(Company)
			cbxCompanies.Items.Refresh()
		End If
	End Sub

	Private Sub txtQuoteLineUnit_KeyDown(sender As Object, e As KeyEventArgs) Handles txtQuoteLineUnit.KeyDown
		'This is a hack because I need a way to account for the 0 that is always there in the Cost by default
		If e.Key = Key.Tab Then
			txtQuoteLineCost.Focus()
			txtQuoteLineCost.SelectAll()
		End If
	End Sub

	Private Sub btnQuoteEmail_Click(sender As Object, e As RoutedEventArgs) Handles btnQuoteEmail.Click
		Dim Quote As Quote = lbxQuotes.SelectedItem
		If Quote Is Nothing Then
			MsgBox("No Quote has been selected")
			Exit Sub
		End If

		Dim Settings = db.Settings.First
		If String.IsNullOrWhiteSpace(Settings.Gmail) Or String.IsNullOrWhiteSpace(Settings.GmailPassword) Then
			MsgBox("Please Setup the GMail Email And Password Settings")
		Else
			Dim Results = frmCreateEmail.Open(DirectCast(cbxCompanies.SelectedItem, Company))
			If Not Results.Canceled Then
				For Each Contact In Results.Contacts
					Dim Attachments As New Dictionary(Of String, String) From {{"Quote.pdf", Create_Quote_Printout(Contact).AsPDF}}
					SendMail(Settings.Name, Settings.Gmail, Settings.GmailPassword, Results.Subject, Results.Body, Contact.Email, Attachments)
				Next
			End If
		End If
	End Sub

	Public Shared Function Get_Item_Names() As String()
		Dim LocalFilePath = AppDomain.CurrentDomain.GetData("DataDirectory") + "\NeoNotes.txt"
		If My.Computer.Network.IsAvailable AndAlso My.Settings.LastUpdated < Now.AddDays(-7) OrElse Not IO.File.Exists(LocalFilePath) Then
			Dim Dbox As New Dropbox.Api.DropboxClient(My.Resources.Dropbox_AccessToken)
			Dim File = Dbox.Files.DownloadAsync("/Storage/NeoNotes.txt").Result.GetContentAsByteArrayAsync().Result


			If IO.File.Exists(LocalFilePath) Then
				IO.File.Delete(LocalFilePath)
			End If

			IO.File.WriteAllBytes(LocalFilePath, File)

			My.Settings.LastUpdated = Now
			My.Settings.Save() '<--- Do I need this?
		End If

		Return IO.File.ReadAllLines(LocalFilePath)
	End Function

	Private Sub btnSettings_Click(sender As Object, e As RoutedEventArgs) Handles btnSettings.Click
		Dim Form As New frmEditSettings
		Form.ShowDialog()
	End Sub

	''' <summary>
	''' 
	''' </summary>
	''' <param name="Disreguard_InUse">Try To Remove This Check</param>
	Private Sub UploadData(Disreguard_InUse As Boolean)
		If Not My.Computer.Network.IsAvailable Then
			MsgBox("Unable to update phone app due to lack of internet")
			Exit Sub
		End If

		SyncLock UploadData_Lock
			Static IsInUse As Boolean
			If Not db.ChangeTracker.HasChanges Or (IsInUse And Not Disreguard_InUse) Then Return
			IsInUse = True


			Try
				Me.db.SaveChanges()
			Catch ex As Exception
				If MsgBox(ex.GetType.Name & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.YesNo, "Error: Click Yes to quit without saving or No to Stay") = MsgBoxResult.No Then
					IsInUse = False
					Exit Sub
				End If
			End Try

			Try
				If Not IO.File.Exists(AppDomain.CurrentDomain.GetData("DataDirectory") + "\NeoInfo.xml") Then
					MsgBox($"Please Create the settings file: {AppDomain.CurrentDomain.GetData("DataDirectory") + "\NeoInfo.xml"}")
				End If

				Dim XML As XElement = XElement.Load(AppDomain.CurrentDomain.GetData("DataDirectory") + "\NeoInfo.xml")


				Dim Companies = db.Companies.Include("Contacts").Include("Contacts.Notes").Include("Quotes").Include("Quotes.Lines").ToArray

				Dim MyNotes =
				<Customers>
					<%= From Company In Companies
						Select
							<Company ID=<%= Company.ID %> Name=<%= Company.Name %> Phone=<%= Company.Phone %> Address=<%= Company.Address %> City=<%= Company.City %> Zip=<%= Company.Zip %> Misc=<%= Company.Misc %>>
								<%= From Contact In Company.Contacts
									Select <Contact ID=<%= Contact.ID %> Name=<%= Contact.Name %> Email=<%= Contact.Email %> Phone=<%= Contact.Phone %> Position=<%= Contact.Position %>>
											   <%= From Note In Contact.Notes Select <Note ID=<%= Note.ID %> Title=<%= Note.Title %> Text=<%= Note.Text %> Date=<%= Note.Date %>/> %>
										   </Contact>
								%>

								<Quotes>
									<%= From Quote In Company.Quotes
										Select <Quote ID=<%= Quote.ID %> Date=<%= If(String.IsNullOrEmpty(Quote.Date), Date.MinValue.ToShortDateString, Quote.Date) %> Name=<%= Quote.Name %>>
												   <%= From Line In Quote.Lines
													   Select <Detail ID=<%= Quote.ID %> Display=<%= Line.Display %> DESC=<%= Line.DESC %> UNIT=<%= Line.UNIT %> Cost=<%= Line.COST %>/> %>
											   </Quote>
									%>
								</Quotes>
							</Company>
					%>
				</Customers>

				MyNotes.Save(IO.Path.GetTempPath & "\Notes.xml")

				Dim Dbox As New Dropbox.Api.DropboxClient(My.Resources.Dropbox_AccessToken)
				Dim File As IO.Stream = IO.File.OpenRead(IO.Path.GetTempPath & "\Notes.xml")
				Dbox.Files.UploadAsync("/Users/" & XML.@Name & "/Notes.xml", body:=File, mode:=Dropbox.Api.Files.WriteMode.Overwrite.Instance).Result.ToString()
			Catch ex As Exception
				MsgBox(ex.GetType.Name & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.OkOnly And MsgBoxStyle.Critical, "Error Uploading Notes")
			End Try

			IsInUse = False
		End SyncLock
	End Sub

	Private Sub dispatcherTimer_Tick(sender As Object, e As EventArgs) Handles dispatcherTimer.Tick
		Dim UploadTask As Task = New Task(Sub() UploadData(False))
		UploadTask.Start()
	End Sub

	Private Sub btnQuoteDetailAddFrinPriceList_Click(sender As Object, e As RoutedEventArgs) Handles btnQuoteDetailAddFrinPriceList.Click
		Dim Products = frmInventory.GetProducts()


		cbxQuoteLineDescription.Focus() 'Hack for ensuring that all controls have there data saved
		txtQuoteLineCost.Focus()        'Hack for ensuring that all controls have there data saved

		Dim Quote As Quote = lbxQuotes.SelectedItem
		For Each Item In Products
			Dim Display = If(Quote.Lines.Any, Quote.Lines.Max(Function(x) x.Display), 0) + 1
			Dim QuoteLine As New QuoteLine With {.DESC = Item.DESCRIP, .Quote = Quote, .Display = Display, .COST = Item.LOADEDCOST, .UNIT = Item.RTDESC1}

			Quote.Lines.Add(QuoteLine)
			dgQuoteDetails.SelectedItem = QuoteLine
		Next

		dgQuoteDetails.Items.Refresh()
	End Sub
End Class