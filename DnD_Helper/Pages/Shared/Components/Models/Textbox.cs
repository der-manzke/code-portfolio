namespace dnd_helper.Pages.Shared.Components.Models
{
    public class TextBox
    {
        public required string ID;
        public string Title = string.Empty;
        public string Text = string.Empty;
        public string[] Classes = [];
        public int Rows = 0;
        public bool IsDisabled = false;
    }
}
