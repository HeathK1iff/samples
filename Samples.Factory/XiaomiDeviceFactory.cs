using Samples.Factory.Device;
using Samples.Factory.Exception;

namespace Samples.Factory
{
    public class XiaomiDeviceFactory : IDeviceFactory
    {
        public BaseDevice CreateDevice(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentException($"Model should not be empty");
            }
            if (model.Equals(MCCGQ11LM.ModelName, StringComparison.InvariantCultureIgnoreCase))
            {
                return new MCCGQ11LM(Guid.NewGuid());
            }

            throw new ClassNotFoundException($"Model {model} is not found for {XiaomiBaseDevice.VendorName} devices");
        }
    }

}