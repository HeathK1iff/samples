using Samples.Bridge.Implemetator;

namespace Samples.Bridge.Abstraction
{
    public class CustomerServiceClient : ServiceClient
    {
        private Uri _host;

        public CustomerServiceClient(Uri host, IClient client, Serializator serializator) : base(serializator, client)
        {
            _host = host;
        }

        public override ServiceResponse Send(ServiceRequest request)
        {
            _client.Connect(_host);
            try
            {
                _client.Write(_serializator.Serialize(request));

                var response = _client.Read();

                if (response == null)
                {
                    throw new InvalidOperationException();
                }

                return _serializator.Deserialize<CustomerServiceResponse>(response);
            }
            finally
            {
                _client.Disconnect();
            }
        }
    }

}