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
    public class PeopleManager : IPeopleManager
    {
        private readonly AddressBookContext _context;
        
        public PeopleManager(AddressBookContext context)
        {
            _context = context;

        }
        public async Task<Person> FindByIdAsync(int id)
        {

            var entity = await _context.Person
                .Include(p => p.Organization)
                .FirstOrDefaultAsync(m => m.Id == id);
            return entity;
        }
        public async Task<List<Person>> GetAll()
        {
            var entity = await _context.Person.Include(p => p.Organization).ToListAsync();
        return entity;
        }
        public async Task<List<Person>> GetByOrganizationIdAsync(int organizationId)
        {
            var entity = await _context.Person.Include(p => p.Organization).Where(p => p.OrganizationId == organizationId).ToListAsync();
            return entity;
        }

        public async Task CreateAsync(Person person)
        {
            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Person.FindAsync(id);
            if (entity == null)
            {
                throw new Exception();
            }
            _context.Person.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            
            if (!_context.Person.Any(m => m.Id == person.Id))
            {
                throw new Exception();
            }
            try
            {
                _context.Person.Update(person);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool Exists(int id)
        {
            return _context.Person.Any(m => m.Id == id);
        }

        public async Task RemoveFromOrganizationAsync(int id)
        {
            Person person = await _context.Person.FirstOrDefaultAsync(m => m.Id == id);
            person.OrganizationId = null;
            await _context.SaveChangesAsync();


        }
    }
}
