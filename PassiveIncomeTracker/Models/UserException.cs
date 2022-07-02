namespace PassiveIncomeTracker.Models
{
    using System.Globalization;

    // custom exception class for throwing application specific exceptions (e.g. for validation) 
    // that can be caught and handled within the application
    public class UserException : Exception
    {
        public UserException() : base() { }

        public UserException(string message) : base(message) { }

        public UserException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
