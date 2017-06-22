Class Application
	Private Sub Application_DispatcherUnhandledException(sender As Object, e As Windows.Threading.DispatcherUnhandledExceptionEventArgs)
		MsgBox(e.Exception.ToString, MsgBoxStyle.Critical & MsgBoxStyle.OkOnly)
		If MsgBox("Click [Yes] to continue the application and [No] to Crash", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
			e.Handled = True
		End If
	End Sub

	' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
	' can be handled in this file.
	Protected Overrides Sub OnStartup(e As StartupEventArgs)
		MyBase.OnStartup(e)

		Dim AppFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\OneDrive\AJP Applications"
		AppDomain.CurrentDomain.SetData("DataDirectory", AppFolder)
	End Sub

End Class
