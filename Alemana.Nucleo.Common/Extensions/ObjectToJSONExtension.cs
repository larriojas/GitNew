
namespace Alemana.Nucleo.Common.Extensions
{
    public static class ObjectToJSONExtension
    {
        public static string ToJSON(this object obj)
        {
            return fastJSON.JSON.Instance.ToJSON(obj);
        }

        public static T FromJSON<T>(this string str)
        {
            return fastJSON.JSON.Instance.ToObject<T>(str);
        }

    }
}
