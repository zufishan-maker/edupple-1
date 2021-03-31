using EDUPPLE.APPLICATION.Country.Models;
using EDUPPLE.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.City.Models
{
    public class CityReadModel : CityModel, ITrackCreated, ITrackUpdated
    {
        public int Id { get; set; }       
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public CountryReadModel Country { get; set; }
    }
}
