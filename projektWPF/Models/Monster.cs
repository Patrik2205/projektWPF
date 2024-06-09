using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projektWPF.Models
{
    enum Directions {Up, Down, Right, Left}

    public class Monster : GameElement
    {
        public override void Update(Game game)
        {
            Move(game);
        }

        //tohle snad funguje dobře, protože znova se v tom nehodlám hrabat
        private void Move(Game game)
        {
            Player player = game.Elements.OfType<Player>().FirstOrDefault();
            if (player == null) return;

            int dx = 0;
            int dy = 0;

            if (direction == Directions.Up) { 
                if (isBorderBlock(X + 1, Y, game)) { // mám po právé straně block?
                    if (IsValidMove(X, Y - 1, game)) { //mám před sebou volno?
                        direction = Directions.Up;
                        Y += -1;
                    } else {
                        if(IsValidMove(X - 1, Y, game)) { //pokud ne, mám nalevo od sebe volno?
                            direction = Directions.Left;
                            X += -1;
                        } else { //pokud ne, jdu dozadu
                            if (IsValidMove(X, Y + 1, game)) { //můžu jít dozadu?
                                direction = Directions.Down;
                                Y += 1; 
                            }
                        }
                    }
                } else {
                    direction = Directions.Right;
                    X += 1;
                }
            } else if (direction == Directions.Down) {
                if (isBorderBlock(X - 1, Y, game)) { // mám po právé straně block?
                    if (IsValidMove(X, Y + 1, game)) { //mám před sebou volno?
                        direction = Directions.Down;
                        Y += 1;
                    } else {
                        if (IsValidMove(X + 1, Y, game)) { //pokud ne, mám nalevo od sebe volno?
                            direction = Directions.Right;
                            X += 1;
                        } else { //pokud ne, jdu nahoru
                            if (IsValidMove(X, Y - 1, game)) { //můžu jít nahoru?
                                direction = Directions.Up;
                                Y += -1;
                            }
                        }
                    }
                } else {
                    direction = Directions.Left; 
                    X -= 1;
                }
            } else if (direction == Directions.Right) {
                if (isBorderBlock(X, Y + 1, game)) { // mám po právé straně block?
                    if (IsValidMove(X + 1, Y, game)) { //mám před sebou volno?
                        direction = Directions.Right;
                        X += 1;
                    } else {
                        if (IsValidMove(X, Y - 1, game)) { //pokud ne, mám nalevo od sebe volno?
                            direction = Directions.Up;
                            Y += -1;
                        } else { //pokud ne, jdu dozadu
                            if (IsValidMove(X - 1, Y, game)) { //můžu jít dozadu?
                                direction = Directions.Left;
                                X += -1;
                            }
                        }
                    }
                } else {
                    direction = Directions.Down;
                    Y += 1;
                }
            } else {
                if (isBorderBlock(X, Y - 1, game)) { // mám po právé straně block?
                    if (IsValidMove(X - 1, Y, game)) { //mám před sebou volno?
                        direction = Directions.Left;
                        X += -1;
                    } else {
                        if (IsValidMove(X, Y + 1, game)) { //pokud ne, mám nalevo od sebe volno?
                            direction = Directions.Down;
                            Y += 1;
                        } else { //pokud ne, jdu dozadu
                            if (IsValidMove(X + 1, Y, game)) { //můžu jít dozadu?
                                direction = Directions.Right;
                                X += 1;
                            }
                        }
                    }
                } else {
                    direction = Directions.Up;
                    Y += -1;
                }
            }

            if (X == player.X && Y == player.Y)
            {
                game.PlayerDied();
            }
        }

        private bool isBorderBlock(int x, int y, Game game) {
            if (x < 0 || x >= game.Width || y < 0 || y >= game.Height) {
                return false;
            }

            foreach (var element in game.Elements)
            {
                if (element.X == x && element.Y == y) {
                    if (element is EmptySpot || element is Player) {
                        return false;
                    } else {
                        return true;
                    }
                }
            }

            return false;
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
                    return element is EmptySpot || element is Player;
                }
            }

            return true;
        }

        private Directions direction = Directions.Right;
    }
}
