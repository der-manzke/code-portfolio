namespace dnd_helper.Data
{
    public class Potion
    {
        public string Name;
        public string Rarity;
        public int Value;

        public Potion(string name, string rarity, int value) {
            this.Name = name;
            this.Rarity = rarity;
            this.Value = value;
        }
    }
}
