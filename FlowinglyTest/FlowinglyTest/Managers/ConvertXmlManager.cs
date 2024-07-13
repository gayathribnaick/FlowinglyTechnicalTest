using FlowinglyTest.Exceptions;
using System.Xml.Linq;

namespace FlowinglyTest.Managers
{
    public static class ConvertXmlManager
    {
        private static Dictionary<string, object>? ExtractAndParseXml(string emailText, out int processedPosition)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            processedPosition = emailText.Length;

            if (string.IsNullOrEmpty(emailText))
            {
                throw new ApiException("The Email is Empty."); // throw exception if email content is empty
            }


            if (emailText.Contains("<") && emailText.Contains(">"))
            {
                int startIndex = emailText.IndexOf("<"); // find xml tag start '<' index 
                int endIndex = emailText.IndexOf(">"); // find xml tag end '<' index
                string tag = emailText.Substring(startIndex + 1, endIndex - startIndex - 1); // find the tag name in bewtween < and > eg: if tage  is <expense>  then return expense

                if (!emailText.Contains("</" + tag + ">")) // make sure tag has ending tag otherwise throw error
                {
                    throw new ApiException($"Missing closing tag for - {tag}");
                }

                processedPosition = emailText.IndexOf("</" + tag + ">") + ("</" + tag + ">").Length; // this is the position upto which we took data for extraction

                int tagStartIndex = emailText.IndexOf("<" + tag + ">"); // find tag start index eg: start index on tag <expense> in email text
                int tagEndIndex = emailText.IndexOf("</" + tag + ">"); // find tag end index eg: index on tag </expense> in email text
                string subString = emailText.Substring(tagStartIndex, tagEndIndex - tagStartIndex + ("</" + tag + ">").Length); // include inner tags inside root tag if any
                try
                {
                    var xmlData = XElement.Parse(subString);
                    if (xmlData.Descendants().Count() > 0) // if inner tags existis take them
                    {
                        foreach (var item in xmlData.Descendants())
                        {
                            result.Add(item.Name.LocalName, item.Value); // loop inner tags and add them as key value pairs inside dictionary , eg: total, cost_centre tags inside expense tag and there values
                        }
                    }
                    else
                    {
                        result.Add(xmlData.Name.LocalName, xmlData.Value); // if no descendants 
                    }
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message);
                }

            }

            return result;
        }


        public static Dictionary<string, object> GetXmlDataManager(string emailText, double taxApplicable)
        {
            Dictionary<string, object>? xmlData = new Dictionary<string, object>(); // Dictionary to get xml attributes based on root nodes
            Dictionary<string, object> completeXmlList = new Dictionary<string, object>(); // Dictionary for complete liost of xml data attributes from rrot nodes as well as individual nodes
            int processedPosition = 0;

            // Loop through the mail content to identify the xml tags
            do
            {
                // function to extract inner xml tags from root or individual xml tags
                xmlData = ExtractAndParseXml(emailText, out processedPosition);

                if (xmlData != null)
                {
                    foreach (var item in xmlData)
                    {
                        completeXmlList.Add(item.Key, item.Value);// add extracted tags on key values basis to accumulative list
                    }
                }
                emailText = emailText.Substring(processedPosition); //Identify last position of xml extrcation and then loop remaing data
            } while (emailText.Length > 0);

            ValidateXmlData(completeXmlList, taxApplicable); // validation for total and cost centre

            return completeXmlList;
        }

        private static void ValidateXmlData(Dictionary<string, object> completeXmlList, double taxApplicable)
        {
            if(taxApplicable < 0)
            {
                throw new ApiException("Tax cannot be less than zero.");
            }

            if (!completeXmlList.ContainsKey("total")) // if total tag is missing throw error
            {
                throw new ApiException($"Total is missing from the message!!.");
            }
            else
            {
                double total = Double.Parse(Convert.ToString(completeXmlList["total"]), System.Globalization.CultureInfo.InvariantCulture);                

                completeXmlList["total"] = Convert.ToDouble(total + (total * (taxApplicable / 100)));
            }

            if (!completeXmlList.ContainsKey("cost_centre")) // if cost centre is not available - UNKNOWN
            {
                completeXmlList.Add("cost_centre", "UNKNOWN");
            }
        }
    }
}
