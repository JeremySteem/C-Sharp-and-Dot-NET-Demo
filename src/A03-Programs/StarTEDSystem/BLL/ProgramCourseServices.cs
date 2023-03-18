using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using StarTEDSystem.DAL;
using StarTEDSystem.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking; 
#endregion


namespace StarTEDSystem.BLL
{
    public class ProgramCourseServices
    {
        #region setup of the context connection variable and class constructor
        private readonly StarTEDContext _context;
        internal ProgramCourseServices(StarTEDContext regcontext)
        {
            _context = regcontext;
        }
        #endregion
    }
}
