using _360.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.DataAccess.Repository.Irepository
{
    public interface IServiceRequestRepository: IRepository<ServiceRequest> 
    {
        void update(ServiceRequest Item);
    }
}
