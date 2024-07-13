using FlowinglyTest.Exceptions;
using FlowinglyTest.Managers;
using ManagerTests.EmailSampleFormats;

namespace ManagerTests
{
    [TestClass]
    public class XmlConverterManagerTests
    {
        public XmlConverterManagerTests()
        {

        }

        [TestMethod]
        public void ValidateEmail_ParsesSuccesfully()
        {
            Dictionary<string, object> xmlTags = ConvertXmlManager.GetXmlDataManager(EmailSamples.WorkingEmail, 0);
            Assert.IsNotNull(xmlTags);
        }


        [TestMethod]
        [ExpectedException(typeof(ApiException))]
        public void ValidateEmail_ParsesFails()
        {
            Dictionary<string, object> xmlTags = ConvertXmlManager.GetXmlDataManager(EmailSamples.ExpenseTagMissingEmail, 0);           
        }
    }
}
