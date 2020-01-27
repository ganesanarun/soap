using System;
using Microsoft.AspNetCore.Mvc;

namespace ApiClient.Controllers
{
    using System.Threading.Tasks;
    using CustomMiddleware;

    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly ISampleService sampleService;

        public ServicesController(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var complexModel = new ComplexModelInput
            {
                StringProperty = Guid.NewGuid().ToString(),
                IntProperty = new Random().Next(),
                ListProperty = new[] {"test", "list", "of", "strings"},
                DateTimeOffsetProperty = RandomDay()
            };
            var complexResult = await sampleService.PingComplexModelAsync(complexModel);
            return $"result. FloatProperty: {complexResult.FloatProperty}, " +
                       $"StringProperty: {complexResult.StringProperty}, " +
                       $"ListProperty: {string.Join(" ", complexResult.ListProperty)}, " +
                       $"DateTimeOffsetProperty: {complexResult.DateTimeOffsetProperty}, " +
                       $"EnumProperty: {complexResult.TestEnum}";
        }

        private static DateTime RandomDay()
        {
            var gen = new Random();
            var start = new DateTime(1995, 1, 1);
            var range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}