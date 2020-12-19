using System;
using Microsoft.AspNetCore.Mvc;

namespace WmfToPngServe.Controllers
{
    [Route("api/serve")]
    [ApiController]
    public class ServeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> testConnect()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}