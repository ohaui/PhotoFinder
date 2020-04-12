using RestSharp;
using System.Linq;

namespace PhotoFinder
{
    class FindImage
    {
        //http://www.google.com/searchbyimage/upload

        public static string GetUriImage(string Path)
        {
            RestClient restClient = new RestClient("http://www.google.com/");
            RestRequest restRequest = new RestRequest("searchbyimage/upload")
            {
                Method = Method.POST
            };
            restClient.FollowRedirects = false;
            restRequest.AddHeader("Content-Type", "multipart/form-data");
            restRequest.AddFile("encoded_image", Path);
            var response = restClient.Execute(restRequest);
            var ToReturn = response.Headers.ToList()
                .Find(x => x.Name == "Location")
                .Value.ToString();
            return ToReturn;
        }
    }
}
