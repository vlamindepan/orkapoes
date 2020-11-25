using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolTemplate.Models
{
  public class PersonModel
  {
    public string name { get; set; }

    [Required]
    [EmailAddress]
        public string email { get; set; }

    }
}