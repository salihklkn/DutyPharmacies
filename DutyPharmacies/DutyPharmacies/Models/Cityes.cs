using System;
using System.Collections.Generic;
using System.Text;

namespace DutyPharmacies.Models
{
    public class Cityes
    {
        public string Name { get; set; }
		public List<Districts> Districts { get; set; }
    }


    public class CitiesListResponse
    {
        public List<Cityes> data { get; set; }
    }
}
