using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Model
{
    [Table("PortfolioAccount")]
    public class PortfolioAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "A/c Number")]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "A/c Name")]
        public string AccountName { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate { get; set; }

        [Required]
        public int EntryUserID { get; set; }
    }
}
