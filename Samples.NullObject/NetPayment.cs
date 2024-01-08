namespace Samples.NullObject
{
    public class NetPayment : Payment
    {
        public NetPayment(Guid id) : base(id)
        {
        }

        public override double Calculate()
        {
            return this.Amount;
        }
    }

}