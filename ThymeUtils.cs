using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    /// <summary>
    /// A class that holds all the utility functions I come across and need in order to live.
    /// </summary>
    public static class ThymesUtils
    {

        /// <summary>
        /// Determines if hex in format [######] is valid.
        /// </summary>
        /// <param name="color">The passed Hex code.</param>
        /// <returns>True if the Hex code is valid, false otherwise.</returns>
        public static bool BracketHexIsValid(string color)
        {
            return Regex.IsMatch(color, "^\\[(?:[0-9a-fA-F]{3}){2}\\]$");
        }

        /// <summary>
        /// Determines if hex in format #123456 is valid.
        /// </summary>
        /// <param name="color">The passed Hex code.</param>
        /// <returns>True if the Hex code is valid, false otherwise.</returns>
        public static bool HashHexIsValid(string color)
        {
            return Regex.IsMatch(color, "^#(?:[0-9a-fA-F]{3}){2}$");
        }

        /// <summary>
        /// Determines if hex in format 123456 is valid.
        /// </summary>
        /// <param name="color">The passed Hex code.</param>
        /// <returns>True if the Hex code is valid, false otherwise.</returns>
        public static bool RawHexIsValid(string color)
        {
            return Regex.IsMatch(color, "^(?:[0-9a-fA-F]{3}){2}$");
        }

        /// <summary>
        /// Returns an array of strings containing Hex codes in the format [123456].
        /// </summary>
        /// <param name="text">The passed string.</param>
        /// <returns>A <see cref="string[]"/> of Hex codes in the format [123456].</returns>
        public static string[] GetAllBracketHexOccurrences(string text)
        {
            const string exp = "\\[(?:[0-9a-fA-F]{3}){2}\\]";
            var matches = Regex.Matches(text, exp);
            string[] res = new string[matches.Count];
            int i = 0;
            foreach (Match match in matches)
            {
                res[i] = match.Value;
                i++;
            }

            return res;
        }


        /// <summary>
        /// Returns the input string with all occurences of hex color codes removed.
        /// </summary>
        /// <param name="text">The text to cleanse.</param>
        /// <returns>The input string with all occurences of hex color codes removed.</returns>
        public static string RemoveAllColorCodes(string text)
        {
            return Regex.Replace(text, "\\[(?:[0-9a-fA-F]{3}){2}\\]", string.Empty);
        }


        /// <summary>
        /// Truncates text containing hex codes, treating the hex code as a single character/token.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string TruncateTextWithHex(string text, int maxLength)
        {
            string[] tokens = TokenizeText(text);
            string result = string.Empty;
            int count = 0;
            for (int i = 0; i < tokens.Length; i++)
            {
                if (count < maxLength)
                {
                    // Hex codes are not included in the name length.
                    if (tokens[i].Length == 1)
                    {
                        count++;
                    }
                    result += tokens[i];
                }
            }
            return result;
        }


        /// <summary>
        /// Returns an Array of tokens where each token is either a character or a hex code.
        /// </summary>
        /// <param name="text">The text to be tokenized.</param>
        /// <returns>An Array of tokens where each token is either a character or a hex code.</returns>
        public static string[] TokenizeText(string text)
        {
            List<string> results = new List<string>();
            const string exp = "\\[(?:[0-9a-fA-F]{3}){2}\\]";
            var matches = Regex.Matches(text, exp);
            int i = 0;

            // Matches are iterated in the order that they are found.
            // results += all characters prior to match + the match itself
            // "[123456]", "abc[123456]", "cde[123456]", "fgh"
            // "fgh" or the remainder letters after the last match will have to be handled separately.
            foreach (Match match in matches)
            {
                int endIndex = match.Index;
                for (int j = i; j < endIndex; j++)
                {
                    results.Add(text.Substring(j, 1));
                }
                results.Add(match.Value);
                i = endIndex + match.Value.Length;
            }

            // capture the remainder
            for (int j = i; j < text.Length; j++)
            {
                results.Add(text.Substring(j, 1));
            }

            return results.ToArray();
        }

        /// <summary>
        /// Converts a hex string in format [123456], #123456, or 123456 into a <see cref="Color"/> object.
        /// </summary>
        /// <param name="color">Hex code for color</param>
        /// <returns><see cref="Color"/> if the passed color was in the correct format. Throws an exception otherwise.</returns>
        public static Color ColorFromHex(string color)
        {
            color = color.Replace("#", string.Empty);
            if (BracketHexIsValid(color))
            {
                color = color.Replace("[", string.Empty);
                color = color.Replace("]", string.Empty);

            }
            else if (HashHexIsValid(color))
            {
                color = color.Replace("#", string.Empty);
            }
            else if (!RawHexIsValid(color))
            {
                throw new Exception($"Passed param {color} was not in a legal Hex color format.");
            }
            int r, g, b;
            r = int.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            g = int.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            b = int.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            Color res = new Color(r / 255.0f, g / 255.0f, b / 255.0f, 1f);
            return res;
        }

        /// <summary>
        /// Source: https://en.wikipedia.org/wiki/Relative_luminance
        /// Function for the relative luminance of a color (perceived brightness).
        /// Supported Hex formats: [123456], #123456, or 123456
        /// </summary>
        /// <param name="color">The hex color passed in.</param>
        /// <returns>A float between 0 and 1 describing how bright the color is. (0 is dark and 1 is light)</returns>
        public static float RelativeLuminance(string color)
        {
            // Convert Hex to RGB Color object.
            Color rgb = ColorFromHex(color);

            // Luminance formula.
            return RelativeLuminance(rgb);
        }

        /// <summary>
        /// Source: https://en.wikipedia.org/wiki/Relative_luminance
        /// Function for the relative luminance of a color (perceived brightness).
        /// </summary>
        /// <param name="color">The <see cref="Color"/> object passed in.</param>
        /// <returns>A float between 0 and 1 describing how bright the color is. (0 is dark and 1 is light)</returns>
        public static float RelativeLuminance(Color color)
        {
            const float rConst = 0.2126f;
            const float gConst = 0.7152f;
            const float bConst = 0.0722f;

            return (color.r * rConst + color.g * gConst + color.b * bConst) * color.a; ;
        }

        /// <summary>
        /// Adding this reference here since the util is the only
        /// function that needs to know about the translation enum.
        /// </summary>
        public enum LANGUAGE : int
        {
            en,
            es,
            pt,
            ru,
            tl,
            th,
            fr,
            it,
            tr
        };

       

        /// <summary>
        /// A function for generating x elements from x ranges that add up to less than or equal to the sum.
        /// Note, an issue with this is that the first element's average is 5, and consecutive ones are < 5.
        /// (not uniformly chosen but can be quickly run in succession)
        /// </summary>
        /// <param name="minRange">An array of integers that define the minimum range.</param>
        /// <param name="maxRange">An array of integers that define the maximum range.</param>
        /// <param name="sum">The constraint on what the x numbers can sum to.</param>
        /// <returns>an array of x numbers whose sum <= 0.</returns>
        public static int[] RandomNumberSets(int[] minRange, int[] maxRange, int sum)
        {
            if (minRange.Length != maxRange.Length)
            {
                throw new Exception("Error: minRange must contain the same number of elements as maxRange");
            }

            int[] results = new int[minRange.Length];
            System.Random rnd = new System.Random();

            for (int i = 0; i < minRange.Length; i++)
            {
                int value = rnd.Next(minRange[i], Math.Min(maxRange[i], sum));
                results[i] = value;
                sum -= value;
            }

            return results;
        }

        /// <summary>
        /// A recursive function to generate a list of all possible combinations of integers
        /// defined by the ranges passed below that sum to be less than or equal to the passed
        /// sum variable. This allows us to pick a uniformly random set of integers in given ranges
        /// that sum to less than or equal to a given value.
        /// </summary>
        /// <param name="minRanges">An array of integers that define the minimum range.</param>
        /// <param name="maxRanges">An array of integers that define the maximum range.</param>
        /// <param name="sum">The constraint on what the x numbers can sum to.</param>
        /// <returns>A <see cref="List{int}"/> containing the solutions.</returns>
        public static List<List<int>> UniformRandomNumberSets(List<int> minRanges, List<int> maxRanges, int sum)
        {
            if (minRanges.Count != maxRanges.Count)
            {
                throw new Exception("Error: UniformRandomNumberSets requires two Lists of equal size.");
            }
            List<List<int>> solutions = new List<List<int>>();
            RecursiveUniformRandomNumberSets(ref solutions, new List<int>(), minRanges, maxRanges, sum);
            return solutions;
        }

        /// <summary>
        /// A recursive helper function to generate a list of all possible combinations of integers
        /// defined by the ranges passed below that sum to be less than or equal to the passed
        /// sum variable. This allows us to pick a uniformly random set of integers in given ranges
        /// that sum to less than or equal to a given value.
        /// </summary>
        /// <param name="solutions">Pass an empty <see cref="List{int}"/> object that will hold the solutions.</param>
        /// <param name="minRanges">An array of integers that define the minimum range.</param>
        /// <param name="maxRanges">An array of integers that define the maximum range.</param>
        /// <param name="sum">The constraint on what the x numbers can sum to.</param>
        public static void RecursiveUniformRandomNumberSets(ref List<List<int>> solutions, List<int> solution, List<int> minRanges, List<int> maxRanges, int sum)
        {
            if (minRanges.Count == 0)
            {
                if (sum >= 0)
                {
                    List<int> copy = new List<int>();
                    foreach (var x in solution)
                    {
                        copy.Add(x);
                    }
                    solutions.Add(copy);
                }
                return;
            }

            int thisMinRange = minRanges[0];
            int thisMaxRange = maxRanges[0];

            for (int i = thisMinRange; i <= thisMaxRange; i++)
            {
                if (sum - i < 0)
                {
                    return;
                }
                solution.Add(i);
                RecursiveUniformRandomNumberSets(ref solutions, solution, minRanges.GetRange(1, minRanges.Count - 1), maxRanges.GetRange(1, minRanges.Count - 1), sum);
                solution.RemoveAt(solution.Count - 1);
            }
        }

        /// <summary>
        /// Linearly interpolates a target value with a known range into a a new range.
        /// Ex: (0 <= x <= 100) -> (0 <= x' <= 255)
        ///     x' will be proportionally equal to x
        ///     
        /// If x is not inside the input range (inMin <= x <= inMax),
        /// an Exception will be thrown.
        /// </summary>
        /// <param name="x">input value to be interpolated</param>
        /// <param name="inMin">input minimum value</param>
        /// <param name="inMax">input maximum value</param>
        /// <param name="outMin">output minimum value</param>
        /// <param name="outMax">output maximum value</param>
        /// <returns>interpolated floating point value of x</returns>
        public static float Map(float x, float inMin, float inMax, float outMin, float outMax)
        {
            if (x > inMax || x < inMin)
            {
                throw new Exception("Error,\npublic float map(float x, float inMin, float inMax, float outMin, float outMax)\nis not defined for values (x > inMax || x < inMin)");
            }
            return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }
    }
}
