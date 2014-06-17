﻿using HtmlAgilityPack;
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
    public class Cards
    {
        public event EventHandler TableDataChanged;

        private List<Card> tableData;
        public List<Card> TableData
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

        public Cards()
        {

        }
    }

    public class GetPage
    {
        // Parse the specified URL, populating one Card for each row found in the 'witiable sortable' class.
        public static Cards GetPageData(string url)
        {
            Cards table = new Cards();
            table.TableData = new List<Card>();

            WebClient dl = new WebClient();
            dl.Encoding = Encoding.UTF8;
            string page = dl.DownloadString(url);
            HtmlDocument mydoc = new HtmlDocument();
            mydoc.LoadHtml(page);

            // Grab the name of each column from the table headers
            HtmlNode htmltable = mydoc.DocumentNode.SelectSingleNode("//table[@class='witiable sortable']");
            HtmlNodeCollection tableHeaders = htmltable.SelectNodes("tr[th]/th");
            foreach (HtmlNode header in tableHeaders)
            {
                string columnName = header.InnerText.Trim();
            }

            List<HtmlNode> tableRows = htmltable.SelectNodes("tr").OfType<HtmlNode>().Skip(1).ToList();
            foreach (HtmlNode row in tableRows)
            {
                Card item = new Card();

                HtmlNodeCollection rowcolumns = row.SelectNodes("td");

                item.Rarity = ParseRarity(rowcolumns[0]);
                item.Name = Convert.ToString(rowcolumns[1].InnerText).TrimEnd('\r', '\n');
                item.Realm = ParseRealm(rowcolumns[2]);
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

        private static ERarity ParseRarity(HtmlNode rowcolumn)
        {
            ERarity eRarity = ERarity.None;
            Match match = Regex.Match(rowcolumn.InnerText, @"\d+");
            if (match.Success)
            {
                int rarity = 0;
                int.TryParse(match.Value, out rarity);
                if (rarity >= 1 && rarity <= 7)
                {
                    eRarity = (ERarity)rarity;
                }
                else
                {
                    eRarity = ERarity.None;
                }
            }
            return eRarity;
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

        // This <td> has a single <span> with a single "color" attribute and a single value.
        private static EFaction ParseFaction(HtmlNode rowcolumns)
        {
            EFaction cfaction = EFaction.None;

            // Get the style from the <span> that contains the "color" attribute.
            HtmlAttribute spanStyle = rowcolumns.SelectSingleNode("./span").Attributes
                .Where(style => style.Value.Contains("color"))
                .First();
            if (spanStyle != null)
            {
                // Split apart all the attributes, and only keep the one with the "color" attribute.
                // This returns the value of that "color:[something]" pair.
                string color = spanStyle.Value.Split(';')
                    .Where(s => s.Contains("color"))
                    .First()
                    .Split(':')[1];
                switch (color)
                {
                    case "Fuchsia":
                            cfaction = EFaction.Charm;
                            break;

                    case "Gold":
                            cfaction = EFaction.Melee;
                            break;

                    case "LimeGreen":
                            cfaction = EFaction.Magic;
                            break;
                }
            }

            return cfaction;
        }
    }
}
