using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PiszczekSzpotek.BookCatalogue.API.Utils
{
    public static class ResponseHelper
    {
        public static string Data(object obj)
        {
            var responseObj = new
            {
                Status = "Success",
                Data = obj
            };
            return JsonConvert.SerializeObject(
                responseObj, 
                Formatting.Indented, 
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        public static string Success(string message)
        {
            var responseObj = new
            {
                Status = "Success",
                Message = message
            };
            return JsonConvert.SerializeObject(responseObj, Formatting.Indented);
        }

        public static string Error(string message)
        {
            var responseObj = new
            {
                Status = "Fail",
                Message = message
            };
            return JsonConvert.SerializeObject(responseObj, Formatting.Indented);
        }
    }
}
