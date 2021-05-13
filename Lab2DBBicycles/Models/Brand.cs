using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

#nullable disable

namespace Lab2DBBicycles
{
    public partial class Brand
    {
        public Brand()
        {
            Bicycles = new HashSet<Bicycle>();
        }

        public int Id { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(50, ErrorMessage = ErrorMessages.StringLength)]
        public string Name { get; set; }

        [Display(Name = "Опис")]
        [StringLength(1000, ErrorMessage = ErrorMessages.StringLength)]
        public string Info { get; set; }

        [Display(Name = "Країна")]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public int CountryId { get; set; }

        [Display(Name = "Країна")]
        public virtual Country Country { get; set; }
        public virtual ICollection<Bicycle> Bicycles { get; set; }
    }
}
