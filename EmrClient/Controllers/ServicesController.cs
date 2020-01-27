namespace EmrClient.Controllers
{
    using System;
    using System.ServiceModel;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ServiceReference;

    [ApiController]
    [Route("/services")]
    public class ServicesController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get()
        {
            var sampleService = GetClient();
            var response = await sampleService.Get_emr_PatientTypeAsync(
                new Get_emr_PatientTypeRequest("IT/00001", ""));
            return response.Get_emr_PatientTypeResult.Any1.InnerText;
        }

        private static WebEmrServiceSoap GetClient()
        {
            var binding = new BasicHttpsBinding();
            var endpoint = new EndpointAddress(new Uri("https://tmc.gov.in/m_webemr_service_tmc/webemrservice.asmx"));
            var channelFactory = new ChannelFactory<WebEmrServiceSoap>(binding, endpoint);
            var serviceClient = channelFactory.CreateChannel();
            return serviceClient;
        }
    }
}