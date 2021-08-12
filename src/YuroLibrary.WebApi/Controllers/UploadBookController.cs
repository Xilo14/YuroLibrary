using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YuroLibrary.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace YuroLibrary.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadFileController : ControllerBase
    {
        private readonly ILogger<UploadFileController> _logger;


        public UploadFileController(ILogger<UploadFileController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile,
                                                 string name,
                                                 string description,
                                                 string author,
                                                 DateTime dateOfWriting)
        {
            var x = Request;
            if (uploadedFile is null)
            {
                return BadRequest();
            }

            var tmpStream = new MemoryStream();
            await uploadedFile.CopyToAsync(tmpStream);

            Core.YuroLibrary.AddBook(
                name, description, author,
                dateOfWriting, uploadedFile.ContentType,
                tmpStream);


            return Ok();
        }
    }
}
