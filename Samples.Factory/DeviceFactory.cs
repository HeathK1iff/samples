namespace Samples.Factory
{
    public interface DeviceFactory
    {
        BaseDevice CreateDevice(string model);
    }

}