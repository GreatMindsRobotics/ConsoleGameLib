using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace ConsoleGameLib.CoreTypes
{
    public abstract class Game
    {
        private Timer _gameTimer;

        protected double _targetFrameRate;
        public double TargetFrameRate
        {
            get { return _targetFrameRate; }
            set { _targetFrameRate = value; }
        }

        private static bool _isGameRunning;
        public static bool IsGameRunning
        {
            get { return _isGameRunning; }
            protected set { _isGameRunning = value; }
        }

        protected ConsoleColor _backgroundColor;
        public ConsoleColor BackGroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        protected ConsoleColor _foregroundColor;
        public ConsoleColor ForeGroundColor
        {
            get { return _foregroundColor; }
            set { _foregroundColor = value; }
        }

        /// <summary>
        /// Factory method for simple game start. Defaults to DarkBlue for background color and White for foreground color. 
        /// These color values are overwritten by each ConsoleSprite's color value for background and foreground color.
        /// </summary>
        /// <typeparam name="T">Type of game to run. Must be a subclass of Game</typeparam>
        public static void RunGame<T>() where T : Game, new()
        {
            RunGame<T>(ConsoleColor.White, ConsoleColor.DarkBlue);
        }

        /// <summary>
        /// Factory method for simple game start.
        /// </summary>
        /// <typeparam name="T">Type of game to run. Must be a subclass of Game</typeparam>
        /// <param name="backgroundColor">Default background color for the game. This value is overwritten by each ConsoleSprite's background color.</param>
        /// <param name="foregroundColor">Default foreground color for the game. This value is overwritten by each ConsoleSprite's foreground color.</param>
        public static void RunGame<T>(ConsoleColor foregroundColor, ConsoleColor backgroundColor) where T : Game, new()
        {
            new T().Run(foregroundColor, backgroundColor);

            while (IsGameRunning)
            {
                //Wait for game to exit
            }
        }

        public virtual void Run(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            _backgroundColor = backgroundColor;
            _foregroundColor = foregroundColor;

            //TODO: Default vars
            Console.SetBufferSize(80, 25);

            InitGame();

            _gameTimer = new Timer();
            _gameTimer.Interval = _targetFrameRate;
            _gameTimer.Elapsed += new ElapsedEventHandler(_gameTimer_Elapsed);
            _gameTimer.Enabled = true;

            _isGameRunning = true;
        }

        public virtual void InitGame()
        {
            _targetFrameRate = 1000.0 / 60.0;    //60 FPS by default

            Console.CursorVisible = false;
        }

        private void _gameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Update();
            Draw();
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            Console.BackgroundColor = _backgroundColor;
            Console.ForegroundColor = _foregroundColor;
            Console.Clear();
        }


    }
}
