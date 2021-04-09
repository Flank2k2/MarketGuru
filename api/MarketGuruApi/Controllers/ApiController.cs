using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
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

