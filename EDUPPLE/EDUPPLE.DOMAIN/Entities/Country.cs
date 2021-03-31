using EDUPPLE.DOMAIN.Interface;
using System;
using System.Collections.Generic;

namespace EDUPPLE.DOMAIN.Entities
{
    public class Country : Entity<int>, ITrackCreated, ITrackUpdated
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ZipCode { get; set; }        
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public ICollection<City> Cities { get; set; } = new HashSet<City>();
        
    }
}
