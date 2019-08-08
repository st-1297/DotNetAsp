using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPDotNetApiProxy.DbModels
{
    /// <summary>
    /// M_USER
    /// </summary>
    [Table("M_USER", Schema = "dbo")]
    internal class M_USER
    {
        /// <summary>
        /// VALUE
        /// </summary>
        [Required]
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// NAME
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// PASSWORD
        /// </summary>
        public string PASSWORD { get; set; }

        /// <summary>
        /// ACTIVE_FLG
        /// </summary>
        public int ACTIVE_FLG { get; set; }
    }
}
