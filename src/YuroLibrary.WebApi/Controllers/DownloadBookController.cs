using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace YuroLibrary.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadBookController : ControllerBase
    {
        private readonly ILogger<DownloadBookController> _logger;

        public DownloadBookController(ILogger<DownloadBookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> DownloadBook(Guid fileGuid)
        {
            var fileEntry = YuroLibrary.GetFileEntry(fileGuid);
            var fileStream = FileStorage.FileStorageProvider.GetFile(fileEntry.FileId);


            return new FileStreamResult(fileStream, "application/pdf")
            {
                FileDownloadName = $"{fileEntry.Book.Author}-{fileEntry.Book.Name}",
            };
        }
    }
}
