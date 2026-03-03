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
        public List<(int X, int Y)> gridCoordinates =
        [
            (0,4), (1,4), (2,4), (3,4), (4,4),
            (0,3), (1,3), (2,3), (3,3), (4,3),
            (0,2), (1,2), (2,2), (3,2), (4,2),
            (0,1), (1,1), (2,1), (3,1), (4,1),
            (0,0), (1,0), (2,0), (3,0), (4,0)
        ];

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
        /// Moves the robot by taking a string based on the direction, this checks if the move is valid before moving the robot.
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="direction"></param>
        public string MoveByCompassDirection(Robot robot, string direction)
        {
            string parsedDirection = direction.ToLower();
            (int currentXpos, int currentYpos) = robot.position;
            int newXpos = currentXpos;
            int newYpos = currentYpos;
            bool validMove = false;
            string directionOfMove = "";

            // I love me a good long list of if statements. It makes sense in my mind, but this is definetly a "Get it working first" type solution.
            if (parsedDirection == "north" || parsedDirection == "n")
            {
                if (gridCoordinates.Contains((currentXpos, currentYpos + 1)))
                {
                    newYpos = currentYpos + 1;
                    Debug.WriteLine("Moving North!");
                    validMove = true;
                    directionOfMove = "North";
                }
                else
                {
                    Debug.WriteLine("Cannot move North, would fall off the grid!");
                    return "Cannot move North, would fall off the grid!";
                }
            }
            else if (parsedDirection == "south" || parsedDirection == "s")
            {
                if (gridCoordinates.Contains((currentXpos, currentYpos - 1)))
                {
                    newYpos = currentYpos - 1;
                    Debug.WriteLine("Moving South!");
                    validMove = true;
                    directionOfMove = "South";
                }
                else
                {
                    Debug.WriteLine("Cannot move South, would fall off the grid!");
                    return "Cannot move South, would fall off the grid!";
                }
            }
            else if (parsedDirection == "west" || parsedDirection == "w")
            {
                if (gridCoordinates.Contains((currentXpos - 1, currentYpos)))
                {
                    newXpos = currentXpos - 1;
                    Debug.WriteLine("Moving West!");
                    validMove = true;
                    directionOfMove = "West";
                }
                else
                {
                    Debug.WriteLine("Cannot move West, would fall off the grid!");
                    return "Cannot move West, would fall off the grid!";
                }
            }
            else if (parsedDirection == "east" || parsedDirection == "e")
            {
                if (gridCoordinates.Contains((currentXpos + 1, currentYpos)))
                {
                    newXpos = currentXpos + 1;
                    Debug.WriteLine("Moving East!");
                    validMove = true;
                    directionOfMove = "East";
                }
                else
                {
                    Debug.WriteLine("Cannot move East, would fall off the grid!");
                    return "Cannot move East, would fall off the grid!";
                }
            }
            else
            {
                Debug.WriteLine($"Invalid move '{direction}'. Can only accept: 'north', 'south', 'east' or 'west'");
                return $"Invalid move '{direction}'. Can only accept: 'north', 'south', 'east' or 'west'";
            }

            if (validMove)
            {
                robot.position = (newXpos, newYpos);
                robot.facingDirection = directionOfMove;
                return $"Moved {directionOfMove}! Now facing: {robot.facingDirection}";
            }

            // This should be impossible to get to... I think.
            return "How did you even get here?";
        }

        public string RotateRobot(Robot robot, string direction)
        {
            // I'll come back to this.
            return "Yes";
        }
    }
}
