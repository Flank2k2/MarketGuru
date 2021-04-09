using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketGuru.Core.Models;
using MarketGuru.Core.Services;
using MarketGuruApi.Reccords;
using Microsoft.Extensions.Logging;

namespace Ras.Web.Mvc.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ApiController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        public ApiController(IConfiguration config,  ILogger<ApiController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [AllowAnonymous]
        [Route("/")]
        public IActionResult Index()
        {
            var assembly = Assembly.GetEntryAssembly() ?? throw new ArgumentNullException(nameof(Assembly));

            return Ok(new
            {
                assembly
                    .GetCustomAttributes(typeof(AssemblyTitleAttribute))
                    .OfType<AssemblyTitleAttribute>()
                    .FirstOrDefault()?.Title,
                assembly
                    .GetCustomAttributes(typeof(AssemblyDescriptionAttribute))
                    .OfType<AssemblyDescriptionAttribute>()
                    .FirstOrDefault()?.Description,
                Version = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion
            });
        }

    }
}

