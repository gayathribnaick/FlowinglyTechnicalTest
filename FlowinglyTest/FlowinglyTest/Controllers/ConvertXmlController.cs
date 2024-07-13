using FlowinglyTest.Managers;
using Microsoft.AspNetCore.Mvc;

namespace FlowinglyTest.Controllers
{
    public class ConvertXmlController : Controller
    {

        [HttpPost]
        [Route("GetXmlData")]
        public Dictionary<string, string>? GetXmlData(string emailText)
        {
            return ConvertXmlManager.GetXmlDataManager(emailText);
        }
    }
}
