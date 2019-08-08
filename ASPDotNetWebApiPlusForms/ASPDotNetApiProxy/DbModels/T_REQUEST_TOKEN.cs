using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPDotNetApiProxy.DbModels
{
    /// <summary>
    /// T_REQUEST_TOKEN
    /// </summary>
    [Table("T_REQUEST_TOKEN", Schema = "dbo")]
    internal class T_REQUEST_TOKEN
    {
        /// <summary>
        /// VALUE
        /// </summary>
        [Required]
        [Key]
        public string VALUE { get; set; }

        /// <summary>
        /// CREATION_TIME
        /// </summary>
        public DateTime? CREATION_TIME { get; set; }

        /// <summary>
        /// EXPIRATION_TIME
        /// </summary>
        public DateTime? EXPIRATION_TIME { get; set; }

    }
}
