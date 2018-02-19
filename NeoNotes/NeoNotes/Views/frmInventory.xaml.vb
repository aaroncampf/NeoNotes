Public Class frmInventory
	Dim Data As INVMAS_Small()

	Public Shared Function GetProducts() As List(Of INVMAS_Small)
		Dim Form As New frmInventory
		Form.ShowDialog()
		Return Form.lbxSelectedItems.Items.OfType(Of INVMAS_Small).ToList
	End Function


	Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
		Dim LocalFilePath = AppDomain.CurrentDomain.GetData("DataDirectory") + "\NeoNotes2.txt"
		If My.Computer.Network.IsAvailable AndAlso My.Settings.LastUpdated_Inventory < Now.AddDays(-7) OrElse Not IO.File.Exists(LocalFilePath) Then
			Dim Dbox As New Dropbox.Api.DropboxClient(My.Resources.Dropbox_AccessToken)
			Dim File = Dbox.Files.DownloadAsync("/Storage/NeoNotes2.txt").Result.GetContentAsByteArrayAsync().Result

			If IO.File.Exists(LocalFilePath) Then
				IO.File.Delete(LocalFilePath)
			End If

			IO.File.WriteAllBytes(LocalFilePath, File)

			My.Settings.LastUpdated = Now
			My.Settings.Save() '<--- Do I need this?
		End If

		Dim Text = String.Join(vbCrLf, IO.File.ReadAllLines(LocalFilePath))
		Dim StringReader As New IO.StringReader(Text)
		Dim Reader As New CsvHelper.CsvReader(StringReader)
		Data = Reader.GetRecords(Of INVMAS_Small).ToArray
		cbxCatg.ItemsSource = Data.Select(Function(x) x.CATG).Distinct
	End Sub

	Private Sub cbxCatg_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbxCatg.SelectionChanged
		Dim Records = Data.Where(Function(x) x.CATG = cbxCatg.SelectedItem).ToArray

		dgINVMAS_Small.ItemsSource = Records
		cbxGroup.ItemsSource = Records.Select(Function(x) x.GROUP).Distinct
	End Sub

	Private Sub cbxGroup_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbxGroup.SelectionChanged
		dgINVMAS_Small.ItemsSource = Data.Where(Function(x) x.GROUP = cbxGroup.SelectedItem).ToArray
	End Sub

	Private Sub btnRemove_Click(sender As Object, e As RoutedEventArgs) Handles btnRemove.Click
		If lbxSelectedItems.SelectedItem Is Nothing Then
			MsgBox("No Selected Item")
		Else
			lbxSelectedItems.Items.Remove(lbxSelectedItems.SelectedItem)
		End If

		lbxSelectedItems.Items.Refresh() 'Not Sure if I need this
	End Sub

	Private Sub dgINVMAS_Small_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dgINVMAS_Small.SelectionChanged
		lbxSelectedItems.Items.Add(dgINVMAS_Small.SelectedItem)
	End Sub
End Class
