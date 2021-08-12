using System;


using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using UrlCombineLib;

namespace YuroLibrary.ClientApi
{
    public class ClientApi
    {
        public Uri ApiUri { get; set; }

        public ClientApi(string uri)
        {
            ApiUri = new Uri(uri);
        }

        public async Task<IEnumerable<FileEntry>> SearchBooks(
            string searchTerm
        )
        {
            var methodUri = ApiUri.Combine("SearchBooks");
            using var webClient = new WebClient();
            webClient.QueryString.Add("searchTerm", searchTerm);

            var response = await webClient.DownloadStringTaskAsync(methodUri);
            var result = JsonConvert.DeserializeObject<List<Book>>(response);
            return result;
        }
        public async Task UploadFile(string path,
                                     string name,
                                     string description,
                                     string author,
                                     DateTime dateOfWriting)
        {
            var methodUri = ApiUri.Combine("UploadFile")
                .Combine($"?name={name}&description={description}&author={author}&dateOfWriting={dateOfWriting.ToString("s")}");

            var request = new HttpRequestMessage(HttpMethod.Post, methodUri);
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            var fileContent = new StreamContent(fs);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(MimeTypes.GetMimeType(fs.Name));
            var form = new MultipartFormDataContent {
                { fileContent, "uploadedFile", fs.Name }
            };



            request.Content = form;

            var httpClient = new HttpClient();

            var response = await httpClient.SendAsync(request);
            // using var webClient = new WebClient();
            // webClient.QueryString = parameters;
            // var responseBytes = await webClient.UploadFileTaskAsync(methodUri, path);
            // var response = Encoding.UTF8.GetString(responseBytes);


        }
        public async Task DownloadBook(Guid guid, string path)
        {
            var methodUri = ApiUri.Combine("DownloadBook");
            var parameters = new System.Collections.Specialized.NameValueCollection()
            {
                { "fileGuid", guid.ToString() }
            };

            using var webClient = new WebClient();
            webClient.QueryString = parameters;
            await webClient.DownloadFileTaskAsync(methodUri, path);
        }
    }
}

