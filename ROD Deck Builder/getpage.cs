using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Xml.Serialization;

namespace ROD_Deck_Builder
{
    public class Cards
    {
        public List<Card> tableData;

        public List<Card> TableData
        {
            get { return tableData; }
            set { tableData = value; }
        }

        public Cards()
        {

        }
    }
    public class GetPage
    {
        public static Cards GetPageData(string url)
        {

            Cards table = new Cards();
            table.TableData = new List<Card>();
            string weburl = url;
            WebClient dl = new WebClient();
            dl.Encoding = Encoding.UTF8;
            string page = dl.DownloadString(weburl);
            HtmlDocument mydoc = new HtmlDocument();
            mydoc.LoadHtml(page);

            HtmlNode htmltable = mydoc.DocumentNode.SelectSingleNode("//table[@class='witiable sortable']");
            var tableHeaders = htmltable.SelectNodes("tr[th]/th");
            foreach (var header in tableHeaders)
            {
                string columnName = header.InnerText.Trim();
            }

            var tableRows = htmltable.SelectNodes("tr").OfType<HtmlNode>().Skip(1);
            foreach (var row in tableRows)
            
            {
                
                //string singleLine;
                //string[] splitApart = singleLine.Split(',');
                Card item = new Card();
                var rowcolumns = row.SelectNodes("td");
                //foreach (var column in rowcolumns)
                //{
                    
                    //string columnValue = column.InnerText.Trim();
                    //item.Rarity = columnValue;
                    //table.TableData.Add(item);
                //}
                    //Card item = new Card();
                    item.Rarity = Convert.ToString(rowcolumns[0].InnerText).TrimEnd('\r', '\n');
                    item.Name = Convert.ToString(rowcolumns[1].InnerText).TrimEnd('\r', '\n');
                    item.Realm = Convert.ToString(rowcolumns[2].InnerText).TrimEnd('\r', '\n');
                    item.Faction = Convert.ToString(rowcolumns[3].InnerText).TrimEnd('\r', '\n');
                    item.MaxAtk = Convert.ToString(rowcolumns[4].InnerText).TrimEnd('\r', '\n');
                    item.MaxDef = Convert.ToString(rowcolumns[5].InnerText).TrimEnd('\r', '\n');
                    item.Total = Convert.ToInt32(rowcolumns[6].InnerText);
                    item.Cost = Convert.ToInt32(rowcolumns[7].InnerText);
                    item.AttEff = Convert.ToInt32(rowcolumns[8].InnerText);
                    item.DefEff = Convert.ToInt32(rowcolumns[9].InnerText);
                    item.OverallEff = Convert.ToInt32(rowcolumns[10].InnerText);
                    item.Skill = Convert.ToString(rowcolumns[11].InnerText).TrimEnd('\r', '\n');
                    item.EventSkl1 = Convert.ToString(rowcolumns[12].InnerText).TrimEnd('\r', '\n');
                    item.EventSkl2 = Convert.ToString(rowcolumns[13].InnerText).TrimEnd('\r', '\n');
                    table.TableData.Add(item);
            }
           return table;
        }
    }
}
