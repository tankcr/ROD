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
        private static Dictionary<string, ERealm> realmKey = null;

        // Parse the specified URL, populating one Card for each row found in the 'witiable sortable' class.
        public static Cards GetPageData(string url)
        {
            if (realmKey == null)
            {
                realmKey = new Dictionary<string, ERealm>();
                realmKey.Add("C", ERealm.Chaos);
                realmKey.Add("G", ERealm.Genesis);
                realmKey.Add("J", ERealm.Justice);
            }
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
                item.Name = ParseStringFromHtml(rowcolumns[1]);
                item.Realm = ParseRealm(rowcolumns[2]);
                item.Faction = ParseFactionByColor(rowcolumns[3]);
                item.MaxAtk = ParseIntFromHtml(rowcolumns[4]);
                item.MaxDef = ParseIntFromHtml(rowcolumns[5]);
                item.Total = (item.MaxAtk + item.MaxDef);
                try { item.Cost = ParseIntFromHtml(rowcolumns[7]); }
                catch { item.Cost = 0 ; }
                if (item.Cost == null) 
                { item.Cost = 0; }
                try { item.AttEff = CalculateAttackEffect(item.MaxAtk, item.Cost); }
                catch { item.AttEff = 0; }
                try { item.DefEff = CalculateDefenseEffect(item.MaxDef, item.Cost); }
                catch { item.DefEff = 0; }
                try { item.OverallEff = CalculateOverallEffect(item.Total, item.Cost); }
                catch { item.OverallEff = 0; }
                try { item.Skill = ParseStringFromHtml(rowcolumns[11]); }
                catch { item.Skill = "None"; }
                if (item.Skill == "")
                { item.Skill = "None"; }
                if (item.Skill == null)
                { item.Skill = "None"; }
                if (item.Skill == "-") 
                { item.Skill = "None"; }
                if (item.Skill.Contains('ε'))
                { item.Skill = item.Skill.Replace(" (ε)",""); }
                item.EventSkl1 = ParseStringFromHtml(rowcolumns[12]);
                item.EventSkl2 = ParseStringFromHtml(rowcolumns[13]);
                table.TableData.Add(item);
            }
            return table;
        }

        private static int CalculateAttackEffect(int maxAtk, int cost)
        {
            if (cost != 0)
            {
                return (maxAtk / cost);
            }
            return 0;
        }

        private static int CalculateDefenseEffect(int maxDef, int cost)
        {
            if (cost != 0)
            {
                return (maxDef / cost);
            }
            return 0;
        }

        private static int CalculateOverallEffect(int total, int cost)
        {
            if (cost != 0)
            {
                return (total / cost);
            }
            return 0;
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
                result = Convert.ToString(htmlNode.InnerText).Trim('\r', '\n',' ');
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Unable to parse the value.");
            }
            return result;
        }

        private static ERarity ParseRarity(HtmlNode rowcolumn)
        {
            ERarity eRarity = ERarity.None;
            Match match = Regex.Match(rowcolumn.InnerText, @"\d+");
            if (match.Success)
            {
                int rarity = 0;
                int.TryParse(match.Value, out rarity);
                if (rarity >= 0 && rarity <= 7)
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
            string realm;
            HtmlNode spanChild1 = rowcolumns.SelectSingleNode("./span");
            try { realm = spanChild1.InnerText; }
            catch { realm = "None"; }
            //if (realm == "G")
            //{ realm = "Genesis"; }
            //else if (realm == "C")
            //{ realm = "Chaos"; }
            //else if (realm == "J")
            //{ realm = "Justice"; }
            ERealm crealm = ERealm.None;
            Match match = Regex.Match(realm.ToString(), @"\w+");
            if (match.Success && realmKey.ContainsKey(realm))
            {
                crealm = realmKey[realm];
            }
            return crealm;
        }

        // This <td> has a single <span> with a single "color" attribute and a single value.
        private static EFaction ParseFactionByColor(HtmlNode rowcolumns)
        {
            EFaction cfaction = EFaction.None;

            // Get the style from the <span> that contains the "color" attribute.
            HtmlAttribute spanStyle = null;
            try
            {
                spanStyle = rowcolumns.SelectSingleNode("./span").Attributes
                    .Where(style => style.Value.Contains("color"))
                    .First();
            }
            catch { spanStyle = null; };
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
