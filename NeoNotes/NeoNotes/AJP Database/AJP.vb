''' <summary>Contains specific functionality for AJP Northwest</summary>
Public NotInheritable Class AJP
	Private Sub New()
	End Sub

	'TODO: Upgrade this to grab one of the XML files that saves the table INVMA
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

End Class
