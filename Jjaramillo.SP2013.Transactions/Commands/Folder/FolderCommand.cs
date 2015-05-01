using Microsoft.SharePoint;

namespace Jjaramillo.SP2013.Transactions.Commands.Folder
{
    public abstract class FolderCommand : SPCommand
    {
        protected SPFolder _Folder;

        protected SPList _List;

        public SPFolder Folder { get { return _Folder; } }

        public SPList List { get { return _List; } }

        public FolderCommand(SPWeb web) : base(web) { }
        
        public FolderCommand(SPFolder folder, SPWeb web) : base(web)
        {
            _Folder = folder;
            _List = folder.Item.ParentList;
        }

        public FolderCommand(SPList list, SPWeb web) : base(web)
        {
            _List = list;
        }

        public FolderCommand(string listUrl, SPWeb web)
            : base(web)
        {
            _List = _SPWeb.GetList(string.Format("{0}/{1}", _SPWeb.Url, listUrl));
        }
    }
}
