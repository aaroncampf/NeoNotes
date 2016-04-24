Imports System.Windows.Forms

Public Class RichTextBoxEditor

	ReadOnly Property Text(BasicText As Boolean) As String
		Get
			If BasicText Then
				Return New TextRange(mainRTB.Document.ContentStart, mainRTB.Document.ContentEnd).Text
			Else
				Dim RTBText As New TextRange(mainRTB.Document.ContentStart, mainRTB.Document.ContentEnd)
				Dim Temp = My.Computer.FileSystem.GetTempFileName

				Using A As IO.FileStream = IO.File.Open(Temp, IO.FileMode.Create)
					RTBText.Save(A, DataFormats.Rtf)
				End Using
				Return My.Computer.FileSystem.ReadAllText(Temp)
			End If
		End Get
	End Property


	ReadOnly Property HTML As String
		Get
			Dim Node = XElement.Parse(Markup.XamlWriter.Save(Me.mainRTB)).Elements()(0)

			If Node Is Nothing Then
				Return <HTML><BODY><P></P></BODY></HTML>.ToString
			Else
				Dim IDK As String = XElement.Parse(Markup.XamlWriter.Save(Me.mainRTB)).Elements()(0).ToString
				Return HTMLConverter.HtmlFromXamlConverter.ConvertXamlToHtml(IDK)
			End If
		End Get
	End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Text">The Text the RichTextBox will Start with</param>
    ''' <remarks></remarks> 
    ''' <stepthrough>Enabled</stepthrough>
    <DebuggerStepThrough()>
	Sub New(Optional Text As String = "")
		InitializeComponent()
		Me.mainRTB.SpellCheck.IsEnabled = True
		Me.mainRTB.AppendText(Text)
		Me.mainRTB.Document.LineHeight = 1
	End Sub



	Private Sub bb_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnFile.Click
		Dim muiPrint As New Controls.MenuItem With {.Header = "muiPrint"}
		AddHandler muiPrint.Click, Sub() MsgBox("Under Construction")

		Dim muiSave As New Controls.MenuItem With {.Header = "Save"}
		AddHandler muiSave.Click,
			Sub()
				Dim File As New SaveFileDialog With {.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop, .AddExtension = True, .DefaultExt = ".rtf"}
				If File.ShowDialog = DialogResult.OK AndAlso File.ValidateNames Then
					Dim RTBText As New Documents.TextRange(mainRTB.Document.ContentStart, mainRTB.Document.ContentEnd)
					RTBText.Save(IO.File.Open(File.FileName, IO.FileMode.Create), DataFormats.Rtf)
				End If
			End Sub

		Dim muiLoad As New Controls.MenuItem With {.Header = "Load"}
		AddHandler muiLoad.Click, Sub() MsgBox("Under Construction")



		btnFile.ContextMenu = New Controls.ContextMenu
		btnFile.ContextMenu.Items.Clear()
		btnFile.ContextMenu.Items.Add(muiPrint)
		btnFile.ContextMenu.Items.Add(muiSave)
		btnFile.ContextMenu.Items.Add(muiLoad)
		btnFile.ContextMenu.IsOpen = True
	End Sub


End Class
