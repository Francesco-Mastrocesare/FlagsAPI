using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlagsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FlagsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlagsController : ControllerBase
    {
        private IWorkerServiceFlags worker;
        private readonly IConfiguration configuration;

        public FlagsController(IWorkerServiceFlags _worker, IConfiguration config)
        {
            worker = _worker;
            configuration = config;
        }


        // GET: api/Flags
        [HttpGet]
        public async Task<List<string>> Get()
        {
            return await worker.GetFlagsUrl();
        }

        // GET: api/Flags/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string flag)
        {
            return "value";
        }

        // POST: api/Flags
        [HttpPost]
        public async Task<string> Post(Flag flag)
        {
            return await worker.ReturnFlagUrl(flag);
        }

        // PUT: api/Flags/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
