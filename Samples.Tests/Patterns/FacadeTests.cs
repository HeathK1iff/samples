using Samples.Facade;

namespace Samples.Tests.Patterns
{
    [TestFixture()]
    public class FacadeTests
    {
        [Test()]
        public void ToJsonTest()
        {
            string expected = "{\"Id\":10}";

            string actual = JsonFacade.ToJson(new JsonObject(10));

            Assert.That(actual.Equals(expected), Is.True);
        }

        [Test()]
        public void ToObjectTest()
        {
            JsonObject expected = new JsonObject(10);
            JsonObject actual = JsonFacade.ToObject<JsonObject>("{\"Id\":10}");

            Assert.That(actual.Equals(expected), Is.True);
        }

        private class JsonObject : IEquatable<JsonObject?>
        {
            public JsonObject(int id)
            {
                Id = id;
            }
            public int Id { get; }

            public override bool Equals(object? obj)
            {
                return Equals(obj as JsonObject);
            }

            public bool Equals(JsonObject? other)
            {
                return other is not null &&
                       Id == other.Id;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id);
            }

            public static bool operator ==(JsonObject? left, JsonObject? right)
            {
                return EqualityComparer<JsonObject>.Default.Equals(left, right);
            }

            public static bool operator !=(JsonObject? left, JsonObject? right)
            {
                return !(left == right);
            }
        }
    }
}