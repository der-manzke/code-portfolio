using dnd_helper.Schema;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using static dnd_helper.Schema.LootJson;

namespace dnd_helper.Helper
{
    public class _5eLootConverterHelper
    {
        public string GenerateOutput(IFormFile upload) {
            JsonSerializer serializer = new JsonSerializer();
            LootJson lootJson = new LootJson();

            // Read uploaded json
            using (Stream fileContent = upload.OpenReadStream())
            using (StreamReader sr = new StreamReader(fileContent))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                while (reader.Read())
                {
                    lootJson = serializer.Deserialize<LootJson>(reader);
                }
            }

            return LoadOutput(lootJson);
        }

        private string LoadOutput(LootJson lootJson)
        {
            string currencies = LoadCurrenciesString(lootJson.currency) ?? string.Empty;
            string items = LoadItemsString(lootJson.entityInfos) ?? string.Empty;

            return string.Format("__**Currencies:**__\r{0}\n\n__**Items:**__\n{1}", currencies, items);
        }

        public string LoadCurrenciesString(Currency currency)
        {   
            return string.Format("PP: {0} \nGP: {1} \nSP: {2} \nCP: {3}", currency.pp, currency.gp, currency.sp, currency.cp);
        }

        public string LoadItemsString(EntityInfo[] entityInfo) 
        {
            ItemTypes items = OrderItems(entityInfo);

            string output = string.Empty;

            // Treasure Gems
            if (items.TreasureList?.Gem?.Any() ?? false) 
            {
                output += string.Format("**Gems:** \n{0}\r\n\n", string.Join("\n", items.TreasureList?.Gem?.ToArray()));
            }

            // Treasure
            if (items.TreasureList?.Treasure?.Any() ?? false)
            {
                output += string.Format("**Treasure:** \n{0}\r\n\n", string.Join("\n", items.TreasureList?.Treasure?.ToArray()));
            }

            // Potions
            if (items.Potions?.Any() ?? false)
            {
                output += string.Format("**Potions:** \n{0}\r\n\n", string.Join("\n", items.Potions?.ToArray()));
            }

            // Scrolls
            if (items.Scrolls?.Any() ?? false)
            {
                output += string.Format("**Scrolls:** \n{0}\r\n\n", string.Join("\n", items.Scrolls?.ToArray()));
            }

            // Spell Scrolls
            if (items.SpellScrolls?.Any() ?? false)
            {
                output += string.Format("**Spell Scrolls:** \n{0}\r\n\n", string.Join("\n", items.SpellScrolls?.ToArray()));
            }

            // Armor
            if ((items.ArmorList?.Heavy?.Any() ?? false) || (items.ArmorList?.Medium?.Any() ?? false) || (items.ArmorList?.Light?.Any() ?? false) || (items.ArmorList?.Shields?.Any() ?? false) || (items.ArmorList?.Unsorted?.Any() ?? false))
            {
                output += "**Armor**:\n";
                if (items.ArmorList?.Heavy?.Any() ?? false)
                {
                    output += $"{string.Join("\n", items.ArmorList?.Heavy?.ToArray())} \n";
                }
                if (items.ArmorList?.Medium?.Any() ?? false)
                {
                    output += $"{string.Join("\n", items.ArmorList?.Medium?.ToArray())} \n";
                }
                if (items.ArmorList?.Light?.Any() ?? false)
                {
                    output += $"{string.Join("\n", items.ArmorList?.Light?.ToArray())} \n";
                }
                if (items.ArmorList?.Shields?.Any() ?? false)
                {
                    output += $"{string.Join("\n", items.ArmorList?.Shields?.ToArray())} \n";
                }
                if (items.ArmorList?.Unsorted?.Any() ?? false)
                {
                    output += $"{string.Join("\n", items.ArmorList?.Unsorted?.ToArray())} \n";
                }

                output += "\r\n";
            }

            // Weapons
            if ((items.WeaponList?.Melee?.Any() ?? false) || (items.WeaponList?.Range?.Any() ?? false) || (items.WeaponList?.Rod?.Any() ?? false) || (items.WeaponList?.Ammunition?.Any() ?? false) || (items.WeaponList?.Unsorted?.Any() ?? false))
            {
                output += "**Weapons**:\n";
                if (items.WeaponList?.Melee?.Any() ?? false)
                {
                    output += $"{string.Join("\n", items.WeaponList?.Melee?.ToArray())} \n";
                }
                if (items.WeaponList?.Range?.Any() ?? false)
                {
                    output += $"{string.Join("\n", items.WeaponList?.Range?.ToArray())} \n";
                }
                if (items.WeaponList?.Rod?.Any() ?? false)
                {
                    output += $"{string.Join("\n", items.WeaponList?.Rod?.ToArray())} \n";
                }
                if (items.WeaponList?.Ammunition?.Any() ?? false)
                {
                    output += $"{string.Join("\n", items.WeaponList?.Ammunition?.ToArray())} \n";
                }
                if (items.WeaponList?.Unsorted?.Any() ?? false)
                {
                    output += $"{string.Join("\n", items.WeaponList?.Unsorted?.ToArray())} \n";
                }
                output += "\r\n";
            }

            // Magic Items
            if ((items.MagicItemList?.Rings?.Any() ?? false) || (items.MagicItemList?.Wands?.Any() ?? false) || (items.MagicItemList?.Staffs?.Any() ?? false))
            {
                output += "**Magic Items**:\n";
                if (items.MagicItemList?.Rings?.Any() ?? false)
                {
                    output += "__Rings__\n";
                    output += $"{string.Join("\n", items.MagicItemList?.Rings?.ToArray())} \n";
                }
                if (items.MagicItemList?.Wands?.Any() ?? false)
                {
                    output += "__Wands__\n";
                    output += $"{string.Join("\n", items.MagicItemList?.Wands?.ToArray())} \n";
                }
                if (items.MagicItemList?.Staffs?.Any() ?? false)
                {
                    output += "__Staffs__\n";
                    output += $"{string.Join("\n", items.MagicItemList?.Staffs?.ToArray())} \n";
                }
                output += "\r\n";
            }

            // Wonderous Items
            if (items.WonderousItems?.Any() ?? false)
            {
                output += string.Format("**Wonderous Items:** \n{0}\r\n\n", string.Join("\n", items.WonderousItems?.ToArray()));
            }

            // Instruments
            if (items.Insturments?.Any() ?? false)
            {
                output += string.Format("**Instruments:** \n{0}\r\n\n", string.Join("\n", items.Insturments?.ToArray()));
            }

            // Miscellaneous
            if (items.Miscellaneous?.Any() ?? false)
            {
                output += string.Format("**Other Items:** \n{0}\r\n\n", string.Join("\n", items.Miscellaneous?.ToArray()));
            }

            return output.TrimEnd();
        }

        private ItemTypes OrderItems(EntityInfo[] entityInfo) {
            ItemTypes itemTypes = new();

            foreach (EntityInfo entity in entityInfo)
            {
                if (entity.page == "items.html")
                {
                    string type = entity.entity?.type ?? entity.entity?._typeHtml ?? string.Empty;
                    switch (type)
                    {
                        // Potions
                        case "potion":
                        case "P":
                            itemTypes.Potions?.Add(string.Format("{0} ({1}GP)", entity.entity?.name, (entity.entity?.value / 100)));
                            break;
                        // Scrolls
                        case "SC":
                        case "scroll":
                            itemTypes.Scrolls?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        // Treasures
                        case "$G":
                            itemTypes.TreasureList.Gem.Add(string.Format("{0}x {1} ({2}GP)", entity.options?.quantity, entity.entity?.name, (entity.entity?.value / 100)));
                            break;
                        case "$A":
                            itemTypes.TreasureList?.Treasure?.Add(string.Format("{0}x {1} ({2}GP)", entity.options?.quantity, entity.entity?.name, (entity.entity?.value / 100)));
                            break;
                        case "treasure":
                            itemTypes.TreasureList?.Treasure?.Add(string.Format("{0}x {1} ({2}GP)", entity.options?.quantity, entity.entity?.name, (entity.entity?.value / 100)));
                            break;
                        // Weapons
                        case "M":
                            itemTypes.WeaponList?.Melee?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        case "R":
                            itemTypes.WeaponList?.Range?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        case "RD":
                        case "rod":
                            itemTypes.WeaponList?.Rod?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        case "A":
                        case "AF":
                        case "ammunition":
                            itemTypes.WeaponList?.Ammunition?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        case "weapon":
                            itemTypes.WeaponList?.Unsorted?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        // Armor
                        case "HA":
                        case "heavy armor":
                            itemTypes.ArmorList?.Heavy?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        case "MA":
                        case "medium armor":
                            itemTypes.ArmorList?.Medium?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        case "LA":
                        case "light armor":
                            itemTypes.ArmorList?.Light?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        case "S":
                            itemTypes.ArmorList?.Shields?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        case "armor":
                            itemTypes.ArmorList?.Unsorted?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        // Magic Items
                        case var val when new Regex(@"staff.").IsMatch(val):
                            itemTypes.MagicItemList?.Staffs?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        case "RG":
                            itemTypes.MagicItemList?.Rings?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        case "WD":
                        case "wand":
                            itemTypes.MagicItemList?.Wands?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        // Wonderous Items
                        case var val when new Regex(@"wonderous item.").IsMatch(val):
                            itemTypes.WonderousItems?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        // Instruments
                        case "INS":
                            itemTypes.Insturments?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                        // Miscellaneous
                        case "OTH":
                            itemTypes.Miscellaneous?.Add(string.Format("{0}", entity.entity?.name));
                            break;
                    }
                }
                else if (entity.page == "spells.html")
                {
                    string? level;
                    switch (entity.entity?.level)
                    {
                        case 1:
                            level = "1st Level";
                            break;
                        case 2:
                            level = "2nd Level";
                            break;
                        case 3:
                            level = "3rd Level";
                            break;
                        default:
                            level = $"{entity.entity?.level}th";
                            break;
                    }
                    itemTypes.SpellScrolls?.Add(string.Format("{0} Spell Scroll: {1}", level, entity.entity?.name));
                }
            }

            return itemTypes;
        }



        private partial class ItemTypes 
        {
            public ItemTypes() {
                Potions = [];
                Scrolls = [];
                SpellScrolls = [];
                TreasureList = new Treasures();
                WeaponList = new Weapons();
                ArmorList = new Armor();
                MagicItemList = new MagicItems();
                WonderousItems = [];
                Insturments = [];
                Miscellaneous = [];
            }

            public List<string> Potions { get; set; }
            public List<string> Scrolls { get; set; }
            public List<string> SpellScrolls { get; set; }
            public Treasures TreasureList { get; set; }
            public Weapons WeaponList { get; set; }
            public Armor ArmorList { get; set; }
            public MagicItems MagicItemList { get; set; }
            public List<string> WonderousItems { get; set; }
            public List<string> Insturments { get; set; }
            public List<string> Miscellaneous {  get; set; }

            public partial class Treasures {
                public Treasures() 
                {
                    Gem = [];
                    Treasure = [];
                }

                public List<string> Gem { get; set; }
                public List<string> Treasure {  get; set; }
            }

            public partial class Weapons {
                public Weapons() 
                {
                    Melee = [];
                    Range = [];
                    Rod = [];
                    Ammunition = [];
                    Unsorted = [];
                }

                public List<string> Melee {  get; set; }
                public List<string> Range { get; set; }
                public List<string> Rod {  get; set; }
                public List<string> Ammunition { get; set; }
                public List<string> Unsorted { get; set; }
            }

            public partial class Armor 
            {
                public Armor()
                {
                    Heavy = [];
                    Medium = [];
                    Light = [];
                    Shields = [];
                    Unsorted = [];
                }

                public List<string> Heavy { get; set; }
                public List<string> Medium { get; set; }
                public List<string> Light { get; set; }
                public List<string> Shields { get; set; }
                public List<string> Unsorted { get; set; }
            }

            public partial class MagicItems 
            {
                public MagicItems() 
                {
                    Rings = [];
                    Wands = [];
                    Staffs = [];
                }

                public List<string> Rings { get; set; }
                public List<string> Wands { get; set; }
                public List<string> Staffs { get; set; }
            }
        }
    }
}
