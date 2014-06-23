using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ROD_Deck_Builder
{
    public class Images
    {
        public event EventHandler TableDataChanged;

        private List<Image> tableData;
        public List<Image> TableData
        {
            get { return tableData; }
            set
            {
                tableData = value;
                if (TableDataChanged != null)
                {
                    TableDataChanged(this, EventArgs.Empty);
                }
            }
        }

        public Images()
        {

        }
    }

    public class GetImages
    {
        // Parse the specified URL, populating one Images for each row found in the 'witiable sortable' class.
        public static Images GetPageData(string url)
        {
            Images table = new Images();
            table.TableData = new List<Image>();

            WebClient dl = new WebClient();
            dl.Encoding = Encoding.UTF8;
            string page = dl.DownloadString(url);
            HtmlDocument mydoc = new HtmlDocument();
            mydoc.LoadHtml(page);

            // Grab the name of each column from the table headers
            HtmlNode htmltable = mydoc.DocumentNode.SelectSingleNode("(//*[@id='mw-content-text'])");
            HtmlNodeCollection tableHeaders = htmltable.SelectNodes("figure");
            foreach (HtmlNode header in tableHeaders)
            {
                string columnName = header.InnerText.Trim();
            }

            List<HtmlNode> tableRows = htmltable.SelectNodes("tr").OfType<HtmlNode>().Skip(1).ToList();
            foreach (HtmlNode row in tableRows)
            {
                Image item = new Image();

                HtmlNodeCollection rowcolumns = row.SelectNodes("td");
                item.ImageURL = ParseStringFromHtml(rowcolumns[0]);
                table.TableData.Add(item);
            }
            return table;
        }

        private static int ParseIntFromHtml(HtmlNode htmlNode)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(htmlNode.InnerText);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Unable to parse the value.");
            }
            return result;
        }

        private static string ParseStringFromHtml(HtmlNode htmlNode)
        {
            string result = "";
            try
            {
                result = Convert.ToString(htmlNode.InnerText).Trim('\r', '\n');
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Unable to parse the value.");
            }
            return result;
        }

        // This <td> has a single <span> with a single "color" attribute and a single value.
    }
}
