using Byndyusoft.Net.Http.ProtoBuf.Models;
using Microsoft.AspNetCore.Mvc;

namespace Byndyusoft.Net.Http.ProtoBuf.Functional
{
    [Controller]
    [Route("msgpack-formatter")]
    public class ProtoBufFormatterController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] SimpleType model)
        {
            return Ok(model);
        }

        [HttpPut]
        public IActionResult Put([FromBody] SimpleType model)
        {
            return Ok(model);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(SimpleType.Create());
        }
    }
}