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
        }
    }
}
