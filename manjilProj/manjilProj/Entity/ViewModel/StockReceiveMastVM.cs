using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.ViewModel
{
    public class StockReceiveMastVM
    {
        public int Id { get; set; }
        [Display(Name = "Porfolio Ac")]
        [Range(0, int.MaxValue, ErrorMessage = "Please Select The Value")]
        public int PortfolioAcId { get; set; }
        public string PortfolioAcName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        
        [Display(Name = "Entry User ID")]
        public int EntryUserID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Value Date")]
        public DateTime ValueDate { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Entry Date")]
        public DateTime EntryDate { get; set; }

        public virtual List<StockRecieveDetlVM> StockRecieveDetlVM { get; set; }
    }
}
