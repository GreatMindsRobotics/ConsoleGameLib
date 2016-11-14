using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGameLib.CoreTypes;
using ConsoleGameLib.PhysicsTypes;
using ConsoleGameLib;
using System.Threading;


namespace PhysicsPlatformer
{
    class Program
    {

        static void Main(string[] args)
        {
            PhysicsWorld world = new PhysicsWorld();
            List<PhysicsPoint> points = new List<PhysicsPoint>();

            Console.CursorVisible = false;

           

            world.GravitationalAcceleration = 1;

            world.LinearDragCalculationInterval = 1;

            UserControlledPoint userPoint = new UserControlledPoint(true,new Point(0,36),true,ConsoleColor.Blue,world);

            userPoint.UpDistance = 0;
            userPoint.RightDistance = 0;
            userPoint.LeftDistance = 0;

            userPoint.OnLegalRight += UserPoint_OnLegalRight;

            userPoint.OnLegalLeft += UserPoint_OnLegalLeft;


            userPoint.OnLegalUp += UserPoint_OnLegalUp;

            world.GravityCalculationInterval = 1;


            userPoint.MoveUpKey = ConsoleKey.Spacebar;
            userPoint.CanMoveDown = false;

            Random rand = new Random();


            for (int i = 0; i < 15; i++)
            {
                points.Add(new PhysicsPoint(false,new Point(i,35),true,ConsoleColor.Green,world));
            }
            Point tempPos = new Point(15,35);

            for (int j = 0; j < 10; j++)
            {

                int length = rand.Next(1, 10);

                tempPos.Y += rand.Next(0, 2);
                tempPos.X += rand.Next(1, 3);

                for (int i = 0; i < length; i++)
                {
                    points.Add(new PhysicsPoint(false, new Point(tempPos.X, tempPos.Y), true, ConsoleColor.Green, world));
                    tempPos.X++;
                }
            }
            /*for (int j = 0; j < 10; j++)
            {

                int length = rand.Next(1, 10);

                tempPos.Y += rand.Next(-1, 1);
                tempPos.X += rand.Next(1, 3);

                for (int i = 0; i < length; i++)
                {
                    points.Add(new PhysicsPoint(false, new Point(tempPos.X, tempPos.Y), true, ConsoleColor.Green, world));
                    tempPos.X++;
                }
            }*/

            world.UserPoint = userPoint;
            world.Contents = points;

            while(true)
            {
                
                //world.PhysicsCamera.DrawReferencePosition = new Point(/*world.UserPoint.Position.X - 3, 27*/0,0);

                world.Update();

                world.Draw();

                if (userPoint.Position.Y <= 10)
                {
                    userPoint.Position = new Point(0,36);
                }



                Thread.Sleep(50);

                


            }

        }

        private static void UserPoint_OnLegalLeft(object sender, EventArgs e)
        {
            ((UserControlledPoint)sender).Velocity.X-= 1;
        }

        private static void UserPoint_OnLegalRight(object sender, EventArgs e)
        {
            ((UserControlledPoint)sender).Velocity.X+= 1;
        }

        private static void UserPoint_OnLegalUp(object sender, EventArgs e)
        {
            UserControlledPoint sendPoint = (UserControlledPoint)sender;
            if (sendPoint.World.Contents.ContainsPoint(new Point(sendPoint.Position.X, sendPoint.Position.Y - 1)) && sendPoint.Velocity.Y == 0)
            {
                sendPoint.Velocity.Y += 3;
            }
        }
    }
}
