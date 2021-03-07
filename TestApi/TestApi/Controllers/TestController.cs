using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;

        private readonly TestService _service;

        public TestController(ILogger<TestController> logger, TestService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public TestModel Index()
        {
            return new TestModel { Identifier = 42 };
        }

        [HttpGet("{id}")]
        public TestModel Details(int id)
        {
            return new TestModel { Identifier = id };
        }

        [HttpPost("{action}")]
        public TestModel Store([FromBody] TestModel model)
        {
            return model;
        }

        [HttpPost("{action}")]
        public TestModel Validate([FromBody] TestModel model)
        {
            model.Identifier++;
            return model;
        }

        [HttpGet("{action}/{key}")]
        public TestModel SetKey(int key)
        {
            _service.Value = key;
            return new TestModel { Identifier = key };
        }

        [HttpGet("{action}")]
        public TestModel GetKey()
        {
            return new TestModel { Identifier = _service.Value };
        }
    }
}
