namespace RAI_MVC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogTable")]
    public partial class LogTable
    {
        [Key]
        public int LogID { get; set; }

        public int? LoanID { get; set; }

        public DateTime? TimeStamp { get; set; }

        public int? UserID { get; set; }

        [StringLength(100)]
        public string Action { get; set; }

        public string Message { get; set; }
    }
}
