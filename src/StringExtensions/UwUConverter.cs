﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UwUConverter.StringExtensions
{
    public static class UwUConverter
    {
        private static readonly Random RandomInt = new((int)DateTime.UtcNow.Ticks);
        public static readonly List<string> Faces = new() { ";w;", "owo", "UwU", ">w<", "^w^", "*w*" };//"(`・ω・´)(・`ω´・)" doesn't encode right :C
        private static readonly MatchEvaluator FaceEvaluator = _ => " " + Faces[RandomInt.Next(Faces.Count)] + " ";
        private static readonly Regex FaceRegex = new(@"\!+", RegexOptions.Compiled);
        private static readonly (Regex, string)[] Replacements = {
            (new Regex(@"(r|l)", RegexOptions.Compiled), "w"),
            (new Regex(@"(R|L)", RegexOptions.Compiled), "W"),
            (new Regex("n([aeiou])", RegexOptions.Compiled), @"ny$1"),
            (new Regex("N([aeiou])", RegexOptions.Compiled), @"Ny$1"),
            (new Regex("N([AEIOU])", RegexOptions.Compiled), @"NY$1"),
            (new Regex(@"(ove)", RegexOptions.Compiled), "uv"),
            (new Regex(@"(ou)", RegexOptions.Compiled), "ew")
        };

        public static string ConvertToUwU(this string input)
        {
            input = Replacements
                .Aggregate(input, (current, replacement) => replacement.Item1.Replace(current, replacement.Item2));
            return FaceRegex.Replace(input, FaceEvaluator);
        }
    }
}
