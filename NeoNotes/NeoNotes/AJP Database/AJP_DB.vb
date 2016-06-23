Imports System.Data.Entity

Public Class AJP_DB
	Inherits DbContext
	Public Overridable Property AJP_INVMAs() As DbSet(Of AJP_INVMA)

	Public Sub New()
		MyBase.New("AJP_ANS_Cache.sdf")
	End Sub


End Class


Public Class AJP_INVMA
	<ComponentModel.DataAnnotations.Key>
	Public Property ITEMNO As Integer
	Public Property DESCRIP As String
End Class
