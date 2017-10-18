using Maze.Library;
using System.Collections.Generic;
using System.Drawing;

namespace Maze.Solver
{
    /// <summary>
    /// Moves a robot from its current position towards the exit of the maze
    /// </summary>
    public class RobotController
    {
        private IRobot robot;
        private bool ready = false;
        //List of all visited Points
        private List<Point> visited = new List<Point>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RobotController"/> class
        /// </summary>
        /// <param name="robot">Robot that is controlled</param>
        public RobotController(IRobot robot)
        {
            // Store robot for later use
            this.robot = robot;
        }

        /// <summary>
        /// Moves the robot to the exit
        /// </summary>
        /// <remarks>
        /// This function uses methods of the robot that was passed into this class'
        /// constructor. It has to move the robot until the robot's event
        /// <see cref="IRobot.ReachedExit"/> is fired. If the algorithm finds out that
        /// the exit is not reachable, it has to call <see cref="IRobot.HaltAndCatchFire"/>
        /// and exit.
        /// </remarks>
        public void MoveRobotToExit()
        {
            //Starting Coordinates
            int x = 0;
            int y = 0;

            robot.ReachedExit += (_, __) => this.ready = true;

            this.moveOut(x, y);

            if (!this.ready)
            {
                robot.HaltAndCatchFire();
            }
        }

        //Backtracker 
        public void moveOut(int x, int y)
        {
            if(!visited.Contains(new Point(x, y)) && !ready ){
                if(robot.TryMove(Direction.Right))
                {
                    
                    visited.Add(new Point(x, y));
                    robot.Move(Direction.Right);
                    moveOut(x+1, y);
                    if (!ready)
                    {
                        robot.Move(Direction.Left);
                        x--;
                    }
                }
            }
            if (!visited.Contains(new Point(x, y)) && !ready)
            {
                if (robot.TryMove(Direction.Left) && !ready)
                {
                    
                    visited.Add(new Point(x, y));
                    robot.Move(Direction.Left);
                    moveOut(x--, y);
                    if (!ready)
                    {
                        robot.Move(Direction.Right);
                        x++;
                    }
                }
            }
            if (!visited.Contains(new Point(x, y)) && !ready)
            {
                if (robot.TryMove(Direction.Up) && !ready)
                {
                    
                    visited.Add(new Point(x, y));
                    robot.Move(Direction.Up);
                    moveOut(x, y--);
                    if (!ready)
                    {
                        robot.Move(Direction.Down);
                        y++;
                    }
                }
            }
            if (!visited.Contains(new Point(x, y)) && !ready)
            {
                if (robot.TryMove(Direction.Down) && !ready)
                {
                    visited.Add(new Point(x, y));
                    robot.Move(Direction.Down);
                    moveOut(x, y);
                    if (!ready)
                    {
                        robot.Move(Direction.Down);
                        y--;
                    }
                }

            }
        }
    }
}