using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GeneroDTO
    {
        public short Id { get; set; }
        public string Genero { get; set; }

        public static GeneroDTO FromModel(Cat_Genero model)
        {
            return new GeneroDTO()
            {
                Id = model.Id,
                Genero = model.Genero,
            };
        }

        public Cat_Genero ToModel()
        {
            return new Cat_Genero()
            {
                Id = Id,
                Genero = Genero,
            };
        }

    }
}
