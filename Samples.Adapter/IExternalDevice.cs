namespace Samples.Adapter
{
    public interface IExternalDevice
    {
        string Read();
        void Write(string value);
    }
}