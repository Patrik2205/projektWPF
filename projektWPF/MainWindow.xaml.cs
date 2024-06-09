using projektWPF.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace projektWPF
{
    public partial class MainWindow : Window
    {
        private Game game;
        private Dictionary<GameElement, Rectangle> elementToUI;
        private const int BlockSize = 40;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game();
            elementToUI = new Dictionary<GameElement, Rectangle>();

            game.Elements = MapParser.ParseMap("map.txt", game);
            game.OnElementChanged += OnElementChanged;
            game.OnGameRestarted += InitializeGameCanvas; // Update UI when the game restarts

            InitializeGameCanvas();
            game.OnTickEvent += UpdateGameCanvas;
            game.Start();
        }

        private void InitializeGameCanvas()
        {
            GameCanvas.Children.Clear();
            elementToUI.Clear();
            foreach (var element in game.Elements)
            {
                AddElementToUI(element);
            }
        }

        private void AddElementToUI(GameElement element)
        {
            Rectangle rect = new Rectangle
            {
                Width = BlockSize,
                Height = BlockSize,
                Fill = GetBrushForElement(element)
            };
            Canvas.SetLeft(rect, element.X * BlockSize);
            Canvas.SetTop(rect, element.Y * BlockSize);
            GameCanvas.Children.Add(rect);
            elementToUI[element] = rect;
        }

        private void RemoveElementFromUI(GameElement element)
        {
            if (elementToUI.TryGetValue(element, out Rectangle rect))
            {
                GameCanvas.Children.Remove(rect);
                elementToUI.Remove(element);
            }
        }

        private Brush GetBrushForElement(GameElement element)
        {
            string imagePath = string.Empty;

            if (element is Player)
            {
                imagePath = "Textures/player.png";
            }
            else if (element is Rock)
            {
                imagePath = "Textures/rock.png";
            }
            else if (element is Diamond)
            {
                imagePath = "Textures/diamond.png";
            }
            else if (element is Wall)
            {
                imagePath = "Textures/wall.png";
            }
            else if (element is Monster)
            {
                imagePath = "Textures/monster.png";
            }
            else if (element is Exit)
            {
                imagePath = "Textures/exit.png";
            }
            else if (element is Sand)
            {
                imagePath = "Textures/sand.png";
            }
            else if (element is EmptySpot)
            {
                return Brushes.Transparent;
            }

            if (!string.IsNullOrEmpty(imagePath))
            {
                ImageBrush brush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative))
                };
                return brush;
            }

            return Brushes.White;
        }

        private void UpdateGameCanvas()
        {
            foreach (var element in game.Elements)
            {
                if (elementToUI.TryGetValue(element, out Rectangle rect))
                {
                    Canvas.SetLeft(rect, element.X * BlockSize);
                    Canvas.SetTop(rect, element.Y * BlockSize);
                }
            }
            ScoreTextBlock.Text = $"Score: {game.Score}";
        }

        private void OnElementChanged(GameElement oldElement, GameElement newElement)
        {
            RemoveElementFromUI(oldElement);
            AddElementToUI(newElement);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    game.Player.Move(0, -1, game);
                    break;
                case Key.Down:
                    game.Player.Move(0, 1, game);
                    break;
                case Key.Left:
                    game.Player.Move(-1, 0, game);
                    break;
                case Key.Right:
                    game.Player.Move(1, 0, game);
                    break;
            }
            UpdateGameCanvas();
        }
    }
}
