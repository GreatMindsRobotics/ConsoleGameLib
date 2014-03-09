using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleGameLib.CoreTypes;

namespace ConsoleGameLib.Fonts
{
    public abstract class Font : ConsoleSprite
    {
        protected byte[] _fontData;

        protected readonly Size _fontCharSize;
        public virtual Size FontCharSize
        {
            get { return _fontCharSize; }
        }

        protected string _text;
        public virtual string Text
        {
            get { return _text; }
            set { _text = value; }
        }


        protected bool _drawBlankLines;
        public virtual bool DrawBlankLines
        {
            get { return _drawBlankLines; }
            set { _drawBlankLines = value; }
        }

        protected bool _useOwnCharAsDrawChar;
        public virtual bool UseOwnCharAsDrawChar
        {
            get { return _useOwnCharAsDrawChar; }
            set { _useOwnCharAsDrawChar = value; }
        }

        public Font(Size fontCharSize, string drawChars, Point location, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
            : base(drawChars, location, foregroundColor, backgroundColor)
        {
            //Main ctor (call to base)

            //Set font size
            _fontCharSize = fontCharSize;

            //Default to full font draw and use specified draw char
            _drawBlankLines = true;
            _useOwnCharAsDrawChar = false;
        }

        public override void Draw()
        {
            if (_text == null)
            {
                return;
            }

            saveColors();

            StringBuilder debugText = new StringBuilder();
            List<KeyValuePair<char, byte[]>> textData = new List<KeyValuePair<char, byte[]>>();

            foreach (byte character in Encoding.ASCII.GetBytes(_text))
            {
                //Calculate position of data in the array
                int arrayPos = character * _fontCharSize.Height;

                //Get the data
                byte[] charData = new byte[_fontCharSize.Height];
                for (int i = 0, j = arrayPos; j < arrayPos + _fontCharSize.Height; i++, j++)
                {
                    charData[i] = _fontData[j];
                }

                textData.Add(new KeyValuePair<char, byte[]>((char)character, charData));
            }

            for (int charLine = 0; charLine < _fontCharSize.Height; charLine++)
            {
                StringBuilder text = new StringBuilder();

                for (int letters = 0; letters < textData.Count; letters++)
                {
                    for (int i = _fontCharSize.Width - 1; i >= 0; i--)
                    {
                        byte checkBit = (byte)Math.Pow(2, i);

                        if ((textData[letters].Value[charLine] & checkBit) == checkBit)
                        {
                            text.Append(_useOwnCharAsDrawChar ? textData[letters].Key.ToString() : DrawChars);
                        }
                        else
                        {
                            text.Append(" ");
                        }
                    }

                    text.Append(new String(' ', _fontCharSize.Width / 2));
                }

                if (isLineVisible(charLine))
                {
                    if (!(!_drawBlankLines && text.ToString().Trim().Length == 0))
                    {
                        drawLine(text.ToString(), charLine);
                    }
                }

                debugText.AppendLine(text.ToString());
            }

            restoreColors();
        }
    }
}
