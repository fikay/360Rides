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
    public class ReceivedRequestDetailsRepository : Repository<ReceivedRequestDetails>, IReceivedRequestDetails
    {
        private readonly ApplicationDbContext _db;
        public ReceivedRequestDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(ReceivedRequestDetails Item)
        {
            _db.Update(Item);
        }
    }
}
