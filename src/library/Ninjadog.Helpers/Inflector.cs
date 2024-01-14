// Copyright (c) 2020-2024, Atypical Consulting SRL. All rights reserved.
// This source code is proprietary and confidential.
// Unauthorized copying, modification, distribution, or use of this source code, in whole or in part,
// without express written permission from Atypical Consulting SRL is strictly prohibited.

using System.Globalization;
using System.Text.RegularExpressions;

// ReSharper disable UnusedMember.Global

namespace Ninjadog.Helpers;

// see: https://github.com/srkirkland/Inflector/blob/master/Inflector/Inflector.cs
// adapted for .NET8 by Philippe Matray

/// <summary>
/// Provides methods for string manipulations commonly used in inflecting words between different cases.
/// This static class includes functionality for pluralization, singularization, and various case conversions.
/// </summary>
public static class Inflector
{
    #region Default Rules

    static Inflector()
    {
        AddPlural("$", "s");
        AddPlural("s$", "s");
        AddPlural("(ax|test)is$", "$1es");
        AddPlural("(octop|vir|alumn|fung)us$", "$1i");
        AddPlural("(alias|status)$", "$1es");
        AddPlural("(bu)s$", "$1ses");
        AddPlural("(buffal|tomat|volcan)o$", "$1oes");
        AddPlural("([ti])um$", "$1a");
        AddPlural("sis$", "ses");
        AddPlural("(?:([^f])fe|([lr])f)$", "$1$2ves");
        AddPlural("(hive)$", "$1s");
        AddPlural("([^aeiouy]|qu)y$", "$1ies");
        AddPlural("(x|ch|ss|sh)$", "$1es");
        AddPlural("(matr|vert|ind)ix|ex$", "$1ices");
        AddPlural("([m|l])ouse$", "$1ice");
        AddPlural("^(ox)$", "$1en");
        AddPlural("(quiz)$", "$1zes");

        AddSingular("s$", "");
        AddSingular("(n)ews$", "$1ews");
        AddSingular("([ti])a$", "$1um");
        AddSingular("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis");
        AddSingular("(^analy)ses$", "$1sis");
        AddSingular("([^f])ves$", "$1fe");
        AddSingular("(hive)s$", "$1");
        AddSingular("(tive)s$", "$1");
        AddSingular("([lr])ves$", "$1f");
        AddSingular("([^aeiouy]|qu)ies$", "$1y");
        AddSingular("(s)eries$", "$1eries");
        AddSingular("(m)ovies$", "$1ovie");
        AddSingular("(x|ch|ss|sh)es$", "$1");
        AddSingular("([m|l])ice$", "$1ouse");
        AddSingular("(bus)es$", "$1");
        AddSingular("(o)es$", "$1");
        AddSingular("(shoe)s$", "$1");
        AddSingular("(cris|ax|test)es$", "$1is");
        AddSingular("(octop|vir|alumn|fung)i$", "$1us");
        AddSingular("(alias|status)es$", "$1");
        AddSingular("^(ox)en", "$1");
        AddSingular("(vert|ind)ices$", "$1ex");
        AddSingular("(matr)ices$", "$1ix");
        AddSingular("(quiz)zes$", "$1");

        AddIrregular("person", "people");
        AddIrregular("man", "men");
        AddIrregular("child", "children");
        AddIrregular("sex", "sexes");
        AddIrregular("move", "moves");
        AddIrregular("goose", "geese");
        AddIrregular("alumna", "alumnae");

        AddUncountable("equipment");
        AddUncountable("information");
        AddUncountable("rice");
        AddUncountable("money");
        AddUncountable("species");
        AddUncountable("series");
        AddUncountable("fish");
        AddUncountable("sheep");
        AddUncountable("deer");
        AddUncountable("aircraft");
    }

    #endregion

    private sealed class Rule(string pattern, string replacement)
    {
        private readonly Regex _regex = new(pattern, RegexOptions.IgnoreCase);

        public string? Apply(string word)
        {
            return _regex.IsMatch(word)
                ? _regex.Replace(word, replacement)
                : null;
        }
    }

    private static void AddIrregular(string singular, string plural)
    {
        AddPlural($"({singular[0]}){singular[1..]}$", $"$1{plural[1..]}");
        AddSingular($"({plural[0]}){plural[1..]}$", $"$1{singular[1..]}");
    }

    private static void AddUncountable(string word)
    {
        Uncountables.Add(word.ToLowerInvariant());
    }

    private static void AddPlural(string rule, string replacement)
    {
        Plurals.Add(new Rule(rule, replacement));
    }

    private static void AddSingular(string rule, string replacement)
    {
        Singulars.Add(new Rule(rule, replacement));
    }

    private static readonly List<Rule> Plurals = [];
    private static readonly List<Rule> Singulars = [];
    private static readonly List<string> Uncountables = [];

    /// <summary>
    /// Converts a word to its plural form.
    /// </summary>
    /// <param name="word">The word to be pluralized.</param>
    /// <returns>The plural form of the word.</returns>
    public static string Pluralize(this string word)
    {
        return ApplyRules(Plurals, word)!;
    }

    /// <summary>
    /// Converts a word to its singular form.
    /// </summary>
    /// <param name="word">The word to be singularized.</param>
    /// <returns>The singular form of the word.</returns>
    public static string Singularize(this string word)
    {
        return ApplyRules(Singulars, word)!;
    }

    private static string? ApplyRules(IReadOnlyList<Rule> rules, string word)
    {
        var result = word;

        if (Uncountables.Contains(word.ToLower(CultureInfo.InvariantCulture)))
        {
            return result;
        }

        for (var i = rules.Count - 1; i >= 0; i--)
        {
            if ((result = rules[i].Apply(word)) != null)
            {
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// Converts a word to title case.
    /// </summary>
    /// <param name="word">The word to be converted to title case.</param>
    /// <returns>The word in title case.</returns>
    public static string Titleize(this string word)
    {
        return Regex.Replace(
            Humanize(Underscore(word)),
            @"\b([a-z])",
            match => match.Captures[0].Value.ToUpperInvariant());
    }

    /// <summary>
    /// Converts an underscored word to a human-readable form.
    /// </summary>
    /// <param name="lowercaseAndUnderscoredWord">The underscored word to be humanized.</param>
    /// <returns>The humanized form of the word.</returns>
    public static string Humanize(this string lowercaseAndUnderscoredWord)
    {
        return Capitalize(Regex.Replace(lowercaseAndUnderscoredWord, "_", " "));
    }

    /// <summary>
    /// Converts an underscored word to Pascal case.
    /// </summary>
    /// <param name="lowercaseAndUnderscoredWord">The underscored word to be Pascalized.</param>
    /// <returns>The Pascal case form of the word.</returns>
    public static string Pascalize(this string lowercaseAndUnderscoredWord)
    {
        return Regex.Replace(
            lowercaseAndUnderscoredWord,
            "(?:^|_)(.)",
            match => match.Groups[1].Value.ToUpperInvariant());
    }

    /// <summary>
    /// Converts an underscored word to camel case.
    /// </summary>
    /// <param name="lowercaseAndUnderscoredWord">The underscored word to be camelized.</param>
    /// <returns>The camel case form of the word.</returns>
    public static string Camelize(this string lowercaseAndUnderscoredWord)
    {
        return Uncapitalize(Pascalize(lowercaseAndUnderscoredWord));
    }

    /// <summary>
    /// Converts a Pascal case word to an underscored format.
    /// </summary>
    /// <param name="pascalCasedWord">The Pascal case word to be underscored.</param>
    /// <returns>The underscored form of the word.</returns>
    public static string Underscore(this string pascalCasedWord)
    {
        return Regex.Replace(
                Regex.Replace(
                    Regex.Replace(pascalCasedWord, "([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])",
                    "$1_$2"),
                @"[-\s]",
                "_")
            .ToLowerInvariant();
    }

    /// <summary>
    /// Capitalizes the first letter of a word.
    /// </summary>
    /// <param name="word">The word to be capitalized.</param>
    /// <returns>The word with its first letter capitalized.</returns>
    public static string Capitalize(this string word)
    {
        return word[..1].ToUpperInvariant() + word[1..].ToLowerInvariant();
    }

    /// <summary>
    /// Converts the first letter of a word to lowercase.
    /// </summary>
    /// <param name="word">The word to be uncapitalized.</param>
    /// <returns>The word with its first letter in lowercase.</returns>
    public static string Uncapitalize(this string word)
    {
        return word[..1].ToLowerInvariant() + word[1..];
    }

    /// <summary>
    /// Converts a numeric string to its ordinal representation.
    /// </summary>
    /// <param name="numberString">The numeric string to be ordinalized.</param>
    /// <returns>The ordinal representation of the number.</returns>
    public static string Ordinalize(this string numberString)
    {
        return Ordanize(int.Parse(numberString, null), numberString);
    }

    /// <summary>
    /// Converts a number to its ordinal representation.
    /// </summary>
    /// <param name="number">The number to be ordinalized.</param>
    /// <returns>The ordinal representation of the number.</returns>
    public static string Ordinalize(this int number)
    {
        return Ordanize(number, number.ToString((IFormatProvider?)null));
    }

    private static string Ordanize(int number, string numberString)
    {
        var nMod100 = number % 100;

        return nMod100 is >= 11 and <= 13
            ? $"{numberString}th"
            : (number % 10) switch
            {
                1 => $"{numberString}st",
                2 => $"{numberString}nd",
                3 => $"{numberString}rd",
                _ => $"{numberString}th"
            };
    }

    /// <summary>
    /// Converts an underscored word to a dashed (kebab case) format.
    /// </summary>
    /// <param name="underscoredWord">The underscored word to be dasherized.</param>
    /// <returns>The dashed form of the word.</returns>
    public static string Dasherize(this string underscoredWord)
    {
        return underscoredWord.Replace('_', '-');
    }
}
