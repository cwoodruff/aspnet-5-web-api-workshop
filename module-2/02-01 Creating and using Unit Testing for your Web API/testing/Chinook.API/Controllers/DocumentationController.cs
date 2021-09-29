using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Chinook.API.Controllers
{
    public class DocumentationController : Controller
    {
        private readonly IApiDescriptionGroupCollectionProvider _apiExplorer;

        public DocumentationController(IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            _apiExplorer = apiExplorer;
        }

        public IActionResult Index()
        {
            return StatusCode(500, _apiExplorer);
        }
    }
}