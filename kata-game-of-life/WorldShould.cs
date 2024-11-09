using FluentAssertions;

namespace kata_game_of_life;

public class WorldShould
{
    [Fact]
    public void allow_to_add_cells()
    {
        var worldDimensions = 3;
        var world = World.CreateEmpty(worldDimensions);
        var position = new Position(1, 1);
        
        world.AddCellAt(position);

        world.IsCellAliveAt(position).Should().BeTrue();
    }

    [Fact]
    public void cell_not_alive_in_current_position()
    {
        var position = new Position(1, 1);
        var worldDimensions = 3;
        
        var world = World.CreateEmpty(worldDimensions);
        world.IsCellAliveAt(position).Should().BeFalse();
    }
}

public record Position
{
    public Position(int x, int y)
    {
    }
}

public class World
{
    public static World CreateEmpty(int worldDimensions) => new();

    public void AddCellAt(Position position)
    {
    }

    public bool IsCellAliveAt(Position position) => true;
}