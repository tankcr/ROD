using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

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
                Card item = new Card();

                HtmlNodeCollection rowcolumns = row.SelectNodes("td");
                    Match match = Regex.Match(rowcolumns[0].InnerText, @"\d+");
                    if (match.Success)
                    {
                        int rarity = 0;
                        int.TryParse(match.Value, out rarity);
                        if (rarity >= 1 && rarity <= 7)
                        {
                            item.Rarity = (ERarity)rarity;
                        }
                        else
                        {
                            item.Rarity = ERarity.None;
                        }
                    }
                    item.Name = Convert.ToString(rowcolumns[1].InnerText).TrimEnd('\r', '\n');
                    item.Realm = ParseRealm(rowcolumns[2]);
                    //item.Realm = Convert.ToString(rowcolumns[2].InnerText).TrimEnd('\r', '\n');
                    item.Faction = ParseFaction(rowcolumns[3]);
                    //item.MaxAtk = Convert.ToString(rowcolumns[4].InnerText).TrimEnd('\r', '\n');
                    //item.MaxDef = Convert.ToString(rowcolumns[5].InnerText).TrimEnd('\r', '\n');
                    item.Total = Convert.ToInt32(rowcolumns[6].InnerText);
                    item.Cost = Convert.ToInt32(rowcolumns[7].InnerText);
                    item.AttEff = Convert.ToInt32(rowcolumns[8].InnerText);
                    item.DefEff = Convert.ToInt32(rowcolumns[9].InnerText);
                    item.OverallEff = Convert.ToInt32(rowcolumns[10].InnerText);
                    //item.Skill = Convert.ToString(rowcolumns[11].InnerText).TrimEnd('\r', '\n');
                    //item.EventSkl1 = Convert.ToString(rowcolumns[12].InnerText).TrimEnd('\r', '\n');
                    //item.EventSkl2 = Convert.ToString(rowcolumns[13].InnerText).TrimEnd('\r', '\n');
                    table.TableData.Add(item);
            }
           return table;
        }

        private static ERealm ParseRealm(HtmlNode rowcolumns)
        {
            HtmlNode spanChild1 = rowcolumns.SelectSingleNode("./span");
            string realm = rowcolumns.SelectSingleNode("./span").InnerText;
            ERealm crealm = ERealm.None;
            Match match = Regex.Match(realm, @"\w+");
            if (match.Success)
            {
                if (realm == "C")
                { crealm = ERealm.Chaos; };
                if (realm == "G")
                { crealm = ERealm.Genesis; };
                if (realm == "J")
                { crealm = ERealm.Justice; };
            }
            return crealm;
        }
        private static EFaction ParseFaction(HtmlNode rowcolumns)
        {
            HtmlNode spanChild1 = rowcolumns.SelectSingleNode("./span");
            string faction = rowcolumns.SelectSingleNode("./span").Attributes;
            EFaction cfaction = EFaction.None;
            Match match = Regex.Match(faction, @"\w+");
            if (match.Success)
            {
                if (faction == "\xe2\x9a\x94\x0a")
                { cfaction = EFaction.Melee; };
                if (faction == "G")
                { cfaction = EFaction.Magic; };
                if (faction == "J")
                { cfaction = EFaction.Charm; };
            }
            return cfaction;
        }
        
    }
}
