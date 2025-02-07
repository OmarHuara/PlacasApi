namespace PlacasAPI.Utils
{
    using HtmlAgilityPack;
    public class HtmlParserService
    {
        public List<string> ParseHtmlToList(string conteudo)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(conteudo);
            var dataTable = htmlDoc.DocumentNode.SelectNodes("//table[@class='fipeTablePriceDetail']");
            return GetDataFromTable(dataTable);
        }

        private List<string> GetDataFromTable(HtmlNodeCollection dataTable)
        {
            List<string> datas = new List<string>();
            foreach (var table in dataTable)
            {
                var rows = table.SelectNodes(".//tr");
                foreach (var row in rows)
                {
                    datas.Add(GetDataToRow(row.InnerText));
                }
            }
            return datas;
        }

        private string GetDataToRow(string innerText)
        {
            int index = innerText.IndexOf(":");
            return innerText.Substring(index + 1);
        }
    }
}
