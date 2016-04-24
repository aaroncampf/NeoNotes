Imports Aaron.Reports

Class MainWindow
	Dim db As New DatabaseContainer

	Private Sub window_Loaded(sender As Object, e As RoutedEventArgs) Handles window.Loaded
		AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory())
		System.Data.Entity.Database.SetInitializer(Of DatabaseContainer)(New DatabaseDbInitializer)

		db.Database.CreateIfNotExists()
		db.Database.Delete()
		db.Database.CreateIfNotExists()
		db.Database.Initialize(True)

		colQuoteDetailDescription.ItemsSource = {"A", "B", "C"}



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
		Dim QuoteLine As New QuoteLine With {.DESC = "Text"}
		CType(lbxQuotes.SelectedItem, Quote).Lines.Add(QuoteLine)

		dgQuoteDetails.SelectedItem = QuoteLine
		dgQuoteDetails.Items.Refresh()
	End Sub

	Private Sub btnQuoteLineUp_Click(sender As Object, e As RoutedEventArgs) 'Handles btnQuoteLineUp.Click

	End Sub

	'Private Sub dgQuoteDetails_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles dgQuoteDetails.LoadingRow
	'	Dim Items As HashSet(Of QuoteLine) = dgQuoteDetails.ItemsSource
	'	Items.OrderBy(Function(x) x.Display)
	'End Sub


	'Private Sub cbxCompanies_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbxCompanies.SelectionChanged
	'	gbxCompany.DataContext = cbxCompanies.SelectedItem
	'End	
End Class
