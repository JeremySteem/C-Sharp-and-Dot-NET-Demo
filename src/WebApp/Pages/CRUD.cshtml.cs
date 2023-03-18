using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using StarTEDSystem.BLL;       
using StarTEDSystem.Entities;  
using WebApp.Helpers;           
using Microsoft.IdentityModel.Tokens;
#endregion

namespace WebApp.Pages
{
    public class CRUDModel : PageModel
    {
        #region Private service fields & class constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly ProgramServices _programServices;
        private readonly SchoolServices _schoolServices;

        public CRUDModel(ILogger<IndexModel> logger,
            ProgramServices programservices,
            SchoolServices schoolservices)
        {
            _logger = logger;
            _programServices = programservices;
            _schoolServices = schoolservices;
        }
        #endregion

        #region Feedback and Error Messages

        [TempData]
        public string Feedback { get; set; }

        public bool HasFeedback => !string.IsNullOrWhiteSpace(Feedback);

        public string ErrorMessage { get; set; }

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

        #endregion


        [BindProperty(SupportsGet = true)]
        public int? programid { get; set; }

        [BindProperty]
        public StarTEDSystem.Entities.Program ProgramInfo { get; set; }

        [BindProperty]
        public List<School> SchoolList { get; set; } = new();

        public void OnGet()
        {
            PopulateLists();

            if (programid.HasValue && programid > 0)
            {
                ProgramInfo = _programServices.Program_by_ID(programid.Value);
            }
            else
            {
                Feedback = "Program ID empty right now.";
            }
        }

        public void PopulateLists()
        {
            SchoolList = _schoolServices.School_List();
        }

        public IActionResult OnPostClear()
        {
            Feedback = "";
            ModelState.Clear();
            return RedirectToPage(new { programid = (int?)null });
        }

        public IActionResult OnPostSearch()
        {
            return RedirectToPage("/CRUD");
        }

        public IActionResult OnPostNew()
        {
          //  if (ModelState.IsValid)
   //         {
                try
                {
                    int newProgramID = _programServices.Program_AddProgram(ProgramInfo);
                    Feedback = $"Program id ({newProgramID}) has been added to the system";
                    return RedirectToPage(new { productid = newProgramID });
                }
                catch (ArgumentNullException ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                    PopulateLists();
                    return Page();
                }
                catch (Exception ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                    PopulateLists();
                    return Page();
                }
      //      }
  //          var a = ModelState.ErrorCount;
  //         IEnumerable<ModelError> errors = ModelState.Values.SelectMany(x => x.Errors);

  //          return Page();
        }

        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int programID = _programServices.Program_UpdateProgram(ProgramInfo);
                    if (programID > 0)
                    {
                        Feedback = $"Program id ({programID}) has been updated on the system";
                    }
                    else
                    {
                        Feedback = "No program was affected. Refresh, search, and try again.";
                    }
                    return RedirectToPage(new { programid = programID });
                }
                catch (ArgumentNullException ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                    PopulateLists();
                    return Page();
                }
                catch (Exception ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;

                    PopulateLists();

                    return Page();
                }
            }

            return Page();
        }

        public IActionResult OnPostDelete()
        {

            if (ModelState.IsValid)
            {

                try
                {

                    int programID = _programServices.Program_DeleteProgram(ProgramInfo);
                    if (programID > 0)
                    {
                        Feedback = $"Program id ({programID}) has been discontinued on the system";
                    }
                    else
                    {
                        Feedback = "No program was affected. Refresh, search, and try again.";
                    }

                    return RedirectToPage(new { programid = programID });
                }
                catch (ArgumentNullException ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                    PopulateLists();
                    return Page();
                }
                catch (Exception ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;

                    PopulateLists();

                    return Page();
                }
            }

            return Page();
        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
    }
}

