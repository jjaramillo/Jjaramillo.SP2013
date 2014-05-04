<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FieldManagement.aspx.cs" Inherits="Jjaramillo.SP2013.ContentTypeManagement.Layouts.ContentTypeManagement.FieldManagement" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="stylesheet" href="/_layouts/15/INC/metro.ui/css/metro-bootstrap.css" />
    <script src="/_layouts/15/INC/ContentTypeManagement/jquery-2.1.0.min.js" type="text/javascript"></script>
    <script src="/_layouts/15/INC/ContentTypeManagement/jquery.widget.min.js" type="text/javascript"></script>
    <script src="/_layouts/15/INC/metro.ui/min/metro.min.js" type="text/javascript"></script>
    <script src="/_layouts/15/INC/metro.ui/js/metro-calendar.js" type="text/javascript"></script>
    <script src="/_layouts/15/INC/metro.ui/js/metro-datepicker.js" type="text/javascript"></script>
    <script src="/_layouts/15/INC/ContentTypeManagement/knockout-3.1.0.js" type="text/javascript"></script>
    <script src="/_layouts/15/INC/ContentTypeManagement/knockout.validation.min.js" type="text/javascript"></script>
    <script src="/_layouts/15/INC/ContentTypeManagement/FieldManagementViewModel.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="metro">
        <div class="container">
            <div class="grid">
                <div class="row">
                    <div class="span6">
                        <div class="input-control text">
                            <label>Name</label>
                            <input type="text" value="" placeholder="Name" data-bind="value: name" />
                            <button class="btn-clear"></button>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="input-control text">
                            <label>Display Name</label>
                            <input type="text" value="" placeholder="Display Name" data-bind="value: displayName" />
                            <button class="btn-clear"></button>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="span6">
                        <div class="input-control select">
                            <label>Field Type</label>
                            <select data-bind="options: fieldTypes, value: fieldType, optionsCaption: '[Select a field type]', event: { change: fieldTypeSelectionChange }">
                            </select>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="input-control text" data-bind="visible: fieldType() == 'Text'">
                            <label>Default Value</label>
                            <input type="text" placeholder="Default value for text field" data-bind="value: defaultValue" />
                            <button class="btn-clear"></button>
                        </div>
                        <div class="input-control textarea" data-bind="visible: fieldType() == 'Note'">
                            <label>Default Value</label>
                            <textarea placeholder="Default value for note field" data-bind="value: defaultValue"></textarea>
                        </div>
                        <div class="input-control" data-bind="visible: fieldType() == 'Boolean'">
                            <label>Default Value</label>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: defaultValue" value="True" />
                                    <span class="check"></span>
                                    True
                                </label>
                            </div>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: defaultValue" value="False" />
                                    <span class="check"></span>
                                    False
                                </label>
                            </div>
                        </div>
                        <div class="input-control text" data-bind="visible: fieldType() == 'Number'">
                            <label>Default Value</label>
                            <input type="text" placeholder="Default value for number field" data-bind="value: defaultValue" />
                            <button class="btn-clear"></button>
                        </div>
                        <div class="input-control text" data-bind="visible: fieldType() == 'Currency'">
                            <label>Default Value</label>
                            <input type="text" placeholder="Default value for currency field" data-bind="value: defaultValue" />
                            <button class="btn-clear"></button>
                        </div>
                        <div class="input-control text" data-role="datepicker" data-bind="visible: fieldType() == 'DateTime', value: defaultValue"
                            data-format='dd/mm/yyyy'
                            data-effect='slide'
                            data-locale='en'
                            data-week-start='0'>
                            <label>Default Value</label>
                            <input type="text" placeholder="Default value for datetime field">
                            <button class="btn-date"></button>
                        </div>
                        <div class="input-control text" data-bind="visible: fieldType() == 'Choice'">
                            <label>Add a Choice</label>
                            <div>
                                <div style="width: 350px; display: inline-block;">
                                    <input type="text" placeholder="Type your choice value here" data-bind="value: choice" />
                                    <button class="btn-clear"></button>
                                    <div class="listview-outlook" data-bind="foreach: choices">
                                        <a href="#" class="list" data-bind="click: $parent.setChoiceDefaultValue">
                                            <div class="list-content">
                                                <span class="list-title" data-bind="text: $data"></span>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div style="width: 85px; float: right;">
                                    <button data-bind="click: addChoice" class="default">Add</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" data-bind="visible: fieldType() == 'Currency'">
                    <div class="span3">
                        <div class="input-control text">
                            <label>Decimals</label>
                            <input type="text" placeholder="Number of decimals for the currency field" data-bind="value: decimals" />
                            <button class="btn-clear"></button>
                        </div>
                    </div>
                    <div class="span3">
                        <div class="input-control select">
                            <label>Locale</label>
                            <select data-bind="optionsCaption: '[Select a locale to format the currency field]', value: localeId, options: locales, optionsText: 'LocaleName', optionsValue: 'LCID'"></select>                            
                        </div>
                    </div>
                    <div class="span3">
                        <div class="input-control text">
                            <label>Minimum Value</label>
                            <input type="text" placeholder="Minimum value for the currency field" data-bind="value: minimumValue"/>
                            <button class="btn-clear"></button>
                        </div>
                    </div>
                    <div class="span3">
                        <div class="input-control text">
                            <label>Maximum Value</label>
                            <input type="text" placeholder="Maximum value for the currency field" data-bind="value: maximumValue" />
                            <button class="btn-clear"></button>
                        </div>
                    </div>                    
                </div>
                <div class="row" data-bind="visible: fieldType() == 'Number'">
                    <div class="span4">
                        <div class="input-control text">
                            <label>Decimals</label>
                            <input type="text" placeholder="Number of decimals for the currency field" data-bind="value: decimals" />
                            <button class="btn-clear"></button>
                        </div>
                    </div>                    
                    <div class="span4">
                        <div class="input-control text">
                            <label>Minimum Value</label>
                            <input type="text" placeholder="Minimum value for the currency field" data-bind="value: minimumValue"/>
                            <button class="btn-clear"></button>
                        </div>
                    </div>
                    <div class="span4">
                        <div class="input-control text">
                            <label>Maximum Value</label>
                            <input type="text" placeholder="Maximum value for the currency field" data-bind="value: maximumValue" />
                            <button class="btn-clear"></button>
                        </div>
                    </div>                    
                </div>
                <div class="row" data-bind="visible: fieldType() == 'DateTime' || fieldType() == 'Choice'">
                    <div class="span4 offset6">
                        <div class="input-control" data-bind="visible: fieldType() == 'DateTime'">
                            <label>Display Mode</label>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: format" value="DateOnly" />
                                    <span class="check"></span>
                                    Date Only
                                </label>
                            </div>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: format" value="DateTime" />
                                    <span class="check"></span>
                                    Date & Time
                                </label>
                            </div>
                        </div>
                        <div class="input-control" data-bind="visible: fieldType() == 'Choice'">
                            <label>Display Mode</label>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: format" value="Drowdown" />
                                    <span class="check"></span>
                                    Drow-Down Menu
                                </label>
                            </div>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: format" value="RadioButtons" />
                                    <span class="check"></span>
                                    Radio Button List
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="span4">
                        <div class="input-control">
                            <label>Required</label>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: required" value="True" />
                                    <span class="check"></span>
                                    Yes
                                </label>
                            </div>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: required" value="False" />
                                    <span class="check"></span>
                                    No
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="span4">
                        <div class="input-control">
                            <label>Hidden</label>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: hidden" value="True" />
                                    <span class="check"></span>
                                    Yes
                                </label>
                            </div>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: hidden" value="False" />
                                    <span class="check"></span>
                                    No
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="span4">
                        <div class="input-control">
                            <label>Indexed</label>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: indexed" value="True" />
                                    <span class="check"></span>
                                    Yes
                                </label>
                            </div>
                            <div class="input-control radio">
                                <label>
                                    <input type="radio" data-bind="checked: indexed" value="False" />
                                    <span class="check"></span>
                                    No
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var fieldManagementViewModel = new FieldManagementViewModel();
        ko.applyBindings(fieldManagementViewModel);
    </script>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
</asp:Content>
