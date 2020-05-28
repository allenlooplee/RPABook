using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ML.Properties;

namespace ML.Tools
{
    public class HTTPMagic
    {
        public static void CheckStatus(HttpResponseMessage res)
        {
            if (res.IsSuccessStatusCode) return;

            switch (res.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_400);
                case HttpStatusCode.Unauthorized:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_401);
                case HttpStatusCode.Forbidden:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_403);
                case HttpStatusCode.NotFound:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_404);
                case HttpStatusCode.MethodNotAllowed:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_405);
                case HttpStatusCode.RequestTimeout:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_408);
                case HttpStatusCode.Gone:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_410);
                case HttpStatusCode.RequestEntityTooLarge:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_413);
                case HttpStatusCode.RequestUriTooLong:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_414);
                case HttpStatusCode.UnsupportedMediaType:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_415);
                case HttpStatusCode.InternalServerError:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_500);
                case HttpStatusCode.NotImplemented:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_501);
                case HttpStatusCode.BadGateway:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_502);
                case HttpStatusCode.ServiceUnavailable:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_503);
                case HttpStatusCode.GatewayTimeout:
                    throw new CustomException(Resources.HTTPMagic_CheckStatus_504);
            }
        }

        public static bool IsValidJson(string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue)) return false;

            var json = stringValue.Trim();
            if ((json.StartsWith("{") && json.EndsWith("}")) || (json.StartsWith("[") && json.EndsWith("]")))
            {
                try
                {
                    JToken.Parse(json);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }

            return false;
        }

        private static StringContent Jsonize(string s)
        {
            return new StringContent(s.ToString(), Encoding.UTF8, "application/json");
        }
    }
}