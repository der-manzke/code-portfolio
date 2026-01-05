using System.Runtime.InteropServices;

namespace dnd_helper.Pages.Shared.Components.Models
{
    public class Button
    {
        public required string ButtonType;
        public string[] Classes = [];
        public string Type = "button";
        public string Value = string.Empty;
        public string Link = string.Empty;
        public bool IsDisabled = false;
        public string OnClick = string.Empty;
    }
}
