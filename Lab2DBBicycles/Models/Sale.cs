using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

#nullable disable

namespace Lab2DBBicycles
{
    public partial class Sale
    {
        public int Id { get; set; }

        [Display(Name = "Велосипед")]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public int BicycleId { get; set; }

        [Display(Name = "Магазин")]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public int StoreId { get; set; }

        [Display(Name = "Велосипед")]
        public virtual Bicycle Bicycle { get; set; }

        [Display(Name = "Магазин")]
        public virtual Store Store { get; set; }
    }
}
