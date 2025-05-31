using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;

namespace PlantandBiologyRecognition.API.Controllers
{
    [Route(ApiEndPointConstant.ApiEndPoint)]
    public class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        protected ILogger<T> _logger;
        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
