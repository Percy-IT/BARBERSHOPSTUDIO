namespace BARBERSHOP_STUDIO.Models
{
    public class Client
    {

        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

    }
}
