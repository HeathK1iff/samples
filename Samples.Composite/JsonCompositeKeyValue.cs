using System.Collections;

namespace Samples.Composite
{
    public class JsonCompositeKeyValue : IKeyValuePair, IEnumerable
    {
        private string _key;
        private List<JsonKeyValue> _outputRows = new();
        public JsonCompositeKeyValue(string key)
        {
            _key = key;
        }

        public void Add(JsonKeyValue row)
        {
            _outputRows.Add(row);
        }

        public IEnumerator GetEnumerator()
        {
            return _outputRows.GetEnumerator();
        }

        public void Print(IKeyValuePairWriter printer)
        {
            printer.Write("{" + _key + @":{");
            bool firstRec = true;
            foreach (JsonKeyValue row in _outputRows)
            {
                if (!firstRec)
                {
                    printer.Write(",");
                }

                row.Print(printer);
                firstRec = false;
            }
            printer.Write("}");
        }
    }
}