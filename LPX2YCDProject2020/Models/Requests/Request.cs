using System;
namespace LPX2YCDProject2020.Models.Requests
{
    public class Request
    {
        public int Id { get; set; }
        public int CenterId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public RequestStatus Status { get; set; }
        public RequestType Type { get; set; }

    }
}
