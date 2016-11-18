using System;
using System.Data;
using System.IO;
using SQLite;

namespace dictionary.ORM
{
    [Table("IrrVerbsUsers")]
    class IrrVerbsUsers
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int Id { get; set; }

        [MaxLength(105)]

        public string form1 { get; set; }

        [MaxLength(105)]

        public string form2 { get; set; }

        [MaxLength(105)]

        public string form3 { get; set; }

        [MaxLength(105)]

        public string translation { get; set; }
    }
}