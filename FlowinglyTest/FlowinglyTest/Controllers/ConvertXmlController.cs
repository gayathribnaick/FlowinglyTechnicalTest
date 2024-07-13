using FlowinglyTest.Managers;
using Microsoft.AspNetCore.Mvc;

namespace FlowinglyTest.Controllers
{
    public class ConvertXmlController : Controller
    {

        [HttpPost]
        [Route("GetXmlData")]
        public Dictionary<string, object>? GetXmlData(string emailText, double taxApplicable)
        {
            return ConvertXmlManager.GetXmlDataManager(emailText, taxApplicable);
        }
    }
}
