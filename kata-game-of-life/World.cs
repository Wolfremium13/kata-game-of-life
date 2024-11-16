namespace kata_game_of_life;

public class World
{
    private readonly int _worldDimensions;
    private readonly List<Cell> _aliveCells = [];

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

        _aliveCells.Add(Cell.Create(position));
    }

    public bool IsCellAliveAt(Position position) =>
        _aliveCells.Any(cell => cell.Position == position);

    public void Tick()
    {
        var nextGenerationAliveCells = new List<Cell>();
        foreach (var aliveCell in _aliveCells)
        {
            var neighbours = aliveCell.GetNeighbours();
            if (aliveCell.GetAliveNeighbours(_aliveCells).Count() is 2 or 3)
            {
                nextGenerationAliveCells.Add(aliveCell);
            }

            foreach (var neighbour in neighbours)
            {
                if (IsCellAliveAt(neighbour.Position))
                    continue;

                var aliveNeighboursOfNeighbour = neighbour.GetAliveNeighbours(_aliveCells).Count();
                if (aliveNeighboursOfNeighbour is 3)
                {
                    nextGenerationAliveCells.Add(neighbour);
                }
            }
        }

        _aliveCells.Clear();
        _aliveCells.AddRange(nextGenerationAliveCells);
    }
}