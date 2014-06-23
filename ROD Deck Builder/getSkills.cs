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
    public class Skills
    {
        public event EventHandler TableDataChanged;

        private List<Skill> tableData;
        public List<Skill> TableData
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

        public Skills()
        {

        }
    }

    public class GetSkills
    {
        // Parse the specified URL, populating one Skills for each row found in the 'witiable sortable' class.
        public static Skills GetPageData(string url)
        {
            Skills table = new Skills();
            table.TableData = new List<Skill>();

            WebClient dl = new WebClient();
            dl.Encoding = Encoding.UTF8;
            string page = dl.DownloadString(url);
            HtmlDocument mydoc = new HtmlDocument();
            mydoc.LoadHtml(page);

            // Grab the name of each column from the table headers
            HtmlNode htmltable = mydoc.DocumentNode.SelectSingleNode("(//table[@class='article-table'])[2]");
            HtmlNodeCollection tableHeaders = htmltable.SelectNodes("tr[th]/th");
            foreach (HtmlNode header in tableHeaders)
            {
                string columnName = header.InnerText.Trim();
            }

            List<HtmlNode> tableRows = htmltable.SelectNodes("tr").OfType<HtmlNode>().Skip(1).ToList();
            foreach (HtmlNode row in tableRows)
            {
                Skill item = new Skill();

                HtmlNodeCollection rowcolumns = row.SelectNodes("td");
                item.SkillName = ParseStringFromHtml(rowcolumns[0]);
                item.Effect = ParseStringFromHtml(rowcolumns[1]);
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
