using Azure.ServiceBus.API.Data.BusinessObject;
using Azure.ServiceBus.API.Data.Core;
using Azure.ServiceBus.API.Data.SimpleModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Azure.ServiceBus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessSBTopicMessageController : ControllerBase
    {
        private IFeedProcessSBTopicCore _feedProcessSBTopicCore;
        private readonly IConfigurationSettings _configuration;

        public ProcessSBTopicMessageController(IConfigurationSettings configuration, IFeedProcessSBTopicCore feedProcessSBTopicCore)
        {
            _feedProcessSBTopicCore = feedProcessSBTopicCore;
            _configuration = configuration;

        }

        // GET api/<ProcessSBQueueMessageController>/5
        [HttpGet]
        public string Get(int id)
        {
            return _feedProcessSBTopicCore.ReadMessage().Result;
        }

        // POST api/<ProcessSBQueueMessageController>
        [HttpPost]
        public void Post([FromBody] OrderSampleModel value)
        {
            _feedProcessSBTopicCore.SendToQueueAsync(value);
        }
    }
}
