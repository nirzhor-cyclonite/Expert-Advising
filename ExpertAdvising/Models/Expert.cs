using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace trial7.Models
{

    public class Expert
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required (ErrorMessage ="Email Id is Required")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        public string ProfileImage { get; set; }
        [DataType(DataType.Password)]
        [DisplayFormat(DataFormatString = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password Should be confirmed")]
        [Compare("Password", ErrorMessage = "passwords Doesn't match")]
        public string ConfirmPassword { get; set; }

        [DisplayFormat(DataFormatString = "Expertise Sector")]
        [Required(ErrorMessage = "Expertise Sector is Reqiured")]
        public String ExpertiseSector { get; set; }
        public string Bio { get; set; }
        public string BankAccount { get; set; }
        public decimal WalletStatus { get; set; }
        public decimal Fee { get; set; }
        public decimal Rating { get; set; }



    }
    public enum Gender
    {
        psycology,
        medical
    }

}
