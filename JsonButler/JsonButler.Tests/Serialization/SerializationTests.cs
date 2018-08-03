using System.Reflection;
using Andeart.JsonButler.CodeSerialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;



namespace JsonButler.Tests.Serialization
{

    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void SerializeType_SimpleCustomType_Serialized ()
        {
            ButlerSerializerSettings serializerSettings = new ButlerSerializerSettings (Assembly.GetExecutingAssembly ());
            serializerSettings.PreferredAttributeTypesOnConstructor = new[] { typeof(JsonConstructorAttribute) };

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings ();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializerSettings.Formatting = Formatting.None;
            serializerSettings.JsonSerializerSettings = jsonSerializerSettings;

            string serialized = ButlerSerializer.SerializeType<ButlerTestClass0> (serializerSettings);
            Assert.AreEqual (serialized, ButlerTestClass0.ExpectedSerialized);
        }

        [TestMethod]
        public void SerializeType_NoButlerSerializerSettings_Serialized ()
        {
            string serialized = ButlerSerializer.SerializeType<ButlerTestClass0> ();
            Assert.AreEqual (serialized, ButlerTestClass0.ExpectedSerialized);
        }

        [TestMethod]
        public void SerializeType_NoPreferredCtorAttributes_Serialized ()
        {
            ButlerSerializerSettings serializerSettings = new ButlerSerializerSettings (Assembly.GetExecutingAssembly ());

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings ();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializerSettings.Formatting = Formatting.None;
            serializerSettings.JsonSerializerSettings = jsonSerializerSettings;

            string serialized = ButlerSerializer.SerializeType<ButlerTestClass0> (serializerSettings);
            Assert.AreEqual (serialized, ButlerTestClass0.ExpectedSerialized);
        }

        [TestMethod]
        public void SerializeType_NoJsonSerializerSettings_Serialized ()
        {
            ButlerSerializerSettings serializerSettings = new ButlerSerializerSettings (Assembly.GetExecutingAssembly ());
            serializerSettings.PreferredAttributeTypesOnConstructor = new[] { typeof(JsonConstructorAttribute) };

            string serialized = ButlerSerializer.SerializeType<ButlerTestClass0> (serializerSettings);
            Assert.AreEqual (serialized, ButlerTestClass0.ExpectedSerialized);
        }

        [TestMethod]
        public void SerializeType_JsonIgnoredProperty_Serialized ()
        {
            string serialized = ButlerSerializer.SerializeType<ButlerTestClass1> ();
            Assert.AreEqual (serialized, ButlerTestClass1.ExpectedSerialized);
        }


        private class ButlerTestClass0
        {
            public const string ExpectedSerialized = "{\"never\":null}";

            [JsonProperty ("never")]
            public string Never { get; private set; }

            [JsonConstructor]
            public ButlerTestClass0 (string never)
            {
                Never = never;
            }
        }


        public class ButlerTestClass1
        {
            public const string ExpectedSerialized = "{\"gonna\":0}";

            [JsonProperty ("gonna")]
            public int Gonna { get; private set; }

            [JsonIgnore]
            public int Give { get; private set; }

            [JsonConstructor]
            public ButlerTestClass1 (int gonna)
            {
                Gonna = gonna;
            }
        }
    }

}