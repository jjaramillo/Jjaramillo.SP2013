using Microsoft.SharePoint;

namespace Jjaramillo.SP2013.Transactions.Commands.Field
{
    /// <summary>
    /// Base class for all SPField related commands.
    /// </summary>
    public abstract class SPFieldCommand : SPCommand
    {
        protected SPField _SPField;

        /// <summary>
        /// The site column to which the command is going to be or has been applied.
        /// </summary>
        public SPField SPField { get { return _SPField; } }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="web">The web site</param>
        public SPFieldCommand(SPWeb web) : base(web) { }
    }
}
