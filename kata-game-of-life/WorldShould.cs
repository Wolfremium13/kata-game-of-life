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
    public void cells_are_not_alive_by_default()
    {
        var positionWithoutCell = new Position(1, 1);

        _world.IsCellAliveAt(positionWithoutCell).Should().BeFalse();
    }

    [Fact]
    public void not_allow_to_add_cells_outside_of_world_dimensions()
    {
        var outsideWorldPosition = new Position(8, 8);

        Action addCellOutsideWorld = () => _world.AddCellAt(outsideWorldPosition);

        addCellOutsideWorld.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void any_live_cell_with_fewer_than_two_live_neighbours_dies()
    {
        var position = new Position(1, 1);
        _world.AddCellAt(position);

        _world.Tick();

        _world.IsCellAliveAt(position).Should().BeFalse();
    }
}