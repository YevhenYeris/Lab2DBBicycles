using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

#nullable disable

namespace Lab2DBBicycles
{
    public partial class Store
    {
        public Store()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(50, ErrorMessage = ErrorMessages.StringLength)]
        public string Name { get; set; }

        [Display(Name = "Опис")]
        [StringLength(1000, ErrorMessage = ErrorMessages.StringLength)]
        public string Info { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
