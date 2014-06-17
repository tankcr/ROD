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
        Rare1,
        Rare2,
        Rare3,
        Rare4,
        Rare5,
        Rare6,
        Rare7
    }

    public enum ERealm
    {
        None
        // TODO: Fill this in
    }

    public enum EFaction
    {
        None
        // TODO: Fill this in
    }

    public enum ESkill
    {
        None
        // TODO: Fill this in
    }

    public enum EEventSkill
    {
        None
        // TODO: Fill this in
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
        public ESkill Skill;
        public EEventSkill EventSkl1;
        public EEventSkill EventSkl2;
    }
}
