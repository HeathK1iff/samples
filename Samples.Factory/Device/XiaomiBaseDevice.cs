namespace Samples.Factory.Device
{
    public class XiaomiBaseDevice : BaseDevice
    {
        public static string VendorName = "Xiaomi";
        protected XiaomiBaseDevice(Guid id, string model) : base(id, VendorName, model)
        {
        }
    }

}