using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.CustomQueryResults
{
    public class RecipeIngredientWithNameAndAmount
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
