using System.ComponentModel.DataAnnotations;

namespace RestAPI.V1.Aggregates.Users.Models
{
    public class UserRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string NationalCode { get; set; }

        public string BirthDay { get; set; }
    }
}
