namespace kata_game_of_life;

public class World
{
    private readonly int _worldDimensions;
    private readonly List<Position> _aliveCells = [];

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

        _aliveCells.Add(position);
    }

    public bool IsCellAliveAt(Position position) =>
        _aliveCells.Contains(position);

    public void Tick()
    {
        var nextGenerationAliveCells = new List<Position>();
        foreach (var cell in _aliveCells)
        {
            var neighbours = GetNeighbours(cell);
            var aliveNeighbours = neighbours.Count(IsCellAliveAt);
            if (aliveNeighbours < 2)
            {
                continue;
            }
            
            if (aliveNeighbours > 3)
            {
                continue;
            }

            nextGenerationAliveCells.Add(cell);
        }

        _aliveCells.Clear();
        _aliveCells.AddRange(nextGenerationAliveCells);
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