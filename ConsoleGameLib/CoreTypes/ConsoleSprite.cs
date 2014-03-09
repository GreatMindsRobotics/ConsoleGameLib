using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGameLib.CoreTypes
{
    public class ConsoleSprite
    {
        //Temporary variables to save/restore console colors
        protected ConsoleColor? _saveCurrentForegroundColor = null;
        protected ConsoleColor? _saveCurrentBackgroundColor = null;

        private string _drawChars;
        public virtual string DrawChars
        {
            get { return _drawChars; }
            set
            {
                _drawChars = value;

                _size.Height = _drawChars.Count<char>(c => c == '\n') + 1;

                if (_size.Height > 1)
                {
                    string[] lines = _drawChars.Replace('\r'.ToString(), "").Split('\n');
                    _size.Width = lines.Max<string>(line => line.Length);
                }
                else
                {
                    _size.Width = _drawChars.Length;
                }                
            }
        }

        protected Point _location;
        public virtual Point Location
        {
            get { return _location; }
            set { _location = value; }
        }

        protected ConsoleColor? _foregroundColor;
        public virtual ConsoleColor? ForegroundColor
        {
            get { return _foregroundColor; }
            set { _foregroundColor = value; }
        }

        protected ConsoleColor? _backgroundColor;
        public virtual ConsoleColor? BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        protected Size _size;
        public virtual Size Size
        {
            get { return _size; }
            set { _size = value; }
        }

        protected bool _isVisible;
        public virtual bool IsVisible
        {
            get { return _isVisible && isLineVisible(); }
            set { _isVisible = value; }
        }

        protected bool isLineVisible(int lineNumber = 0)
        {
            return Location.X >= 0 && Location.X < Console.BufferWidth && Location.Y + lineNumber >= 0 && Location.Y + lineNumber < Console.BufferHeight;
        }

        public ConsoleSprite(string drawChars, Point location, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
        {
            //Set DrawChars via property, to calculate size
            DrawChars = drawChars;
            _location = location;
            _foregroundColor = foregroundColor;
            _backgroundColor = backgroundColor;

            //Default to visible
            _isVisible = true;
        }

        public ConsoleSprite(string drawChars, Point location)
            : this(drawChars, location, null, null)
        {
            //Pass-through Ctor
        }


        public virtual bool Intersects(ConsoleSprite sprite)
        {
            if ((_location.X <= sprite.Location.X && _location.X + _size.Width - 1 >= sprite.Location.X) &&
               (_location.Y <= sprite.Location.Y && _location.Y + _size.Height - 1 >= sprite.Location.Y))
            {
                return true;
            }

            return false;
        }


        public virtual void Update()
        {

        }

        protected virtual void saveColors()
        {
            if (_foregroundColor.HasValue)
            {
                //Save old value
                _saveCurrentForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = _foregroundColor.Value;
            }

            if (_backgroundColor.HasValue)
            {
                //Save old value
                _saveCurrentBackgroundColor = Console.BackgroundColor;
                Console.BackgroundColor = _backgroundColor.Value;
            }        
        }

        protected virtual void restoreColors()
        {
            if (_saveCurrentForegroundColor.HasValue)
            {
                Console.ForegroundColor = _saveCurrentForegroundColor.Value;
            }

            if (_saveCurrentBackgroundColor.HasValue)
            {
                Console.BackgroundColor = _saveCurrentBackgroundColor.Value;
            }
        }

        protected void drawLine(string line, int lineNumber = 0)
        {
            Console.SetCursorPosition(Location.X, Location.Y + lineNumber);
            Console.Write(line);            
        }

        public virtual void Draw()
        {
            saveColors();

            //Set position and draw; unless position is invalid (off-screen draw)
            if (IsVisible)
            {
                if (_size.Height == 1)
                {
                    drawLine(_drawChars);
                }
                else
                { 
                    //draw each line separately
                    string[] lines = _drawChars.Split('\n');

                    for (int lineNum = 0; lineNum < lines.Length; lineNum++)
                    {
                        string line = lines[lineNum];
                        if (isLineVisible(lineNum))
                        {
                            drawLine(line.Replace('\r', ' '), lineNum);
                        }
                    }
                }
            }

            restoreColors();
        }
    }
}
