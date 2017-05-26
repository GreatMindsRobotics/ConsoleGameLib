using ConsoleGameLib.CoreTypes;
using ConsoleGameLib.PhysicsTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameLib
{
    public class PhysicsWorld
    {
        private List<PhysicsPoint> points;


               


        public List<PhysicsPoint> Contents
        {
            get { return points; }
            set { points = value; }

        }

        public UserControlledPoint UserPoint = null;

        //public Camera PhysicsCamera = new Camera();



        [Obsolete]
        public int ScreenHeight = 20;
        [Obsolete]
        public int ScreenWidth = 40;

        private Size screenSize;

        /// <summary>
        /// Updates between gravity calculations. 1 or below means calculate every update.
        /// </summary>
        public int GravityCalculationInterval = 1;

        /// <summary>
        /// Linear drag in the X direction.
        /// </summary>
        public int LinearDrag = 1;

        /// <summary>
        /// Updates between drag calculations. 1 or below means calculate every update.
        /// </summary>
        public int LinearDragCalculationInterval = 1;


        int currentGravUpdate = 0;

        int currentDragUpdate = 0;

        public int CurrentGravityUpdateInCycle
        {
            get { return currentGravUpdate; }
        }

        public int CurrentDragUpdateInCycle
        {
            get { return currentDragUpdate; }
        }

        public bool ClearKeyBufferInUpdate = true;


        public Size ScreenSize
        {
            get
            {
                return screenSize;
            }
            set
            {
                screenSize = value;
                Console.SetBufferSize(ScreenSize.Width, ScreenSize.Height);
            }
        }

        



        public int GravitationalAcceleration = 1;


        public int TerminalFallVelocity = -3;

        public PhysicsWorld()
            :this(new Size(Console.BufferWidth, Console.BufferHeight))
        {
            
        }

        public PhysicsWorld(Size screenSize)
        {
            ScreenSize = screenSize;
        }

        public void Update()
        {
            currentGravUpdate++;
            currentDragUpdate++;
            foreach (PhysicsPoint point in points)
            {
                point.World = this;
                point.Update();
                if (point.ObeysGravity)
                {
                    
                }
            }
            UserPoint.World = this;
            UserPoint.Update();

            while(Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
            if(currentGravUpdate >= GravityCalculationInterval)
            {
                currentGravUpdate = 0;
            }
            if (currentDragUpdate >= LinearDragCalculationInterval)
            {
                currentDragUpdate = 0;
            }

        }

        public void Draw()
        {
            Console.Clear();
            foreach (PhysicsPoint point in points)
            {
                //PhysicsPoint point = pt;
                //point.Position = new Point(point.Position.X - PhysicsCamera.DrawReferencePosition.X, point.Position.Y - PhysicsCamera.DrawReferencePosition.Y);
                if (point.Position.X >= 0 && point.Position.X <= ScreenSize.Width && point.Position.Y > 0 && point.Position.Y <= ScreenSize.Height)
                {
                    point.Draw();
                }
            }
            //UserControlledPoint userPoint = UserPoint;
            //userPoint.Position = new Point(userPoint.Position.X - PhysicsCamera.DrawReferencePosition.X, userPoint.Position.Y - PhysicsCamera.DrawReferencePosition.Y);
            if (UserPoint.Position.X >= 0 && UserPoint.Position.X <= ScreenSize.Width && UserPoint.Position.Y > 0 && UserPoint.Position.Y <= ScreenSize.Height)
            {
                UserPoint.Draw();
            }
            //if (PhysicsCamera.CursorPlacementPosition.X > 0 && PhysicsCamera.CursorPlacementPosition.X <= Console.BufferWidth && PhysicsCamera.CursorPlacementPosition.Y > 0 && PhysicsCamera.CursorPlacementPosition.Y <= Console.BufferHeight)
            //{
            //    Console.SetCursorPosition(PhysicsCamera.CursorPlacementPosition.X, PhysicsCamera.ViewSize.Height - PhysicsCamera.CursorPlacementPosition.Y);
            //}
            
        }

        
    }
}
