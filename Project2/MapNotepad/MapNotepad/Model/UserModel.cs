using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Model
{
    [Table("Users")]
    public class UserModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get ; set; }
        public string UserName { get; set; }
        [Unique]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
