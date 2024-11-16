namespace kata_game_of_life;

public record Cell(Position Position)
{
    public static Cell Create(Position position) => new(position);

    public IEnumerable<Cell> GetAliveNeighbours(IEnumerable<Cell> aliveCells)
    {
        return GetNeighbours()
            .Where(neighbour => aliveCells.Any(cell => cell.Position == neighbour.Position));
    }

    public IEnumerable<Cell> GetNeighbours()
    {
        yield return Create(new Position(Position.X - 1, Position.Y - 1));
        yield return Create(Position with { X = Position.X - 1 });
        yield return Create(new Position(Position.X - 1, Position.Y + 1));
        yield return Create(Position with { Y = Position.Y - 1 });
        yield return Create(Position with { Y = Position.Y + 1 });
        yield return Create(new Position(Position.X + 1, Position.Y - 1));
        yield return Create(Position with { X = Position.X + 1 });
        yield return Create(new Position(Position.X + 1, Position.Y + 1));
    }
}