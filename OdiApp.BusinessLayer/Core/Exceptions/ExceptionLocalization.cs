using System.Globalization;

namespace OdiApp.BusinessLayer.Core.Exceptions
{
    public class ExceptionLocalization : Exception
    {
        public ExceptionLocalization() : base() { }

        public ExceptionLocalization(string message) : base(message) { }

        public ExceptionLocalization(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
