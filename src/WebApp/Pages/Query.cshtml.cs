#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Addional Namespaces
using StarTEDSystem.BLL;
using StarTEDSystem.Entities;
using WebApp.Helpers;
#endregion

namespace WebApp.Pages
{
    public class QueryModel : PageModel
    {
        #region Private service fields & class constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly ProgramServices _programServices;
        private readonly SchoolServices _schoolServices;

        public QueryModel(ILogger<IndexModel> logger,
                                            ProgramServices programServices,
                                            SchoolServices schoolServices)
        {
            _logger = logger;
            _programServices = programServices;
            _schoolServices = schoolServices;
        }
        #endregion

        [TempData]
        public string Feedback { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SchoolCode { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchArg { get; set; }

        public List<StarTEDSystem.Entities.Program> ProgramInfo { get; set; }

        [BindProperty]
        public List<School> SchoolList { get; set; }

        #region Paginator
        private const int PAGE_SIZE = 10;
        public Paginator Pager { get; set; }
        #endregion

        public void OnGet(int? currentPage)
        {
            SchoolList = _schoolServices.School_List();

            if (!string.IsNullOrWhiteSpace(SearchArg))
            {
                int pageNumber = currentPage.HasValue ? currentPage.Value : 1;
                PageState current = new(pageNumber, PAGE_SIZE);
                int totalCount;

                ProgramInfo = _programServices.Programs_by_Name(SearchArg,
                                            pageNumber, PAGE_SIZE, out totalCount);
                Pager = new Paginator(totalCount, current);
            }
        }

        public IActionResult OnPostFetch()
        {
            if (string.IsNullOrWhiteSpace(SearchArg))
            {
                Feedback = "Required:  Search argument is empty";
            }
            return RedirectToPage(new { SearchArg = SearchArg });
        }

        public IActionResult OnPostNew()
        {
            return RedirectToPage("/CRUD");
        }
    }
}

