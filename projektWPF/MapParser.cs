using projektWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;


namespace projektWPF
{
    public static class MapParser
    {
        public static List<GameElement> ParseMap(string filePath, Game game)
        {
            var elements = new List<GameElement>();
            string[] lines = File.ReadAllLines(filePath);
            game.Width = lines[0].Length;
            game.Height = lines.Length;

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    char c = lines[y][x];
                    switch (c)
                    {
                        case 'W':
                            elements.Add(new Wall { X = x, Y = y });
                            break;
                        case 'R':
                            elements.Add(new Rock { X = x, Y = y });
                            break;
                        case 'D':
                            elements.Add(new Diamond { X = x, Y = y });
                            break;
                        case 'M':
                            elements.Add(new Monster { X = x, Y = y });
                            break;
                        case 'S':
                            elements.Add(new Sand { X = x, Y = y });
                            break;
                        case 'E':
                            elements.Add(new EmptySpot { X = x, Y = y });
                            break;
                        case 'P':
                            var player = new Player { X = x, Y = y };
                            elements.Add(player);
                            game.Player = player;
                            break;
                        case 'X':
                            elements.Add(new Exit { X = x, Y = y });
                            break;
                        default:
                            elements.Add(new EmptySpot { X = x, Y = y });
                            break;
                    }
                }
            }

            return elements;
        }
    }
}