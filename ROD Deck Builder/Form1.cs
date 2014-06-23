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
        List<string> factionSelections = new List<string>(0);
        // List of all of the currently seleced skills
        List<string> skillSelections = new List<string>(0);
        // List of all of the currently seleced realms
        List<string> realmSelections = new List<string>(0);
        // List of all of the currently selected rarities
        List<string> raritySelections = new List<string>(0);

        public Form1()
        {
            Array values;

            InitializeComponent();
            List<Card> cardlist = (newpage.TableData.ToList());

            lbxRealms.DisplayMember = "Realm";
            // Add each of the realm values
            values = System.Enum.GetNames(typeof(ERealm));
            realmSelections=new List<string>(values.Length);
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
            //values = Skill(typeof(Skill));
            skillSelections = new List<string>(values.Length);
            foreach (Skill skill in skills)
            {
                lbxSkills.Items.Add(skill);
                skillSelections.Add(skill);
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
                DataRow drnew = cardTable.NewRow();
                drnew["Rarity"] = currCard.Rarity;
                drnew["Name"] = currCard.Name;
                drnew["Realm"] = currCard.Realm;
                drnew["Faction"] = currCard.Faction;
                drnew["MATK"] = currCard.MaxAtk;
                try { drnew["MDEF"] = currCard.MaxDef; }
                catch { drnew["MDEF"] = "missing"; }
                drnew["Total"] = currCard.Total;
                drnew["Cost"] = currCard.Cost;
                drnew["AttEff"] = currCard.AttEff;
                drnew["DefEff"] = currCard.DefEff;
                drnew["OverallEff"] = currCard.OverallEff;
                drnew["Skill"] = currCard.Skill;
                cardTable.Rows.Add(drnew);
            }

            dataGridView1.DataSource = cardTable;
        }


        private void lbxRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selecteditems = lbxRealms.SelectedItems;
            realmSelections = new List<string>(selecteditems.Count);
            foreach (object selecteditem in selecteditems)
            { 
                realmSelections.Add((string)selecteditem);
            }
            UpdateGrid();
        }


        private void lbxFactions_SelectedIndexChanged(object sender, EventArgs e)
        {
          // Faction
          ListBox.SelectedObjectCollection selecteditems = lbxFactions.SelectedItems;
          factionSelections = new List<string>(selecteditems.Count);
          foreach (object selecteditem in selecteditems)
          {
            factionSelections.Add((string)selecteditem);
          }
          UpdateGrid();
        }


        private void lbxSkills_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Faction
            ListBox.SelectedObjectCollection selecteditems = lbxSkills.SelectedItems;
            skillSelections = new List<string>(selecteditems.Count);
            foreach (object selecteditem in selecteditems)
            {
                skillSelections.Add((string)selecteditem);
            }
            UpdateGrid();
        }




        private void lbxRarity_SelectedIndexChanged(object sender, EventArgs e)
        {
          // Rarity
          ListBox.SelectedObjectCollection selecteditems = lbxRarity.SelectedItems;
          if (selecteditems.Count == 0)
          {
            Array values = System.Enum.GetNames(typeof(ERarity));
            raritySelections = new List<string>(values.Length);
            foreach (string rarity in values)
            {
              raritySelections.Add(rarity);
            }
          }
          else
          {
            raritySelections = new List<string>(selecteditems.Count);
            foreach (object selecteditem in selecteditems)
            {
              raritySelections.Add((string)selecteditem);
            }
          }
          UpdateGrid();
        }

        private void UpdateGrid()
        {
            bool cardPassed;

            // use the three sets of selections to determine if the card can be viewed
            cardTable.Rows.Clear();
            List<Card> cardlist = (newpage.TableData.ToList());
            foreach (Card currCard in cardlist)
            {
              cardPassed = false;
              if (realmSelections.Count != 0)
              {
                foreach (string realm in realmSelections)
                {
                  if (System.Enum.GetName(typeof(ERealm), currCard.Realm).Equals(realm))
                  {
                    cardPassed = true;
                    break;
                  }
                }
                if (!cardPassed) continue;
              }
              if (factionSelections.Count != 0)
              {
                cardPassed = false;
                foreach (string faction in factionSelections)
                {
                  if (System.Enum.GetName(typeof(EFaction), currCard.Faction).Equals(faction))
                  {
                    cardPassed = true;
                    break;
                  }
                }
                if (!cardPassed) continue;
              }


              if (skillSelections.Count != 0)
              {
                  cardPassed = false;
                  foreach (string skill in skillSelections)
                  {
                      if (System.Enum.GetName(typeof(Card), currCard.Skill).Equals(skill))
                      {
                          cardPassed = true;
                          break;
                      }
                  }
                  if (!cardPassed) continue;
              }


              if (raritySelections.Count != 0)
              {
                cardPassed = false;
                foreach (string rarity in raritySelections)
                {
                  if (System.Enum.GetName(typeof(ERarity), currCard.Rarity).Equals(rarity))
                  {
                    cardPassed = true;
                    break;
                  }
                }
                if (!cardPassed) continue;
              }
              DataRow drnew = cardTable.NewRow();
              drnew["Rarity"] = currCard.Rarity;
              drnew["Name"] = currCard.Name;
              drnew["Realm"] = currCard.Realm;
              drnew["Faction"] = currCard.Faction;
              drnew["MATK"] = currCard.MaxAtk;
              try { drnew["MDEF"] = currCard.MaxDef; }
              catch { drnew["MDEF"] = "missing"; }
              drnew["Total"] = currCard.Total;
              drnew["Cost"] = currCard.Cost;
              drnew["AttEff"] = currCard.AttEff;
              drnew["DefEff"] = currCard.DefEff;
              drnew["OverallEff"] = currCard.OverallEff;
              drnew["Skill"] = currCard.Skill;
              cardTable.Rows.Add(drnew);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
