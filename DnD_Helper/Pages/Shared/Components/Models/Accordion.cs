namespace dnd_helper.Pages.Shared.Components.Models
{
    public class Accordion
    {
        public required string ID;
        public string[] Classes = [];
        public List<Element> Elements = [];

        public partial class Element { 
            public required string ID;
            public string Title = "";
            public object Content = new();

            public bool IsExpanded = false;
        }
    }
}
