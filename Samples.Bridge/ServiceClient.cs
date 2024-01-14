using System.Text.Encodings.Web;
using Samples.Bridge.Implemetator;

namespace Samples.Bridge
{
    public abstract class ServiceClient
    {
        protected Serializator _serializator;
        protected IClient _client;

        protected ServiceClient(Serializator serializator, IClient client)
        {
            _serializator = serializator;
            _client = client;
        }

        public abstract ServiceResponse Send(ServiceRequest request);
    }
}