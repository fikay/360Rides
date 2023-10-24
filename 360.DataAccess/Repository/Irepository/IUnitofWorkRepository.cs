using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.DataAccess.Repository.Irepository
{
   public interface IUnitofWorkRepository
    {  
       IServicesRepository ServicesRepository { get; }
       IServiceRequestRepository RequestRepository { get; }
        IChildrenRepository ChildrenRepository { get; }
        void save();
    }
}
