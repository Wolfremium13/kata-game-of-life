namespace kata_game_of_life;

public class World
{
    private readonly int _worldDimensions;
    private readonly List<Position> _currentGenerationAliveCells = [];

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

        _currentGenerationAliveCells.Add(position);
    }

    public bool IsCellAliveAt(Position position) =>
        _currentGenerationAliveCells.Contains(position);

    public void Tick()
    {
        var nextGenerationAliveCells = new List<Position>();
        foreach (var cell in _currentGenerationAliveCells)
        {
            var neighbours = GetNeighbours(cell);
            var aliveNeighbours = neighbours.Count(IsCellAliveAt);
            if (aliveNeighbours is 2 or 3)
                nextGenerationAliveCells.Add(cell);
        }
        _currentGenerationAliveCells.Clear();
        _currentGenerationAliveCells.AddRange(nextGenerationAliveCells);
    }

    private static IEnumerable<Position> GetNeighbours(Position position)
    {
        yield return new Position(position.X - 1, position.Y - 1);
        yield return position with { X = position.X - 1 };
        yield return new Position(position.X - 1, position.Y + 1);
        yield return position with { Y = position.Y - 1 };
        yield return position with { Y = position.Y + 1 };
        yield return new Position(position.X + 1, position.Y - 1);
        yield return position with { X = position.X + 1 };
        yield return new Position(position.X + 1, position.Y + 1);
    }
}