namespace kata_game_of_life;

public class World
{
    private readonly int _worldDimensions;
    private readonly List<Position> _cells = [];

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

        _cells.Add(position);
    }

    public bool IsCellAliveAt(Position position) =>
        _cells.Contains(position);

    public void Tick()
    {
        _cells.Clear();
    }
}