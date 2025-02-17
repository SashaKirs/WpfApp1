using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Recipes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public bool IsFavorite { get; set; }
        //public virtual ICollection<FavoriteRecipes> FavoriteRecipes { get; set; }
    }

}
