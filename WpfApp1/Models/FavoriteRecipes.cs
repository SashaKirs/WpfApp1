using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
   
        public class FavoriteRecipes
        {
            public int Id { get; set; }
            public int UsersId { get; set; }
            public int RecipesId { get; set; }
            public virtual User User { get; set; }
            public virtual Recipes Recipes { get; set; }
        }
    
}
