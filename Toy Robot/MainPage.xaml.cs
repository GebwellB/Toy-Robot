using System;
using Windows.UI.Xaml.Controls;

namespace Toy_Robot
{
    public sealed partial class MainPage : Page
    {
        bool robotSpawnable = true;
        bool firstRun = true;
        RobotGrid grid = new RobotGrid();
        Robot robot = null;

        public MainPage()
        {
            InitializeComponent();
        }

        private void spawnRobot_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (robotSpawnable)
            {
                gridContext.Text = "";
                robot = new Robot((0, 0), "north");
                gridContext.Text += "Robot spawned!\n";
                robotSpawnable = false;
            }
            else
            {
                gridContext.Text += "Robot already spawned\n";
            }
        }

        private void robotMovement_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                string moveDirection = robotMovement.Text;

                if (robotSpawnable)
                {
                    gridContext.Text = "";
                    gridContext.Text += "Spawn a robot first\n";
                }
                else
                {
                    if (firstRun)
                    {
                        gridContext.Text = "";
                        firstRun = false;
                    }

                    string moveResult = grid.MoveByCompassDirection(robot, moveDirection);
                    gridContext.Text += moveResult + "\n";
                }

                robotMovement.Text = "";
            }
        }

        // This follows a set of instructions set by the requirements. It doesn't allow direct user control as it takes no arguments, but can easily be changed to suit. I simply just didn't have enough time to implement input fields correctly
        private void automateRobot_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Spawn a fresh Robot, seperate to the one controlled via the GUI
            Robot automatedRobot = new Robot((0, 0), "north");
        }
    }
}
