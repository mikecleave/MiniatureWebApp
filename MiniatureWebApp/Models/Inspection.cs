using System.ComponentModel.DataAnnotations;

namespace MiniatureWebApp.Models
{
    public enum Status
    {
        SCHEDULED, PASS, FAIL
    }    

    public class Inspection
    {
        public int Id { get; set; }
        public int PowerStationId { get; set; }
        public DateTime Date { get; set; }
        
        [Display(Name = "Inspector Name")]
        public string InspectorName { get; set; }
        public string Comment { get; set; }

        public Status Status { get; set; }
        //public string Status { get; set; }

        public PowerStation? PowerStation { get; set; }

    }
}
