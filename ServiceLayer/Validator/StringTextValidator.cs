using ServiceLayer.Messages;
using ServiceLayer.Utils.Interfaces;

namespace ServiceLayer.Validator
{
    public class StringTextValidator : IStringTextValidator
    {
        public StringTextValidator()
        {            
        }
        public string ValidateStrings(string stringBase, string stringComparer)
        {
            if(string.IsNullOrEmpty(stringBase))
            {
                return Notification.WARNING_KWD_EMPTY;
            }

            if(string.IsNullOrEmpty(stringComparer))
            {
                return Notification.WARNING_CONFIRM_KWD_EMPTY;
            }
            
            if (stringBase.Trim() != stringComparer.Trim())
            {
                return Notification.WARNING_KWDS_DISCREPANCY;
            }

            return string.Empty;
        }
    }
}
