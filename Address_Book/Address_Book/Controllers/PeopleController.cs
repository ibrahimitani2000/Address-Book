using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Address_Book.Data;
using Address_Book.Models;
using Address_Book.Services;

namespace Address_Book.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPeopleManager _peoplemanager;
        private readonly IOrganizationsManager _organizationsmanager;

        


        public PeopleController(IPeopleManager peoplemanager, IOrganizationsManager organizationsmanager)
        {
            _peoplemanager = peoplemanager;
            _organizationsmanager = organizationsmanager;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var addressBookContext = await _peoplemanager.GetAll();
            return View(addressBookContext);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _peoplemanager.FindByIdAsync((int)id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["OrganizationId"] = new SelectList(await _organizationsmanager.GetAll(), "Id", "Name");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                await _peoplemanager.CreateAsync(person);
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizationId"] = new SelectList(await _organizationsmanager.GetAll(), "Id", "Name", person.OrganizationId);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _peoplemanager.FindByIdAsync((int)id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["OrganizationId"] = new SelectList(await _organizationsmanager.GetAll(), "Id", "Name", person.OrganizationId);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrganizationId,FirstName,LastName,PhoneNumber,Street,City,PostCode")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _peoplemanager.UpdateAsync(person);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizationId"] = new SelectList(await _organizationsmanager.GetAll(), "Id", "Name", person.OrganizationId);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _peoplemanager.FindByIdAsync((int)id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _peoplemanager.FindByIdAsync(id);
            await _peoplemanager.DeleteAsync(person.Id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _peoplemanager.FindByIdAsync((int)id);
            
            if (person == null)
            {
                return NotFound();
            }


            return View(person);
        }

        // POST: People/Remove/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            var person = await _peoplemanager.FindByIdAsync(id);
            await _peoplemanager.RemoveFromOrganizationAsync(person.Id);
        
            return RedirectToAction(nameof(Index));
        
        }


        private bool PersonExists(int id)
        {
            return _peoplemanager.Exists(id);
        }
    }
}
