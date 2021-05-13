using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

#nullable disable

namespace Lab2DBBicycles
{
    public partial class Bicycle
    {
        public Bicycle()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        [Display(Name="Назва")]
        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(50, ErrorMessage = ErrorMessages.StringLength)]
        public string Name { get; set; }

        [Display(Name = "Ціна")]
        public double? Price { get; set; }

        [Display(Name = "Рік випуску")]
        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(2000, 3000)]
        public int Year { get; set; }

        [Display(Name = "Опис")]
        [StringLength(1000, ErrorMessage = ErrorMessages.StringLength)]
        public string Info { get; set; }

        [Display(Name = "Бренд")]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public int BrandId { get; set; }

        [Display(Name = "Бренд")]
        public virtual Brand Brand { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
