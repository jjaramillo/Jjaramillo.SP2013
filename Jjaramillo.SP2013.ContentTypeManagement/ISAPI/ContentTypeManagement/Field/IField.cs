using System.ServiceModel;

namespace Jjaramillo.SP2013.ContentTypeManagement.ISAPI.ContentTypeManagement
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IField" in both code and config file together.
    [ServiceContract]
    public interface IField
    {
        [OperationContract]
        void AddField(string displayName, string name, string group, string fieldType, object defaultValue, bool hidden, bool required, bool indexed, string[] choices, string contextUrl);

        [OperationContract]
        string[] GetSPFieldTypes();

        [OperationContract]
        string[] GetSPFieldGroups(string contextUrl);
    }
}

