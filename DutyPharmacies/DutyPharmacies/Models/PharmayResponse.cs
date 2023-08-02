using System;
using System.Collections.Generic;
using System.Text;

namespace DutyPharmacies.Models
{
    public class Pharmacies
    {
        public string name { get; set; }
        public string dist { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string loc { get; set; }
    }

    public class PharmaciesResponse
    {
        public bool success { get; set; }
        public List<Pharmacies> result { get; set; }
    }
}
