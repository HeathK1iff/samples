namespace Samples.NullObject
{
    public interface IPaymentTransactionFactory
    {
        public Payment CreatePayment(DateTime date);
    }

}