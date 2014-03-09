using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleGameLib.CoreTypes;
using ConsoleSnake.CoreTypes;

namespace ConsoleSnake
{
    public class SnakeGame : Game
    {
        private Snake _snake;
        private Food _food;
        private Random _random;

        //TODO: Gamefield object

        public override void InitGame()
        {
            base.InitGame();

            //Test
            ConsoleGameLib.Fonts.SVGALibTFont font = new ConsoleGameLib.Fonts.SVGALibTFont(new Point(15, 8));
            font.Text = "Snake";
            font.DrawBlankLines = false;
            font.ForegroundColor = ConsoleColor.Red;
            font.UseOwnCharAsDrawChar = true;
            font.Draw();

            Console.WriteLine("\nPress any key to start...");
            Console.ReadKey();

            _targetFrameRate = 1000.0 / 15.0;       //15 FPS

            //DEBUG
            //_targetFrameRate = 1000.0 * 60.0;       //1 Frame per minute

            _snake = new Snake(new Point(20, 10), Direction.Right);
            _snake.ForegroundColor = ConsoleColor.White;

            _random = new Random();

            _food = new Food(new Point(_random.Next(1, 79), _random.Next(1, 25)));

            //Start with 3 pieces (head + 2)
            _snake.Grow(2);

        }

        public override void Update()
        {
            base.Update();

            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (_snake.CurrentDirection != Direction.Down)
                        {
                            _snake.CurrentDirection = Direction.Up;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (_snake.CurrentDirection != Direction.Up)
                        {
                            _snake.CurrentDirection = Direction.Down;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        if (_snake.CurrentDirection != Direction.Right)
                        {
                            _snake.CurrentDirection = Direction.Left;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        if (_snake.CurrentDirection != Direction.Left)
                        {
                            _snake.CurrentDirection = Direction.Right;
                        }
                        break;

                    case ConsoleKey.Escape:
                        IsGameRunning = false;
                        break;
                }
            }

            //Check if the snake ate food
            if (_snake.Intersects(_food))
            {
                _food.Location = new Point(_random.Next(1, 79), _random.Next(1, 25));
                _snake.Grow();
            }

            //IsVisible property of ConsoleSprite returns false if the character runs offscreen (that is, outside of screen buffer size)
            //We can use that to easily check for "wall collision"
            if (!_snake[0].IsVisible)
            { 
                //TODO: Game Over!
            }

            _snake.Update();
        }

        public override void Draw()
        {
            base.Draw();

            _snake.Draw();
            _food.Draw();
        }
    }
}
