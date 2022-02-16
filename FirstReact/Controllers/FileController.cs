using FirstReact.Core.Models.Dtos;
using FirstReact.Core.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileProcessor _fileProcessor;

        public FileController(ILogger<FileController> logger, IFileProcessor fileProcessor)
        {
            _logger = logger;
            _fileProcessor = fileProcessor;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] IFormFile formFile)
        {
            List<CarSale> result;

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                stream.Position = 0;
                result = _fileProcessor.ProcessCsvFile(stream);
            }

            return Ok(result);
        }
    }
}
