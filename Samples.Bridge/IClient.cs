namespace Samples.Bridge
{
    public interface IClient
    {
        void Connect(Uri uri);
        void Disconnect();
        void Write(string body);
        string Read();
    }

}