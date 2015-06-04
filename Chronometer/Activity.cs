using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Chronometer
{
    [Table("Activities")]
    public class Activity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Column("Started")]
        public DateTime Started { get; set; }

        [Column("Ended")]
        public DateTime Ended { get; set; }
    }
}
