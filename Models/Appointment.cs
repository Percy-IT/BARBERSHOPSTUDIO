using System.ComponentModel.DataAnnotations;

namespace BARBERSHOP_STUDIO.Models
{
  
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } 

        public int BarberId { get; set; }
        public Barber Barber { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Id {  get; set; }
        public Service Service{ get; set; }



    }
}
