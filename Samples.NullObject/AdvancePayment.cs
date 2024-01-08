namespace Samples.NullObject
{
    public class AdvancePayment : Payment
    {
        public AdvancePayment(Guid id) : base(id)
        {
        }

        public override double Calculate()
        {
            return this.Amount * 0.5;
        }
    }

}