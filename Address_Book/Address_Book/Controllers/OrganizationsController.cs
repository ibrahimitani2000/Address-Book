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
using Address_Book.Implementations;
using Address_Book.ViewModels;

namespace Address_Book.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationsManager _organizationsmanager;

        private readonly IPeopleManager _peoplemanager;

        public OrganizationsController(AddressBookContext context, IPeopleManager peoplemanager, IOrganizationsManager organizationsmanager)
        {
            _peoplemanager = peoplemanager;
            _organizationsmanager = organizationsmanager;
        }

        // GET: Organizations
        public async Task<IActionResult> Index()
        {
            return View(await _organizationsmanager.GetAll());
        }
        public async Task<IActionResult> Manage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Organization organizationfromdb = await _organizationsmanager.FindByIdAsync((int)(id));
            List<Person> peoplefromdb=await _peoplemanager.GetByOrganizationIdAsync((int)id);
            PeopleInOrganizationViewModel viewmodel = new PeopleInOrganizationViewModel()
            {
                organization = organizationfromdb,
                people = peoplefromdb

            };

            return View(viewmodel);
        }



        // GET: Organizations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _organizationsmanager.FindByIdAsync((int)id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // GET: Organizations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Street,City,PostCode")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                await _organizationsmanager.CreateAsync(organization);
                return RedirectToAction(nameof(Index));
            }
            return View(organization);
        }

        // GET: Organizations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _organizationsmanager.FindByIdAsync((int)id);
            if (organization == null)
            {
                return NotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Street,City,PostCode")] Organization organization)
        {
            if (id != organization.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _organizationsmanager.UpdateAsync(organization);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationExists(organization.Id))
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
            return View(organization);
        }

        // GET: Organizations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _organizationsmanager.FindByIdAsync((int)id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organization = await _organizationsmanager.FindByIdAsync(id);
            await _organizationsmanager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizationExists(int id)
        {
            return _organizationsmanager.Exists(id);
        }
    }
}
