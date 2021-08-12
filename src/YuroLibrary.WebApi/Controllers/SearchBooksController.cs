using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YuroLibrary.Domain;

namespace YuroLibrary.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchBooksController : ControllerBase
    {
        private readonly ILogger<SearchBooksController> _logger;
        public SearchBooksController(ILogger<SearchBooksController> logger)
        {
            _logger = logger;
        }
         [HttpGet]
        public async Task<List<Book>> SearchBooks(string searchTerm)
            => await YuroLibrary.SearchBooks(searchTerm);
    }
}
