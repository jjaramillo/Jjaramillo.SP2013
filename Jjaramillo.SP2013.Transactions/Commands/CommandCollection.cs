using Jjaramillo.SP2013.Transactions.Contracts;
using System.Collections.Generic;

namespace Jjaramillo.SP2013.Transactions.Commands
{
    public abstract class CommandCollection : ICommand
    {
        protected Stack<ICommand> _Commands;

        /// <summary>
        /// Default empty base constructor.
        /// </summary>
        public CommandCollection()
        {
            _Commands = new Stack<ICommand>();
        }

        /// <summary>
        /// Executes the operation defined by the command
        /// </summary>
        public abstract void Execute();        

        /// <summary>
        /// Undo the collection of commands executed by this command, starting from the last one to the first one.
        /// </summary>
        public virtual void Undo()
        {
            while (_Commands.Count > 0)
            {
                _Commands.Pop().Undo();
            }
        }
    }
}
