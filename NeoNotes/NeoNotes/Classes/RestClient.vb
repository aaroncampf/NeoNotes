Imports System.IO
Imports System.Net
Imports System.Text

Public Enum HttpVerb
	[GET]
	POST
	PUT
	DELETE
End Enum

Public Class RestClient
	Public Property EndPoint As String
	Public Property Method As HttpVerb
	Public Property ContentType As String
	Public Property PostData As String

	Public Sub New()
		Me.EndPoint = ""
		Me.Method = HttpVerb.[GET]
		Me.ContentType = "text/xml"
		Me.PostData = ""
	End Sub

	Public Sub New(EndPoint As String)
		Me.EndPoint = EndPoint
		Me.Method = HttpVerb.[GET]
		Me.ContentType = "text/xml"
		Me.PostData = ""
	End Sub

	Public Sub New(EndPoint As String, Method As HttpVerb)
		Me.EndPoint = EndPoint
		Me.Method = Method
		Me.ContentType = "text/xml"
		Me.PostData = ""
	End Sub

	Public Sub New(EndPoint As String, Method As HttpVerb, PostData As String)
		Me.EndPoint = EndPoint
		Me.Method = Method
		Me.ContentType = "text/xml"
		Me.PostData = PostData
	End Sub


	Public Function MakeRequest() As String
		Return MakeRequest("")
	End Function

	Public Function MakeRequest(parameters As String) As String
		Dim request = DirectCast(WebRequest.Create(EndPoint & parameters), HttpWebRequest)

		request.Method = Method.ToString()
		request.ContentLength = 0
		request.ContentType = ContentType

		If Not String.IsNullOrEmpty(PostData) AndAlso Method = HttpVerb.POST Then
			Dim encoding__1 = New UTF8Encoding()
			Dim bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData)
			request.ContentLength = bytes.Length

			Using writeStream = request.GetRequestStream()
				writeStream.Write(bytes, 0, bytes.Length)
			End Using
		End If

		Using response = DirectCast(request.GetResponse(), HttpWebResponse)
			Dim responseValue = String.Empty

			If response.StatusCode <> HttpStatusCode.OK Then
				Dim message = [String].Format("Request failed. Received HTTP {0}", response.StatusCode)
				Throw New ApplicationException(message)
			End If

			' grab the response
			Using responseStream = response.GetResponseStream()
				If responseStream IsNot Nothing Then
					Using reader = New StreamReader(responseStream)
						responseValue = reader.ReadToEnd()
					End Using
				End If
			End Using

			Return responseValue
		End Using
	End Function
End Class