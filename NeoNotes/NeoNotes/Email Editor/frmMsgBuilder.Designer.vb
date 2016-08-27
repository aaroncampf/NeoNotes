<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMsgBuilder
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.txtSubject = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
		Me.btnOk = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer2.Panel1.SuspendLayout()
		Me.SplitContainer2.Panel2.SuspendLayout()
		Me.SplitContainer2.SuspendLayout()
		Me.SuspendLayout()
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.txtSubject)
		Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.ElementHost1)
		Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
		Me.SplitContainer1.Size = New System.Drawing.Size(590, 471)
		Me.SplitContainer1.SplitterDistance = 112
		Me.SplitContainer1.TabIndex = 1
		'
		'txtSubject
		'
		Me.txtSubject.Dock = System.Windows.Forms.DockStyle.Fill
		Me.txtSubject.Location = New System.Drawing.Point(0, 13)
		Me.txtSubject.Multiline = True
		Me.txtSubject.Name = "txtSubject"
		Me.txtSubject.Size = New System.Drawing.Size(590, 99)
		Me.txtSubject.TabIndex = 0
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Label1.Location = New System.Drawing.Point(0, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(43, 13)
		Me.Label1.TabIndex = 1
		Me.Label1.Text = "Subject"
		'
		'ElementHost1
		'
		Me.ElementHost1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ElementHost1.Location = New System.Drawing.Point(0, 13)
		Me.ElementHost1.Name = "ElementHost1"
		Me.ElementHost1.Size = New System.Drawing.Size(590, 342)
		Me.ElementHost1.TabIndex = 0
		Me.ElementHost1.Text = "ElementHost1"
		Me.ElementHost1.Child = Nothing
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
		Me.Label2.Location = New System.Drawing.Point(0, 0)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(31, 13)
		Me.Label2.TabIndex = 3
		Me.Label2.Text = "Body"
		'
		'SplitContainer2
		'
		Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
		Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
		Me.SplitContainer2.Name = "SplitContainer2"
		'
		'SplitContainer2.Panel1
		'
		Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer1)
		'
		'SplitContainer2.Panel2
		'
		Me.SplitContainer2.Panel2.Controls.Add(Me.btnOk)
		Me.SplitContainer2.Panel2.Controls.Add(Me.btnCancel)
		Me.SplitContainer2.Size = New System.Drawing.Size(996, 471)
		Me.SplitContainer2.SplitterDistance = 590
		Me.SplitContainer2.TabIndex = 2
		'
		'btnOk
		'
		Me.btnOk.Location = New System.Drawing.Point(89, 436)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 6
		Me.btnOk.Text = "Ok"
		Me.btnOk.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.Location = New System.Drawing.Point(8, 436)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 5
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'frmMsgBuilder
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(996, 471)
		Me.ControlBox = False
		Me.Controls.Add(Me.SplitContainer2)
		Me.Name = "frmMsgBuilder"
		Me.Text = "Message Writer"
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel1.PerformLayout()
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		Me.SplitContainer1.Panel2.PerformLayout()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.ResumeLayout(False)
		Me.SplitContainer2.Panel1.ResumeLayout(False)
		Me.SplitContainer2.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer2.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents ElementHost1 As System.Windows.Forms.Integration.ElementHost
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
