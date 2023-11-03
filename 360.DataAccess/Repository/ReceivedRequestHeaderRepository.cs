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
   public class ReceivedRequestHeaderRepository : Repository<ReceivedRequestHeader>, IReceivedRequestHeader
    {
        private readonly ApplicationDbContext _db;
        public ReceivedRequestHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(ReceivedRequestHeader Item)
        {
            _db.Update(Item);
        }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
