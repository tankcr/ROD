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
<<<<<<< HEAD
            //Card items = (newpage.TableData).Items[0].Name;
            //List<ROD_Deck_Builder.EFaction> efactions = (newpage.TableData.Select(x => x.Faction).ToList());
            //List<string> factions = new List<string>();
            //foreach(EFaction efaction in efactions)
            //{ factions.Add(efaction.ToString()); }
            listBox1.DisplayMember = "Faction";
            listBox1.DataSource = (newpage.TableData.Select(x => x.Faction).ToList());
            
            //(new System.Collections.Generic.Mscorlib_CollectionDebugView<ROD_Deck_Builder.Card>(newpage.TableData)).Items[0].Name;    
=======
                listBox1.DisplayMember = "Faction";
                listBox1.DataSource = (newpage.TableData.Select(x => x.Faction).ToList());

>>>>>>> 12f9156719031be04c4e9f5d0a6f9ac706759dd0
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
