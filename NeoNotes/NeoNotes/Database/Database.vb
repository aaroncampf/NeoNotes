Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq

Partial Public Class Database
	Inherits DbContext

	Public Sub New()
		'MyBase.New("(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|NeoNotes.mdf")
		MyBase.New("Data Source=|DataDirectory|NeoNotesContainer.sdf")
	End Sub

	Public Overridable Property Companies As DbSet(Of Company)
	Public Overridable Property Contacts As DbSet(Of Contact)
	Public Overridable Property Locations As DbSet(Of Location)
	Public Overridable Property Notes As DbSet(Of Note)
	Public Overridable Property QuoteLines As DbSet(Of QuoteLine)
	Public Overridable Property Quotes As DbSet(Of Quote)
	Public Overridable Property QuoteSectionDetails As DbSet(Of QuoteSectionDetail)
	Public Overridable Property QuoteSections As DbSet(Of QuoteSection)
	Public Overridable Property Settings As DbSet(Of Setting)

	Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
		'modelBuilder.Entity(Of Company)() _
		'	.HasMany(Function(e) e.Contacts) _
		'	.WithRequired(Function(e) e.Company) _
		'	.HasForeignKey(Function(e) e.Company_ID) _
		'	.WillCascadeOnDelete(False)

		modelBuilder.Entity(Of Company)() _
			.HasMany(Function(e) e.Contacts) _
			.WithRequired(Function(e) e.Company) _
			.WillCascadeOnDelete(True)

		modelBuilder.Entity(Of Company)() _
			.HasMany(Function(e) e.Quotes) _
			.WithRequired(Function(e) e.Company) _
			.HasForeignKey(Function(e) e.Company_ID) _
			.WillCascadeOnDelete(True)

		modelBuilder.Entity(Of Company)() _
			.HasMany(Function(e) e.Locations) _
			.WithRequired(Function(e) e.Company) _
			.HasForeignKey(Function(e) e.Companies_ID) _
			.WillCascadeOnDelete(True)

		modelBuilder.Entity(Of Contact)() _
			.HasMany(Function(e) e.Notes) _
			.WithRequired(Function(e) e.Contact) _
			.HasForeignKey(Function(e) e.Contact_ID) _
			.WillCascadeOnDelete(True)

		modelBuilder.Entity(Of QuoteLine)() _
			.Property(Function(e) e.COST) _
			.HasPrecision(18, 0)

		modelBuilder.Entity(Of Quote)() _
			.HasMany(Function(e) e.QuoteLines) _
			.WithRequired(Function(e) e.Quote) _
			.HasForeignKey(Function(e) e.Quote_ID) _
			.WillCascadeOnDelete(True)

		modelBuilder.Entity(Of QuoteSectionDetail)() _
			.Property(Function(e) e.COST) _
			.HasPrecision(18, 0)

		modelBuilder.Entity(Of QuoteSection)() _
			.HasMany(Function(e) e.QuoteSectionDetails) _
			.WithRequired(Function(e) e.QuoteSection) _
			.HasForeignKey(Function(e) e.QuoteSection_ID) _
			.WillCascadeOnDelete(True)
	End Sub
End Class
