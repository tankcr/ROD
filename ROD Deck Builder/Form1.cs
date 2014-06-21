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
        Cards newpage = GetPage.GetPageData("http://reignofdragons.wikia.com/wiki/Category:All_Cards");
        public Form1()
        {
            
            InitializeComponent();
            List<Card> cardlist = (newpage.TableData.ToList());
            //dataTable1.Rows.Clear();
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
            listBox1.SelectedItem = null;
            if (listBox1.SelectedItems.Count == 0) { }
            listBox2.DisplayMember = "Faction";
            listBox2.DataSource = orderFactions.ToList();
            listBox2.SelectionMode = SelectionMode.MultiExtended;
            listBox2.SelectedItem = null;
            listBox3.DisplayMember = "Rarity";
            listBox3.DataSource = orderRarity.ToList();
            listBox3.SelectionMode = SelectionMode.MultiExtended;
            listBox3.SelectedItem = null;
            //DataSet cardtableDataset = new DataSet();
            int numCards = cardlist.Count;
            for (int currCardIndex =0; currCardIndex < numCards; currCardIndex++)
            {
                DataRow drnew = cardTable.NewRow();
                Card currCard = cardlist[currCardIndex];
                drnew["Rarity"] = currCard.Rarity;
                drnew["Name"] = currCard.Name;
                drnew["Realm"] = currCard.Realm;
                drnew["Faction"] = currCard.Faction;
                drnew["MATK"] = currCard.MaxAtk;
                try { drnew["MDEF"] = currCard.MaxDef; }
                catch { drnew["MDEF"] = "missing"; }
                cardTable.Rows.Add(drnew);
            }

            dataGridView1.DataSource = cardTable;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            cardTable.Rows.Clear();
            ListBox.SelectedObjectCollection selecteditems = listBox1.SelectedItems;
            List<object> selectedrealms = new List<object>();
            List<ERealm> myrealms = new List<ERealm>();
            foreach (object selecteditem in selecteditems)
            { 
                string item = selecteditem.ToString();
                selectedrealms.Add(item);
                //ERealm currRealm = ERealm.item;           
            }
            
            //listBox1.Select(x => x.ToString());
            
            List<Card> cardlist = (newpage.TableData.ToList());
            var orderRealms =
                from r in selectedrealms
                group r by r into rg
                select new { Realm = rg.Key, realms = rg };
            int numCards = cardlist.Count;
            for (int currCardIndex = 0; currCardIndex < numCards; currCardIndex++)
            {
                
                DataRow drnew = cardTable.NewRow();
                Card currCard = cardlist[currCardIndex];
                if (currCard.Realm == selectedrealms.ToString())
                {
                    drnew["Rarity"] = currCard.Rarity;
                    drnew["Name"] = currCard.Name;
                    drnew["Realm"] = currCard.Realm;
                    drnew["Faction"] = currCard.Faction;
                    drnew["MATK"] = currCard.MaxAtk;
                    try { drnew["MDEF"] = currCard.MaxDef; }
                    catch { drnew["MDEF"] = "missing"; }
                    cardTable.Rows.Add(drnew);
                }
            }

                
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
