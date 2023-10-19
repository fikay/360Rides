using _360.Models;
using _360.DataAccess.Repository.Irepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _360.DataAccess.Data;

namespace _360.DataAccess.Repository
{
    public class ServicesRepository : Repository<ServicesModel>,IServicesRepository
    {
        private readonly ApplicationDbContext _db;
        public ServicesRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void update(ServicesModel Item)
        {
            _db.Update(Item);
        }
    }
}
