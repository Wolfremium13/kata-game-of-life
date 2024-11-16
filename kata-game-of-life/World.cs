namespace kata_game_of_life;

public class World
{
    private readonly int _worldDimensions;
    private readonly List<Cell> _currentGenerationAliveCells2 = [];

    private World(int worldDimensions)
    {
        _worldDimensions = worldDimensions;
    }

    public static World CreateEmpty(int worldDimensions) => new(worldDimensions);

    public void AddCellAt(Position position)
    {
        if (position.X >= _worldDimensions || position.Y >= _worldDimensions)
        {
            throw new ArgumentOutOfRangeException();
        }

        _currentGenerationAliveCells2.Add(Cell.Create(position));
    }

    public bool IsCellAliveAt(Position position) =>
        _currentGenerationAliveCells2.Any(cell => cell.Position == position);

    public void Tick()
    {
        var nextGenerationAliveCells2 = new List<Cell>();
        foreach (var aliveCell in _currentGenerationAliveCells2)
        {
            var neighbours = aliveCell.GetNeighbours();
            var aliveNeighbours = neighbours.Count(IsCellAliveAt);
            if (aliveNeighbours is 2 or 3)
            {
                nextGenerationAliveCells2.Add(Cell.Create(aliveCell.Position));
            }

            foreach (var neighbour in neighbours)
            {
                if (IsCellAliveAt(neighbour))
                    continue;

                var aliveNeighboursOfNeighbour = Cell.Create(neighbour).GetNeighbours().Count(IsCellAliveAt);
                if (aliveNeighboursOfNeighbour is 3)
                {
                    nextGenerationAliveCells2.Add(Cell.Create(neighbour));
                }
            }
        }

        _currentGenerationAliveCells2.Clear();
        _currentGenerationAliveCells2.AddRange(nextGenerationAliveCells2);
    }
}

public record Cell(Position Position)
{
    public static Cell Create(Position position) => new(position);

    public IEnumerable<Position> GetNeighbours()
    {
        yield return new Position(Position.X - 1, Position.Y - 1);
        yield return Position with { X = Position.X - 1 };
        yield return new Position(Position.X - 1, Position.Y + 1);
        yield return Position with { Y = Position.Y - 1 };
        yield return Position with { Y = Position.Y + 1 };
        yield return new Position(Position.X + 1, Position.Y - 1);
        yield return Position with { X = Position.X + 1 };
        yield return new Position(Position.X + 1, Position.Y + 1);
    }
}