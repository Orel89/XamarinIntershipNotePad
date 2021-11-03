using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Model
{
    [Table("PinsPhoto")]
    public class PhotoPinModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string PinId { get; set; }
        public string ImageUrl { get; set; }
    }
}
