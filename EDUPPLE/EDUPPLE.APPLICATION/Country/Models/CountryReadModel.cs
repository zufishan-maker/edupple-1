using EDUPPLE.DOMAIN.Interface;
using System;

namespace EDUPPLE.APPLICATION.Country.Models
{
    public class CountryReadModel : CountryModel, ITrackCreated, ITrackUpdated
    {
        public int Id { get; set; }        
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

    }
}
