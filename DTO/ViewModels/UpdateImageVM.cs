using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels
{
    public class UpdateImageVM
    {
        public Guid MovieId { get; set; }
        public IFormFile? ImagePath { get; set; }

    }
}
