using Microsoft.SharePoint;

namespace Jjaramillo.SP2013.Transactions.Commands
{
    public abstract class SPCommandCollection : CommandCollection
    {
        protected SPWeb _SPWeb;

        public SPWeb SPWeb { get { return _SPWeb; } }

        public SPCommandCollection(SPWeb web)
            : base()
        {
            _SPWeb = web;
        }
    }
}
