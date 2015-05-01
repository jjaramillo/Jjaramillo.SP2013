using Microsoft.SharePoint;

namespace Jjaramillo.SP2013.Transactions.Commands.Folder
{
    public class MoveFolderCommand : FolderCommand
    {        
        private SPListItem _Item;        

        private string _NewUrl;
        private string _OldUrl;
        private string _ContentTypeId;

        public SPListItem Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        public MoveFolderCommand(SPFolder folder, string newUrl, string contentTypeId, SPWeb web)
            : base(web)
        {
            _NewUrl = newUrl;
            _Folder = folder;
            _Item = _Folder.Item;
            _OldUrl = string.Format("{0}/{1}", _SPWeb.Url, _Folder.Url);
            _ContentTypeId = contentTypeId;
        }

        public override void Execute()
        {
            MoveFolder();
        }

        public override void Undo()
        {
            RestoreFolder();
        }

        private void MoveFolder()
        {            
            _Folder.MoveTo(_NewUrl);
            _Folder = _SPWeb.GetFolder(_NewUrl);
            _Item = _Folder.Item;
            _Item["ContentTypeId"] = _ContentTypeId;
            _Item.Update();
        }

        private void RestoreFolder()
        {            
            _Folder.MoveTo(_OldUrl);
            _Folder = _SPWeb.GetFolder(_OldUrl);
            _Item = _Folder.Item;
            _Item["ContentTypeId"] = _ContentTypeId;
            _Item.Update();
        }
    }
}
