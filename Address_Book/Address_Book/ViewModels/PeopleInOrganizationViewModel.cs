using Address_Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book.ViewModels
{
    public class PeopleInOrganizationViewModel
    {
        public List<Person> people { get; set; }
        public Organization organization { get; set; }


    }
}
