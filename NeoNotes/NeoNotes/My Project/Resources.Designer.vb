﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("NeoNotes.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to a_hWEmajHdoAAAAAAAABQa7YbKfAH0mrPauZ-rFiwtp-0oWPVXSZKEB3d6WD4el9.
        '''</summary>
        Friend ReadOnly Property Dropbox_AccessToken() As String
            Get
                Return ResourceManager.GetString("Dropbox_AccessToken", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;ArrayOfKeyValueOfstringstring xmlns=&quot;http://schemas.microsoft.com/2003/10/Serialization/Arrays&quot; xmlns:i=&quot;http://www.w3.org/2001/XMLSchema-instance&quot;&gt;&lt;KeyValueOfstringstring&gt;&lt;Key&gt;TokenProvConfigType&lt;/Key&gt;&lt;Value&gt;AppLimit.CloudComputing.SharpBox.StorageProvider.DropBox.DropBoxConfiguration&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;&lt;KeyValueOfstringstring&gt;&lt;Key&gt;TokenCredType&lt;/Key&gt;&lt;Value&gt;AppLimit.CloudComputing.SharpBox.StorageProvider.DropBox.DropBoxToken&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;&lt;KeyValueOfstringstring&gt;&lt;Key&gt;TokenD [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property DropBox_Token() As String
            Get
                Return ResourceManager.GetString("DropBox_Token", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;FlowDocument xmlns=&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot; 
        '''			  xmlns:x=&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot; 
        '''			  xmlns:xrd=&quot;clr-namespace:Aaron.Reports;assembly=Aaron.Reports&quot;
        '''              ColumnWidth=&quot;21cm&quot; PageHeight=&quot;27.0cm&quot; PageWidth=&quot;21cm&quot; FontFamily=&quot;Century Gothic&quot;&gt;
        '''    &lt;!--
        '''    ColumnWidth=&quot;21cm&quot; PageHeight=&quot;29.7cm&quot; PageWidth=&quot;21cm&quot;
        '''    xmlns:crcv=&quot;clr-namespace:CodeReason.Reports.Charts.Visifire;assembly=CodeReason.Reports.Charts.Visifire&quot; 
        '''    xmlns:xrd=&quot;c [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property Hand_Made_Quote() As String
            Get
                Return ResourceManager.GetString("Hand_Made_Quote", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
