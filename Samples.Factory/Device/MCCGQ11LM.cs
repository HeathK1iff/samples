namespace Samples.Factory.Device
{
    public class MCCGQ11LM : XiaomiBaseDevice
    {
        public static string ModelName = "MCCGQ11LM";
        public MCCGQ11LM(Guid id) : base(id, ModelName)
        {
        }
    }

}