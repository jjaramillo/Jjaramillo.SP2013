using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jjaramillo.SP2013.Transactions.Commands.ListItem
{
    public class MoveListItemCommand : ListItemCommand
    {
        private SPListItem _CopyItem;
        private Guid _BinElementId;
        private List<string> _SiteColumnGroups;
        private List<string> _AdditionalFields;

        public SPListItem CopyItem
        {
            get { return _CopyItem; }
        }

        public MoveListItemCommand(SPListItem copyItem, string targetListName, List<string> siteColumnGroups, SPWeb web)
            : base(web)
        {
            _CopyItem = copyItem;
            _SiteColumnGroups = siteColumnGroups;
            _List = _SPWeb.Lists.TryGetList(targetListName);
        }

        public MoveListItemCommand(SPListItem copyItem, string targetListName, List<string> siteColumnGroups, List<string> additionalFields, SPWeb web)
            : base(web)
        {
            _CopyItem = copyItem;
            _SiteColumnGroups = siteColumnGroups;
            _AdditionalFields = additionalFields;
            _List = _SPWeb.Lists.TryGetList(targetListName);
        }

        public MoveListItemCommand(SPListItem copyItem, SPList targetList, List<string> siteColumnGroups, SPWeb web)
            : base(web)
        {
            _CopyItem = copyItem;
            _SiteColumnGroups = siteColumnGroups;
            _List = targetList;
        }

        public MoveListItemCommand(SPListItem copyItem, SPList targetList, List<string> siteColumnGroups, List<string> additionalFields, SPWeb web)
            : base(web)
        {
            _CopyItem = copyItem;
            _AdditionalFields = additionalFields;
            _SiteColumnGroups = siteColumnGroups;
            _List = targetList;
        }

        public MoveListItemCommand(int itemId, string sourceName, string targetListName, List<string> siteColumnGroups, SPWeb web)
            : base(web)
        {
            _SiteColumnGroups = siteColumnGroups;
            SPList sourceList = _SPWeb.Lists.TryGetList(sourceName);
            _CopyItem = sourceList.GetItemById(itemId);
            _List = _SPWeb.Lists.TryGetList(targetListName);
        }

        public MoveListItemCommand(int itemId, string sourceName, string targetListName, List<string> siteColumnGroups, List<string> additionalFields, SPWeb web)
            : base(web)
        {
            _SiteColumnGroups = siteColumnGroups;
            _AdditionalFields = additionalFields;
            SPList sourceList = _SPWeb.Lists.TryGetList(sourceName);
            _CopyItem = sourceList.GetItemById(itemId);
            _List = _SPWeb.Lists.TryGetList(targetListName);
        }

        public MoveListItemCommand(int itemId, string sourceName, SPList targetList, List<string> siteColumnGroups, SPWeb web)
            : base(web)
        {
            _SiteColumnGroups = siteColumnGroups;
            SPList sourceList = _SPWeb.Lists.TryGetList(sourceName);
            _CopyItem = sourceList.GetItemById(itemId);
            _List = targetList;
        }

        public MoveListItemCommand(int itemId, string sourceName, SPList targetList, List<string> siteColumnGroups, List<string> additionalFields, SPWeb web)
            : base(web)
        {
            _SiteColumnGroups = siteColumnGroups;
            _AdditionalFields = additionalFields;
            SPList sourceList = _SPWeb.Lists.TryGetList(sourceName);
            _CopyItem = sourceList.GetItemById(itemId);
            _List = targetList;
        }

        public MoveListItemCommand(int itemId, SPList sourceList, SPList targetList, List<string> siteColumnGroups, SPWeb web)
            : base(web)
        {
            _SiteColumnGroups = siteColumnGroups;
            _CopyItem = sourceList.GetItemById(itemId);
            _List = targetList;
        }

        public MoveListItemCommand(int itemId, SPList sourceList, SPList targetList, List<string> siteColumnGroups, List<string> additionalFields, SPWeb web)
            : base(web)
        {
            _SiteColumnGroups = siteColumnGroups;
            _AdditionalFields = additionalFields;
            _CopyItem = sourceList.GetItemById(itemId);
            _List = targetList;
        }

        private void MoveListItem()
        {
            _ListItem = _List.AddItem();
            IEnumerable<SPField> fields;
            foreach (string siteGroup in _SiteColumnGroups)
            {
                fields = from SPField field in _CopyItem.Fields
                         where field.Group.Equals(siteGroup)
                         select field;
                foreach (SPField field in fields)
                {
                    _ListItem[field.InternalName] = _CopyItem[field.InternalName];
                }
            }
            if (_AdditionalFields != default(List<string>))
                foreach (string field in _AdditionalFields)
                    _ListItem[field] = _CopyItem[field];

            _ListItem.Update();
            _BinElementId = _CopyItem.Recycle();
        }

        private void RestoreListItem()
        {
            if (_ListItem != default(SPListItem)) { _ListItem.Delete(); }
            if (_BinElementId != default(Guid)) { _SPWeb.RecycleBin.Restore(new Guid[] { _BinElementId }); }
        }

        public override void Execute()
        {
            MoveListItem();
        }

        public override void Undo()
        {
            RestoreListItem();
        }
    }
}
