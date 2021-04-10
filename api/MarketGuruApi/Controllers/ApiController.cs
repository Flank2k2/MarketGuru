using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Ras.Web.Mvc.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ApiController : ControllerBase
    {
        [AllowAnonymous]
        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
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

