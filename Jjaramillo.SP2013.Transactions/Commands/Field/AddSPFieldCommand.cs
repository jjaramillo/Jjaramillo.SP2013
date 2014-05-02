using Microsoft.SharePoint;
using System;

namespace Jjaramillo.SP2013.Transactions.Commands.Field
{
    public class AddSPFieldCommand : SPFieldCommand
    {
        protected string _DisplayName;
        protected string _Name;
        protected string _StaticName;
        protected string _Group;
        protected string _FieldType;

        protected bool _Hidden;
        protected bool _Required;        
        protected bool _Indexed;

        private object _DefaultValue;

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
        /// <param name="web">The web site</param>
        public AddSPFieldCommand(string displayName, string name, string group, string fieldType, object defaultValue, bool hidden, bool required, bool indexed, SPWeb web)
            : base(web)
        {
            _DisplayName = displayName;
            _Name = name;
            _Group = group;
            _FieldType = fieldType;
            _Required = required;            
            _Indexed = indexed;
            _DefaultValue = defaultValue;
        }

        /// <summary>
        /// Adds the field to the website's site column collection
        /// </summary>
        protected virtual void AddSPField()
        {
            if (_SPWeb.Fields.ContainsField(_Name) || _SPWeb.Fields.ContainsField(_DisplayName)) { throw new Exception("There's already a field with either the display name or the name on the site"); }
            _SPField = _SPWeb.Fields.CreateNewField( _FieldType, _Name);
            _SPField.StaticName = _Name;
            _SPField.Group = _Group;
            _SPWeb.Fields.Add(_SPField);

            _SPField = _SPWeb.Fields.GetField(_Name);
            _SPField.Title = _DisplayName;
            _SPField.Hidden = _Hidden;
            _SPField.Required = _Required;
            _SPField.Indexed = _Indexed;
            _SPField.DefaultValue = _DefaultValue.ToString();
            _SPField.Update();
        }

        /// <summary>
        /// Removes the field from the site's column collection
        /// </summary>
        protected virtual void RemoveSPField()
        {
            _SPWeb.Fields.Delete(_Name);
            _SPWeb.Update();
        }

        public override void Execute()
        {
            AddSPField();
        }

        public override void Undo()
        {
            RemoveSPField();
        }
    }
}
