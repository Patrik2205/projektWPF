using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektWPF.Models
{
    public abstract class GameElement
    {
        public int X { get; set; }
        public int Y { get; set; }

        public abstract void Update(Game game);
    }
}
