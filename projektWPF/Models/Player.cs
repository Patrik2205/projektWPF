using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projektWPF.Models
{
    public class Player : GameElement
    {
        public override void Update(Game game)
        {
            
        }

        public void Move(int dx, int dy, Game game)
        {
            int newX = X + dx;
            int newY = Y + dy;

            if (CanMoveTo(newX, newY, game))
            {
                RemoveSandIfPresent(newX, newY, game);

                X = newX;
                Y = newY;

                foreach (var element in game.Elements)
                {
                    if (element.X == newX && element.Y == newY && element is Exit)
                    {
                        MessageBox.Show("You won!");
                        game.CompleteLevel();
                        return;
                    }
                }
            }
        }

        private bool CanMoveTo(int x, int y, Game game)
        {
            if (x < 0 || x >= game.Width || y < 0 || y >= game.Height)
            {
                return false;
            }

            foreach (var element in game.Elements)
            {
                if (element.X == x && element.Y == y)
                {
                    if (element is Rock || element is Wall || element is Monster)
                    {
                        return false;
                    }
                    if (element is Diamond)
                    {
                        game.Elements.Remove(element);
                        game.Elements.Add(new EmptySpot { X = x, Y = y });
                        game.IncreaseScore(100);
                        game.UpdateElementUI(element, new EmptySpot { X = x, Y = y });
                        return true;
                    }
                }
            }
            return true;
        }

        private void RemoveSandIfPresent(int x, int y, Game game)
        {
            GameElement sandElement = null;
            foreach (var element in game.Elements)
            {
                if (element.X == x && element.Y == y && element is Sand)
                {
                    sandElement = element;
                    break;
                }
            }

            if (sandElement != null)
            {
                game.Elements.Remove(sandElement);
                game.Elements.Add(new EmptySpot { X = x, Y = y });
                game.UpdateElementUI(sandElement, new EmptySpot { X = x, Y = y });
            }
        }
    }
}
