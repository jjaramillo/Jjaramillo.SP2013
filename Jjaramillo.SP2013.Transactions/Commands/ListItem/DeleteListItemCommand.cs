using Microsoft.SharePoint;
using System;

namespace Jjaramillo.SP2013.Transactions.Commands.ListItem
{
    public class DeleteListItemCommand : ListItemCommand
    {
        protected int _ListItemId;

        protected Guid _RecycleBinListItemId;

        protected string _ListItemUrl;

        /// <summary>
        /// The item's Id on the parent list
        /// </summary>
        public int ListItemId { get { return _ListItemId; } }

        /// <summary>
        /// The item's unique identifier on site's the recycle bin
        /// </summary>
        public Guid RecycleBinListItemId { get { return _RecycleBinListItemId; } }

        /// <summary>
        /// The item's url before being recycled
        /// </summary>
        public string ListItemUrl { get { return _ListItemUrl; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listItem">The item to be recycled</param>
        /// <param name="web">The web site</param>
        public DeleteListItemCommand(SPListItem listItem, SPWeb web) : base(listItem, web) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listItemId">The item to be recycled's Id on the parent list</param>
        /// <param name="list">The parent list</param>
        /// <param name="web">The web site</param>
        public DeleteListItemCommand(int listItemId, SPList list, SPWeb web) : base(list, web)
        {
            _ListItemId = listItemId;
            _ListItem = _List.GetItemById(_ListItemId);
            _ListItemUrl = string.Format("{0}/{1}",_SPWeb.Url, _ListItem.Url);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listItemId">The item to be recycled's Id on the parent list</param>
        /// <param name="listUrl">The parent list's url</param>
        /// <param name="web">The web site</param>
        public DeleteListItemCommand(int listItemId, string listUrl, SPWeb web) : base(listUrl, web)
        {
            _ListItemId = listItemId;
            _ListItem = _List.GetItemById(_ListItemId);
            _ListItemUrl = string.Format("{0}/{1}", _SPWeb.Url, _ListItem.Url);
        }

        public override void Execute()
        {
            DeleteListItem();
        }

        public override void Undo()
        {
            RestoreListItem();
        }

        /// <summary>
        /// Recycles the item (sents the item to the site's recycle bin)
        /// </summary>
        protected virtual void DeleteListItem()
        {
            _RecycleBinListItemId = _ListItem.Recycle();
        }

        /// <summary>
        /// Restores the item from the recycle bin into its original location and reloads it
        /// </summary>
        protected virtual void RestoreListItem()
        {
            _SPWeb.RecycleBin.Restore(new Guid[] { _RecycleBinListItemId });
            _ListItem = _SPWeb.GetListItem(_ListItemUrl);
        }
    }
}
