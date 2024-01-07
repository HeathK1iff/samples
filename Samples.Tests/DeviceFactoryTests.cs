using NUnit.Framework;
using Samples;
using Samples.Factory;
using Samples.Factory.Device;
using Samples.Factory.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Tests
{
    [TestFixture()]
    public class DeviceFactoryTests
    {
        [Test()]
        public void CreateDevice_PutEmptyModel_ThrowArgumentException()
        {
            DeviceFactory factory = new XiaomiDeviceFactory();
            
            Assert.Throws<ArgumentException>(()=>factory.CreateDevice(String.Empty));
            Assert.Throws<ArgumentException>(() => factory.CreateDevice(" "));
        }

        
        [Test()]
        public void CreateDevice_PutNotExistedModel_ThrowClassNotFoundException()
        {
            DeviceFactory factory = new XiaomiDeviceFactory();

            Assert.Throws<ClassNotFoundException>(() => factory.CreateDevice("XYZ"));
        }

        [Test()]
        public void CreateDevice_PutCorrectModel_True()
        {
            DeviceFactory factory = new XiaomiDeviceFactory();

            var device = factory.CreateDevice(MCCGQ11LM.ModelName);

            Assert.That(device.Vendor, Is.EqualTo(XiaomiBaseDevice.VendorName));
            Assert.That(device.Model, Is.EqualTo(MCCGQ11LM.ModelName));
            Assert.That(device is MCCGQ11LM, Is.True);
        }


    }
}