using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

#nullable disable

namespace Lab2DBBicycles
{
    public partial class Country
    {
        public Country()
        {
            Brands = new HashSet<Brand>();
        }

        public int Id { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(50, ErrorMessage = ErrorMessages.StringLength)]
        public string Name { get; set; }

        public virtual ICollection<Brand> Brands { get; set; }
    }
}
