using System.Runtime.InteropServices.JavaScript;
using FluentAssertions;
using LanguageExt;

namespace kata_game_of_life;

// 1. Any live cell with fewer than two live neighbours dies (referred to as underpopulation or exposure).
// 2. Any live cell with more than three live neighbours dies (referred to as overpopulation or overcrowding).
// 3. Any live cell with two or three live neighbours lives, unchanged, to the next generation.
// 4. Any dead cell with exactly three live neighbours will come to life.
public class CellShould
{
    [Fact]
    public void Reborn()
    {
        var neighbours = 3;
        var deadCell = Cell.CrateDead();

        deadCell.NextGeneration(neighbours).Match(
            error => error.Should().BeNull(),
            cell => cell.Should().Be(Cell.CrateAlive()));
    }

    [Fact]
    public void DieByUnderpopulation()
    {
        var neighbours = 1;
        var aliveCell = Cell.CrateAlive();

        aliveCell.NextGeneration(neighbours).Match(
            error => error.Should().BeNull(),
            cell => cell.Should().Be(Cell.CrateDead()));
    }
    
    [Fact]
    public void DieByOverpopulation()
    {
        var neighbours = 4;
        var aliveCell = Cell.CrateAlive();

        aliveCell.NextGeneration(neighbours).Match(
            error => error.Should().BeNull(),
            cell => cell.Should().Be(Cell.CrateDead()));
    }
    
    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public void Survive(int neighbours)
    {
        var aliveCell = Cell.CrateAlive();

        aliveCell.NextGeneration(neighbours).Match(
            error => error.Should().BeNull(),
            cell => cell.Should().Be(Cell.CrateAlive()));
    }
}

public record ErrorMessage(string Message);

public record Cell
{
    private readonly bool _isDead;

    private Cell(bool isDead)
    {
        _isDead = isDead;
    }

    public static Cell CrateDead()
    {
        return new Cell(false);
    }

    public Either<ErrorMessage, Cell> NextGeneration(int neighbours)
    {
        if (neighbours < 2) return CrateDead();
        if(neighbours > 3) return CrateDead();
        return CrateAlive();
    }

    public static Cell CrateAlive()
    {
        return new Cell(true);
    }
}