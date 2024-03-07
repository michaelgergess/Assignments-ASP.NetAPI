using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Model
{
    public class BaseClass :BaseClass2
    {
        [Required]
        public int Id { get; set; }
    }
}
