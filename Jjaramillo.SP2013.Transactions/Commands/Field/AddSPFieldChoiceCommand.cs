using Microsoft.SharePoint;

namespace Jjaramillo.SP2013.Transactions.Commands.Field
{
    public class AddSPFieldChoiceCommand : AddSPFieldCommand
    {
        protected string[] _Choices;

        /// <summary>
        /// Adds a site column to the website
        /// </summary>
        /// <param name="displayName">The site column's name</param>
        /// <param name="name">The site column's internal name</param>
        /// <param name="group">The site column's group</param>
        /// <param name="fieldType">The site column's field type enumeration string value</param>
        /// <param name="defaultValue">The site column's default value</param>
        /// <param name="hidden">The site column's default visibility</param>
        /// <param name="required">The site column's default obligatoriness</param>
        /// <param name="indexed"></param>
        /// <param name="choices">A string array containing the site column's choices</param>
        /// <param name="web">The web site</param>
        public AddSPFieldChoiceCommand(string displayName, string name, string group, string fieldType, object defaultValue, bool hidden, bool required, bool indexed, string[] choices, SPWeb web)
            : base(displayName, name, group, fieldType, defaultValue, hidden, required, indexed, web)
        {
            _Choices = choices;
        }

        protected override void AddSPField()
        {
            base.AddSPField();
            _SPField = _SPWeb.Fields.GetField(_Name);
            SPFieldChoice spFieldChoice = _SPField as SPFieldChoice;
            spFieldChoice.Choices.AddRange(_Choices);            
            spFieldChoice.Update(true);
        }
    }
}
