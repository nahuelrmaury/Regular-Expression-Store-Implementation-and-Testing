using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;

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

            Regex regex = new Regex(pattern);

            return regex.IsMatch(input);
        }

        // the method should return a collection of field names from the json input
        public static IEnumerable<string> Method2(string inputJson)
        {
            List<string> fieldNames = new List<string>();

            Regex regex = new Regex("\"(\\w+)\":");

            MatchCollection matches = regex.Matches(inputJson);

            foreach (Match match in matches)
            {
                string fieldName = match.Groups[1].Value;
                fieldNames.Add(fieldName);
            }

            return fieldNames;
        }

        // the method should return a collection of field values from the json input
        public static IEnumerable<string> Method3(string inputJson)
        {

            Regex regex = new Regex(":\\s*\"?(.*?[^\\\\])\"?(?:,|}|$)");

            List<string> fieldValues = new List<string>();

            MatchCollection matches = regex.Matches(inputJson);

            foreach (Match match in matches)
            {
                string fieldValue = match.Groups[1].Value;
                fieldValues.Add(fieldValue);
            }

            return fieldValues;
        }

        // the method should return a collection of field names from the xml input
        public static IEnumerable<string> Method4(string inputXml)
        {
            string pattern = @"(?<=<)(?!TestClass\b)\w+\b";
            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(inputXml);

            List<string> fieldNames = new List<string>();

            foreach (Match match in matches)
            {
                string fieldName = match.Value;
                fieldNames.Add(fieldName);
            }

            return fieldNames;
        }

        // the method should return a collection of field values from the input xml
        // omit null values
        public static IEnumerable<string> Method5(string inputXml)
        {
            string pattern = @"(?<=>)[^<>]+(?=<)";
            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(inputXml);

            List<string> fieldNames = new List<string>();

            foreach (Match match in matches)
            {
                string fieldName = match.Value;
                fieldNames.Add(fieldName);
            }

            return fieldNames;
        }

        // read from the input string and return Ukrainian phone numbers written in the formats of 0671234567 | +380671234567 | (067)1234567 | (067) - 123 - 45 - 67
        // +38 - optional Ukrainian country code
        // (067)-123-45-67 | 067-123-45-67 | 38 067 123 45 67 | 067.123.45.67 etc.
        // make a decision for operators 067, 068, 095 and any subscriber part.
        // numbers can be separated by symbols , | ; /
        public static IEnumerable<string> Method6(string input)
        {
            string[] numbers = Regex.Split(input, @"[,|;/]");

            string pattern = @"^(?:\+38|\(067\)|067|068|095|38)(?!\d{8}$).*";

            Regex regex = new Regex(pattern);

            List<string> phoneNumbers = new List<string>();

            foreach (string number in numbers)
            {
                if (regex.IsMatch(number.Trim()))
                {
                    string formattedNumber = number.Trim();
                    if (formattedNumber.StartsWith("38"))
                    {
                        formattedNumber = "+" + formattedNumber;
                    }
                    phoneNumbers.Add(formattedNumber);
                }
            }

            return phoneNumbers;
        }
    }
}