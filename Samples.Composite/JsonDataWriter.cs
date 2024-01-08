namespace Samples.Composite
{
    public class JsonDataWriter
    {
        private Stream _stream;
        private List<IKeyValuePair> _list = new();
        public JsonDataWriter(Stream stream)
        {
            _stream = stream;
        }

        public void Add(IKeyValuePair pair)
        {
            _list.Add(pair);
        }

        public void Flush()
        {
            using (var writer = new StreamWriter(_stream, null, 01, true))
            {
                writer.Write("{");
                var keyValWriter = new KeyValueWriter(writer);

                bool firstRow = true;
                foreach (var item in _list)
                {
                    if (!firstRow)
                    {
                        keyValWriter.Write(",");
                    }

                    item.Print(keyValWriter);

                    firstRow = false;
                }
                
                writer.Write("}");
                writer.Flush();
            }
        }


        private class KeyValueWriter : IKeyValuePairWriter
        {
            private TextWriter _writer;

            public KeyValueWriter(TextWriter writer)
            {
                _writer = writer;
            }

            public void Write(string text)
            {
                _writer.Write(text);
            }
        }

    }


}