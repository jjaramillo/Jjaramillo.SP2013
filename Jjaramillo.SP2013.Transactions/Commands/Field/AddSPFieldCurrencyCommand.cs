using Microsoft.SharePoint;

namespace Jjaramillo.SP2013.Transactions.Commands.Field
{
    public class AddSPFieldCurrencyCommand : AddSPFieldCommand
    {
        protected int _LocaleId;
        protected double _MaximumValue;
        protected double _MinimumValue;
        protected int _Decimals;

        public AddSPFieldCurrencyCommand(string displayName, string name, string group, string fieldType, object defaultValue,  double maximumValue, double minimumValue, int decimals,
            int localeId, bool hidden, bool required, bool indexed, SPWeb web) 
            : base( displayName,  name,  group,  fieldType,  defaultValue,  hidden,  required,  indexed,  web)
        {
            _LocaleId = localeId;
            _MaximumValue = maximumValue;
            _MinimumValue = minimumValue;
            _Decimals = decimals;
        }

        protected override void AddSPField()
        {            
            base.AddSPField();
            _SPField = _SPWeb.Fields.GetField(_Name);
            SPFieldCurrency spFieldCurrency = _SPField as SPFieldCurrency;
            spFieldCurrency.MaximumValue = _MaximumValue;
            spFieldCurrency.MinimumValue = _MinimumValue;
            spFieldCurrency.CurrencyLocaleId = _LocaleId;
            spFieldCurrency.Update(true);
        }
    }
}
