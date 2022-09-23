using System.Text;

namespace ToyHouse.General.Tools
{
    internal static class TextEncoder
    {
        private const int KEY = 26;

        public static string GetEncodedText(string text)
        {
            return Encode(Encrypt(text));
        }
        public static string GetDecodedText(string text)
        {
            return Decrypt(Decode(text));
        }

        private static string Encrypt(string text)
        {
            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                result += (char)(text[i] ^ KEY);
            } 
            return result;
        }

        private static string Decrypt(string text)
        {
            return Encrypt(text);
        }

        private static string Encode(string text)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(text);

            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);

            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}