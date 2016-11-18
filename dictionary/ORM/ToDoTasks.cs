using System;
using System.Data;
using System.IO;
using SQLite;

namespace dictionary.ORM
{
    [Table("ToDoTasks")]
    class ToDoTasks
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int Id { get; set; }

        [MaxLength(105)]

        public string Task { get; set; }
    }
}