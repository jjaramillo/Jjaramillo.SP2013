using Microsoft.SharePoint;
using System.Collections.Generic;

namespace Jjaramillo.SP2013.Transactions.Commands.ListItem
{
    public class UpdateListItemCommand : ListItemCommand
    {
        protected int _ListItemId;

        protected Dictionary<string, object> _ListItemProperties;

        protected Dictionary<string, object> _OldListItemProperties;

        /// <summary>
        /// The item's Id on the parent list
        /// </summary>
        public int ListItemId { get { return _ListItemId; } }

        /// <summary>
        /// A internal field name, property value dictionary containing the list item's new property values.
        /// </summary>
        public Dictionary<string, object> ListItemProperties { get { return _ListItemProperties; } }

        /// <summary>
        /// A internal field name, property value dictionary containing the list item's old property values.
        /// </summary>
        public Dictionary<string, object> OldListItemProperties { get { return _OldListItemProperties; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listItemProperties">A internal field name, property value dictionary containing the list item's new property values.</param>
        /// <param name="listItem">The item to be updated</param>
        /// <param name="web">The web site</param>
        public UpdateListItemCommand(Dictionary<string, object> listItemProperties, SPListItem listItem, SPWeb web) : base(listItem, web) 
        {
            _ListItemProperties = listItemProperties;
            _OldListItemProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listItemProperties">A internal field name, property value dictionary containing the list item's new property values.</param>
        /// <param name="listItemId">The item to be updated's Id on the parent list</param>
        /// <param name="list">The parent list</param>
        /// <param name="web">The web site</param>
        public UpdateListItemCommand(Dictionary<string, object> listItemProperties, int listItemId, SPList list, SPWeb web)
            : base(list, web)
        {
            _ListItemId = listItemId;
            _ListItem = _List.GetItemById(_ListItemId);
            _ListItemProperties = listItemProperties;
            _OldListItemProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listItemProperties">A internal field name, property value dictionary containing the list item's new property values.</param>
        /// <param name="listItemId">The item to be update's Id on the parent list</param>
        /// <param name="listUrl">The parent list's url</param>
        /// <param name="web">The web site</param>
        public UpdateListItemCommand(Dictionary<string, object> listItemProperties, int listItemId, string listUrl, SPWeb web)
            : base(listUrl, web)
        {
            _ListItemId = listItemId;
            _ListItem = _List.GetItemById(_ListItemId);
            _ListItemProperties = listItemProperties;
            _OldListItemProperties = new Dictionary<string, object>();
        }

        public override void Execute()
        {
            LoadOldValues();
            UpdateListItem();
        }

        public override void Undo()
        {
            RestoreOldValues();
        }

        /// <summary>
        /// Updates the list item with the new properties
        /// </summary>
        protected virtual void UpdateListItem()
        {
            foreach (KeyValuePair<string, object> listItemProperty in _ListItemProperties)
            {
                _ListItem[listItemProperty.Key] = listItemProperty.Value;
            }
            _ListItem.Update();
        }

        /// <summary>
        /// Restore the list item to it's property values before being updated
        /// </summary>
        protected virtual void RestoreOldValues()
        {
            foreach (KeyValuePair<string, object> listItemProperty in _OldListItemProperties)
            {
                _ListItem[listItemProperty.Key] = listItemProperty.Value;
            }
            _ListItem.Update();
        }

        /// <summary>
        /// Loads the list item's old values into memory
        /// </summary>
        protected virtual void LoadOldValues()
        {
            foreach(KeyValuePair<string, object> listItemProperty in _ListItemProperties)
            {
                _OldListItemProperties.Add(listItemProperty.Key, _ListItem[listItemProperty.Key]);
            }
        }
    }
}
