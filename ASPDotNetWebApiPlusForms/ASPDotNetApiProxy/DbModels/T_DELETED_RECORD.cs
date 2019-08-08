using ASPDotNetApiProxy.DataAccess;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPDotNetApiProxy.DbModels
{
    /// <summary>
    /// T_DELETED_RECORD
    /// </summary>
    [Table("T_DELETED_RECORD", Schema = "ACNSM")]
    internal class T_DELETED_RECORD
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Required]
        [Sequence("NSM_T_DELETED_RECORD_SEQ", Schema = "ACNSM")]
        public Int32 ID { get; set; }

        /// <summary>
        /// DELETE_TIME
        /// </summary>
        [Required]
        public DateTime DELETE_TIME { get; set; }

        /// <summary>
        /// USR_ID
        /// </summary>
        [Required]
        public Int32 USR_ID { get; set; }

        /// <summary>
        /// DATA
        /// </summary>
        [Required]
        public String DATA { get; set; }

    }
}
