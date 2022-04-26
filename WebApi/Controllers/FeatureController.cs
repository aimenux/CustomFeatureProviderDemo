using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureManagerSnapshot _featureManager;
        private readonly ILogger<FeatureController> _logger;

        public FeatureController(IFeatureManagerSnapshot featureManager, ILogger<FeatureController> logger)
        {
            _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet(Name = "GetFeature")]
        public async Task<IActionResult> GetFeatureAsync(string featureName, CancellationToken cancellationToken = default)
        {
            var isEnabled = await _featureManager.IsEnabledAsync(featureName, cancellationToken);
            return new OkObjectResult(new
            {
                FeatureName = featureName,
                IsEnabled = isEnabled
            });
        }
    }
}