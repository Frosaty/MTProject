using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MTProject.Models;

namespace MTProject.Models
{
    [MetadataType(typeof(TraineeMetadata))]
    public partial class Trainee
    {
        [Display(Name = "User Name")]
        public string userName { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Display(Name = "Password")]

        public string password { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Compare("password", ErrorMessage = "Password Not Match")]
        [Display(Name = "Retype-Password")]
        public string RPassword { get; set; }
    }
    public partial class TraineeMetadata
    {
        [Required(ErrorMessage = "cant be empty!", AllowEmptyStrings = false)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "cant be empty!", AllowEmptyStrings = false)]

        [Display(Name = "Date of Birth")]
        public Nullable<System.DateTime> DoB { get; set; }
        [Required(ErrorMessage = "cant be empty!", AllowEmptyStrings = false)]
        public string Education { get; set; }
        [Required(ErrorMessage = "cant be empty!", AllowEmptyStrings = false)]
        [Display(Name = "Programing Languages")]
        public string ProLanguage { get; set; }
        [Required(ErrorMessage = "cant be empty!", AllowEmptyStrings = false)]
        [Display(Name = "Toeic Score")]
        public Nullable<int> ToeicScore { get; set; }
        [Required(ErrorMessage = "cant be empty!", AllowEmptyStrings = false)]
        public string Experience { get; set; }
        [Required(ErrorMessage = "cant be empty!", AllowEmptyStrings = false)]
        public string Location { get; set; }
        [Required(ErrorMessage = "cant be empty!", AllowEmptyStrings = false)]
        public string Department { get; set; }
    }
    /*-----------------------------------------TRAINER-------------------------------------------*/
    public partial class Trainer
    {
        [Display(Name = "User Name")]
        public string userName { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Display(Name = "Password")]

        public string password { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Compare("password", ErrorMessage = "Password Not Match")]
        [Display(Name = "Retype-Password")]
        public string RPassword { get; set; }
    }

}