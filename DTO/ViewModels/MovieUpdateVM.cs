using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTO.ViewModels
{
    public class MovieUpdateVM
    {
        public string? MovieName { get; set; }
        public string? MovieDescription { get; set; }
        public string? MovieGenre { get; set; }
    }
}
