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
}

public record Position
{
    public Position(int x, int y) => throw new NotImplementedException();
}

public class World
{
    public static World CreateEmpty(int worldDimensions) => throw new NotImplementedException();

    public void AddCellAt(Position position) => throw new NotImplementedException();

    public bool IsCellAliveAt(Position position) => throw new NotImplementedException();
}