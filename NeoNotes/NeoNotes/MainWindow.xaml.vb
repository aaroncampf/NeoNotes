Class MainWindow
	Dim db As New DatabaseContainer

	Private Sub window_Loaded(sender As Object, e As RoutedEventArgs) Handles window.Loaded
		AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory())
		System.Data.Entity.Database.SetInitializer(Of DatabaseContainer)(New DatabaseDbInitializer)

		db.Database.CreateIfNotExists()
		db.Database.Delete()
		db.Database.CreateIfNotExists()
		db.Database.Initialize(True)



		Application.Current.MainWindow.WindowState = WindowState.Maximized
		For Each Item In db.Companies
			cbxCompanies.Items.Add(Item)
		Next
	End Sub

	Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
		MsgBox("Test")
	End Sub
End Class
