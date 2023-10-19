using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _360.DataAccess.Repository.Irepository
{
    public interface IRepository<T> where T : class
    {
        // A list of object of type T will be returned.
        // Parameters(optional): LINQ Expression, includefromotherTables, tracking
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties =null);

        // An object of type T will be returned.
        // Parameters(optional): LINQ Expression, includefromotherTables, tracking
        T Get(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false);

        // An object of type T will be added to the table.
        // Parameters: Variable of type T
        void Add(T item);

        // An object of type T will be deleted from the table.
        // Parameters: Variable of type T
        void Delete(T item);
    }
}
