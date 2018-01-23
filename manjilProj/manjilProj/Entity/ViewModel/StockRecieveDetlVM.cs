using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.ViewModel
{
    public class StockRecieveDetlVM
    {
        public int Id { get; set; }
        public int MastId { get; set; }
        public int StockId { get; set; }
        public string StockName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Value Date")]
        public DateTime OwnershipDate { get; set; }

        public int Qty { get; set; }

        public decimal Rate { get; set; }

        public decimal Amount { get; set; }
        public Flag Flag { get; set; }
    }
    public enum Flag
    {
        New = 0,
        Deleted = 1
    }
}
