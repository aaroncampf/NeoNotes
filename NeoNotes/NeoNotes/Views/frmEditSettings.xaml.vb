Public Class frmEditSettings
	Dim db As New NeoNotesContainer
	Public SettingsRecord As Setting

	Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
		SettingsRecord = If(db.Settings.Any(), db.Settings.FirstOrDefault(), New Setting())
		Me.DataContext = SettingsRecord
		gridMain.DataContext = SettingsRecord
	End Sub

	Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
		'TODO: Check to see if this works 100% correctly, I think caching causes this not to update until restart
		If Not db.Settings.Any Then
			db.Settings.Add(SettingsRecord)
		End If

		db.SaveChanges()
		Me.Close()
	End Sub
End Class
