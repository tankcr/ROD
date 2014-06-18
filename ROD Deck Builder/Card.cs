using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROD_Deck_Builder
{
    public enum ERarity
    {
        None,
        Common,
        Uncommon,
        Rare,
        SuperRare,
        MegaRare,
        UltraRare,
        LegendaryRare
    }

    public enum ERealm
    {
        None,
        Chaos,
        Genesis,
        Justice,
    }

    public enum EFaction
    {
        None,
        Melee,
        Magic,
        Charm
    }
    
    public class Card
    {
        public ERarity Rarity;
        public string Name;
        public ERealm Realm;
        public EFaction Faction;
        public int MaxAtk;
        public int MaxDef;
        public int Total;
        public int Cost;
        public int AttEff;
        public int DefEff;
        public int OverallEff;
        public string Skill;
        public string EventSkl1;
        public string EventSkl2;
    }
}
