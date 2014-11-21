using Jjaramillo.SP2013.Transactions.Commands.ListItem;
using Microsoft.SharePoint;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jjaramillo.SP2013.Transactions.Commands.File
{
    public class AddFileItemCommand : ListItemCommand
    {
        protected string _ContentTypeId;
        protected string _FileName;
        protected Hashtable _FieldValues;        
        protected byte[] _FileData;
        protected bool _Overwrite;
        protected SPFile _File;

        /// <summary>
        /// The new list item's Content Type Id
        /// </summary>
        public string ContentTypeId { get { return _ContentTypeId; } }

        /// <summary>
        /// The item's field values
        /// </summary>
        public Hashtable FieldValues { get { return _FieldValues; } }

        /// <summary>
        /// The item's file name (including extension)
        /// </summary>
        public string FileName { get { return _FileName; } }

        /// <summary>
        /// The item's file contents
        /// </summary>
        public byte[] FileData { get { return _FileData; } }

        /// <summary>
        /// Indicates if the file should be overwritten in case it exists already
        /// </summary>
        public bool Overwrite { get { return _Overwrite; } }

        /// <summary>
        /// The underlying file added to the library
        /// </summary>
        public SPFile File { get { return _File; } }

        public AddFileItemCommand(string fileName, byte[] fileData, Hashtable fieldValues, string contentTypeId, bool overwrite, SPList list, SPWeb web)
            : base(list, web)
        {
            _FileName = fileName;
            _FileData = fileData;
            _FieldValues = fieldValues;
            _ContentTypeId = contentTypeId;
            _Overwrite = overwrite;
        }

        public AddFileItemCommand(string fileName, byte[] fileData, Hashtable fieldValues, string contentTypeId, bool overwrite, string listUrl, SPWeb web)
            : base(listUrl, web)
        {
            _FileName = fileName;
            _FileData = fileData;
            _FieldValues = fieldValues;
            _ContentTypeId = contentTypeId;
            _Overwrite = overwrite;
        }

        public override void Execute()
        {
            AddFileItem();            
        }

        public override void Undo()
        {
            RemoveListItem();
        }

        protected void AddFileItem()
        {
            SPContentTypeId listItemContentTypeId = new SPContentTypeId(_ContentTypeId);
            _FieldValues.Add("ContentType", _SPWeb.ContentTypes[listItemContentTypeId].Name);
            _File= _List.RootFolder.Files.Add(_FileName, _FileData, _FieldValues, _Overwrite);
            _ListItem = _File.Item;
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
