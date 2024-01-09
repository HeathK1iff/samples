using System.IO;

namespace Samples.Adapter
{
    public class StreamExternalDeviceAdapter : IExternalDevice
    {
        private Stream _stream;
        public StreamExternalDeviceAdapter(Stream stream)
        {
            _stream = stream;
        }

        public string Read()
        {
            _stream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(_stream, null, true, -1, true))
            {
                return reader.ReadToEnd();
            }
        }

        public void Write(string value)
        {
            _stream.SetLength(0);
            _stream.Seek(0, SeekOrigin.Begin);
            using (var writer = new StreamWriter(_stream, null, -1, true))
            {
                writer.Write(value);
                writer.Flush();
            }
        }
    }
}