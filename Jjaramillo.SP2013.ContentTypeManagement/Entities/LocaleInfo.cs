using System.Runtime.Serialization;

namespace Jjaramillo.SP2013.ContentTypeManagement.Entities
{
    [DataContract]
    public class LocaleInfo
    {
        [DataMember]
        public int LCID { get; set; }
        
        [DataMember]
        public string LocaleName { get; set; }

    }
}
