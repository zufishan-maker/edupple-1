using EDUPPLE.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.DOMAIN.Entities
{
    public class City: Entity<int>, ITrackCreated, ITrackUpdated
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Country Country { get; set; }
    }
}
