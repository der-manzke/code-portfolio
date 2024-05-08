using Filter;
using Helper;
using Infrastructure;
using Interface;
using Models.Device;
using Models.Navigation;
using Models.Custom;
using Models.Program;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using NuGet.Packaging.Signing;
using System.Linq;
using static Models.Custom.CustomViewModel;

namespace CodePortfolio.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="userSession"></param>
        /// <param name="translator"></param>
        /// <param name="db"></param>
        public CustomController(IConfiguration? configuration, ILogger<BaseController>? logger, ITranslator? translator, IDb? db)
        {
            _configuration = configuration;
            _logger = logger;
            _translator = translator;
            _db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [SessionCheckFilter]
        public IActionResult Overview()
        {
            adjustDataForTable(out List<CustomViewModel> Customs);

            ViewData["DataJson"] = System.Text.Json.JsonSerializer.Serialize(Customs);

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RequestFormLimits(ValueCountLimit = int.MaxValue)]
        public IActionResult LoadOverview()
        {
            adjustDataForTable(out List<CustomViewModel> Customs);

            return DataTableHelper.Filter(Request, Customs, new List<string>() { "refcode", "title", "container1Name", "container2Name", "activeLabel" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [SessionCheckFilter]
        [HttpGet]
        public IActionResult Add()
        {
            List<ContainerViewModel> container = ContainerViewModel.GetContainers(_db, new List<string>() { "container1", "container2" });

            ViewData["ModalContent"] = "";

            ViewData["Container"] = container;

            ViewData["altContainer"] = container.Where(x => x.alternativeContainer != null && x.alternativeContainer != "").ToList();

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newCustom"></param>
        /// <returns></returns>
        [SessionCheckFilter]
        [HttpPost]
        public async Task<IActionResult> Add(CustomViewModel newCustom)
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;
            IFormFile imageUserFile;
            string imageUser = string.Empty;
            IFormFile imageGuestFile;
            string imageGuest = string.Empty;

            StringValues imageGuestBase64;

            if (HttpContext.Request.Form.ContainsKey("imageGuestBase64"))
            {
                HttpContext.Request.Form.TryGetValue("imageGuestBase64", out imageGuestBase64);
            }


            try
            {
                string filePath = "file_upload/" + UserSession.Current(HttpContext, _configuration).GetCustomerReference() + "/Custom/";
                string basePath = "wwwroot";

                if (files.Where(f => f.Name == "imageUserFile").Count() >= 1)
                {
                    imageUserFile = HttpContext.Request.Form.Files.First(f => f.Name == "imageUserFile");

                    newCustom.imageUser = await WriteFile(newCustom.imageUser, basePath, filePath, imageUserFile);
                }

                if (files.Where(f => f.Name == "imageGuestFile").Count() >= 1)
                {
                    imageGuestFile = HttpContext.Request.Form.Files.First(f => f.Name == "imageGuestFile");

                    newCustom.imageGuest = await WriteFile(newCustom.imageGuest, basePath, filePath, imageGuestFile);
                }

                if (imageGuestBase64.First().Length != 0)
                {
                    string filename = string.Empty;
                    do
                    {
                        filename = FileHelper.RandomString(60) + ".jpg";
                    } while (System.IO.File.Exists(basePath + "/" + filePath + filename));
                    FileHelper.DecodeBase64ToFile(imageGuestBase64.First(), basePath + "/" + filePath, filename);

                    newCustom.imageGuest = "/" + filePath + filename;
                }

                List<ContainerViewModel> container = ContainerViewModel.GetContainers(_db, new List<string>() { "container1", "container2" });

                if (newCustom.container2Object.containerType == 0 || newCustom.container2Object.containerType == null)
                {
                    newCustom.container2 = null;
                }
                else if (container.Where(x => x.id == newCustom.container1Object.containerType).First().alternativeContainer == null)
                {
                    newCustom.container2 = null;
                }
                else if (newCustom.container2 == null)
                {
                    newCustom.container2 = 0;
                }

                ViewData["Container"] = container;

                ViewData["altContainer"] = container.Where(x => x.alternativeContainer != null && x.alternativeContainer != "").ToList();

                TempData["SuccessMessage"] = T("success_add");

                CustomViewModel dbCustom = UpdateCustom(_db, GetSession().GetCustomerReference(), newCustom);

                return RedirectToAction("Edit", "Custom", routeValues: new { ID = dbCustom.id });
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = T("error_add");
                Log.AddLog4NetEntry("error", T("error_add") + " " + e.ToString());

                return RedirectToAction("Overview");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [SessionCheckFilter]
        [HttpGet]
        public IActionResult Edit(int ID)
        {
            try
            {
                CustomViewModel dbCustom = GetCustoms(_db, UserSession.Current(HttpContext, _configuration).GetCustomerReference(), ID).First();

                List<ContainerViewModel> container = ContainerViewModel.GetContainers(_db, new List<string>() { "container1", "container2" });

                if (dbCustom.imageGuestOverride == null)
                {
                    ViewData["ModalContent"] = "";
                }
                else 
                {
                    ViewData["ModalContent"] = dbCustom.imageGuestOverride;
                }

                ViewData["Container"] = container;

                ViewData["altContainer"] = container.Where(x => x.alternativeContainer != null && x.alternativeContainer != "").ToList();

                return View(dbCustom);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = T("Custom_cant_load");
                Log.AddLog4NetEntry("error", T("Custom_cant_load") + " " + e.ToString());

                return RedirectToAction("Overview");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editCustom"></param>
        /// <returns></returns>
        [SessionCheckFilter]
        [HttpPost]
        public async Task<IActionResult> Edit(CustomViewModel editCustom)
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;
            IFormFile imageUserFile;
            string imageUser = string.Empty;
            IFormFile imageGuestFile;
            string imageGuest = string.Empty;

            StringValues imageGuestBase64;

            if (HttpContext.Request.Form.ContainsKey("imageGuestBase64"))
            {
                HttpContext.Request.Form.TryGetValue("imageGuestBase64", out imageGuestBase64);
            }

            try
            {
                string filePath = "file_upload/" + UserSession.Current(HttpContext, _configuration).GetCustomerReference() + "/Custom/";
                string basePath = "wwwroot";

                if (files.Where(f => f.Name == "imageUserFile").Count() >= 1)
                {
                    imageUserFile = HttpContext.Request.Form.Files.First(f => f.Name == "imageUserFile");

                    editCustom.imageUser = await WriteFile(editCustom.imageUser, basePath, filePath, imageUserFile);
                }

                if (files.Where(f => f.Name == "imageGuestFile").Count() >= 1)
                {
                    imageGuestFile = HttpContext.Request.Form.Files.First(f => f.Name == "imageGuestFile");

                    editCustom.imageGuest = await WriteFile(editCustom.imageGuest, basePath, filePath, imageGuestFile);
                }

                if (imageGuestBase64.First().Length != 0)
                {
                    string filename = string.Empty;
                    if (System.IO.File.Exists(basePath + editCustom.imageGuest))
                    {
                        System.IO.File.Delete(basePath + "/" + editCustom.imageGuest);
                    }
                    do
                    {
                        filename = FileHelper.RandomString(60) + ".jpg";
                    } while (System.IO.File.Exists(basePath + "/" + filePath + filename));
                    FileHelper.DecodeBase64ToFile(imageGuestBase64.First(), basePath + "/" + filePath, filename);

                    editCustom.imageGuest = "/" + filePath + filename;
                }

                List<ContainerViewModel> container = ContainerViewModel.GetContainers(_db, new List<string>() { "culinarioTouch", "culinarioTouchMaster" });

                ViewData["Container"] = container;

                ViewData["altContainer"] = container.Where(x => x.alternativeContainer != null && x.alternativeContainer != "").ToList();

                if (editCustom.container2Object.containerType == 0 || editCustom.container2Object.containerType == null)
                {
                    editCustom.container2 = null;
                }
                else if (container.Where(x => x.id == editCustom.container1Object.containerType).First().alternativeContainer == null)
                {
                    editCustom.container2 = null;
                }
                else if (editCustom.container2 == null)
                {
                    editCustom.container2 = 0;
                }

                if (editCustom.active == false)
                {
                    List<ProgramViewModel> programs = UsedInPrograms(_db, editCustom.id);
                    if (programs.Count > 0)
                    {
                        List<string> programList = new List<string>();
                        programs.ForEach(x => programList.Add(string.Format("{0:D3}", x.number) + "-" + x.title));
                        TempData["WarningMessage"] = T("Custom_cant_deactivate") + String.Join(", ", programList);

                        return RedirectToAction("Edit", new { ID = editCustom.id });
                    }
                }

                CustomViewModel dbCustom = UpdateCustom(_db, UserSession.Current(HttpContext, _configuration).GetCustomerReference(), editCustom, true);

                TempData["SuccessMessage"] = T("success_edit");

                return RedirectToAction("Edit", new { ID = editCustom.id });
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = T("error_edit");
                Log.AddLog4NetEntry("error", T("error_edit") + " " + e.ToString());

                return RedirectToAction("Overview");
            }
        }

        private async Task<string> WriteFile(string image, string basePath, string filePath, IFormFile file)
        {
            string filename;
            if (System.IO.File.Exists(basePath + "/" + image))
            {
                System.IO.File.Delete(basePath + "/" + image);
            }
            string extension = Path.GetExtension(file.FileName);
            do
            {
                filename = FileHelper.RandomString(60) + extension;
            } while (System.IO.File.Exists(basePath + "/" + filePath + filename));

            await FileHelper.WriteFileToPath(basePath + "/" + filePath + filename, file);

            return "/" + filePath + filename;
        }

        [SessionCheckFilter]
        public IActionResult Copy(int ID)
        {
            try
            {
                CustomViewModel dbCustom = GetCustoms(_db, UserSession.Current(HttpContext, _configuration).GetCustomerReference(), ID).First();

                dbCustom.id = 0;
                dbCustom.title += "_COPY";
                dbCustom.active = false;

                string filePath = "file_upload/" + UserSession.Current(HttpContext, _configuration).GetCustomerReference() + "/Custom/";
                string base64Path = "wwwroot/" + filePath;
                if (dbCustom.imageUser != "" && dbCustom.imageUser != string.Empty)
                {
                    string userFileBase64 = FileHelper.EncodeFileToBase64("wwwroot/" + dbCustom.imageUser);
                    string newUserFileName;
                    do
                    {
                        newUserFileName = FileHelper.RandomString(60) + Path.GetExtension(dbCustom.imageUser);
                    }
                    while (System.IO.File.Exists(base64Path + newUserFileName));

                    FileHelper.DecodeBase64ToFile(userFileBase64, base64Path, newUserFileName);
                }
                if (dbCustom.imageGuest != "" && dbCustom.imageGuest != string.Empty)
                {
                    string guestFileBase64 = FileHelper.EncodeFileToBase64("wwwroot/" + dbCustom.imageGuest);
                    string newGuestFileName;
                    do
                    {
                        newGuestFileName = FileHelper.RandomString(60) + Path.GetExtension(dbCustom.imageGuest);
                    }
                    while (System.IO.File.Exists(filePath + newGuestFileName));

                    FileHelper.DecodeBase64ToFile(guestFileBase64, filePath, newGuestFileName);
                }

                dbCustom.container1 = 0;
                if (dbCustom.container2 != null)
                {
                    dbCustom.container2 = 0;
                }

                UpdateCustom(_db, UserSession.Current(HttpContext, _configuration).GetCustomerReference(), dbCustom);

                TempData["SuccessMessage"] = T("success_copy");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = T("error_copy");
                Log.AddLog4NetEntry("error", T("error_copy") + " " + e.ToString());
            }


            return RedirectToAction("Overview");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [SessionCheckFilter]
        [HttpGet]
        public IActionResult Delete(int ID)
        {
            try
            {
                List<ProgramViewModel> programs = UsedInPrograms(_db, ID);
                if (programs.Count > 0)
                {
                    List<string> programList = new List<string>();
                    programs.ForEach(x => programList.Add(string.Format("{0:D3}", x.number) + "-" + x.title));
                    TempData["WarningMessage"] = T("Custom_cant_delete") + String.Join(", ", programList);

                    return RedirectToAction("Overview");
                }

                CustomViewModel deletedCustom = CustomViewModel.GetCustoms(_db, UserSession.Current(HttpContext, _configuration).GetCustomerReference(), ID).First();

                DeleteCustom(_db, UserSession.Current(HttpContext, _configuration).GetCustomerReference(), deletedCustom.id);

                TempData["SuccessMessage"] = T("success_delete");

                return RedirectToAction("Overview");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = T("error_delete");
                Log.AddLog4NetEntry("error", T("error_delete") + " " + e.ToString());

                return RedirectToAction("Overview");
            }
        }

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Customs"></param>
        /// <returns></returns>
        private List<CustomViewModel> adjustDataForTable(out List<CustomViewModel> Customs)
        {
            Customs = GetCustoms(_db, GetSession().GetCustomerReference());

            List<ContainerViewModel> containerList = ContainerViewModel.GetContainers(_db, new List<string>() { "culinarioTouch", "culinarioTouchMaster" });
            List<CustomContainer> CustomContainers = GetCustomContainer(_db);

            foreach (CustomViewModel Custom in Customs)
            {
                try
                {
                    Custom.container1Name = T(containerList.First(x => x.id == CustomContainers.First(y => y.id == Custom.container1).containerType).titleKey);
                }
                catch (Exception e)
                {
                    Custom.container1Name = "---";
                }

                if (Custom.container2 != null)
                {
                    try
                    {
                        Custom.container2Name = T(containerList.First(x => x.id == CustomContainers.First(y => y.id == Custom.container2).containerType).titleKey);
                    }
                    catch (Exception e)
                    {
                        Custom.container1Name = "---";
                    }
                }

                switch (Custom.active)
                {
                    case false:
                        Custom.activeLabel = T("controls_no");
                        break;
                    case true:
                        Custom.activeLabel = T("controls_yes");
                        break;
                }
            }

            return Customs;
        }
        #endregion
    }
}
