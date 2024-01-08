namespace Samples.NullObject
{
    public class DateBasedPaymentTransactionFactory : IPaymentTransactionFactory
    {
        public Payment CreatePayment(DateTime date)
        {
            if (IsTodayAndFuture(date))
            {
                if (IsLastDayOfMonth(date))
                {
                    return new NetPayment(Guid.NewGuid());
                }
                return new AdvancePayment(Guid.NewGuid());
            }

            return Payment.Null;
        }

        private bool IsTodayAndFuture(DateTime date)
        {
            return date > GetToday();
        }

        protected virtual DateTime GetToday()
        {
            return DateTime.Today;
        }

        private bool IsLastDayOfMonth(DateTime date)
        {
           DateTime firstDayOfNextMonth = new DateTime(GetToday().Year, GetToday().Month + 1, 1);
           return date.Equals(firstDayOfNextMonth.AddDays(-1));
        }
    }

}