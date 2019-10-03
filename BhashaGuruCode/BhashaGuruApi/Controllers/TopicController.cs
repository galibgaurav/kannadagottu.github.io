using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BhashaguruModel;
using BusinessLayerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BhashaGuruApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private IConfiguration _iconfiguration;
        public TopicController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        /// <summary>
        /// Get All the Topics Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<List<Card>> GetCardSet(string topicName)
        {
            TopicService topicService = new TopicService(_iconfiguration);
            return await topicService.GetCardSet(topicName);
        }


        // GET: api/Topic
        [HttpGet]
        public async Task<TopicDetail> Get()
        {
            TopicService topicService = new TopicService(_iconfiguration);
            return await topicService.GetTopicDetails();
        }

        // GET: api/Topic/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Topic
        [HttpPost]
        public async Task<Topic> Post(Topic topic)
        {
            TopicService topicService = new TopicService(_iconfiguration);
            return await topicService.AddTopic(topic);
        }

        // PUT: api/Topic/5
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
