Public Class DatabaseDbInitializer
	Inherits System.Data.Entity.DropCreateDatabaseAlways(Of DatabaseContainer)

	Protected Overrides Sub Seed(context As DatabaseContainer)
		MyBase.Seed(context)
		context.Companies.Add(New Company With {.ID = 1, .Name = "USA Market #1", .Address = "1245 Oxford Dr"})
		context.Companies.Add(New Company With {.ID = 2, .Name = "Canada Market #1", .Address = "1245 Oxford Dr"})
		context.Companies.Add(New Company With {.ID = 3, .Name = "USA Market #3", .Address = "1245 Oxford Dr"})


		context.Companies.Find(1).Contacts.Add(New Contact With {.ID = 1, .Name = "Aaron Campf", .Phone = "5039298022", .Position = "CEO"})
		context.Contacts.Find(1).Notes.Add(New Note With {.ID = 1, .Date = Now, .Title = "Test", .Text = "Hello World"})
		context.Contacts.Find(1).Notes.Add(New Note With {.ID = 2, .Date = Now.AddDays(-1), .Title = "Test 2", .Text = "Hello World 2"})

		context.Companies.Find(1).Quotes.Add(New Quote With {.ID = 1, .Date = Now, .Name = "Test"})
		context.Quotes.Find(1).Lines.Add(New QuoteLine With {.ID = 1, .Display = 2, .DESC = "Desc 1", .UNIT = "Unit", .COST = 1.25})
		context.Quotes.Find(1).Lines.Add(New QuoteLine With {.ID = 1, .Display = 1, .DESC = "Desc 2", .UNIT = "Unit", .COST = 1.25})



		Dim XML = XElement.Load("C:\Aaron\UserSettings.xml")
		context.Settings.Add(New Setting With {
							 .ID = 1,
							 .Address = "Address",
							 .Email = "a@gmail.com",
							 .Gmail = XML.@GMail,
							 .GmailPassword = XML.@GMailPassword,
							 .Name = "Aaron Campf",
							 .Phone = "(503) 929-8022"
							})

	End Sub

End Class
