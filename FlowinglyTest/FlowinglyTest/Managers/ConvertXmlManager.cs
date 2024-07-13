using System.Xml;
using System.Xml.Linq;

namespace FlowinglyTest.Managers
{
    public static class ConvertXmlManager
    {       
        private static Dictionary<string, string>? ExtractAndParseXml(string emailText, out int processedPosition)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            processedPosition = emailText.Length;

            if (string.IsNullOrEmpty(emailText))
            {
                throw new ApiException("The Email is Empty.");
            }


            if (emailText.Contains("<") && emailText.Contains(">"))
            {
                int startIndex = emailText.IndexOf("<");
                int endIndex = emailText.IndexOf(">");
                string tag = emailText.Substring(startIndex + 1, endIndex - startIndex - 1);

                if (!emailText.Contains("</" + tag + ">"))
                {
                    throw new ApiException($"Missing closing tag for - {tag}");
                }

                processedPosition = emailText.IndexOf("</" + tag + ">") + ("</" + tag + ">").Length;

                int tagStartIndex = emailText.IndexOf("<" + tag + ">");
                int tagEndIndex = emailText.IndexOf("</" + tag + ">");
                string subString = emailText.Substring(tagStartIndex, tagEndIndex - tagStartIndex + ("</" + tag + ">").Length);
                try
                {
                    var xmlData = XElement.Parse(subString);
                    if (xmlData.Descendants().Count() > 0)
                    {
                        foreach (var item in xmlData.Descendants())
                        {
                            result.Add(item.Name.LocalName, item.Value);
                        }
                    }
                    else
                    {
                        result.Add(xmlData.Name.LocalName, xmlData.Value);
                    }
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message);
                }

            }
            return result;
        }


        public static Dictionary<string, string> GetXmlDataManager(string emailText)
        {
            Dictionary<string, string>? xmlData = new Dictionary<string, string>();
            Dictionary<string, string> completeXmlList = new Dictionary<string, string>();
            int processedPosition = 0;
            do
            {
                xmlData = ExtractAndParseXml(emailText, out processedPosition);
                if (xmlData != null)
                {
                    foreach (var item in xmlData)
                    {
                        completeXmlList.Add(item.Key, item.Value);
                    }
                }
                emailText = emailText.Substring(processedPosition);
            } while (emailText.Length > 0);

            ValidateXmlData(completeXmlList);

            return completeXmlList;
        }

        private static void ValidateXmlData(Dictionary<string, string> completeXmlList)
        {
            if (!completeXmlList.ContainsKey("total"))
            {
                throw new ApiException($"Total is missing from the message!!.");
            }
            else if (!completeXmlList.ContainsKey("cost_centre"))
            {
                completeXmlList.Add("cost_centre", "UNKNOWN");
            }
        }
    }
}
