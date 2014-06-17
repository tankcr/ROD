using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace ROD_Deck_Builder
{
//    public class GetTable
//    {
//        public static Cards GetTableData(string tablepath)
//        {
//            Cards table = new Cards();
//            using (StreamReader streamReader = new StreamReader(tablepath)) 
                //("Http://");
//            { 
//                string singleLine;
//                while ((singleLine = streamReader.ReadLine()) != null)
//                { 
//                    string[] splitApart = singleLine.Split(',');
//                        if (splitApart[0] != "RY ")
//                        {
//                            Card item = new Card();
//                            splitApart[0] = splitApart[0].Replace("?","");
//                            item.Rarity = Convert.ToString(splitApart[0]);
//                            item.Name = splitApart[1];
//                            item.Realm = splitApart[2];
//                            item.Faction = splitApart[3];
//                            item.MaxAtk = splitApart[4];
//                            item.MaxDef = splitApart[5];
//                            item.Total = Convert.ToInt32(splitApart[6]);
//                            item.Cost = Convert.ToInt32(splitApart[7]);
//                            item.AttEff = Convert.ToInt32(splitApart[8]);
//                            item.DefEff = Convert.ToInt32(splitApart[9]);
//                            item.OverallEff = Convert.ToInt32(splitApart[10]);
//                            item.Skill = splitApart[11];
//                            item.EventSkl1 = splitApart[12];
//                            item.EventSkl2 = splitApart[13];
//                            table.TableData.Add(item);
//                    }

                }

//            }
//            var reader = new StreamReader(File.OpenRead(@tablepath));
//            while (!reader.EndOfStream)
//            {
//                var line = reader.ReadLine();
//                var values = line.Split(';');
                
//            }
//        }

//    }
//}
