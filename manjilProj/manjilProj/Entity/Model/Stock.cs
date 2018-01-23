using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Model
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate { get; set; }

        [Required]
        public int EntryUserID { get; set; }

        [Required]
        [StringLength(100)]
        public string StockName { get; set; }
    }
}
