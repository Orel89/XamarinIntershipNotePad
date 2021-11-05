using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Model
{
    [Table("Events")]
    public class EventModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public bool IsHappened { get; set; }
        public DateTime EventTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
