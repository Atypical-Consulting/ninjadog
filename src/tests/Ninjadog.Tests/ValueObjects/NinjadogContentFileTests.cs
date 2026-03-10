using Ninjadog.Engine.Core.ValueObjects;

namespace Ninjadog.Tests.ValueObjects;

public class NinjadogContentFileTests
{
    [Fact]
    public void Empty_IsEmptyTrue()
    {
        Assert.True(NinjadogContentFile.Empty.IsEmpty);
    }

    [Fact]
    public void Empty_HasEmptyContent()
    {
        Assert.Equal(string.Empty, NinjadogContentFile.Empty.Content);
    }

    [Fact]
    public void Empty_HasEmptyFileName()
    {
        Assert.Equal(string.Empty, NinjadogContentFile.Empty.FileName);
    }

    [Fact]
    public void Constructor_WithContent_IsEmptyFalse()
    {
        var file = new NinjadogContentFile("test.cs", "public class Foo { }");
        Assert.False(file.IsEmpty);
    }

    [Fact]
    public void Constructor_WithContent_WrapsInDefaultLayout()
    {
        var file = new NinjadogContentFile("test.cs", "public class Foo { }");
        Assert.Contains("Ninjadog Engine", file.Content);
        Assert.Contains("public class Foo { }", file.Content);
    }

    [Fact]
    public void Constructor_WithoutDefaultLayout_DoesNotAddHeader()
    {
        var file = new NinjadogContentFile("test.cs", "public class Foo { }", useDefaultLayout: false);
        Assert.DoesNotContain("Ninjadog Engine", file.Content);
        Assert.Equal("public class Foo { }", file.Content);
    }

    [Fact]
    public void Constructor_WithEmptyContent_SetsIsEmpty()
    {
        var file = new NinjadogContentFile("test.cs", string.Empty);
        Assert.True(file.IsEmpty);
        Assert.Equal(string.Empty, file.Content);
    }

    [Fact]
    public void Constructor_WithWhitespaceContent_SetsIsEmpty()
    {
        var file = new NinjadogContentFile("test.cs", "   ");
        Assert.True(file.IsEmpty);
    }

    [Fact]
    public void FileName_ReturnsProvidedName()
    {
        var file = new NinjadogContentFile("MyClass.cs", "code");
        Assert.Equal("MyClass.cs", file.FileName);
    }

    [Fact]
    public void Category_ReturnsProvidedCategory()
    {
        var file = new NinjadogContentFile("test.cs", "code", "Endpoints");
        Assert.Equal("Endpoints", file.Category);
    }

    [Fact]
    public void Category_DefaultsToNull()
    {
        var file = new NinjadogContentFile("test.cs", "code");
        Assert.Null(file.Category);
    }

    [Fact]
    public void Key_WithCategory_ReturnsCategorySlashFileName()
    {
        var file = new NinjadogContentFile("Get.cs", "code", "Endpoints");
        Assert.Equal("Endpoints/Get.cs", file.Key);
    }

    [Fact]
    public void Key_WithoutCategory_ReturnsFileName()
    {
        var file = new NinjadogContentFile("Get.cs", "code");
        Assert.Equal("Get.cs", file.Key);
    }

    [Fact]
    public void Length_ReturnsContentLength()
    {
        var file = new NinjadogContentFile("test.cs", "hello", useDefaultLayout: false);
        Assert.Equal(5, file.Length);
    }

    [Fact]
    public void Length_WhenEmpty_ReturnsZero()
    {
        var file = new NinjadogContentFile("test.cs", string.Empty);
        Assert.Equal(0, file.Length);
    }

    [Fact]
    public void DefaultCodeLayout_IncludesHeaderAndContent()
    {
        var result = NinjadogContentFile.DefaultCodeLayout("test code");
        Assert.Contains("Ninjadog Engine", result);
        Assert.Contains(NinjadogContentFile.CompanyName, result);
        Assert.Contains(NinjadogContentFile.DeveloperName, result);
        Assert.Contains("test code", result);
    }

    [Fact]
    public void DefaultCodeLayout_WithNullable_IncludesNullableDirectives()
    {
        var result = NinjadogContentFile.DefaultCodeLayout("test code", nullable: true);
        Assert.Contains("#nullable enable", result);
        Assert.Contains("#nullable disable", result);
    }

    [Fact]
    public void DefaultCodeLayout_WithoutNullable_OmitsNullableDirectives()
    {
        var result = NinjadogContentFile.DefaultCodeLayout("test code", nullable: false);
        Assert.DoesNotContain("#nullable enable", result);
        Assert.DoesNotContain("#nullable disable", result);
    }

    [Fact]
    public void Constants_AreCorrect()
    {
        Assert.Equal("Philippe Matray", NinjadogContentFile.DeveloperName);
        Assert.Equal("Atypical Consulting SRL", NinjadogContentFile.CompanyName);
    }
}
