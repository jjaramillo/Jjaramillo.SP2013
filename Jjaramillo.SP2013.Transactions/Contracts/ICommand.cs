
namespace Jjaramillo.SP2013.Transactions.Contracts
{
    public interface ICommand
    {
        /// <summary>
        /// Executes the operation defined by the command
        /// </summary>
        
        void Execute();
        /// <summary>
        /// Undo the operation previously executed by the command
        /// </summary>
        void Undo();
    }
}
