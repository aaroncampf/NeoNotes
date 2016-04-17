Public Class DatabaseDbInitializer
	Inherits System.Data.Entity.DropCreateDatabaseAlways(Of DatabaseContainer)

	Protected Overrides Sub Seed(context As DatabaseContainer)
		MyBase.Seed(context)
		context.Companies.Add(New Company With {.ID = 1, .Name = "USA Market #1", .Address = "1245 Oxford Dr"})

	End Sub

End Class
