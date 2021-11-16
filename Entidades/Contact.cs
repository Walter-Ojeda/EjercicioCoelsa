using System;
using System.Data;

namespace Entidades
{
    public class Contact : EntidadBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}
