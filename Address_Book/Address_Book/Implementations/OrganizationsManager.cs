using Address_Book.Data;
using Address_Book.Models;
using Address_Book.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book.Implementations
{
    public class OrganizationsManager : IOrganizationsManager
    {
        private readonly AddressBookContext _context;

        public OrganizationsManager(AddressBookContext context)
        {
            _context = context;

        }
        public async Task<Organization> FindByIdAsync(int id)
        {

            var entity = await _context.Organization.FirstOrDefaultAsync(m => m.Id == id);
            return entity;
        }

        public async  Task<IEnumerable<Organization>> GetAll()
        {
            var entity =  _context.Organization;
            return entity;
        }

        public async Task CreateAsync(Organization organization)
        {
            await _context.Organization.AddAsync(organization);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Organization.FindAsync(id);
            if (entity == null)
            {
                throw new Exception();
            }
           DeletePeopleInOrganization(id);
            _context.Organization.Remove(entity);
            await _context.SaveChangesAsync();
        }
        private  void DeletePeopleInOrganization(int idOrganization)
        {
            //Get all people that have idOrganization and change it to null

            var oragnizations = _context.Person.Where(c => c.OrganizationId==idOrganization).ToList();
            oragnizations.ForEach(c => c.OrganizationId= null);
        }



        public async Task UpdateAsync(Organization organization)
        {

            if (!_context.Organization.Any(m => m.Id == organization.Id))
            {
                throw new Exception();
            }
            try
            {

                _context.Organization.Update(organization);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool Exists(int id)
        {
            return _context.Organization.Any(m => m.Id == id);
        }
    }
}
