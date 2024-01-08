using Samples.Factory;
using Samples.Factory.Device;
using Samples.Factory.Exception;

namespace Samples.Tests.Patterns
{
    [TestFixture()]
    public class FactoryTests
    {
        [Test()]
        public void CreateDevice_PutEmptyModel_ThrowArgumentException()
        {
            IDeviceFactory factory = new XiaomiDeviceFactory();

            Assert.Throws<ArgumentException>(() => factory.CreateDevice(string.Empty));
            Assert.Throws<ArgumentException>(() => factory.CreateDevice(" "));
        }


        [Test()]
        public void CreateDevice_PutNotExistedModel_ThrowClassNotFoundException()
        {
            IDeviceFactory factory = new XiaomiDeviceFactory();

            Assert.Throws<ClassNotFoundException>(() => factory.CreateDevice("XYZ"));
        }

        [Test()]
        public void CreateDevice_PutCorrectModel_True()
        {
            IDeviceFactory factory = new XiaomiDeviceFactory();

            var device = factory.CreateDevice(MCCGQ11LM.ModelName);

            Assert.That(device.Vendor, Is.EqualTo(XiaomiBaseDevice.VendorName));
            Assert.That(device.Model, Is.EqualTo(MCCGQ11LM.ModelName));
            Assert.That(device is MCCGQ11LM, Is.True);
        }


    }
}