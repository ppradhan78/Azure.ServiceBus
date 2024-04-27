using Azure.ServiceBus.API.Data.BusinessObject;
using Azure.ServiceBus.API.Data.Core;
using Azure.ServiceBus.API.Data.SimpleModels;
using Microsoft.AspNetCore.Mvc;


namespace Azure.ServiceBus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessSBQueueMessageController : ControllerBase
    {
        private IFeedProcessSBQueueCore _feedProcessSBQueueCore;
        private readonly IConfigurationSettings _configuration;
        public ProcessSBQueueMessageController(IConfigurationSettings configuration,IFeedProcessSBQueueCore feedProcessSBQueueCore)
        {
            _feedProcessSBQueueCore = feedProcessSBQueueCore;
            _configuration = configuration;
        }

        // GET api/<ProcessSBQueueMessageController>/5
        [HttpGet]
        public string Get()
        {
            return _feedProcessSBQueueCore.ReadMessage().Result;
        }

        // POST api/<ProcessSBQueueMessageController>
        [HttpPost]
        public  void Post([FromBody] OrderSampleModel value)
        {
              _feedProcessSBQueueCore.SendToQueueAsync(value);
        }

     
    }
}
