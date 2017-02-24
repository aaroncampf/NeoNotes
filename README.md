# NeoNotes

A basic CRM application created for use at AJP Northwest by Aaron Campf.
The goal of this application is to create a very lightweight CRM application that can export data to other applications. Maintainability and minimize are the primary focus of this application to ensure maximum productivity with minimal overhead for entering data.

## Download
https://aaroncampf.itch.io/neonotes
Note: Need to create a branch for this

## Disclaimer
In return for porting my original Winforms application into WPF and for the initial redesign I was allowed to use the project as an example.
Please note that there are a few issues using it when you do not have the correct setup intended for AJP Northwest

## Software Details
* Programming Language: VB.Net
* User Interface: Windows Presentation Foundation (WPF) and XAML
* Report Generation: Simple-WPF-Reports
* Dropbox.Api used for Dropbox intergration
* Entity Framework for data storage
* Fody and ProperyChanged.Fody to add in INotifyPropertyChanged automatically


## Data Structure
Company -> Contact -> Note
Company -> Quote

## Features

### Company
* Store contacts and quotes
* Print all companies
* Search all companies

### Contact
* Store notes
* Search all contacts
* Send email in Gmail
* Email quote


### Note
* Edit

### Quote
* Edit
* Autocomplete for Description textbox
* Centering textbox (think secion headers)
* Re-ordering items

## Dropbox Intergration
* Upload data as XML to dropbox for use in Android applications
* Will not work when downloaded due to missing keys