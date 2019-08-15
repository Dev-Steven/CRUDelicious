    using System.ComponentModel.DataAnnotations;
    using System;
    namespace CRUDelicious.Models
    {
        public class Dish
        {
            // auto-implemented properties need to match the columns in your table
            // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
            [Key]
            public int id { get; set; }
            // MySQL VARCHAR and TEXT types can be represeted by a string
            public string chef_name { get; set; }
            public string dish_name { get; set; }
            public int calories { get; set; }
            public int tastiness { get; set; }
            public string description { get; set; }
            // The MySQL DATETIME type can be represented by a DateTime
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
        }
    }
    