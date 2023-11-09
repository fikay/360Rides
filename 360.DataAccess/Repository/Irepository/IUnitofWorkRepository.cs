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
        IReceivedRequestHeader ReceivedRequestHeader { get; }
        IReceivedRequestDetails ReceivedRequestDetails { get; }
        IEmployeeRepository EmployeesRepository { get; }
        IStoredChildrenRepository StoredChildrenRepository { get; }
        void save();
        void EntityAdd<T>(T item) where T : class;
    }
}
