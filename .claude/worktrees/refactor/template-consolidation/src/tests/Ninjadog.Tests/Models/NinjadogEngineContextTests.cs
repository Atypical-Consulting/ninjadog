using Ninjadog.Engine.Core.Models;
using Ninjadog.Engine.Core.ValueObjects;

namespace Ninjadog.Tests.Models;

public class NinjadogEngineContextTests
{
    [Fact]
    public void NewContext_HasZeroFilesGenerated()
    {
        var context = new NinjadogEngineContext();
        Assert.Equal(0, context.TotalFilesGenerated);
    }

    [Fact]
    public void NewContext_HasZeroCharactersGenerated()
    {
        var context = new NinjadogEngineContext();
        Assert.Equal(0, context.TotalCharactersGenerated);
    }

    [Fact]
    public void FileGenerated_IncrementsFileCount()
    {
        var context = new NinjadogEngineContext();
        var file = new NinjadogContentFile("test.cs", "hello", useDefaultLayout: false);

        context.FileGenerated(file);

        Assert.Equal(1, context.TotalFilesGenerated);
    }

    [Fact]
    public void FileGenerated_AccumulatesCharacterCount()
    {
        var context = new NinjadogEngineContext();
        var file1 = new NinjadogContentFile("a.cs", "hello", useDefaultLayout: false);
        var file2 = new NinjadogContentFile("b.cs", "world!", useDefaultLayout: false);

        context.FileGenerated(file1);
        context.FileGenerated(file2);

        Assert.Equal(2, context.TotalFilesGenerated);
        Assert.Equal(11, context.TotalCharactersGenerated);
    }

    [Fact]
    public void Reset_ClearsAllMetrics()
    {
        var context = new NinjadogEngineContext();
        var file = new NinjadogContentFile("test.cs", "hello", useDefaultLayout: false);
        context.FileGenerated(file);
        context.StartCollectMetrics();
        context.StopCollectMetrics();

        context.Reset();

        Assert.Equal(0, context.TotalFilesGenerated);
        Assert.Equal(0, context.TotalCharactersGenerated);
        Assert.Equal(TimeSpan.Zero, context.TotalTimeElapsed);
    }

    [Fact]
    public void StartAndStopCollectMetrics_TracksElapsedTime()
    {
        var context = new NinjadogEngineContext();
        context.StartCollectMetrics();
        Thread.Sleep(10);
        context.StopCollectMetrics();

        Assert.True(context.TotalTimeElapsed > TimeSpan.Zero);
    }

    [Fact]
    public void GetSnapshot_ReturnsCorrectValues()
    {
        var context = new NinjadogEngineContext();
        var file = new NinjadogContentFile("test.cs", "hello world test", useDefaultLayout: false);
        context.FileGenerated(file);

        var snapshot = context.GetSnapshot();

        Assert.Equal(1, snapshot.TotalFilesGenerated);
        Assert.Equal(16, snapshot.TotalCharactersGenerated);
    }

    [Fact]
    public void ErrorOccurred_DoesNotThrow()
    {
        var context = new NinjadogEngineContext();
        var ex = new InvalidOperationException("test");
        context.ErrorOccurred(ex);

        Assert.Equal(0, context.TotalFilesGenerated);
    }
}

public class NinjadogEngineContextSnapshotTests
{
    [Fact]
    public void TotalWordsGenerated_DividesCharactersBy5()
    {
        var snapshot = new NinjadogEngineContextSnapshot(1, 500, TimeSpan.Zero);
        Assert.Equal(100, snapshot.TotalWordsGenerated);
    }

    [Fact]
    public void TotalMinutesSaved_DividesCharactersBy150()
    {
        var snapshot = new NinjadogEngineContextSnapshot(1, 1500, TimeSpan.Zero);
        Assert.Equal(10, snapshot.TotalMinutesSaved);
    }

    [Fact]
    public void Snapshot_PreservesAllValues()
    {
        var elapsed = TimeSpan.FromSeconds(5);
        var snapshot = new NinjadogEngineContextSnapshot(10, 5000, elapsed);

        Assert.Equal(10, snapshot.TotalFilesGenerated);
        Assert.Equal(5000, snapshot.TotalCharactersGenerated);
        Assert.Equal(elapsed, snapshot.TotalTimeElapsed);
    }
}
