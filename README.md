# Toy Robot
A solution by Ben Timewell

## This application has two parts:
### A GUI:
On first load, you'll see a very bare GUI, a text box and two buttons. The first button spawns a single robot into the grid. You can then send it commands via the text field. This text field ONLY accepts 'north', 'south', 'east', 'west'. I may come back to this and fix it so it can accept all commands

### Automated Tests:
The second button, is for automated tests. Once pressed, check out the debug log to view the results. They're a very basic Unit Test, but they run through random movements, invalid entries, rotations, "move" commands as well as example tests, found in [Instructions.cs](https://github.com/GebwellB/Toy-Robot/blob/main/Toy%20Robot/Instructions.cs)