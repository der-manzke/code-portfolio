using Microsoft.AspNetCore.Mvc.RazorPages;
using dnd_helper.Pages.Shared.Components.Models;
using dnd_helper.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;


namespace dnd_helper.Pages.Randomizer
{
    public class PotionsModel : PageModel
    {
        [BindProperty]
        public int amount_potions { get; set; }
        public float common_potion { get; set; }
        [BindProperty]
        public float uncommon_potion { get; set; }
        [BindProperty]
        public float rare_potion { get; set; }
        [BindProperty]
        public float veryRare_potion { get; set; }
        [BindProperty]
        public float legendary_potion { get; set; }

        private float givenPercent = 1;
        [BindProperty]
        public List<Potion> potions { get; set; }

        public void OnGet()
        { 
        }

        public void OnPost() 
        {
            try 
            {
                if (amount_potions == 0)
                    throw new Exception("greater0");

                string filePath = "wwwroot/data/Potions.json";
                potions = JsonConvert.DeserializeObject<List<Potion>>(System.IO.File.ReadAllText(filePath));

                float allPercent = common_potion + uncommon_potion + rare_potion + veryRare_potion + legendary_potion;

                float[] potion_list = [common_potion, uncommon_potion, rare_potion, veryRare_potion, legendary_potion];
                foreach (float potion in potion_list) {
                    if (potion > 0)
                        givenPercent += 1;
                }


                if (allPercent > 100)
                {
                    float overtake = allPercent - 100;
                    float reduceAll = overtake / givenPercent;
                    common_potion -= reduceAll;
                    uncommon_potion -= reduceAll;
                    rare_potion -= reduceAll;
                    veryRare_potion -= reduceAll;
                    legendary_potion -= reduceAll;
                }
                else if (allPercent < 100)
                {
                    float undertake = 100 - allPercent;
                    common_potion += undertake;
                }

                List<Potion> commons = new List<Potion>();
                List<Potion> uncommons = new List<Potion>();
                List<Potion> rares = new List<Potion>();
                List<Potion> veryRares = new List<Potion>();
                List<Potion> legendaries = new List<Potion>();

                if (legendary_potion > 0) 
                {
                    int amount = (int)Math.Ceiling((amount_potions * legendary_potion) / 100);
                    for (int i = 0; i < amount; i++) 
                    {
                        List<Potion> potList = potions.Where(potion => potion.Rarity == "legendary").ToList();
                        int rand = new Random().Next(0, potList.Count);
                        legendaries.Add(potions.Where(p => p.Rarity == "legendary").ToList()[rand]);
                    }

                }
                if (veryRare_potion > 0)
                {
                    int amount = (int)Math.Ceiling((amount_potions * veryRare_potion) / 100);
                    for (int i = 0; i < amount; i++)
                    {
                        List<Potion> potList = potions.Where(potion => potion.Rarity == "very_rare").ToList();
                        int rand = new Random().Next(0, potList.Count);
                        veryRares.Add(potions.Where(p => p.Rarity == "very_rare").ToList()[rand]);
                    }

                }
                if (rare_potion > 0)
                {
                    int amount = (int)Math.Ceiling((amount_potions * rare_potion) / 100);
                    for (int i = 0; i < amount; i++)
                    {
                        List<Potion> potList = potions.Where(potion => potion.Rarity == "rare").ToList();
                        int rand = new Random().Next(0, potList.Count);
                        rares.Add(potions.Where(p => p.Rarity == "rare").ToList()[rand]);
                    }

                }
                if (uncommon_potion > 0)
                {
                    int amount = (int)Math.Ceiling((amount_potions * uncommon_potion) / 100);
                    for (int i = 0; i < amount; i++)
                    {
                        List<Potion> potList = potions.Where(potion => potion.Rarity == "uncommon").ToList();
                        int rand = new Random().Next(0, potList.Count);
                        uncommons.Add(potions.Where(p => p.Rarity == "uncommon").ToList()[rand]);
                    }

                }

                int cAmount = amount_potions - legendaries.Count - veryRares.Count - rares.Count - uncommons.Count;
                for (int i = 0; i < cAmount; i++)
                {
                    List<Potion> potList = potions.Where(potion => potion.Rarity == "common").ToList();
                    int rand = new Random().Next(0, potList.Count);
                    commons.Add(potions.Where(p => p.Rarity == "common").ToList()[rand]);
                }

                ViewData["DiscordText"] = LoadOutput(commons, uncommons, rares, veryRares, legendaries);

                ViewData["alert"] = null;
            } 
            catch (Exception ex) 
            {
                switch (ex.Message) 
                {
                    case "no_amount":
                        ViewData["alert"] = new Alert() { Type = "danger", Content = "You have entered no amount." };
                        break;
                    case "greater0":
                        ViewData["alert"] = new Alert() { Type = "warning", Content = "The amount must be greater than 0." };
                        break;
                    default:
                        ViewData["alert"] = new Alert() { Type = "danger", Content = ex.Message };
                        break;
                }
            }
        }

        private string LoadOutput(List<Potion> commons, List<Potion> uncommons, List<Potion> rares, List<Potion> veryRares, List<Potion> legendaries) 
        {
            string output = "__**Potions**__\r";
            var cList = commons.GroupBy(p => p.Name).ToList();
            var ucList = uncommons.GroupBy(p => p.Name).ToList();
            var rList = rares.GroupBy(p => p.Name).ToList();
            var vrList = veryRares.GroupBy(p => p.Name).ToList();
            var lList = legendaries.GroupBy(p => p.Name).ToList();

            if (cList.Count != 0)
                output += $"__Common ({commons.Count})__\r";
            foreach ( var c in cList )
            {
                output += string.Format("{0}x {1} ({2}GP)\r", c.Count(), c.First().Name, c.First().Value);
            }

            if (ucList.Count != 0)
                output += $"__Uncommon ({uncommons.Count})__\r";
            foreach (var uc in ucList)
            {
                output += string.Format("{0}x {1} ({2}GP)\r", uc.Count(), uc.First().Name, uc.First().Value);
            }

            if (rList.Count != 0)
                output += $"__Rare ({rares.Count})__\r";
            foreach (var r in rList)
            {
                output += string.Format("{0}x {1} ({2}GP)\r", r.Count(), r.First().Name, r.First().Value);
            }

            if (vrList.Count != 0)
                output += $"__Very Rare ({veryRares.Count})__\r";
            foreach (var vr in vrList)
            {
                output += string.Format("{0}x {1} ({2}GP)\r", vr.Count(), vr.First().Name, vr.First().Value);
            }

            if (lList.Count != 0)
                output += $"__Legendary ({legendaries.Count})__\r";
            foreach (var l in lList)
            {
                output += string.Format("{0}x {1} ({2}GP)\r", l.Count(), l.First().Name, l.First().Value);
            }

            return output.TrimEnd();
        }
    }
}