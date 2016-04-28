
<PropertyChanged.ImplementPropertyChanged>
Partial Public Class Company
	Public Function ToXML() As XElement
		Return <Company ID=<%= Me.ID %> Name=<%= Me.Name %> Address=<%= Me.Address %> City=<%= Me.City %> Misc=<%= Me.Misc %>>
				   <%= From Contact In Me.Contacts Select Contact.AsXML %>

				   <Quotes>
					   <%= From Quote In Me.Quotes Select Quote.AsXML %>
				   </Quotes>
			   </Company>
	End Function
End Class

<PropertyChanged.ImplementPropertyChanged>
Partial Public Class Contact
	Public Function AsXML() As XElement
		Return <Contact ID=<%= Me.ID %> Name=<%= Me.Name %> Email=<%= Me.Email %> Phone=<%= Me.Phone %> Position=<%= Me.Position %>>
				   <%= From Note In Me.Notes Select Note.ToXML %>
			   </Contact>
	End Function
End Class

<PropertyChanged.ImplementPropertyChanged>
Partial Public Class Note
	Public Function ToXML() As XElement
		Return <Note ID=<%= Me.ID %> Title=<%= Me.Title %> Text=<%= Me.Text %>/>
	End Function
End Class

<PropertyChanged.ImplementPropertyChanged>
Partial Public Class Quote
	Public Function AsXML() As XElement
		Return <Quote ID=<%= Me.ID %> Date=<%= Me.Date %> Name=<%= Me.Name %>>
				   <%= From Line In Me.Lines Select Line.ToXML %>
			   </Quote>
	End Function
End Class

<PropertyChanged.ImplementPropertyChanged>
Partial Public Class QuoteLine
	Public Function ToXML() As XElement
		Return <Detail ID=<%= Me.ID %> Display=<%= Me.Display %> DESC=<%= Me.DESC %> UNIT=<%= Me.UNIT %> Cost=<%= Me.COST %>/>
	End Function
End Class
