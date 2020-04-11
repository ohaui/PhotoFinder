using RestSharp;

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
                AlwaysMultipartFormData = true,
                Method = Method.POST
            };
            restClient.FollowRedirects = false;
            restRequest.AddFile("encoded_image", Path);
            var response = restClient.Execute(restRequest);
            return (string)response.Headers[0].Value;
        }
    }
}
