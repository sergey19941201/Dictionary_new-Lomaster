using System;
using System.Data;
using System.IO;
using SQLite;

namespace dictionary.ORM
{
    [Table("Category1Cards")]
    class Category1Cards
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int Id { get; set; }

        [MaxLength(105)]

        public string Eng1c { get; set; }

        [MaxLength(105)]

        public string Rus1c { get; set; }

        [MaxLength(105)]

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }
    }
}