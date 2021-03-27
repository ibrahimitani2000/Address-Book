using Address_Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book.Services
{
    public interface IOrganizationsManager
    {
        public Task<Organization> FindByIdAsync(int id);
        
        public Task<IEnumerable<Organization>> GetAll();

        public Task CreateAsync(Organization organization);
        public Task DeleteAsync(int id);

        public Task UpdateAsync(Organization organization);

        public bool Exists(int id);


    }
}
