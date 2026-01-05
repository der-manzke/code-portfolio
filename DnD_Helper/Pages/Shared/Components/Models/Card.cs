using System.Runtime.InteropServices;

namespace dnd_helper.Pages.Shared.Components.Models
{
    public class Card
    {
        public CardClasses? Classes = null;
        public Image? CardImage = null;
        public Body? CardBody = null;

        public partial class CardClasses 
        {
            public string[] Classes = [];
            public string[] ImageClasses = [];
            public string[] BodyClasses = [];
            public string[] TitleClasses = [];
            public string[] TextClasses = [];
            public string[] LinkClasses = [];
        }

        public partial class Image 
        {
            public string ImagePath = string.Empty;
            public string ImageName = string.Empty;
        }

        public partial class Body 
        {
            public string Title = string.Empty;
            public string Text = string.Empty;
            public string Link = string.Empty;
            public string LinkUrl = string.Empty;
        }
    }
}
