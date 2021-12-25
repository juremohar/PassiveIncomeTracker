namespace PassiveIncomeTracker.Helpers
{
    public class InterestCalculator
    {
        public static CalculatedInterest CalculateCompoundingInterest(double amount, double yearlyRate, string intervalPayoutCode) 
        {
            // https://www.thecalculatorsite.com/articles/finance/compound-interest-formula.php

            var n = intervalPayoutCode switch
            {
                CalculationInterval.Daily => DateTime.IsLeapYear(DateTime.UtcNow.Year) ? 366 : 365,
                CalculationInterval.Weekly => 52,
                CalculationInterval.Monthly => 12,
                CalculationInterval.Yearly => 1,
                _ => throw new Exception("Wrong calculation interval"),
            };

            double compoundedAmount = amount * Math.Pow((1 + yearlyRate / 100 / n), n);

            return new CalculatedInterest
            {
                CompoundedAmount = compoundedAmount,
                Interest = compoundedAmount - amount
            };
        }
    }

    public class CalculatedInterest 
    {
        public double CompoundedAmount { get; set; }
        public double Interest { get; set; }
    }

    public class CalculationInterval
    {
        public const string Daily = "daily";
        public const string Weekly = "weekly";
        public const string Monthly = "monthly";
        public const string Yearly = "yearly";
    }
}
