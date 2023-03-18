#nullable disable
#region Additional Namespaces
using StarTEDSystem.DAL;
using StarTEDSystem.Entities;
#endregion

namespace StarTEDSystem.BLL
{
    public class SchoolServices
    {
        #region Setup of the context connection
        private readonly StarTEDContext _context;
        internal SchoolServices(StarTEDContext regContext)
        {
            _context = regContext;
        }
        #endregion
        #region Services: Query
        public List<School> School_List()
        {
            IEnumerable<School> info = _context.Schools
                                       .OrderBy(x => x.SchoolName);
            return info.ToList();
        }
        #endregion
    }
}
