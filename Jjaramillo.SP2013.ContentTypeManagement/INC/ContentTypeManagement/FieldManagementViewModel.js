/// <reference path="jquery-2.1.0.min.js" />
/// <reference path="knockout-3.1.0.js" />
/// <reference path="knockout.validation.min.js" />
var FieldManagementViewModel = function () {
    var self = this;
    var statusId = '';

    this.displayName = ko.observable('').extend({ required: true });
    this.name = ko.observable('').extend({ required: true });
    this.group = ko.observable('').extend({ required: true });
    this.fieldType = ko.observable('').extend({ required: true });
    this.defaultValue = ko.observable('');
    this.hidden = ko.observable('False');
    this.required = ko.observable('False');
    this.indexed = ko.observable('True');
    this.choices = ko.observableArray([]);
    this.fieldTypes = ko.observableArray([]);
    this.groups = ko.observableArray([]);
    this.choice = ko.observable('');
    this.format = ko.observable('');
    this.localeId = ko.observable(0);
    this.locales = ko.observableArray([]);
    this.decimals = ko.observable(0);
    this.minimumValue = ko.observable(0);
    this.maximumValue =  ko.observable(0);

    this.fieldTypeSelectionChange = function () {
        self.defaultValue('');
    }
    this.addChoice = function (data, event) {
        self.choices.push(self.choice());
        self.choice('');
        return false;
    }
    this.setChoiceDefaultValue = function(data)
    {
        self.defaultValue(data);
    }
    this.cancelSubmit = function (data, event) {
        if (event.keyCode === 13) { return false; }
        else { return true; }
    }

    ko.validatedObservable(self);
    validationResult = ko.validation.group(self, { deep: true });

    $.ajax(
        {
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            type: 'GET',
            url: _spPageContextInfo.siteAbsoluteUrl + '/15/_vti_bin/ContentTypeManagement/Field/Field.svc/SPFieldTypes',
            error: OnError,
            success: OnSPFieldTypeFetch
        });


    function OnError(jqXHR, status, errorMessage) {
        CloseWaitingModal();
        ExecuteOrDelayUntilScriptLoaded(function () {
            statusId = SP.UI.Status.addStatus('Error', errorMessage, true);
            SP.UI.Status.setStatusPriColor(statusId, 'red');
        }, 'sp.js');
    }

    function OnSPFieldTypeFetch(data, status, jqXHR) {
        var fieldTypeCollection = data.GetSPFieldTypesResult;
        for (var index = 0; index < fieldTypeCollection.length; index++) {
            self.fieldTypes.push(fieldTypeCollection[index]);
        }
    }
}