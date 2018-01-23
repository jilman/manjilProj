using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Model
{
    [Table("StockReceiveMast")]
    public class StockReceiveMast
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

        public int PortfolioAcId { get; set; }

        [Required]
        [StringLength(100)]
        public string Remarks { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ValueDate { get; set; }

        [ForeignKey("PortfolioAcId")]
        public virtual PortfolioAccount PortfolioAccounts { get; set; }

        public virtual List<StockReceiveDetl> StockReceiveDetls { get; set; }
    }
}
