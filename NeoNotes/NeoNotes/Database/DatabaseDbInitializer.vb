Public Class DatabaseDbInitializer
	Inherits System.Data.Entity.DropCreateDatabaseAlways(Of DatabaseContainer)

	Protected Overrides Sub Seed(context As DatabaseContainer)
		MyBase.Seed(context)
		context.Companies.Add(New Company With {.ID = 1, .Name = "USA Market #1", .Address = "1245 Oxford Dr"})
		context.Companies.Find(1).Contacts.Add(New Contact With {.ID = 1, .Name = "Aaron Campf", .Phone = "5039298022", .Position = "CEO"})
		context.Contacts.Find(1).Notes.Add(New Note With {.ID = 1, .Date = Now, .Name = "Test", .Text = "Hello World"})
		context.Contacts.Find(1).Notes.Add(New Note With {.ID = 2, .Date = Now.AddDays(-1), .Name = "Test 2", .Text = "Hello World 2"})

	End Sub

End Class
