using Microsoft.SharePoint;

namespace Jjaramillo.SP2013.Transactions.Commands.Folder
{
    public class CopyFolderCommand : FolderCommand
    {
        private SPListItem _Item;        
        private SPFolder _SourceFolder;

        private string _NewUrl;
        private string _ContentTypeId;

        public SPListItem Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        public SPFolder Folder
        {
            get { return _Folder; }
            set { _Folder = value; }
        }

        public CopyFolderCommand(SPFolder sourceFolder, string newUrl, string contentTypeId, SPWeb web)
            : base(web)
        {
            _NewUrl = newUrl;
            _SourceFolder = sourceFolder;
            _ContentTypeId = contentTypeId;
        }

        public override void Execute()
        {
            CopyFolder();
        }

        public override void Undo()
        {
            DeleteFolder();
        }

        private void CopyFolder()
        {
            _SourceFolder.CopyTo(_NewUrl);
            _Folder = _SPWeb.GetFolder(_NewUrl);
            _Item = _Folder.Item;
            _Item["ContentTypeId"] = _ContentTypeId;
            _Item.Update();
        }

        private void DeleteFolder()
        {           
            _Item.Delete();
        }
    }
}
