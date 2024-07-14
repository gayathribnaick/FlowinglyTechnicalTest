using FlowinglyTest.Exceptions;
using FlowinglyTest.Managers;
using ManagerTests.TestData;

namespace ManagerTests
{
    [TestClass]
    public class XmlConverterManagerTests
    {
        public XmlConverterManagerTests()
        {

        }

        /// <summary>
        /// Working email
        /// </summary>
        [TestMethod]
        public void ValidateEmail_ParsesSuccesfully()
        {
            Dictionary<string, object> xmlTags = ConvertXmlManager.GetXmlDataManager(EmailSamples.WorkingEmail, TaxData.ZeroTax);
            double total = 35000;
            Assert.AreEqual(xmlTags["total"], total);
            Assert.IsNotNull(xmlTags);
        }

        /// <summary>
        /// Working email with valid tax applied on total
        /// </summary>
        [TestMethod]
        public void ValidateEmailWithValidTax_ParsesSuccesfully()
        {
            Dictionary<string, object> xmlTags = ConvertXmlManager.GetXmlDataManager(EmailSamples.WorkingEmail, TaxData.ValidTax);
            double totalIncludingTax = 38500;
            Assert.AreEqual(xmlTags["total"], totalIncludingTax);
            Assert.IsNotNull(xmlTags);
        }


        /// <summary>
        /// no email content
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(APIExceptionBase))]
        public void ValidateEmptyEmail_ParsesFails()
        {
            Dictionary<string, object> xmlTags = ConvertXmlManager.GetXmlDataManager("", TaxData.ZeroTax);
        }


        /// <summary>
        /// tag missing in email content
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlTagNotFoundException))]
        public void ValidateEmail_ParsesFails()
        {
            Dictionary<string, object> xmlTags = ConvertXmlManager.GetXmlDataManager(EmailSamples.ExpenseTagMissingEmail, TaxData.ZeroTax);           
        }

        /// <summary>
        /// Xml parse errors
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ParseXmlException))]
        public void ValidateDataErrorEmail_ParsesFails()
        {
            Dictionary<string, object> xmlTags = ConvertXmlManager.GetXmlDataManager(EmailSamples.DataErrorEmail, TaxData.ZeroTax);
        }

        /// <summary>
        /// Xml errors - total tag missing
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DataErrorException))]
        public void ValidateDataErrorEmailWithTotalTagMissing_ParsesFails()
        {
            Dictionary<string, object> xmlTags = ConvertXmlManager.GetXmlDataManager(EmailSamples.TotalTagMissing, TaxData.ZeroTax);
        }

        /// <summary>
        /// Xml errors - cost_centre tag missing
        /// </summary>
        [TestMethod]
        public void ValidateDataErrorEmailWithCostCentreTagMissing_ParsesFails()
        {
            Dictionary<string, object> xmlTags = ConvertXmlManager.GetXmlDataManager(EmailSamples.CostCentreMissing, TaxData.ZeroTax);
            Assert.AreEqual(xmlTags["cost_centre"], "UNKNOWN");
        }
    }
}
