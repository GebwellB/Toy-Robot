using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Toy_Robot
{
    public class Robot
    {
        public (int X, int Y) position { get; set; }

        public string? facingDirection { get; set; }

        public string report
        {
            get
            {
                string returnString = $"Robot's current positon: {position.ToString()}, Facing Direction: {facingDirection}";
                return returnString;
            }
        }

        /// <summary>
        /// Constructs a robot object to be placed on the grid.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="facingDirection"></param>
        public Robot((int, int)? position = null, string? facingDirection = null)
        {
            // Default to 0, 0 if no position is given during construction
            this.position = position ?? (0, 0);

            // Face north if no direction is gvien
            this.facingDirection = facingDirection ?? "north";
        }
    }
}
