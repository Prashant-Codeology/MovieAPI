using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels
{
    public class GetAllMoviesVM
    {
        public IEnumerable<MovieVM> Movies { get; set; } 
    }
}
