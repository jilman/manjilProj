using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Model
{
    [Table("StockReceiveDetl")]
    public class StockReceiveDetl
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MastId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OwnershipDate { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        public decimal Rate { get; set; }

        [Required]
        public int StockId { get; set; }

        [ForeignKey("StockId")]
        public virtual Stock Stocks { get; set; }

        [ForeignKey("MastId")]
        public virtual StockReceiveMast StockRecieveMasts { get; set; }

    }
}
