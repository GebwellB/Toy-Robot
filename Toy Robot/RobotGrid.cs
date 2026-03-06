using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI.Xaml.Documents;

namespace Toy_Robot
{
    public class RobotGrid
    {
        // I hate this layout. 0, 0 should be the middle, not south west. Disgusting.
        private List<(int X, int Y)> gridCoordinates =
        [
            (0,4), (1,4), (2,4), (3,4), (4,4),
            (0,3), (1,3), (2,3), (3,3), (4,3),
            (0,2), (1,2), (2,2), (3,2), (4,2),
            (0,1), (1,1), (2,1), (3,1), (4,1),
            (0,0), (1,0), (2,0), (3,0), (4,0)
        ];

        private List<string> validMoves = new List<string> { "north", "south", "east", "west", "move" };

        /// <summary>
        /// Checks if the coordinates exist in the grid, then moves the robot to that location. Does not change direction, simply places it at X, Y positions
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="Xpos"></param>
        /// <param name="Ypos"></param>
        public void MoveByCoordinates(Robot robot, int Xpos, int Ypos)
        {
            if (gridCoordinates.Contains((Xpos, Ypos)))
            {
                robot.position = (Xpos, Ypos);
                Debug.WriteLine($"Move is valid! Moving Robot to: X: {Xpos}, Y: {Ypos}");
            }
        }

        /// <summary>
        /// This function controls moving the robot North, South, East or west and changes it's direction based on which way it moved
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="moveCommand"></param>
        /// <returns></returns>
        private string MoveRobot(Robot robot, string moveCommand)
        {
            string robotFacingDirection = robot.facingDirection.ToLower();
            moveCommand = moveCommand.ToLower();

            if (validMoves.Contains(moveCommand))
            {
                if ((robotFacingDirection == "north" && moveCommand == "move") || moveCommand == "north")
                {
                    if (gridCoordinates.Contains((robot.position.X, robot.position.Y + 1)))
                    {
                        robot.position = (robot.position.X, robot.position.Y + 1);
                        Debug.WriteLine("Moving North!");
                        robot.facingDirection = "North";
                        return "Moving North!";
                    }
                    else
                    {
                        Debug.WriteLine("Cannot move North, would fall off the grid!");
                        return "Cannot move North, would fall off the grid!";
                    }
                }
                else if ((robotFacingDirection == "south" && moveCommand == "move") || moveCommand == "south")
                {
                    if (gridCoordinates.Contains((robot.position.X, robot.position.Y - 1)))
                    {
                        robot.position = (robot.position.X, robot.position.Y - 1);
                        Debug.WriteLine("Moving South!");
                        robot.facingDirection = "South";
                        return "Moving South!";
                    }
                    else
                    {
                        Debug.WriteLine("Cannot move South, would fall off the grid!");
                        return "Cannot move South, would fall off the grid!";
                    }
                }
                else if ((robotFacingDirection == "west" && moveCommand == "move") || moveCommand == "west")
                {
                    if (gridCoordinates.Contains((robot.position.X - 1, robot.position.Y)))
                    {
                        robot.position = (robot.position.X - 1, robot.position.Y);
                        Debug.WriteLine("Moving West!");
                        robot.facingDirection = "West";
                        return "Moving West!";
                    }
                    else
                    {
                        Debug.WriteLine("Cannot move West, would fall off the grid!");
                        return "Cannot move West, would fall off the grid!";
                    }
                }
                else if ((robotFacingDirection == "east" && moveCommand == "move") || moveCommand == "east")
                {
                    if (gridCoordinates.Contains((robot.position.X + 1, robot.position.Y)))
                    {
                        robot.position = (robot.position.X + 1, robot.position.Y);
                        Debug.WriteLine("Moving East!");
                        robot.facingDirection = "East";
                        return "Moving East!";
                    }
                    else
                    {
                        Debug.WriteLine("Cannot move East, would fall off the grid!");
                        return "Cannot move East, would fall off the grid!";
                    }
                }
                else
                {
                    Debug.WriteLine("No valid moves found");
                    return "No valid moves found";
                }
            }
            else
            {
                Debug.WriteLine("No valid moves found");
                return "No valid moves found";
            }
        }

        /// <summary>
        /// Accepts only "move", which will move the robot one place in the direction that it is facing
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public string MoveRobotByCommand(Robot robot, string move)
        {
            string parsedDirection = move.ToLower();
            
            if (parsedDirection == "move")
            {
                string currentFacingDirection = robot.facingDirection.ToLower();
                string result = MoveRobot(robot, currentFacingDirection);
                return result;
            }
            else
            {
                Debug.WriteLine($"Move command only accepts 'move'");
                return $"Move command only accepts 'move'";
            }
        }

        /// <summary>
        /// Rotates the robot left or right, based on input. Accepts 'left' or 'right' as input.
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="turnedDirection"></param>
        /// <returns></returns>
        public string RotateRobot(Robot robot, string turnedDirection)
        {
            string currentFacingDirection = robot.facingDirection.ToLower();
            string parsedFacingDirection = turnedDirection.ToLower();
            string newFacingDirection = "";
            bool validFacing = false;

            // God these if statements are so fking messy. If I get time, I'll come back and refactor them, but right now, it's "get it to work"
            if (turnedDirection == "left")
            {
                if(currentFacingDirection == "north")
                {
                    newFacingDirection = "west";
                    validFacing = true;
                }
                else if(currentFacingDirection == "west")
                {
                    newFacingDirection = "south";
                    validFacing = true;
                }
                else if (currentFacingDirection == "south")
                {
                    newFacingDirection = "east";
                    validFacing = true;
                }
                else if (currentFacingDirection == "east")
                {
                    newFacingDirection = "north";
                    validFacing = true;
                }
                else
                {
                    Debug.WriteLine("Failed to rotate left");
                }
            }
            else if (turnedDirection == "right")
            {
                if (currentFacingDirection == "north")
                {
                    newFacingDirection = "east";
                    validFacing = true;
                }
                else if (currentFacingDirection == "east")
                {
                    newFacingDirection = "south";
                    validFacing = true;
                }
                else if (currentFacingDirection == "south")
                {
                    newFacingDirection = "west";
                    validFacing = true;
                }
                else if (currentFacingDirection == "west")
                {
                    newFacingDirection = "north";
                    validFacing = true;
                }
                else
                {
                    Debug.WriteLine("Failed to rotate right");
                }
            }
            else
            {
                Debug.WriteLine($"'{turnedDirection}' is not a valid direction to turn. Accepts only: 'left' or 'right'");
                return $"'{turnedDirection}' is not a valid direction to turn. Accepts only: 'left' or 'right'";
            }

            if (validFacing)
            {
                robot.facingDirection = newFacingDirection;
                Debug.WriteLine($"Successfully Rotated the robot to the {parsedFacingDirection}. It is now facing: {robot.facingDirection}");
                return $"Successfully Rotated the robot to the {parsedFacingDirection}. It is now facing: {robot.facingDirection}";
            }

            // This should be impossible to get to... I think.
            return "How did you even get here?";
        }
    }
}
