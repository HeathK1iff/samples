namespace Samples.Bridge.Abstraction
{
    public class CustomerServiceRequest : ServiceRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }


}