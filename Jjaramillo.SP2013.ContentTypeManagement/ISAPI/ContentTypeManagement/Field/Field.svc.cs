using Jjaramillo.SP2013.ContentTypeManagement.Entities;
using Jjaramillo.SP2013.Transactions.Commands.Field;
using Microsoft.SharePoint;
using System;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace Jjaramillo.SP2013.ContentTypeManagement.ISAPI.ContentTypeManagement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Field : IField
    {

        [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Add", BodyStyle = WebMessageBodyStyle.Wrapped)]
        public void AddField(string displayName, string name, string group, string fieldType, object defaultValue, string format, bool hidden, bool required, bool indexed, string[] choices,
            double maximumValue, double minimumValue, int decimals, int localeId, string contextUrl)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(contextUrl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        AddSPFieldCommand addSPFieldCommand = default(AddSPFieldCommand);
                        SPFieldType spFieldType = (SPFieldType)Enum.Parse(typeof(SPFieldType), fieldType, true);
                        switch (spFieldType)
                        {
                            case SPFieldType.Choice:
                                addSPFieldCommand = new AddSPFieldChoiceCommand(displayName, name, group, fieldType, defaultValue, hidden, required, indexed, choices, web);
                                break;
                            case SPFieldType.Currency:
                                addSPFieldCommand = new AddSPFieldCurrencyCommand(displayName, name, group, fieldType, defaultValue, maximumValue, minimumValue, decimals, localeId
                                ,hidden, required, indexed, web);
                                break;
                            default:
                                addSPFieldCommand = new AddSPFieldCommand(displayName, name, group, fieldType, defaultValue, hidden, required, indexed, web);
                                break;
                        }
                        addSPFieldCommand.Execute();
                    }
                }
            });
        }

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "SPFieldTypes", BodyStyle = WebMessageBodyStyle.Wrapped)]
        public string[] GetSPFieldTypes()
        {
            return (from spfieldTypeName in Enum.GetNames(typeof(SPFieldType))
                    orderby spfieldTypeName
                    select spfieldTypeName).ToArray();
        }

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Groups?contextUrl={contextUrl}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        public string[] GetSPFieldGroups(string contextUrl)
        {
            string[] groups = default(string[]);
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(contextUrl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPFieldCollection fields = web.Fields;
                        groups = (from SPField field in fields
                                  select field.Group).Distinct().OrderBy(group => group).ToArray();
                    }
                }
            });
            return groups;
        }

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Locales", BodyStyle = WebMessageBodyStyle.Wrapped)]
        public LocaleInfo[] GetLocales()
        {
            LocaleInfo[] locales = default(LocaleInfo[]);
            CultureInfo[] allLocales = CultureInfo.GetCultures(CultureTypes.AllCultures);
            locales = (from cultureInfo in allLocales
                       orderby cultureInfo.DisplayName
                       select new LocaleInfo
                       {
                           LCID = cultureInfo.LCID,
                           LocaleName = cultureInfo.DisplayName
                       }).ToArray();
            return locales;
        }
    }
}
