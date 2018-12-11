using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace WebBirthdayManager.Http
{
    public class BodyReader
    {
        public string ReadFrom(HttpRequest request) => ReadFrom(request.Body);
        public string ReadFrom(HttpResponse response) => ReadFrom(response.Body);

        private string ReadFrom(Stream bodyStream)
        {
            try
            {
                using (var bodyReader = new HttpRequestStreamReader(bodyStream, Encoding.UTF8))
                {
                    return bodyReader.ReadToEnd();
                }
            }
            catch (Exception ex) when (ex is IOException
                                       || ex is OutOfMemoryException
                                       || ex is ArgumentOutOfRangeException
                                       || ex is ObjectDisposedException)
            {
                return string.Empty;
            }
        }
    }
}