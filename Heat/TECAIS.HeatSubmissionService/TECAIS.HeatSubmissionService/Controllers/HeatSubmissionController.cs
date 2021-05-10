using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECAIS.HeatSubmissionService.Helpers;
using TECAIS.HeatSubmissionService.Models;

namespace TECAIS.HeatSubmissionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeatSubmissionController : ControllerBase
    {
        private readonly ILogger<HeatSubmissionController> _logger;

        public HeatSubmissionController(ILogger<HeatSubmissionController> logger)
        {
            _logger = logger;
        }

        //POST - Creates a new heat submission
        [HttpPost]
        public IActionResult CreateHeatSubmission(HeatSubmission heatSubmission)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            try
            {
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "HeatSubmissionsPublish",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                        string msg = JsonConvert.SerializeObject(heatSubmission);
                        var reqBody = Encoding.UTF8.GetBytes(msg);

                        Console.WriteLine(reqBody);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "HeatSubmissionsPublish",
                                             basicProperties: null,
                                             body: reqBody);
                    }
                }

                return Ok();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
