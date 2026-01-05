using dnd_helper.Pages.Shared.Components.Models;
using dnd_helper.Helper;
using dnd_helper.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace dnd_helper.Pages
{
    public class _5eLootConverterModel : PageModel
    {
        readonly _5eLootConverterHelper _5ELootConverterHelper = new();

        [BindProperty]
        public IFormFile? Upload { get; set; }


        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (System.IO.Path.GetExtension(Upload.FileName) != ".json")
            {
                ViewData["alert"] = new Alert() { Type = "danger", Content = "The given file is not in <b>json</b> format." };
            }

            try
            {
                ViewData["DiscordText"] = _5ELootConverterHelper.GenerateOutput(Upload);
            }
            catch (Exception e)
            {
                ViewData["alert"] = new Alert() { Type = "danger", Content = "Something went wrong. Erro Stack: " + e.ToString() };
            }


        }

    }
}
