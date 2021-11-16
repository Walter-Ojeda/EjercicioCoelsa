using System;
using System.ComponentModel.DataAnnotations;

namespace EjercicioEntrevistaCoelsa.Models
{
    public class ContactModel
    {

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Company { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
