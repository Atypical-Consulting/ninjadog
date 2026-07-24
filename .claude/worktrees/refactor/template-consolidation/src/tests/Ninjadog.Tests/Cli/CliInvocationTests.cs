using Ninjadog.CLI;

namespace Ninjadog.Tests.Cli;

public class CliInvocationTests
{
    [Fact]
    public void RequiresProjectSettings_InitCommand_ReturnsFalse()
    {
        Assert.False(CliInvocation.RequiresProjectSettings(["init"]));
    }

    [Fact]
    public void RequiresProjectSettings_AddEntityCommand_ReturnsFalse()
    {
        Assert.False(CliInvocation.RequiresProjectSettings(["add-entity", "Product"]));
    }

    [Fact]
    public void RequiresProjectSettings_BuildCommand_ReturnsTrue()
    {
        Assert.True(CliInvocation.RequiresProjectSettings(["build"]));
    }

    [Fact]
    public void RequiresProjectSettings_BuildHelp_ReturnsFalse()
    {
        Assert.False(CliInvocation.RequiresProjectSettings(["build", "--help"]));
    }
}
