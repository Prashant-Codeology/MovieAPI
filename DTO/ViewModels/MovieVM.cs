using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels
{
    public class MovieVM
    {
        public Guid MovieId { get; set; }
        public string MovieName { get; set; }
        public string? MovieDescription { get; set; }
        public string? MovieGenre { get; set; }
        public string? ImagePath { get; set; }
        public decimal AverageRating { get; set; }
    }
}
