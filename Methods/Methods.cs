using System.Text.Json;
using RestSharp;

namespace BaselinkerREST.Methods;

public class Methods
{
    /*
    * The API expects base64 content in some endpoints and it is very important
    * to replace "+" character with "%2B" sequence before sending it to our API to avoid an incorrect decoding.
    */
    public string ReplaceChar(string TextToReplace)
    {
        var newString = TextToReplace.Replace("+", "%2B");
        return newString;
    }

    public RestRequest CreateRestRequest(string method, object parameters)
    {
        RestRequest request = new RestRequest("https://api.baselinker.com/connector.php", Method.Put);
        request.AddHeader("X-BLToken", "");
        //request.AddHeader("X-BLToken", "1-23-ABC");
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("method", method);
        request.AddParameter("parameters", JsonSerializer.Serialize(parameters));
        request.Timeout = 1000;
        return request;
    }
}