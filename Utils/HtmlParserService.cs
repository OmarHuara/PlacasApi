using System.Globalization;
using System.Text;

namespace PlacasAPI.Utils
{
    using HtmlAgilityPack;
    public class HtmlParserService
    {
        public Dictionary<string, string> ParseHtmlToList(string conteudo, string plate)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(conteudo);
            var dataTable = htmlDoc.DocumentNode.SelectNodes("//table[@class='fipeTablePriceDetail']");
            if (dataTable != null)
            {
                return GetDataFromTable(dataTable, plate);
            }
            else
            {
                return null;
            }
        }

        private Dictionary<string, string> GetDataFromTable(HtmlNodeCollection dataTable, string plate)
        {
            Dictionary<string, string> datas = new Dictionary<string, string>();
            datas.Add("PLATE", plate);
            foreach (var table in dataTable)
            {
                var rows = table.SelectNodes(".//tr");
                foreach (var row in rows)
                {
                    datas.Add(GetNameToRow(row.InnerText), GetDataToRow(row.InnerText));
                }
            }
            return datas;
        }

        private string GetNameToRow(string rowInnerText)
        {
            int index = rowInnerText.IndexOf(":");
            return RemoveAccents(rowInnerText.Substring(0, index).Trim()).ToUpper();
        }

        private string RemoveAccents(string text)
        {
            var textNormalize = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var character in textNormalize)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(character);
                }
            }

            return sb.ToString();
        }

        private string GetDataToRow(string innerText)
        {
            int index = innerText.IndexOf(":");
            return innerText.Substring(index + 1);
        }
    }
}
