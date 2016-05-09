Class Application
	Private Sub Application_DispatcherUnhandledException(sender As Object, e As Windows.Threading.DispatcherUnhandledExceptionEventArgs)
		MsgBox(e.Exception.ToString)
	End Sub

	' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
	' can be handled in this file.

End Class
