using Address_Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book.Services
{
    public interface IPeopleManager
    {
        public Task<Person> FindByIdAsync(int id);
        public Task<List<Person>> GetAll();

        public Task CreateAsync(Person person);
        public Task DeleteAsync(int id);

        public Task UpdateAsync(Person person);

        public bool Exists(int id);
        public Task RemoveFromOrganizationAsync(int id);

        public Task<List<Person>> GetByOrganizationIdAsync(int organizationId);



    }
}
