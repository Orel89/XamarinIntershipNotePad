using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Model.Pin
{
    [Table("Pins")]
    public class PinModel : IEntityBase
    {
        
        [PrimaryKey, AutoIncrement]
        public int Id { get; set ; }
        public int UserId { get; set;}
        public string Label { get; set; }
        public string Description { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
