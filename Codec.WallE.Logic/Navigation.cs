using System.Linq;
namespace Codec.WallE.Logic
{
    public class Navigation
    {
        int currentX = 1;
        int currentY = 1;
        readonly int _xMAx;
        readonly int _yMAx;
        readonly string _commands;
        Direction currentDirection = Direction.North;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridSize">5x3 means : x: 5, y: 3</param>
        /// <param name="commands">Only acceps L,R,F</param>
        public Navigation(string gridSize, string commands)
        {
            ValidateGridSize(gridSize);
            ValidateCommands(commands);
            var gridSizes = ParseGridSize(gridSize);
           _xMAx = gridSizes[0];
           _yMAx = gridSizes[1];
           _commands = commands;
        }

        public string Run()
        {
            for (int i = 0; i < _commands.Length; i++)
            {
                switch (_commands[i])
                {
                    case 'F':
                        MoveForward();
                        break;
                    case 'L':
                        TurnLeft();
                        break;
                    case 'R':
                        TurnRight();
                        break;
                    default:
                        break;
                }
            }

            return $"{currentX},{currentY},{currentDirection.ToString()}";
        }
        private void ValidateGridSize(string gridSize)
        {
            if (string.IsNullOrEmpty(gridSize))
            {
                throw new ArgumentNullException("Please enter the size of the grid");
            }
            var splittedArray = gridSize.Split('x');
            var valid = true;
            if (splittedArray.Length != 2)
            {
                valid = false;
            }
            else if (!int.TryParse(splittedArray[0], out var x) || !int.TryParse(splittedArray[1], out var y))
            {
                valid = false;
            }
            else if (x < 1 || y < 1)
            {
                valid = false;
            }

            if (!valid)
            {
                throw new ArgumentException("Please enter a valid grid size.");
            }
           
        }
        private void ValidateCommands(string commands)
        {
            if (string.IsNullOrEmpty(commands))
            {
                throw new ArgumentNullException("Please enter et least one command");
            }
            foreach (var item in commands)
            {
                if (!"LRF".Contains(item))
                {
                    throw new ArgumentException("Please enter only R,L or F commands.");
                }
            }
        }
        private void TurnRight()
        {
            if (currentDirection == Direction.North)
            {
                currentDirection = Direction.East;
            }
            else if (currentDirection == Direction.South)
            {
                currentDirection = Direction.West;
            }
            else if (currentDirection == Direction.West)
            {
                currentDirection = Direction.North;
            }
            else
            {
                currentDirection = Direction.South;
            }
        }

        private void TurnLeft()
        {
            if (currentDirection == Direction.North)
            {
                currentDirection = Direction.West;
            }
            else if (currentDirection == Direction.South)
            {
                currentDirection = Direction.East;
            }
            else if (currentDirection == Direction.West)
            {
                currentDirection = Direction.South;
            }
            else
            {
                currentDirection = Direction.North;
            }
        }

        private void MoveForward()
        {
            if (currentDirection == Direction.North)
            {
                currentY = IgnoreIfItIsBoundry(currentY, currentY + 1, "y");
            }
            else if (currentDirection == Direction.South)
            {
                currentY = IgnoreIfItIsBoundry(currentY, currentY - 1, "y");
            }
            else if (currentDirection == Direction.East)
            {
                currentX = IgnoreIfItIsBoundry(currentX, currentX + 1, "x");
            }
            else
            {
                currentX = IgnoreIfItIsBoundry(currentX, currentX - 1, "x");
            }
        }

        private int IgnoreIfItIsBoundry(int step, int newStep, string axis)
        {
            if (newStep < 1 || (axis == "x" && newStep > _xMAx) || (axis == "y" && newStep > _yMAx))
            {
                return step;
            }

            return newStep;
        }

        private int[] ParseGridSize(string gridSize) => gridSize.Split('x').Select(x => Convert.ToInt32(x)).ToArray();

        enum Direction
        {
            North,
            South,
            West,
            East,

        }
    }
}