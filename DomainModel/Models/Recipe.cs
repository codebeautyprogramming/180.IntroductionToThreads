using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int RecipeTypeId { get; set; }

        public Recipe(string name, string description, byte[]? image, int recipeTypeId, int id=-1)
        {
            Name = name;
            Description = description;
            Image = image;
            RecipeTypeId = recipeTypeId;
            if(id!=-1)
                Id = id;
        }
        public Recipe() { }
    }
}
