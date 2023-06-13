using System.Text.Json;
using System.Text.RegularExpressions;

namespace RegularExpression
{
    public static class RegularExpressionStore
    {
        // should return a bool indicating whether the input string is
        // a valid team international email address: firstName.lastName@domain (serhii.mykhailov@teaminternational.com etc.)
        // address cannot contain numbers
        // address cannot contain spaces inside, but can contain spaces at the beginning and end of the string
        public static bool Method1(string input)
        {
            string pattern = @"^\s*[a-zA-Z]+\.[a-zA-Z]+@[a-zA-Z]+\.[a-zA-Z]{3}\s*$";

            bool isMatch = Regex.IsMatch(input, pattern);

            return isMatch;
        }

        // the method should return a collection of field names from the json input
        public static IEnumerable<string> Method2(string inputJson)
        {
            List<string> fieldNames = new List<string>();

            JsonDocument jsonDoc = JsonDocument.Parse(inputJson);

            /* traverse the JSON document recursively */
            TraverseJsonElement(jsonDoc.RootElement, fieldNames);

            return fieldNames;
        }

        private static void TraverseJsonElement(JsonElement element, List<string> fieldNames)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in element.EnumerateObject())
                {
                    fieldNames.Add(property.Name);

                    TraverseJsonElement(property.Value, fieldNames);
                }
            }
        }

        // the method should return a collection of field values from the json input
        public static IEnumerable<string> Method3(string inputJson)
        {
            var fieldValues = new List<string>();

            JsonDocumentOptions options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };

            JsonDocument jsonDoc = JsonDocument.Parse(inputJson, options);

            TraverseJson(jsonDoc.RootElement, fieldValues);

            return fieldValues;
        }

        private static void TraverseJson(JsonElement element, List<string> fieldValues)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                foreach (var property in element.EnumerateObject())
                    TraverseJson(property.Value, fieldValues);
            }
            else
            {
                if (element.ValueKind == JsonValueKind.Null)
                    fieldValues.Add("null");

                else if (element.ValueKind == JsonValueKind.True)
                    fieldValues.Add("true");

                else
                    fieldValues.Add(element.ToString());
            }
        }

        // the method should return a collection of field names from the xml input
        public static IEnumerable<string> Method4(string inputXml)
        {
            throw new NotImplementedException();
        }

        // the method should return a collection of field values from the input xml
        // omit null values
        public static IEnumerable<string> Method5(string inputXml)
        {
            throw new NotImplementedException();
        }

        // read from the input string and return Ukrainian phone numbers written in the formats of 0671234567 | +380671234567 | (067)1234567 | (067) - 123 - 45 - 67
        // +38 - optional Ukrainian country code
        // (067)-123-45-67 | 067-123-45-67 | 38 067 123 45 67 | 067.123.45.67 etc.
        // make a decision for operators 067, 068, 095 and any subscriber part.
        // numbers can be separated by symbols , | ; /
        public static IEnumerable<string> Method6(string input)
        {
            throw new NotImplementedException();
        }
    }
}