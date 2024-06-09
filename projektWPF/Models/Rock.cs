using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projektWPF.Models
{
    public class Rock : GameElement
    {
        public override void Update(Game game)
        {
            Fall(game);
        }

        private void Fall(Game game)
        {
            int belowY = Y + 1;

            if (IsPlayerAtPosition(X, belowY, game))
            {
                game.PlayerDied();
                return;
            }

            if (IsValidMove(X, belowY, game))
            {
                Y = belowY;
            }
            else if (IsRockUnder(X, Y + 1, game) && (CanRollLeft(game) || CanRollRight(game)))
            {
                if (CanRollLeft(game))
                {
                    MoveLeft(belowY, game);
                }
                else if (CanRollRight(game))
                {
                    MoveRight(belowY, game);
                }
            }
        }

        private bool CanRollLeft(Game game)
        {
            return IsEmptySpot(X - 1, Y, game) && IsEmptySpot(X - 1, Y + 1, game);
        }

        private bool CanRollRight(Game game)
        {
            return IsEmptySpot(X + 1, Y, game) && IsEmptySpot(X + 1, Y + 1, game);
        }

        private void MoveLeft(int belowY, Game game)
        {
            if (IsPlayerAtPosition(X - 1, belowY, game) || IsPlayerAtPosition(X - 1, Y, game))
            {
                game.PlayerDied();
            }
            else
            {
                X -= 1;
                Y = belowY;
            }
        }

        private void MoveRight(int belowY, Game game)
        {
            if (IsPlayerAtPosition(X + 1, belowY, game) || IsPlayerAtPosition(X + 1, Y, game))
            {
                game.PlayerDied();
            }
            else
            {
                X += 1;
                Y = belowY;
            }
        }

        private bool IsValidMove(int x, int y, Game game)
        {
            if (x < 0 || x >= game.Width || y < 0 || y >= game.Height)
            {
                return false;
            }

            foreach (var element in game.Elements)
            {
                if (element.X == x && element.Y == y)
                {
                    return element is EmptySpot;
                }
            }

            return true;
        }

        private bool IsEmptySpot(int x, int y, Game game)
        {
            foreach (var element in game.Elements)
            {
                if (element.X == x && element.Y == y)
                {
                    return element is EmptySpot || element is Player;
                }
            }

            return false;
        }

        private bool IsPlayerAtPosition(int x, int y, Game game)
        {
            var player = game.Elements.OfType<Player>().FirstOrDefault();
            return player != null && player.X == x && player.Y == y;
        }

        private bool IsRockUnder(int x, int y, Game game)
        {
            return game.Elements.OfType<Rock>().Any(rock => rock.X == x && rock.Y == y);
        }
    }
}
