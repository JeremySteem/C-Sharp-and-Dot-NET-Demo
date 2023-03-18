#nullable disable
#region Additional Namespaces
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StarTEDSystem.DAL;
using StarTEDSystem.Entities;
#endregion

namespace StarTEDSystem.BLL
{
    public class ProgramServices
    {
        #region Setup of the context connection
        private readonly StarTEDContext _context;
        internal ProgramServices(StarTEDContext regContext)
        {
            _context = regContext;
        }
        #endregion

        #region Services: Query
        public List<Program> Programs_by_Name(string partialName,
                                                        int pageNumber,
                                                        int pageSize,
                                                        out int totalCount)
        {
            IEnumerable<Program> info = _context.Programs
                .Where(x => x.ProgramName.Contains(partialName))
                .OrderBy(x => x.ProgramName);

            totalCount = info.Count();

            int skipRows = (pageNumber - 1) * pageSize;

            return info.Skip(skipRows).Take(pageSize).ToList();
        }

        public Program Program_by_ID(int programID)
        {
            Program info = _context.Programs
                           .Where(x => x.ProgramID == programID)
                           .FirstOrDefault();
            return info;
        }
        #endregion

        #region Add, Update, Delete
        public int Program_AddProgram(Program item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Program data is missing");
            }

            Program exists = _context.Programs
                            .Where(x => x.ProgramName.Equals(item.ProgramName) &&
                                        x.SchoolCode == item.SchoolCode)
                            .FirstOrDefault();
            if (exists != null)
            {
                throw new Exception($"{item.ProgramName}" +
                    $" from the selected school is already on file");
            }

            _context.Programs.Add(item);
            _context.SaveChanges();
            return item.ProgramID;
        }

        public int Program_UpdateProgram(Program item)
        {
            bool exists = _context.Programs.Any(x => x.ProgramID == item.ProgramID);
            if (!exists)
            {
                throw new Exception($"{item.ProgramName} with a diploma of {item.DiplomaName}" +
                    $" from the selected school is not on file");
            }

            EntityEntry<Program> updating = _context.Entry(item);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
            return item.ProgramID;
        }

        public int Program_DeleteProgram(Program item)
        {

            bool exists = _context.Programs.Any(x => x.ProgramID == item.ProgramID);

            if (!exists)
            {
                throw new Exception($"{item.ProgramName} with a diploma of {item.DiplomaName}" +
                    $" from the selected school is not on file");
            }

            // item.Discontinued = true; /* Discontinued for Soft Delete */
            EntityEntry<Program> updating = _context.Entry(item);

            // updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified; /* Soft Delete */

            updating.State = Microsoft.EntityFrameworkCore.EntityState.Deleted; /* Hard Delete */
            _context.SaveChanges();
            return item.ProgramID;
        }

        #endregion
    }
}

