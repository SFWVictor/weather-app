﻿namespace WeatherApp.Helpers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using Newtonsoft.Json;

    public static class JsonDeserializer
    {
        public static List<T> LoadFromJson<T>(string jsonFileName)
            where T : class
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(JsonDeserializer)).Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{nameof(WeatherApp)}.{jsonFileName}");
            List<T> objects = new List<T>();

            using (var reader = new StreamReader(stream))
            {
                objects = DeserializeObjects<T>(reader);
            }

            return objects;
        }

        public static List<T> DeserializeObjects<T>(StreamReader reader)
            where T : class
        {
            var json = (reader.ReadToEnd());
            var objects = JsonConvert.DeserializeObject<List<T>>(json);
            return objects;
        }
    }
}
