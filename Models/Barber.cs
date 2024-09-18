namespace BARBERSHOP_STUDIO.Models
{
    public class Barber
    {
        public int BarberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    } 
}
