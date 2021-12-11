using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalMVCApplication.Models
{
    public class JobsModel
    {
        [Required]
        [StringLength(100)]
        public string? JobTitle { get; set; }

        [Required]
        [StringLength(100)]
        public string? JobLocation { get; set; }

        [Required]
        public string? JobDescription { get; set; }

        [Required]
        public string? Company { get; set; }

        [Required]
        [StringLength(10)]
        public string? Vacancy { get; set; }

        [Required]
        [StringLength(10)]
        public string? JobNature { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CTC must be numeric")]
        public string? Salary { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public string? PostedBy { get; set; }
        public string? Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsApplied { get; set; }
    }

    public class JobApplyModel
    {
        public string? UserId { get; set; }

        public string? JobId { get; set; }
        public DateTime AppliedOn { get; set; }
    }
}
