using System.Diagnostics;

namespace Samples.NullObject
{
    public abstract class Payment : IEquatable<Payment?>
    {
        private static Payment _nullObject = new NullPayment(Guid.Empty);

        public Payment(Guid id)
        {
            Id = id;
        }

        public abstract double Calculate();

        public Guid Id { get; }
        public double Amount { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public static Payment Null => _nullObject;

        public override bool Equals(object? obj)
        {
            return Equals(obj as Payment);
        }

        public bool Equals(Payment? other)
        {
            return other is not null &&
                   Id.Equals(other.Id) &&
                   Amount == other.Amount &&
                   CurrencyCode == other.CurrencyCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Amount, CurrencyCode);
        }

        public static bool operator ==(Payment? left, Payment? right)
        {
            return EqualityComparer<Payment>.Default.Equals(left, right);
        }

        public static bool operator !=(Payment? left, Payment? right)
        {
            return !(left == right);
        }

        private class NullPayment : Payment
        {
            public NullPayment(Guid id) : base(id)
            {
                Amount = default(double);
                CurrencyCode = string.Empty;
            }

            public override double Calculate()
            {
                return 0;
            }
        }
    }
}