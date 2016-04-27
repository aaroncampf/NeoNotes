Imports AppLimit.CloudComputing.SharpBox

''' <summary>
'''
''' </summary>
''' <remarks></remarks>
''' <features></features>
''' <stepthrough>Enabled</stepthrough>
Public NotInheritable Class App
    '<DebuggerStepThrough()>
    ' Public Property Cloud As New CloudStorage
    Public ReadOnly Cloud As New CloudStorage

	Private Shared _App As App
	Public Shared ReadOnly Property App As App
        Get
            Return _App
        End Get
    End Property


	''Move all of the New Properties and Sub New into the DropBox Application
	'Private Shared _GetApp As App
	'Public Shared ReadOnly Property GetApp As App
	'    Get
	'        Return _GetApp
	'    End Get
	'End Property

	''' <summary>
	''' Creates the Single Instance of the App
	''' </summary>
	''' <param name="KeepOld">If True then If the App has Already been created Then the CreateApp will be Canceled</param>
	''' <param name="Email">The DropBox Account Email</param>
	''' <param name="Password">The Account's Password</param>
	''' <param name="ConsumerKey">The Application's ConsumerKey</param>
	''' <param name="ConsumerSecret">The Application's ConsumerSecret</param>
	''' <remarks></remarks>
	''' <stepthrough>Enabled</stepthrough>
	Public Shared Sub CreateApp(KeepOld As Boolean, Email As String, Password As String, Optional ConsumerKey As String = "l2pq95nq3wxmlt7", Optional ConsumerSecret As String = "ajfu0aer1c5bh9c")
        If _App Is Nothing OrElse KeepOld = False Then
            _App = New App(Email, Password, ConsumerKey, ConsumerSecret)
        End If
    End Sub

    ''' <summary>
    ''' Generates A DropBox SecurityToken, Serializes it in the File at the Specified Path [Creates the File], Then Return
    ''' </summary>
    ''' <param name="Config">The DropBoxConfiguration you are using for the DropBox App</param>
    ''' <param name="ConsumerKey">The ConsumerKey for the App</param>
    ''' <param name="ConsumerSecret">The ConsumerSecret for the App</param>
    ''' <param name="Path">The Path where you want to Serialize the Key</param>
    ''' <remarks><para>
    ''' Do Not Delete this! You will Need it.
    ''' If you save the Created XML File in A Resource File, Then Save it as Binary so you can Add it to A MemoryStream Directly [Also Prevents an Error]
    ''' </para></remarks>
    ''' <exception cref="Net.WebException">The Program was Unable to Access DropBox.com</exception>
    ''' <stepthrough>Disabled</stepthrough>
    Private Shared Sub GenerateSecurityToken(Config As StorageProvider.DropBox.DropBoxConfiguration, Cloud As AppLimit.CloudComputing.SharpBox.CloudStorage, ConsumerKey As String, ConsumerSecret As String, Path As String) 'As ICloudStorageAccessToken
        Dim GotAccessToken As TriState = TriState.UseDefault
        Try
            If Not My.Computer.Network.IsAvailable OrElse Not My.Computer.Network.Ping("www.DropBox.com") Then
				Throw New Net.WebException("Unable to Connect to DropBox.com", Net.WebExceptionStatus.ConnectFailure)
			End If

            Config.AuthorizationCallBack = New Uri("C:\") 'Try to make it so it does not Open anything
            Dim requestToken As StorageProvider.DropBox.DropBoxRequestToken = StorageProvider.DropBox.DropBoxStorageProviderTools.GetDropBoxRequestToken(Config, ConsumerKey, ConsumerSecret)

			Process.Start(StorageProvider.DropBox.DropBoxStorageProviderTools.GetDropBoxAuthorizationUrl(Config, requestToken))
			Threading.Thread.Sleep(1000)

            GotAccessToken = TriState.False
            Dim AccessToken As ICloudStorageAccessToken = StorageProvider.DropBox.DropBoxStorageProviderTools.ExchangeDropBoxRequestTokenIntoAccessToken(Config, ConsumerKey, ConsumerSecret, requestToken)
            GotAccessToken = True

            Using FSteam As IO.FileStream = New IO.FileInfo(Path).Create
                Cloud.SerializeSecurityTokenToStream(Cloud.Open(Config, AccessToken), FSteam)
            End Using
        Catch ex As UnauthorizedAccessException
            If GotAccessToken = TriState.False Then
                Throw New UnauthorizedAccessException("Unable to Create the AccessToken. Odds are it is because AuthorizationUrl has not been Opened yet", ex)
            Else
                Throw ex
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub 'Do Not Delete this! You will Need it.

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="Email"></param>
    ''' <param name="Password"></param>
    ''' <param name="ConsumerKey"></param>
    ''' <param name="ConsumerSecret"></param>
    ''' <remarks></remarks>
    ''' <stepthrough>Enabled</stepthrough>
    Private Sub New(Email As String, Password As String, Optional ConsumerKey As String = "l2pq95nq3wxmlt7", Optional ConsumerSecret As String = "ajfu0aer1c5bh9c")
		Try
			Using memoryStream As New IO.MemoryStream(My.Resources.DropBox_Token)
                'In-lined     CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox)
                Me.Cloud.Open(CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox), Me.Cloud.DeserializeSecurityToken(memoryStream))
			End Using
		Catch ex As Exception
			Dim MSG$ = "So long as this is not working you cannot Upload Data onto the internet" & vbCrLf & vbCrLf & "There is also Automatic uploading"
			Trace.Fail("CloudApp.App Failed to be Created.", MSG & vbCrLf & vbCrLf & "The Error has been logged")
		End Try
    End Sub

    ' ''' <summary>
    ' '''
    ' ''' </summary>
    ' ''' <param name="Email"></param>
    ' ''' <param name="Password"></param>
    ' ''' <param name="ConsumerKey"></param>
    ' ''' <param name="ConsumerSecret"></param>
    ' ''' <remarks>Do Not Remove this! I will Create A New Project at some Point and Move this into A New BaseClass</remarks>
    ' ''' <stepthrough>Enabled</stepthrough>
    'Private Sub New( Email As String,  Password As String, Optional  ConsumerKey As String = "l2pq95nq3wxmlt7", Optional  ConsumerSecret As String = "ajfu0aer1c5bh9c")
    '    Dim dropBoxConfig = CloudStorage.GetCloudConfigurationEasy(nSupportedCloudConfigurations.DropBox)
    '    Dim dropBoxCredentials As New StorageProvider.DropBox.DropBoxCredentials With {.UserName = Email, .Password = Password,
    '                                                                                   .ConsumerKey = ConsumerKey, .ConsumerSecret = ConsumerSecret}
    '    Dim storageToken = Cloud.Open(dropBoxConfig, dropBoxCredentials)
    '    _Users = New AppUsers(Cloud)
    'End Sub

End Class