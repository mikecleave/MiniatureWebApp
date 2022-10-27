using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace MiniatureWebApp.Models
{
    public class PowerStation
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }
        public string Name { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        
        public ICollection<Inspection>? Inspections { get; set; }
    }
}