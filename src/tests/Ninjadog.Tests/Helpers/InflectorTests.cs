using Ninjadog.Helpers;

namespace Ninjadog.Tests.Helpers;

public class InflectorTests
{
    [Theory]
    [InlineData("product", "products")]
    [InlineData("category", "categories")]
    [InlineData("status", "statuses")]
    [InlineData("bus", "buses")]
    [InlineData("octopus", "octopi")]
    [InlineData("virus", "viri")]
    [InlineData("alias", "aliases")]
    [InlineData("buffalo", "buffaloes")]
    [InlineData("tomato", "tomatoes")]
    [InlineData("datum", "data")]
    [InlineData("analysis", "analyses")]
    [InlineData("half", "halves")]
    [InlineData("hive", "hives")]
    [InlineData("query", "queries")]
    [InlineData("box", "boxes")]
    [InlineData("church", "churches")]
    [InlineData("matrix", "matrices")]
    [InlineData("mouse", "mice")]
    [InlineData("ox", "oxen")]
    [InlineData("quiz", "quizzes")]
    public void Pluralize_ReturnsCorrectPluralForm(string singular, string expected)
    {
        Assert.Equal(expected, singular.Pluralize());
    }

    [Theory]
    [InlineData("products", "product")]
    [InlineData("categories", "category")]
    [InlineData("statuses", "status")]
    [InlineData("buses", "bus")]
    [InlineData("octopi", "octopus")]
    [InlineData("aliases", "alias")]
    [InlineData("analyses", "analysis")]
    [InlineData("halves", "half")]
    [InlineData("hives", "hive")]
    [InlineData("queries", "query")]
    [InlineData("boxes", "box")]
    [InlineData("churches", "church")]
    [InlineData("matrices", "matrix")]
    [InlineData("mice", "mouse")]
    [InlineData("oxen", "ox")]
    [InlineData("quizzes", "quiz")]
    public void Singularize_ReturnsCorrectSingularForm(string plural, string expected)
    {
        Assert.Equal(expected, plural.Singularize());
    }

    [Theory]
    [InlineData("person", "people")]
    [InlineData("man", "men")]
    [InlineData("child", "children")]
    [InlineData("goose", "geese")]
    public void Pluralize_HandlesIrregularWords(string singular, string expected)
    {
        Assert.Equal(expected, singular.Pluralize());
    }

    [Theory]
    [InlineData("people", "person")]
    [InlineData("men", "man")]
    [InlineData("children", "child")]
    [InlineData("geese", "goose")]
    public void Singularize_HandlesIrregularWords(string plural, string expected)
    {
        Assert.Equal(expected, plural.Singularize());
    }

    [Theory]
    [InlineData("equipment")]
    [InlineData("information")]
    [InlineData("rice")]
    [InlineData("money")]
    [InlineData("species")]
    [InlineData("series")]
    [InlineData("fish")]
    [InlineData("sheep")]
    [InlineData("deer")]
    [InlineData("aircraft")]
    public void Pluralize_ReturnsUncountableWordsUnchanged(string word)
    {
        Assert.Equal(word, word.Pluralize());
    }

    [Theory]
    [InlineData("equipment")]
    [InlineData("information")]
    [InlineData("sheep")]
    public void Singularize_ReturnsUncountableWordsUnchanged(string word)
    {
        Assert.Equal(word, word.Singularize());
    }

    [Theory]
    [InlineData("some_title", "Some Title")]
    [InlineData("the_first_name", "The First Name")]
    public void Titleize_ConvertsUnderscoreToTitleCase(string input, string expected)
    {
        Assert.Equal(expected, input.Titleize());
    }

    [Theory]
    [InlineData("some_words", "Some words")]
    [InlineData("hello_world", "Hello world")]
    public void Humanize_ConvertsUnderscoreToHumanReadable(string input, string expected)
    {
        Assert.Equal(expected, input.Humanize());
    }

    [Theory]
    [InlineData("some_word", "SomeWord")]
    [InlineData("hello_world", "HelloWorld")]
    [InlineData("one", "One")]
    public void Pascalize_ConvertsUnderscoreToPascalCase(string input, string expected)
    {
        Assert.Equal(expected, input.Pascalize());
    }

    [Theory]
    [InlineData("some_word", "someWord")]
    [InlineData("hello_world", "helloWorld")]
    [InlineData("one", "one")]
    public void Camelize_ConvertsUnderscoreToCamelCase(string input, string expected)
    {
        Assert.Equal(expected, input.Camelize());
    }

    [Theory]
    [InlineData("SomeWord", "some_word")]
    [InlineData("HelloWorld", "hello_world")]
    [InlineData("HTMLParser", "html_parser")]
    public void Underscore_ConvertsPascalCaseToUnderscore(string input, string expected)
    {
        Assert.Equal(expected, input.Underscore());
    }

    [Theory]
    [InlineData("hello", "Hello")]
    [InlineData("HELLO", "Hello")]
    public void Capitalize_CapitalizesFirstLetter(string input, string expected)
    {
        Assert.Equal(expected, input.Capitalize());
    }

    [Theory]
    [InlineData("Hello", "hello")]
    [InlineData("HELLO", "hELLO")]
    public void Uncapitalize_LowercasesFirstLetter(string input, string expected)
    {
        Assert.Equal(expected, input.Uncapitalize());
    }

    [Theory]
    [InlineData("1", "1st")]
    [InlineData("2", "2nd")]
    [InlineData("3", "3rd")]
    [InlineData("4", "4th")]
    [InlineData("11", "11th")]
    [InlineData("12", "12th")]
    [InlineData("13", "13th")]
    [InlineData("21", "21st")]
    [InlineData("22", "22nd")]
    [InlineData("23", "23rd")]
    [InlineData("100", "100th")]
    [InlineData("101", "101st")]
    [InlineData("111", "111th")]
    public void Ordinalize_String_ReturnsCorrectOrdinal(string input, string expected)
    {
        Assert.Equal(expected, input.Ordinalize());
    }

    [Theory]
    [InlineData(1, "1st")]
    [InlineData(2, "2nd")]
    [InlineData(3, "3rd")]
    [InlineData(4, "4th")]
    [InlineData(11, "11th")]
    [InlineData(12, "12th")]
    [InlineData(13, "13th")]
    public void Ordinalize_Int_ReturnsCorrectOrdinal(int input, string expected)
    {
        Assert.Equal(expected, input.Ordinalize());
    }

    [Theory]
    [InlineData("some_word", "some-word")]
    [InlineData("hello_world", "hello-world")]
    public void Dasherize_ConvertsUnderscoreToDash(string input, string expected)
    {
        Assert.Equal(expected, input.Dasherize());
    }
}
