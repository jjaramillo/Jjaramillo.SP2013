using Microsoft.SharePoint;
using System.Collections.Generic;

namespace Jjaramillo.SP2013.Transactions.Commands.ListItem
{
    public class AddListItemCommand : ListItemCommand
    {
        protected string _ContentTypeId;

        protected Dictionary<string, object> _ListItemProperties;

        /// <summary>
        /// A internal name / object dictionary containing the list item's field values
        /// </summary>
        public Dictionary<string, object> ListItemProperties { get { return _ListItemProperties; } }

        /// <summary>
        /// The new list item's Content Type Id
        /// </summary>
        public string ContentTypeId { get { return _ContentTypeId; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentTypeId">The content type to wich the item belongs</param>
        /// <param name="listItemProperties">A Dictionary with the field internal names and properties which are going to be assigned to the field</param>
        /// <param name="list">The list in which the item is going to be created</param>
        /// <param name="web">The web site</param>
        public AddListItemCommand(string contentTypeId, Dictionary<string, object> listItemProperties, SPList list, SPWeb web)
            : base(list, web)
        {
            _ListItemProperties = listItemProperties;
            _ContentTypeId = contentTypeId;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentTypeId">The content type to wich the item belongs</param>
        /// <param name="listItemProperties">A Dictionary with the field internal names and properties which are going to be assigned to the field</param>
        /// <param name="listUrl">The list in which the item is going to be created's url ('Lists/ListName' format)</param>
        /// <param name="web">The web site</param>
        public AddListItemCommand(string contentTypeId, Dictionary<string, object> listItemProperties, string listUrl, SPWeb web)
            : base(listUrl, web)
        {
            _ListItemProperties = listItemProperties;
            _ContentTypeId = contentTypeId;
        }

        public override void Execute()
        {
            AddListItem();
        }

        public override void Undo()
        {
            
        }

        /// <summary>
        /// Adds the list item to the list and sets the field values
        /// </summary>
        protected virtual void AddListItem()
        {
            SPContentTypeId listItemContentTypeId = new SPContentTypeId(_ContentTypeId);

            _ListItem = _List.AddItem();
            _ListItem[SPBuiltInFieldId.ContentTypeId] = listItemContentTypeId;
            foreach (KeyValuePair<string, object> listItemProperty in _ListItemProperties)            
            {
                _ListItem[listItemProperty.Key] = listItemProperty.Value;
            }
            _ListItem.Update();
        }

        /// <summary>
        /// Permanently removes the item from the list
        /// </summary>
        protected virtual void RemoveListItem()
        {
            _ListItem.Delete();
        }
    }
}
