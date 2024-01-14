namespace Samples.Bridge.Implemetator
{
    public abstract class Serializator
    {
        public abstract string GetContentType();
        public abstract string Serialize<T>(T obj) where T : ServiceRequest;
        public abstract T Deserialize<T>(string text) where T : ServiceResponse;
    }

}