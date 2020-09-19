Public Class frmEditSettings
	Dim db As New Database
	Public SettingsRecord As Setting

	Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
		SettingsRecord = If(db.Settings.Any(), db.Settings.FirstOrDefault(), New Setting())
		Me.DataContext = SettingsRecord
		gridMain.DataContext = SettingsRecord

		txtOutlookPath.Text = My.Settings.OutlookPath
	End Sub

	Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
		My.Settings.OutlookPath = txtOutlookPath.Text
		My.Settings.Save()

		'TODO: Check to see if this works 100% correctly, I think caching causes this not to update until restart
		If Not db.Settings.Any Then
			db.Settings.Add(SettingsRecord)
		End If

		db.SaveChanges()
		Me.Close()
	End Sub

	Private Sub cbxSpecialTools_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbxSpecialTools.SelectionChanged
		If cbxSpecialTools.SelectedItem Is Nothing Then Exit Sub
		Select Case CType(cbxSpecialTools.SelectedItem, ComboBoxItem).Name
			Case cbiDataDirectory.Name
				Process.Start(AppDomain.CurrentDomain.GetData("DataDirectory"))
		End Select

		cbxSpecialTools.SelectedItem = Nothing
	End Sub

End Class
