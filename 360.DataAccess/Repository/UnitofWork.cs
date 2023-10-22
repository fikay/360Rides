using _360.DataAccess.Data;
using _360.DataAccess.Repository.Irepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.DataAccess.Repository
{
    public class UnitofWork : IUnitofWorkRepository
    {
        private readonly ApplicationDbContext _db;

        public IServicesRepository ServicesRepository {  get; private  set; }
        public IChildrenRepository ChildrenRepository { get; private set; }
        public IServiceRequestRepository RequestRepository { get; private set; }

        public UnitofWork (ApplicationDbContext db)
        {
            _db = db;
            ServicesRepository = new ServicesRepository(_db);
            ChildrenRepository  = new ChildrenNameRepository(_db);
            RequestRepository = new ServiceRequestRepository(_db);
        }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
