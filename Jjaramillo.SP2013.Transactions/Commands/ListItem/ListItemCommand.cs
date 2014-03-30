using Microsoft.SharePoint;
using System.Collections.Generic;

namespace Jjaramillo.SP2013.Transactions.Commands.ListItem
{
    public abstract class ListItemCommand : SPCommand
    {
        protected SPListItem _ListItem;

        protected SPList _List;        

        public SPListItem ListItem { get { return _ListItem; } }

        public SPList List { get { return _List; } }

        public ListItemCommand(SPWeb web) : base(web) { }
        
        public ListItemCommand(SPListItem listItem, SPWeb web) : base(web)
        {
            _ListItem = listItem;
            _List = listItem.ParentList;
        }

        public ListItemCommand(SPList list, SPWeb web) : base(web)
        {
            _List = list;
        }

        public ListItemCommand(string listUrl, SPWeb web) : base(web)
        {
            _List = _SPWeb.GetList(string.Format("{0}/{1}", _SPWeb.Url, listUrl));
        }
    }
}
