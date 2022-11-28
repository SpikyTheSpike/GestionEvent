namespace GestionEvent.Models
{
    public class ConnectedUserDTO
    {
        public int MemberId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Pseudo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
