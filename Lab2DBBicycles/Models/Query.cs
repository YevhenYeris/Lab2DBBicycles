using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2DBBicycles.Models
{
    public class Query
    {
        public int Id { get; set; }

        public string Error { get; set; }

        public int ErrorFlag { get; set; }

        [Display(Name = "Велосипед")]
        public string BicycleName { get; set; }

        public List<string> BicycleNames { get; set; }

        [Display(Name = "Магазин А")]
        public string StoreName1 { get; set; }

        [Display(Name = "Магазин В")]
        public string StoreName2 { get; set; }
        public List<string> StoreNames { get; set; }

        [Display(Name = "Країна")]
        public string CountryName { get; set; }

        [Display(Name = "Кількість магазинів")]
        public int StoreCount { get; set; }

        [Display(Name = "Середня ціна")]
        public double AvgPrice { get; set; }

        [Display(Name = "Бренди")]
        public List<string> BrandNames { get; set; }
        public List<double> Prices { get; set; }

        [Display(Name = "Бренд")]
        public string BrandName { get; set; }

        [Display(Name = "Ціна")]
        public int Price { get; set; }
        public int Price1 { get; set; }

        [Display(Name = "N1")]
        public int Count1 { get; set; }

        [Display(Name = "N2")]
        public int Count2 { get; set; }
    }
}