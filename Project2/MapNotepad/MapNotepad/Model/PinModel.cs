using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Model
{
    [Table("Pins")]
    public class PinModel : IEntityBase
    {
        
        [PrimaryKey, AutoIncrement]
        public int Id { get; set ; }
        public int UserId { get; set;}
        public string Label { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
