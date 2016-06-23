Imports AppLimit.CloudComputing.SharpBox

Public NotInheritable Class AJP
	Private Sub New()
	End Sub

	Private Shared Sub DownloadDatabase()
		Dim dropBoxStorage As New CloudStorage()
		Dim dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox)
		Dim accessToken As ICloudStorageAccessToken

		'load a valid security token from file
		Dim byt As Byte() = Text.Encoding.UTF8.GetBytes(My.Resources.DropBox_Token)
		accessToken = dropBoxStorage.DeserializeSecurityTokenFromBase64(Convert.ToBase64String(byt))

		'open the connection
		Dim storageToken = dropBoxStorage.Open(dropBoxConfig, accessToken)
		dropBoxStorage.DownloadFile("/Storage/AJP_ANS_Cache.sdf", AppDomain.CurrentDomain.GetData("DataDirectory"))

		My.Settings.LastUpdated = Now
		My.Settings.Save() '<--- Do I need this?
	End Sub

	Public Shared Function Get_Item_Names() As List(Of String)
		If My.Computer.Network.IsAvailable And My.Settings.LastUpdated < Now.AddDays(-7) Then
			DownloadDatabase()
		End If


		Return Aggregate x In New AJP_DB().AJP_INVMAs Select x.DESCRIP Into ToList
	End Function

End Class
