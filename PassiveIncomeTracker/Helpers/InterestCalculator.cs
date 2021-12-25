namespace PassiveIncomeTracker.Helpers
{
    public class InterestCalculator
    {
        public static CalculatedInterest CalculateCompoundingInterest(double amount, double yearlyRate, CalculationInterval interval) 
        {
            // https://www.thecalculatorsite.com/articles/finance/compound-interest-formula.php

            int n;
            switch (interval)
            {
                case CalculationInterval.Daily: n = DateTime.IsLeapYear(DateTime.UtcNow.Year) ? 366 : 365; break;
                case CalculationInterval.Weekly: n = 52; break;
                case CalculationInterval.Monthly: n = 12; break;
                case CalculationInterval.Yearly: n = 1; break;
                default: throw new Exception("Wrong calculation interval");
            }

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

    public enum CalculationInterval : int
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}
