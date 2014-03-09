using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleGameLib.CoreTypes;

namespace ConsoleGameLib.Helpers
{
    public abstract class ConsoleSpriteCollection<T> : ICollection<T> where T: ConsoleSprite
    {
        private List<T> _sprites = new List<T>();


        /// <summary>
        /// Gets or sets a ConsoleSprite at a specified index in this ConsoleSpriteCollection
        /// </summary>
        /// <param name="index">Index at which to get or set a ConsoleSprite</param>
        /// <returns>ConsoleSprite at a specified index</returns>
        public T this[int index]
        {
            get { return _sprites[index]; }
            set { _sprites[index] = value; }
        }

        /// <summary>
        /// Updates all ConsoleSprites in this ConsoleSpriteCollection
        /// </summary>
        public virtual void Update()
        {
            foreach (T sprite in _sprites)
            {
                sprite.Update();
            }
        }

        /// <summary>
        /// Draws all ConsoleSprites in this ConsoleSpriteCollection
        /// </summary>
        public virtual void Draw()
        {
            foreach (T sprite in _sprites)
            {
                sprite.Draw();
            }        
        }

        #region ICollection Interface
        public void Add(T item)
        {
            _sprites.Add(item);
        }

        public void Clear()
        {
            _sprites.Clear();
        }

        public bool Contains(T item)
        {
            return _sprites.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _sprites.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _sprites.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return _sprites.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _sprites.AsEnumerable<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _sprites.GetEnumerator();
        }

        #endregion ICollection Interface

    }
}
