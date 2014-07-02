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
        // Stores all of the cards
        Cards newpage = GetPage.GetPageData("http://reignofdragons.wikia.com/wiki/Category:All_Cards");
        // Stores all of the Skills
        Skills skills = GetSkills.GetPageData("http://reignofdragons.wikia.com/wiki/Category:Skills");
        // List of all of the currently selected factions
        List<string> factionSelections = new List<string>();
        // List of all of the currently selected skills
        List<string> skillSelections = new List<string>();
        // List of all of the currently selected realms
        List<string> realmSelections = new List<string>();
        // List of all of the currently selected rarities
        List<string> raritySelections = new List<string>();
        //List of Cards from Typed Text
        List<string> typednames = new List<string>();

        public Form1()
        {
            Array values;

            InitializeComponent();
            pictureBox5.Image = ROD_Deck_Builder.Properties.Resources.ROD;
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            List<Card> cardlist = (newpage.TableData.ToList());

            lbxRealms.DisplayMember = "Realm";
            // Add each of the realm values
            values = System.Enum.GetNames(typeof(ERealm));
            realmSelections = new List<string>(values.Length);
            foreach (string realm in values)
            {
                lbxRealms.Items.Add(realm);
                realmSelections.Add(realm);
            }
            //listBox1.DataSource = orderRealms.ToList();
            lbxRealms.SelectionMode = SelectionMode.MultiExtended;
            lbxRealms.SelectedItem = null;
            lbxFactions.DisplayMember = "Faction";
            // Add each of the faction values
            values = System.Enum.GetNames(typeof(EFaction));
            factionSelections = new List<string>(values.Length);
            foreach (string faction in values)
            {
                lbxFactions.Items.Add(faction);
                factionSelections.Add(faction);
            }
            //listBox2.DataSource = orderFactions.ToList();
            lbxFactions.SelectionMode = SelectionMode.MultiExtended;
            lbxFactions.SelectedItem = null;


            lbxSkills.DisplayMember = "Skills";
            // Add each of the faction values
            values = skills.TableData.ToArray();
            skillSelections = new List<string>(values.Length);
            foreach (Skill skill in values)
            {
                lbxSkills.Items.Add(skill.SkillName);
                skillSelections.Add(skill.SkillName);
            }
            //listBox2.DataSource = orderFactions.ToList();
            lbxSkills.SelectionMode = SelectionMode.MultiExtended;
            lbxSkills.SelectedItem = null;



            lbxRarity.DisplayMember = "Rarity";
            // Add each of the rarity selections
            values = System.Enum.GetNames(typeof(ERarity));
            raritySelections = new List<string>(values.Length);
            foreach (string rarity in values)
            {
                lbxRarity.Items.Add(rarity);
                raritySelections.Add(rarity);
            }
            //listBox3.DataSource = orderRarity.ToList();
            lbxRarity.SelectionMode = SelectionMode.MultiExtended;
            lbxRarity.SelectedItem = null;
            //DataSet cardtableDataset = new DataSet();

            foreach (Card currCard in cardlist)
            {
                AddCardsToCardtable(ref cardTable, currCard);
            }

            dataGridView1.DataSource = cardTable;
        }


        private void AddCardsToCardtable(ref System.Data.DataTable dataTable, Card currCard)
        {
            DataRow drnew = dataTable.NewRow();
            drnew["Rarity"] = currCard.Rarity;
            drnew["Name"] = currCard.Name;
            drnew["Realm"] = currCard.Realm;
            drnew["Faction"] = currCard.Faction;
            drnew["MATK"] = currCard.MaxAtk;
            drnew["MDEF"] = currCard.MaxDef;
            drnew["Total"] = currCard.Total;
            drnew["Cost"] = currCard.Cost;
            drnew["AttEff"] = currCard.AttEff;
            drnew["DefEff"] = currCard.DefEff;
            drnew["OverallEff"] = currCard.OverallEff;
            drnew["Skill"] = currCard.Skill;
            dataTable.Rows.Add(drnew);
        }


        private void lbxRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Realm
            ListBox.SelectedObjectCollection selecteditems = lbxRealms.SelectedItems;
            realmSelections = new List<string>(selecteditems.Count);
            foreach (string selecteditem in selecteditems)
            {
                realmSelections.Add(selecteditem);
            }
            UpdateGrid();
        }


        private void lbxFactions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Faction
            ListBox.SelectedObjectCollection selecteditems = lbxFactions.SelectedItems;
            factionSelections = new List<string>(selecteditems.Count);
            foreach (string selecteditem in selecteditems)
            {
                factionSelections.Add(selecteditem);
            }
            UpdateGrid();
        }


        private void lbxSkills_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Skill
            ListBox.SelectedObjectCollection selecteditems = lbxSkills.SelectedItems;
            skillSelections = new List<string>(selecteditems.Count);
            foreach (string selecteditem in selecteditems)
            {
                skillSelections.Add(selecteditem);
            }
            UpdateGrid();
        }


        private void lbxRarity_SelectedIndexChanged(object sender, EventArgs e)
       {
            // Faction
            ListBox.SelectedObjectCollection selecteditems = lbxRarity.SelectedItems;
            raritySelections = new List<string>(selecteditems.Count);
            foreach (string selecteditem in selecteditems)
            {
                raritySelections.Add(selecteditem);
            }
            UpdateGrid();
        }
        
        //{
            // Rarity
            //ListBox.SelectedObjectCollection selecteditems = lbxRarity.SelectedItems;
            //if (selecteditems.Count == 0)
            //{
            //    string[] values = System.Enum.GetNames(typeof(ERarity));
            //    raritySelections = values.ToList();
            //}
            //else
            //{
                //raritySelections = new List<string>(selecteditems.Count);
                //foreach (object selecteditem in selecteditems)
                //{
                    //raritySelections.Add((string)selecteditem);
                //}
            //}
            //UpdateGrid();
        //}


        private void UpdateGrid()
        {
            bool cardPassed;

            // use the three sets of selections to determine if the card can be viewed
            cardTable.Rows.Clear();
            List<Card> cardlist = (newpage.TableData.ToList());
            foreach (Card currCard in cardlist)
            {
                string myrarity = currCard.Rarity.ToString();
                if (realmSelections.Count != 0 && !realmSelections.Contains(currCard.Realm.ToString()))
                    continue;

                else if (factionSelections.Count != 0 && !factionSelections.Contains(currCard.Faction.ToString()))
                    continue;

                else if (skillSelections.Count != 0 && !skillSelections.Contains(currCard.Skill.ToString()))
                    continue;
                
                else if (raritySelections.Count != 0 && !raritySelections.Contains(myrarity))
                    continue;

                else if (typednames.Count != 0 && !typednames.Contains(currCard.Name.ToString()))
                    continue;
                AddCardsToCardtable(ref cardTable, currCard);
            }
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            DataView currtable = new DataView(cardTable);
            string selectName = currtable[rowindex]["Name"].ToString().Replace(" ", "_");
            Images imgurl = GetImages.GetPageData("http://reignofdragons.wikia.com/wiki/" + selectName);

            if (imgurl != null && imgurl.TableData != null)
            {
                //pictureBox2.Height = imgurl.TableData[0].ImageHeight;
                try
                {
                    pictureBox2.Load(imgurl.TableData[0].ImageURL);
                    pictureBox3.Load(imgurl.TableData[1].ImageURL);
                    pictureBox4.Load(imgurl.TableData[2].ImageURL);
                    pictureBox5.Load(imgurl.TableData[3].ImageURL);
                    pictureBox6.Load(imgurl.TableData[4].ImageURL);
                }
                catch (Exception)
                {
                }
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void searchbox_cards_TextChanged(object sender, EventArgs e)
        {
            TextBox searchbox = sender as TextBox;
            string searchtext = searchbox.Text;

            typednames.Clear();
            List<Card> cardlist = (newpage.TableData.ToList());
            foreach (Card card in cardlist)
            {
                if (Contains(card.Name, searchtext, StringComparison.OrdinalIgnoreCase))
                {
                    typednames.Add(card.Name);
                }
            }
            UpdateGrid();
        }

        public static bool Contains(string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        private void WarriorPicBox_MouseClick(object sender, MouseEventArgs e)
        {
            classpictureBox.Image = ROD_Deck_Builder.Properties.Resources.warrior_64;
        }

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            classpictureBox.Image = ROD_Deck_Builder.Properties.Resources.Knight_64;
        }
    }
}
