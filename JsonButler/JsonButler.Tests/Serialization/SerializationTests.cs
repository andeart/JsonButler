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
            const string expected = ButlerTestClass0.ExpectedSerialized;
            string errorMessage = $"Expected: {expected}; Actual: {serialized}";
            Assert.AreEqual (expected, serialized, errorMessage);
        }

        [TestMethod]
        public void SerializeType_NoButlerSerializerSettings_Serialized ()
        {
            string serialized = ButlerSerializer.SerializeType<ButlerTestClass0> ();
            const string expected = ButlerTestClass0.ExpectedSerialized;
            string errorMessage = $"Expected: {expected}; Actual: {serialized}";
            Assert.AreEqual (expected, serialized, errorMessage);
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
            const string expected = ButlerTestClass0.ExpectedSerialized;
            string errorMessage = $"Expected: {expected}; Actual: {serialized}";
            Assert.AreEqual (expected, serialized, errorMessage);
        }

        [TestMethod]
        public void SerializeType_NoJsonSerializerSettings_Serialized ()
        {
            ButlerSerializerSettings serializerSettings = new ButlerSerializerSettings (Assembly.GetExecutingAssembly ());
            serializerSettings.PreferredAttributeTypesOnConstructor = new[] { typeof(JsonConstructorAttribute) };

            string serialized = ButlerSerializer.SerializeType<ButlerTestClass0> (serializerSettings);
            const string expected = ButlerTestClass0.ExpectedSerialized;
            string errorMessage = $"Expected: {expected}; Actual: {serialized}";
            Assert.AreEqual (expected, serialized, errorMessage);
        }

        [TestMethod]
        public void SerializeType_JsonIgnoredProperty_PropertyIgnored ()
        {
            string serialized = ButlerSerializer.SerializeType<ButlerTestClass1> ();
            const string expected = ButlerTestClass1.ExpectedSerialized;
            string errorMessage = $"Expected: {expected}; Actual: {serialized}";
            Assert.AreEqual (expected, serialized, errorMessage);
        }

        [TestMethod]
        public void SerializeType_JsonConstructorAttribute_ConstructorRespected ()
        {
            string serialized = ButlerSerializer.SerializeType<ButlerTestClass2> ();
            const string expected = ButlerTestClass2.ExpectedSerialized;
            string errorMessage = $"Expected: {expected}; Actual: {serialized}";
            Assert.AreEqual (expected, serialized, errorMessage);
        }

        [TestMethod]
        public void SerializeType_ArrayProperty_SerializedAsArray ()
        {
            string serialized = ButlerSerializer.SerializeType<ButlerTestClass3> ();
            const string expected = ButlerTestClass3.ExpectedSerialized;
            string errorMessage = $"Expected: {expected}; Actual: {serialized}";
            Assert.AreEqual (expected, serialized, errorMessage);
        }

        [TestMethod]
        public void SerializeType_NoJsonPropertyAttribute_PropertySerialized ()
        {
            string serialized = ButlerSerializer.SerializeType<ButlerTestClass4> ();
            const string expected = ButlerTestClass4.ExpectedSerialized;
            string errorMessage = $"Expected: {expected}; Actual: {serialized}";
            Assert.AreEqual (expected, serialized, errorMessage);
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


        public class ButlerTestClass2
        {
            public const string ExpectedSerialized = "{\"you\":42}";

            [JsonProperty ("you")]
            public int You { get; private set; }

            public ButlerTestClass2 (float you)
            {
                You = 7;
            }

            [JsonConstructor]
            public ButlerTestClass2 (string you)
            {
                You = you?.Length ?? 42;
            }
        }


        public class ButlerTestClass3
        {
            public const string ExpectedSerialized = "{\"up\":[],\"never\":[]}";

            [JsonProperty ("up")]
            public int[] Up { get; private set; }

            [JsonProperty ("never")]
            public string[] Never { get; private set; }

            public ButlerTestClass3 (int[] up, string[] never)
            {
                Up = up;
                Never = never;
            }
        }


        public class ButlerTestClass4
        {
            public const string ExpectedSerialized = "{\"Gonna\":false}";

            public bool Gonna { get; }

            [JsonConstructor]
            public ButlerTestClass4 (bool gonna)
            {
                Gonna = gonna;
            }
        }
    }

}