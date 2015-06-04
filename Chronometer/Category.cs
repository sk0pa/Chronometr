using GalaSoft.MvvmLight;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Chronometer
{
    [Table("Categories")]
    public class Category : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("IsStarted")]
        public bool isStarted { get; set; }

        [Ignore]
        public bool IsStarted
        {
            get
            {
                return isStarted;
            }
            set
            {
                isStarted = value;
                RaisePropertyChanged("Icon");
            }
        }

        [Column("Started")]
        public DateTime Started { get; set; }

        [Ignore]
        public string Icon
        {
            get
            {
                return IsStarted ? "\uE121" : Name[0].ToString().ToUpper();
            }
        }

        [Ignore]
        public Color Color
        {
            get
            {
                return ColorTools.ColorFromArgb(ColorRGB);
            }
        }

        [Column("ColorRGB")]
        public uint ColorRGB { get; set; }

        private void RaisePropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
