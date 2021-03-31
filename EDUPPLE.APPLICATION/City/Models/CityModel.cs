using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.City.Models
{
    public abstract class CityModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
    }
}
