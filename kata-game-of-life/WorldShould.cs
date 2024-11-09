using FluentAssertions;

namespace kata_game_of_life;

public class WorldShould
{
    private readonly World _world;
    public WorldShould()
    {
        var worldDimensions = 3;
        _world = World.CreateEmpty(worldDimensions);
        
    }

    [Fact]
    public void allow_to_add_cells()
    {
        var position = new Position(1, 1);

        _world.AddCellAt(position);

        _world.IsCellAliveAt(position).Should().BeTrue();
    }

    [Fact]
    public void cell_not_alive_in_current_position()
    {
        var position = new Position(1, 1);

        _world.IsCellAliveAt(position).Should().BeFalse();
    }
}

public record Position(int X, int Y);

public class World
{
    private readonly List<Position> _cells = [];
    public static World CreateEmpty(int worldDimensions) => new();

    public void AddCellAt(Position position)
    {
        _cells.Add(position);
    }

    public bool IsCellAliveAt(Position position) =>
        _cells.Contains(position);
}