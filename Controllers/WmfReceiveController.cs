using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WmfToPngServe.Service;

namespace WmfToPngServe.Controllers
{
    [Route("api/wmf")]
    [ApiController]
    public class WmfReceiveController : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceiveWmf()
        {
            try
            {
                var files = Request.Form.Files;
                IFormFile formFile = null;
                if (files != null && (formFile = files.GetFile("file")) != null)
                {
                    var streamFileName = formFile.FileName;
                    var dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Console.WriteLine("[" + dateTime + "]正在转换wmf：" + streamFileName);
                    streamFileName =
                        streamFileName.Substring(0, streamFileName.LastIndexOf(".", StringComparison.Ordinal));
                    var readStream = formFile.OpenReadStream();
                    var outStream = WmfToPng.Convert(readStream);
                    outStream.Position = 0;
                    var file = File(outStream, "application/octet-stream", streamFileName + ".png");
                    return file;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new JsonResult("{status: error}");
        }
    }
}