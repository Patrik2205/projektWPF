using projektWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace projektWPF
{
    public class Game
    {
        public List<GameElement> Elements { get; set; } = new List<GameElement>();
        private DispatcherTimer timer;
        public event Action OnTickEvent;
        public event Action OnGameRestarted;
        public Player Player { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Score { get; private set; }

        public Game()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500); 
            timer.Tick += OnTick;
        }

        public void Start()
        {
            timer.Start();
        }

        public void RestartLevel()
        {
            timer.Stop();
            Elements = MapParser.ParseMap("map.txt", this);
            OnTickEvent?.Invoke();
            OnGameRestarted?.Invoke(); 
            timer.Start();
        }

        public void CompleteLevel()
        {
            ShowEndDialog("You won! What would you like to do?");
        }

        public void PlayerDied()
        {
            ShowEndDialog("You died. What would you like to do?");
        }

        private void ShowEndDialog(string message)
        {
            timer.Stop();

            var result = CustomMessageBox.Show(message, "Game Over");

            if (result == CustomMessageBox.CustomMessageBoxResult.Restart)
            {
                RestartLevel();
            }
            else if (result == CustomMessageBox.CustomMessageBoxResult.Quit)
            {
                Application.Current.Shutdown();
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            foreach (var element in Elements)
            {
                element.Update(this);
            }
            OnTickEvent?.Invoke();
        }

        public void IncreaseScore(int points)
        {
            Score += points;
        }

        public void UpdateElementUI(GameElement oldElement, GameElement newElement)
        {
            OnElementChanged?.Invoke(oldElement, newElement);
        }

        public event Action<GameElement, GameElement> OnElementChanged;
    }
}
