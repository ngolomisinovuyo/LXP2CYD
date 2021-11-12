using System;
namespace LPX2YCDProject2020.Areas.Coordinator.Dtos
{
    public class CreateEmplyeeInputDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string RoleId { get; set; }
        public string Region { get; set; }
    }
}
