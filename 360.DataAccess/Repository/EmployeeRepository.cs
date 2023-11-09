using _360.DataAccess.Data;
using _360.DataAccess.Repository.Irepository;
using _360.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.DataAccess.Repository
{
    public class EmployeeRepository: Repository<Employees>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(Employees Item)
        {
            _db.Update(Item);
        }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
