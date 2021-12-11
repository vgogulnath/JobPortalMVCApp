using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalMVCApplication.Models
{
    public class UserRegisterModel
    {
        public string? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(20)]
        public string? Password { get; set; }

        [Required]
        [MaxLength(12)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be numeric")]
        public string? Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string? CurrentCompany { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Experience must be numeric")]
        public string? Experience { get; set; }

        [Required]
        [StringLength(100)]
        public string? SkillSet { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CTC must be numeric")]
        public string? CurrentCTC { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }

        [Required]
        [StringLength(1)]
        
        public string? UserType { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(20)]
        public string? Password { get; set; }
    }
}
