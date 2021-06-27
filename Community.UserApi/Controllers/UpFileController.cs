using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community.Common;

namespace Community.UserApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UpFileController : ControllerBase
    {
        IHostingEnvironment _host;
        public UpFileController(IHostingEnvironment hostingEnvironment)
        {
            _host = hostingEnvironment;
        }

        [HttpPost]
        [Route("api/File")]
        public string FileUpLoad()
        {
            IFormFile formFile = Request.Form.Files[0];
            UploadFilesHelper helper = new UploadFilesHelper(_host);
            return helper.Main(formFile);
        }

    }
}
