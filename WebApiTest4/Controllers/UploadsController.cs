using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Profile;
using System.Xml;
using System.Xml.Linq;

namespace WebApiTest4.Controllers
{
    public class UploadsController : ApiController
    {
        private static XDocument _uploadConfig;
        private static readonly string _xmlPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\UploadsConfig.xml";
        static UploadsController()
        {
            _uploadConfig = XDocument.Load(_xmlPath, LoadOptions.SetBaseUri);
        }
        public int LastId
        {
            get
            { 
                //return Int32.Parse(_uploadConfig.DocumentElement.SelectSingleNode("uploadLastId").Value);
                return Convert.ToInt32(_uploadConfig.Root.Descendants("uploadLastId").FirstOrDefault().Value);
            }
            private set
            { //_uploadConfig.DocumentElement.SelectSingleNode("uploadLastId").Value = value.ToString(); _uploadConfig.sa
                _uploadConfig.Root.Descendants("uploadLastId").FirstOrDefault().Value = value.ToString();
                _uploadConfig.Save(new Uri(_uploadConfig.BaseUri).LocalPath);
            }
        }

        public string UploadsStoragePath
        {
            get
            {
                return _uploadConfig.Root.Descendants("uploadsStoragePath").FirstOrDefault().Value;
                //return _uploadConfig.DocumentElement.SelectSingleNode("uploadsStoragePath").Value; 
            }
        }

        // GET: api/Uploads/5
        [HttpGet]
        [Route("api/v1/Uploads/{id}")]
        public HttpResponseMessage Get([FromUri]string id)
        {
            string path = UploadsStoragePath + id;
            if (!File.Exists(path))
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound); ;
            }

            try
            {
                MemoryStream responseStream = new MemoryStream();
                Stream fileStream = File.Open(path, FileMode.Open);
                bool fullContent = true;
                /*if (this.Request.Headers.Range != null)
                {
                    fullContent = false;

                    // Currently we only support a single range.
                    RangeItemHeaderValue range = this.Request.Headers.Range.Ranges.First();


                    // From specified, so seek to the requested position.
                    if (range.From != null)
                    {
                        fileStream.Seek(range.From.Value, SeekOrigin.Begin);

                        // In this case, actually the complete file will be returned.
                        if (range.From == 0 && (range.To == null || range.To >= fileStream.Length))
                        {
                            fileStream.CopyTo(responseStream);
                            fullContent = true;
                        }
                    }
                    if (range.To != null)
                    {
                        // 10-20, return the range.
                        if (range.From != null)
                        {
                            long? rangeLength = range.To - range.From;
                            int length = (int)Math.Min(rangeLength.Value, fileStream.Length - range.From.Value);
                            byte[] buffer = new byte[length];
                            fileStream.Read(buffer, 0, length);
                            responseStream.Write(buffer, 0, length);
                        }
                        // -20, return the bytes from beginning to the specified value.
                        else
                        {
                            int length = (int)Math.Min(range.To.Value, fileStream.Length);
                            byte[] buffer = new byte[length];
                            fileStream.Read(buffer, 0, length);
                            responseStream.Write(buffer, 0, length);
                        }
                    }
                    // No Range.To
                    else
                    {
                        // 10-, return from the specified value to the end of file.
                        if (range.From != null)
                        {
                            if (range.From < fileStream.Length)
                            {
                                int length = (int)(fileStream.Length - range.From.Value);
                                byte[] buffer = new byte[length];
                                fileStream.Read(buffer, 0, length);
                                responseStream.Write(buffer, 0, length);
                            }
                        }
                    }
                }
                // No Range header. Return the complete file.
                else
                {
                    fileStream.CopyTo(responseStream);
                }*/
                fileStream.CopyTo(responseStream);
                fileStream.Close();
                responseStream.Position = 0;
                var startIndex = id.LastIndexOf(".") + 1;
                HttpResponseMessage response = new HttpResponseMessage();
                var extension = id.Substring(startIndex);
                response.Content = new StreamContent(responseStream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/" + extension);
                response.StatusCode = fullContent ? HttpStatusCode.OK : HttpStatusCode.PartialContent;

                return response;
            }
            catch (IOException)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        public static List<string> AllowedExtensions = new List<string>() {".png", ".jpg", ".jpeg", ".bmp", ".gif"};
        // POST: api/Uploads
        [Authorize]
        [HttpPost]
        [Route("api/v1/Uploads")]
        public IHttpActionResult Post()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count == 1)
            {
                
                var postedFile = httpRequest.Files[0];
                var startIndex = postedFile.FileName.LastIndexOf(".", StringComparison.Ordinal);
                var extension = postedFile.FileName.Substring(startIndex);
                if (startIndex < 0 || !AllowedExtensions.Any(ext => ext.Equals(extension.ToLower())))
                {
                    return BadRequest();
                }
                var filePath = UploadsStoragePath + LastId + extension;
                postedFile.SaveAs(filePath);
                var id = LastId;
                LastId++;
                return Ok(new {id = id, picture = Request.RequestUri.AbsolutePath + "/" + id + extension});
            }
            else
            {
                return BadRequest();
            }
        }
    }
}