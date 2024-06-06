namespace Snackis.Models
{
    public class PrivateMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string ReceiverId { get; set; }
        public string SenderId { get; set; }
    }
}
