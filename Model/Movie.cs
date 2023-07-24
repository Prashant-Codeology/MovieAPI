using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Movie
    {
        [Key]
        public Guid MovieId { get; set; }
        public string MovieName { get; set; }
        public string? MovieDescription { get; set; } 
        public string? MovieGenre { get; set; }
        public string? ImagePath { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [DefaultValue(0)]
        public decimal AverageRating { get; set; }
    }
}
