''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
''' <features></features>
''' <stepthrough></stepthrough>
Public Class frmMsgBuilder
    'Dim Attachments As New List(Of Mod_Interfaces.IXML)

    <DebuggerStepThrough()>
	Private Sub New()
		InitializeComponent()
	End Sub


	<DebuggerStepThrough()>
	Private Function OkShowDialog() As Forms.DialogResult
		Return MyBase.ShowDialog()
	End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="GetHTML"></param>
    ''' <param name="Multiline_Subject"></param>
    ''' <param name="Title"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <stepthrough></stepthrough>
    Shared Shadows Function ShowDialog(GetHTML As Boolean, Multiline_Subject As Boolean, Optional Title As String = "Message Writer") _
																			 As Tuple(Of Forms.DialogResult, Tuple(Of String, String))
		Dim Form As New frmMsgBuilder With {.Text = Title}, RTBE1 = New RichTextBoxEditor
		Form.txtSubject.Multiline = Multiline_Subject
		Form.ElementHost1.Child = RTBE1
		If Form.OkShowDialog() = Forms.DialogResult.No Then Return Nothing

		If GetHTML Then
			Return New Tuple(Of Forms.DialogResult, Tuple(Of String, String))(Form.DialogResult, New Tuple(Of String, String)(Form.txtSubject.Text, RTBE1.HTML))
		Else
			Return New Tuple(Of Forms.DialogResult, Tuple(Of String, String))(Form.DialogResult, New Tuple(Of String, String)(Form.txtSubject.Text, RTBE1.Text(False)))
		End If
	End Function

	Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
		Me.DialogResult = Forms.DialogResult.Cancel
		Me.Close()
	End Sub

	Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
		Me.DialogResult = Forms.DialogResult.OK
		Me.Close()
	End Sub

End Class