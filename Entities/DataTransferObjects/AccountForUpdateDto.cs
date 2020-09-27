using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class AccountForUpdateDto
    {
        [Required(ErrorMessage = "Owner Id is required")]
        public Owner OwnerId { get; set; }
        [Required(ErrorMessage = "Date Created is required")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "Account Type is required")]        
        public string AccountType { get; set; }
    }
}
