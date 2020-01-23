using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ApiClient.Controllers
{
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
        public IEnumerable<string> Get()
        {
            return Enumerable.Range(1, 5).Select(index =>
            {
                var complexModel = new ComplexModelInput
                {
                    StringProperty = Guid.NewGuid().ToString(),
                    IntProperty = new Random().Next(),
                    ListProperty = new List<string> {"test", "list", "of", "strings"},
                    DateTimeOffsetProperty = RandomDay()
                };

                var complexResult = sampleService.PingComplexModel(complexModel);
                return $"result. FloatProperty: {complexResult.FloatProperty}, " +
                       $"StringProperty: {complexResult.StringProperty}, " +
                       $"ListProperty: {string.Join(" ", complexResult.ListProperty)}, " +
                       $"DateTimeOffsetProperty: {complexResult.DateTimeOffsetProperty}, " +
                       $"EnumProperty: {complexResult.TestEnum}";
            }).ToArray();
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