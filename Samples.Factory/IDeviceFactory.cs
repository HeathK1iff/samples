namespace Samples.Factory
{
    public interface IDeviceFactory
    {
        BaseDevice CreateDevice(string model);
    }

}