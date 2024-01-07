namespace Samples.Factory
{
    public abstract class BaseDevice {
        public BaseDevice(Guid id, string vendor, string model) 
        { 
            Id = id;
            Vendor = vendor;
            Model = model;
        }
        public Guid Id { get; }
        public string Vendor { get; }
        public string Model { get; }
    }

}