using Jjaramillo.SP2013.Transactions.Contracts;
using Microsoft.SharePoint;

namespace Jjaramillo.SP2013.Transactions.Commands
{
    public abstract class SPCommand : ICommand
    {
        protected SPWeb _SPWeb;

        public SPWeb SPWeb { get { return _SPWeb; } }

        public SPCommand(SPWeb web) : base()
        {
            _SPWeb = web;
        }

        public abstract void Execute();

        public abstract void Undo();
        
    }
}
