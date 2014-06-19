using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace ROD_Deck_Builder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Cards newpage = GetPage.GetPageData("http://reignofdragons.wikia.com/wiki/Category:All_Cards");
            
            //Cards currentPageData = GetTable.GetTableData(newpage);
//            HtmlAgilityPack.HtmlDocument webdoc = new HtmlAgilityPack.HtmlDocument();
//            webdoc.LoadHtml("http://reignofdragons.wikia.com/wiki/All");
//            //
//            foreach(HtmlNode table in webdoc.DocumentNode.SelectNodes("//a[@class=]"))
//            {HtmlNode newpagedata = table;}
            //Card items = (newpage.TableData).Items[0].Name;
            //List<ROD_Deck_Builder.EFaction> efactions = (newpage.TableData.Select(x => x.Faction).ToList());
            //List<string> factions = new List<string>();
            //foreach(EFaction efaction in efactions)
            //{ factions.Add(efaction.ToString()); }

            List<Card> cardlist = (newpage.TableData.ToList());
            var orderFactions =
                from f in cardlist
                group f by f.Faction into fg
                select new { Faction = fg.Key, factions = fg };
            var orderRealms =
                from r in cardlist
                group r by r.Realm into rg
                select new { Realm = rg.Key, realms = rg };
            var orderRarity =
                from cr in cardlist
                group cr by cr.Rarity into crg
                select new { Rarity = crg.Key, rarity = crg };
 
            listBox1.DisplayMember = "Realm";
            listBox1.DataSource = orderRealms.ToList();
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox2.DisplayMember = "Faction";
            listBox2.DataSource = orderFactions.ToList();
            listBox2.SelectionMode = SelectionMode.MultiExtended;
            listBox3.DisplayMember = "Rarity";
            listBox3.DataSource = orderRarity.ToList();
            listBox3.SelectionMode = SelectionMode.MultiExtended;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cardBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
