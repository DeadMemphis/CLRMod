using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ExitGames.Client.Photon;
using UnityEngine;
using BRM;


public static class RCextensions
{
    public static void Add<T>(ref T[] source, T value)
    {
        T[] localArray = new T[source.Length + 1];
        for (int i = 0; i < source.Length; i++)
        {
            localArray[i] = source[i];
        }
        localArray[localArray.Length - 1] = value;
        source = localArray;
    }
    public static ExitGames.Client.Photon.Hashtable NewCopy(this ExitGames.Client.Photon.Hashtable sourceHash, int capacity)
    {
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable(capacity);
        foreach (DictionaryEntry dictionaryEntry in sourceHash)
        {
            hashtable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
        }
        return hashtable;
    }
    public static float RoundTo(this float value, int decimals)
    {
        return (float)decimal.Round((decimal)value, decimals);
    }
    public static string hexColor(this string text)
    {
        if (text.Contains("]"))
        {
            text = text.Replace("]", ">");
        }
        bool flag2 = false;
        while (text.Contains("[") && !flag2)
        {
            int index = text.IndexOf("[");
            if (text.Length >= (index + 7))
            {
                string str = text.Substring(index + 1, 6);
                text = text.Remove(index, 7).Insert(index, "<color=#" + str);
                int length = text.Length;
                if (text.Contains("["))
                {
                    length = text.IndexOf("[");
                }
                text = text.Insert(length, "</color>");
            }
            else
            {
                flag2 = true;
            }
        }
        if (flag2)
        {
            return string.Empty;
        }
        return text;
    }
    public static bool isLowestID(this PhotonPlayer player)
    {
        foreach (PhotonPlayer player2 in PhotonNetwork.playerList)
        {
            if (player2.ID < player.ID)
            {
                return false;
            }
        }
        return true;
    }
    public static Texture2D loadimage(WWW link, bool mipmap, int size)
    {
        Texture2D tex = new Texture2D(4, 4, TextureFormat.DXT1, mipmap);
        if (link.size >= size)
        {
            return tex;
        }
        Texture2D texture = link.texture;
        int width = texture.width;
        int height = texture.height;
        int num3 = 0;
        if ((width < 4) || ((width & (width - 1)) != 0))
        {
            num3 = 4;
            width = Math.Min(width, 0x3ff);
            while (num3 < width)
            {
                num3 *= 2;
            }
        }
        else if ((height < 4) || ((height & (height - 1)) != 0))
        {
            num3 = 4;
            height = Math.Min(height, 0x3ff);
            while (num3 < height)
            {
                num3 *= 2;
            }
        }
        if (num3 == 0)
        {
            if (mipmap)
            {
                try
                {
                    link.LoadImageIntoTexture(tex);
                }
                catch
                {
                    tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
                    link.LoadImageIntoTexture(tex);
                }
                return tex;
            }
            link.LoadImageIntoTexture(tex);
            return tex;
        }
        if (num3 < 4)
        {
            return tex;
        }
        Texture2D textured3 = new Texture2D(4, 4, TextureFormat.DXT1, false);
        link.LoadImageIntoTexture(textured3);
        if (mipmap)
        {
            try
            {
                textured3.Resize(num3, num3, TextureFormat.DXT1, mipmap);
            }
            catch
            {
                textured3.Resize(num3, num3, TextureFormat.DXT1, false);
            }
        }
        else
        {
            textured3.Resize(num3, num3, TextureFormat.DXT1, mipmap);
        }
        textured3.Apply();
        return textured3;
    }
    public static void RemoveAt<T>(ref T[] source, int index)
    {
        if (source.Length == 1)
        {
            source = new T[0];
        }
        else if (source.Length > 1)
        {
            T[] localArray = new T[source.Length - 1];
            int num = 0;
            int num2 = 0;
            while (num < source.Length)
            {
                if (num != index)
                {
                    localArray[num2] = source[num];
                    num2++;
                }
                num++;
            }
            source = localArray;
        }
    }
    public static bool returnBoolFromObject(object obj)
    {
        return (((obj != null) && (obj is bool)) && ((bool) obj));
    }
    public static float returnFloatFromObject(object obj)
    {
        if ((obj != null) && (obj is float))
        {
            return (float) obj;
        }
        return 0f;
    }
    public static int returnIntFromObject(object obj)
    {
        if ((obj != null) && (obj is int))
        {
            return (int) obj;
        }
        return 0;
    }
    public static bool HasHexAt(this string text, int i)
    {
        int num;
        return text[i] == '[' && text.Substring(i).Length > 7 && text[i + 7] == ']' && int.TryParse(text.Substring(i + 1, 2), System.Globalization.NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(i + 3, 2), System.Globalization.NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(i + 5, 2), System.Globalization.NumberStyles.HexNumber, null, out num);
    }
    public static string ToRGBA(this string value)
    {
        string result;
        try
        {
            string text = value.Replace("[-]", string.Empty);
            if (text.Contains("[") && text.Contains("]"))
            {
                int num = text.IndexOf("[");
                while (num >= 0 && text.HasHexAt(num))
                {
                    text = text.Insert(num + 8, "<color=#" + text.Substring(num + 1, 6).ToLower() + ">").Remove(num, 8);
                    text += "</color>";
                    num = text.IndexOf("[", num);
                }
            }
            result = text;
        }
        catch
        {
            try
            {
                result = value.StripHex();
            }
            catch
            {
                result = value;
            }
        }
        return result;
    }
    public static string NullFix(this string nullorempty)
    {
        if (nullorempty == null)
        {
            return string.Empty;
        }
        return nullorempty;
    }
    public static bool IsNullOrEmpty(this string value)
    {
        return value == null || value.Length == 0;
    }
    //public static float NextFloat(System.Random random, double min, double max)
    //{
    //    double mantissa = (random.NextDouble() * 2.0) - 1.0;
    //    // choose -149 instead of -126 to also generate subnormal floats (*)
    //    double exponent = Math.Pow(2.0, random.Next(-126, 128));
    //    return (float)(mantissa * exponent);
    //}
    public static T MinValueOrNull<T, TNum>(this IEnumerable<T> collection, Func<T, TNum> func, Func<T, bool> where) where T : class where TNum : IComparable, IFormattable, IConvertible, IComparable<TNum>, IEquatable<TNum>
    {
        List<T> list = collection.ToList<T>();
        if (list.Count > 0)
        {
            bool flag = true;
            TNum other = default(TNum);
            T result = list[0];
            foreach (T t in list)
            {
                TNum tnum;
                if (t != null && where(t) && (tnum = func(t)) != null)
                {
                    if (flag)
                    {
                        result = t;
                        flag = false;
                        other = tnum;
                    }
                    else if (tnum.CompareTo(other) <= 0)
                    {
                        result = t;
                        other = tnum;
                    }
                }
            }
            return result;
        }
        return default(T);
    }
    public static string ToEngFormat<T>(this IEnumerable<T> collection, string word, Func<T, string> select)
    {
        List<string> list = new List<string>();
        if (typeof(T).IsValueType)
        {
            using (IEnumerator<T> enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    T arg = enumerator.Current;
                    string item;
                    if (!string.IsNullOrEmpty(item = select(arg)))
                    {
                        list.Add(item);
                    }
                }
                goto IL_A2;
            }
        }
        foreach (T t in collection)
        {
            string item2;
            if (t != null && !string.IsNullOrEmpty(item2 = select(t)))
            {
                list.Add(item2);
            }
        }
        IL_A2:
        return RCextensions.HandleEngFormat(word, list);
    }
    public static T RandomPick<T>(this List<T> collection)
    {
        return collection[UnityEngine.Random.Range(0, collection.Count)];
    }
    public static bool ContainsLightHex(this string text)
    {
        if (text.IsNullOrWhiteSpace())
        {
            return false;
        }
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\\[([a-fA-F0-9]{6})\\]");
        if (regex.IsMatch(text))
        {
            foreach (System.Text.RegularExpressions.Match match in regex.Matches(text))
            {
                if (LightHex(match.Groups[1].Value))
                {
                    bool result = true;
                    return result;
                }
            }
        }
        System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex("\\#([a-fA-F0-9]{6})");
        if (regex2.IsMatch(text))
        {
            foreach (System.Text.RegularExpressions.Match match2 in regex2.Matches(text))
            {
                if (LightHex(match2.Groups[1].Value))
                {
                    bool result = true;
                    return result;
                }
            }
        }
        return false;
    }
    public static bool DarkHex(string hex)
    {
        if (hex.IsNullOrWhiteSpace())
        {
            return false;
        }
        int num;
        if (int.TryParse(hex.Replace("#", string.Empty), System.Globalization.NumberStyles.HexNumber, null, out num))
        {
            double num2 = (double)(num >> 16 & 255);
            double num3 = (double)(num >> 8 & 255);
            double num4 = (double)(num & 255);
            double num5 = 0.2126 * num2 + 0.7152 * num3 + 0.0722 * num4;
            if (num5 < 40.0)
            {
                return true;
            }
        }
        return false;
    }
    public static string Limit(this string value, int count, TextColor color = TextColor.None, string addon = "")
    {
        int i = 0;
        switch (color)
        {
            case TextColor.Hexadecimal:
                while (i < value.Length)
                {
                    int num;
                    if (value.HasHexAt(i, out num))
                    {
                        i += num;
                    }
                    else
                    {
                        count--;
                        if (count <= 0)
                        {
                            return value.Remove(i) + addon;
                        }
                        i++;
                    }
                }
                return value;
            case TextColor.RGBA:
                while (i < value.Length)
                {
                    int num;
                    if (value.HasRGBAAt(i, out num))
                    {
                        i += num;
                    }
                    else
                    {
                        count--;
                        if (count <= 0)
                        {
                            return value.Remove(i) + addon;
                        }
                        i++;
                    }
                }
                return value;
            case TextColor.RGBAHex:
                while (i < value.Length)
                {
                    int num;
                    if (value.HasHexAt(i, out num))
                    {
                        i += num;
                    }
                    else if (value.HasRGBAAt(i, out num))
                    {
                        i += num;
                    }
                    else
                    {
                        count--;
                        if (count <= 0)
                        {
                            return value.Remove(i) + addon;
                        }
                        i++;
                    }
                }
                return value;
            default:
                if (count < value.Length)
                {
                    return value.Remove(count) + addon;
                }
                return value;
        }
    } 
    public static bool HasRGBAAt(this string text, int i, out int count)
    {
        count = 0;
        string text2 = text.Substring(i);
        bool flag;
        if (flag = text2.StartsWith("<color="))
        {
            text2 = text2.Substring(7);
        }
        if (text2[0] == '#' && text2.Length >= 7)
        {
            int num;
            if (int.TryParse(text2.Substring(1, 2), System.Globalization.NumberStyles.HexNumber, null, out num) && int.TryParse(text2.Substring(3, 2), System.Globalization.NumberStyles.HexNumber, null, out num) && int.TryParse(text2.Substring(5, 2), System.Globalization.NumberStyles.HexNumber, null, out num))
            {
                if (flag)
                {
                    count += 7;
                }
                if (text2.Length >= 9 && text2.Substring(7, 2).ToLower() == "ff")
                {
                    count += 9;
                    if (text2.Length >= 10 && text2[9] == '>')
                    {
                        count++;
                    }
                    return true;
                }
                if (text2.Length >= 8 && text2[7] == '>')
                {
                    count += 8;
                    return true;
                }
                count += 7;
                return true;
            }
        }
        else
        {
            if (!text2.Contains(">"))
            {
                text2 = text2.ToUpper();
                foreach (string current in Hexadecimal.colors.Keys)
                {
                    if (text2.StartsWith(current))
                    {
                        if (flag)
                        {
                            count += 7;
                        }
                        count += current.Length;
                        return true;
                    }
                }
                return false;
            }
            text2 = text2.Remove(text2.IndexOf(">")).ToUpper();
            if (Hexadecimal.colors.ContainsKey(text2))
            {
                if (flag)
                {
                    count += 7;
                }
                count += text2.Length;
                return true;
            }
            return false;
        }
        return false;
    }
    public static bool HasHex(this string text)
    {
        if (text.IsNullOrEmpty() || text.Length < 8)
        {
            return false;
        }
        int num;
        if (text[7] == ']' && int.TryParse(text.Substring(1, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(3, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(5, 2), NumberStyles.HexNumber, null, out num))
        {
            return true;
        }
        int num2 = text.IndexOf("[", 0, text.Length, StringComparison.CurrentCulture);
        while (num2 >= 0 && num2 < text.Length)
        {
            if (text.Substring(num2, text.Length - num2).Length > 7 && text[num2 + 7] == ']' && int.TryParse(text.Substring(num2 + 1, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(num2 + 3, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(num2 + 5, 2), NumberStyles.HexNumber, null, out num))
            {
                return true;
            }
            if (++num2 < 0 || num2 > text.Length || text.Length < 8)
            {
                return false;
            }
            num2 = text.IndexOf("[", num2, text.Length - num2, StringComparison.CurrentCulture);
        }
        return false;
    }
    public static bool HasHexAt(this string text, int i, out int count)
    {
        count = 0;
        int num;
        bool flag = text[i] == '[' && text.Substring(i).Length > 7 && text[i + 7] == ']' && int.TryParse(text.Substring(i + 1, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(i + 3, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(i + 5, 2), NumberStyles.HexNumber, null, out num);
        bool result;
        if (flag)
        {
            count += 8;
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }
    public static bool LightHex(string hex)
    {
        if (hex.IsNullOrWhiteSpace())
        {
            return false;
        }
        int num;
        if (int.TryParse(hex.Replace("#", string.Empty), System.Globalization.NumberStyles.HexNumber, null, out num))
        {
            double num2 = (double)(num >> 16 & 255);
            double num3 = (double)(num >> 8 & 255);
            double num4 = (double)(num & 255);
            double num5 = 0.2126 * num2 + 0.7152 * num3 + 0.0722 * num4;
            if (num5 > 60.0)
            {
                return true;
            }
        }
        return false;
    }
    //private static string FixRGBA(this string text)
    //{
    //    MatchCollection matchCollection = Regex.Matches(text, "(<color=((.*?)(\\s|>))?)|(<\\/color>)|(\\<b\\>)|(<\\/b>)|(\\<\\i\\>)|(\\<\\/i\\>)|(<size=((.*?)(\\s|>))?)|(<\\/size>)");
    //    List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
    //    using (IEnumerator enumerator = matchCollection.GetEnumerator())
    //    {
    //        while (enumerator.MoveNext())
    //        {
    //            Match match = (Match)enumerator.Current;
    //            if (!list.Any((KeyValuePair<int, string> p) => p.Key == match.Index))
    //            {
    //                list.Add(new KeyValuePair<int, string>(match.Index, match.Value));
    //            }
    //        }
    //    }
    //    list = (from pair in list
    //            orderby pair.Key descending
    //            select pair).ToList<KeyValuePair<int, string>>();
    //    Queue<Triplet<int, int, string>> queue = new Queue<Triplet<int, int, string>>();
    //    new Stack<Triplet<int, int, string>>();
    //    int num = list.Count;
    //    foreach (KeyValuePair<int, string> keyValuePair in list)
    //    {
    //        num--;
    //        string value = keyValuePair.Value;
    //        string a;
    //        if (value.StartsWith("<color="))
    //        {
    //            if (queue.Count == 0)
    //            {
    //                text = text.Remove(keyValuePair.Key, value.Length);
    //            }
    //            else
    //            {
    //                Triplet<int, int, string> triplet = queue.Dequeue();
    //                if (value.EndsWith(">"))
    //                {
    //                    if (!(triplet.Third == "</color>"))
    //                    {
    //                        foreach (Triplet<int, int, string> triplet2 in queue)
    //                        {
    //                            if (triplet2.Second > keyValuePair.Key)
    //                            {
    //                                triplet2.Second -= value.Length;
    //                            }
    //                        }
    //                        queue.Enqueue(new Triplet<int, int, string>(triplet.First, (triplet.Second > keyValuePair.Key) ? (triplet.Second - value.Length) : triplet.Second, triplet.Third));
    //                        text = text.Remove(keyValuePair.Key, value.Length);
    //                    }
    //                }
    //                else
    //                {
    //                    foreach (Triplet<int, int, string> triplet3 in queue)
    //                    {
    //                        if (triplet3.Second > keyValuePair.Key)
    //                        {
    //                            triplet3.Second -= value.Length;
    //                        }
    //                    }
    //                    queue.Enqueue(new Triplet<int, int, string>(triplet.First, (triplet.Second > keyValuePair.Key) ? (triplet.Second - value.Length) : triplet.Second, triplet.Third));
    //                    text = text.Remove(keyValuePair.Key, value.Length);
    //                }
    //            }
    //        }
    //        else if (value.StartsWith("<size="))
    //        {
    //            if (queue.Count == 0)
    //            {
    //                text = text.Remove(keyValuePair.Key, value.Length);
    //            }
    //            else
    //            {
    //                Triplet<int, int, string> triplet4 = queue.Dequeue();
    //                if (value.EndsWith(">"))
    //                {
    //                    if (!(triplet4.Third == "</size>"))
    //                    {
    //                        foreach (Triplet<int, int, string> triplet5 in queue)
    //                        {
    //                            if (triplet5.Second > keyValuePair.Key)
    //                            {
    //                                triplet5.Second -= value.Length;
    //                            }
    //                        }
    //                        queue.Enqueue(new Triplet<int, int, string>(triplet4.First, (triplet4.Second > keyValuePair.Key) ? (triplet4.Second - value.Length) : triplet4.Second, triplet4.Third));
    //                        text = text.Remove(keyValuePair.Key, value.Length);
    //                    }
    //                }
    //                else
    //                {
    //                    foreach (Triplet<int, int, string> triplet6 in queue)
    //                    {
    //                        if (triplet6.Second > keyValuePair.Key)
    //                        {
    //                            triplet6.Second -= value.Length;
    //                        }
    //                    }
    //                    queue.Enqueue(new Triplet<int, int, string>(triplet4.First, (triplet4.Second > keyValuePair.Key) ? (triplet4.Second - value.Length) : triplet4.Second, triplet4.Third));
    //                    text = text.Remove(keyValuePair.Key, value.Length);
    //                }
    //            }
    //        }
    //        else if ((a = value) != null)
    //        {
    //            if (!(a == "</b>") && !(a == "</i>") && !(a == "</size>") && !(a == "</color>"))
    //            {
    //                if (!(a == "<b>"))
    //                {
    //                    if (a == "<i>")
    //                    {
    //                        if (queue.Count == 0)
    //                        {
    //                            text = text.Remove(keyValuePair.Key, value.Length);
    //                        }
    //                        else
    //                        {
    //                            Triplet<int, int, string> triplet7 = queue.Dequeue();
    //                            if (!(triplet7.Third == "</i>"))
    //                            {
    //                                foreach (Triplet<int, int, string> triplet8 in queue)
    //                                {
    //                                    if (triplet8.Second > keyValuePair.Key)
    //                                    {
    //                                        triplet8.Second -= 3;
    //                                    }
    //                                }
    //                                queue.Enqueue(new Triplet<int, int, string>(triplet7.First, (triplet7.Second > keyValuePair.Key) ? (triplet7.Second - 3) : triplet7.Second, triplet7.Third));
    //                                text = text.Remove(keyValuePair.Key, 3);
    //                            }
    //                        }
    //                    }
    //                }
    //                else if (queue.Count == 0)
    //                {
    //                    text = text.Remove(keyValuePair.Key, value.Length);
    //                }
    //                else
    //                {
    //                    Triplet<int, int, string> triplet9 = queue.Dequeue();
    //                    if (!(triplet9.Third == "</b>"))
    //                    {
    //                        foreach (Triplet<int, int, string> triplet10 in queue)
    //                        {
    //                            if (triplet10.Second > keyValuePair.Key)
    //                            {
    //                                triplet10.Second -= 3;
    //                            }
    //                        }
    //                        queue.Enqueue(new Triplet<int, int, string>(triplet9.First, (triplet9.Second > keyValuePair.Key) ? (triplet9.Second - 3) : triplet9.Second, triplet9.Third));
    //                        text = text.Remove(keyValuePair.Key, 3);
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                queue.Enqueue(new Triplet<int, int, string>(num, keyValuePair.Key, value));
    //            }
    //        }
    //    }
    //    foreach (Triplet<int, int, string> triplet11 in from t in queue
    //                                                    orderby t.Second descending
    //                                                    select t)
    //    {
    //        if (triplet11.Second + triplet11.Third.Length <= text.Length)
    //        {
    //            text = text.Remove(triplet11.Second, triplet11.Third.Length);
    //        }
    //    }
    //    queue.Clear();
    //    return text;
    //}
    public static string FixRGBA2(this string text)
    {
        List<string> list = new List<string>();
        for (int i = 0; i < text.Length; i++)
        {
            string text2 = text.Substring(i);
            if (text2.StartsWith("<color="))
            {
                list.Insert(0, "<color=");
                if (text2.StartsWith("<color=#"))
                {
                    if (!text2.HasRGBA())
                    {
                        if (text2.Contains(">"))
                        {
                            text = text.Remove(i, text.IndexOf(">", i) - i);
                        }
                        else
                        {
                            text = text.Remove(i, 8);
                        }
                        list.RemoveAt(0);
                        i--;
                    }
                }
                else if (!text2.HasRGBA())
                {
                    if (text2.Contains(">"))
                    {
                        text = text.Remove(i, text.IndexOf(">", i) - i);
                    }
                    else
                    {
                        text = text.Remove(i, 7);
                    }
                    list.RemoveAt(0);
                    i--;
                }
            }
            else if (text2.StartsWith("</color>"))
            {
                if (list.Count > 0 && list[0] == "<color=")
                {
                    list.RemoveAt(0);
                }
                else
                {
                    int num = 0;
                    while (num < list.Count && !(list[num] == "<color="))
                    {
                        num++;
                    }
                    if (list.Count > num)
                    {
                        list.RemoveAt(num);
                    }
                    if (text.Remove(i).Contains("<color="))
                    {
                        int num2 = text.Remove(i).LastIndexOf("<color=");
                        text = text.Remove(i, 8);
                        i--;
                        if (text.HasRGBAAt(num2 + 7))
                        {
                            if (text[num2 + 7] == '#')
                            {
                                if (text[num2 + 14] == '>')
                                {
                                    text = text.Remove(num2, 15);
                                    i -= 7;
                                }
                                else
                                {
                                    text = text.Remove(num2, 14);
                                    i -= 7;
                                }
                            }
                            else if (text.Substring(num2).Contains(">"))
                            {
                                text = text.Remove(num2, text.IndexOf('>', num2) + 1 - num2);
                                i -= 7;
                            }
                            else
                            {
                                text = text.Remove(num2, 7);
                                i -= 7;
                            }
                        }
                        else
                        {
                            text = text.Remove(num2, 7);
                            i -= 7;
                        }
                    }
                    else
                    {
                        text = text.Remove(i, 8);
                        i--;
                    }
                }
            }
            else if (text2.StartsWith("<b>"))
            {
                list.Insert(0, "<b>");
            }
            else if (text2.StartsWith("</b>"))
            {
                if (list.Count > 0 && list[0] == "<b>")
                {
                    list.RemoveAt(0);
                }
                else
                {
                    int num = 0;
                    while (num < list.Count && !(list[num] == "<b>"))
                    {
                        num++;
                    }
                    if (list.Count > num)
                    {
                        list.RemoveAt(num);
                    }
                    if (text.Remove(i).Contains("<b>"))
                    {
                        int num2 = text.Remove(i).LastIndexOf("<b>");
                        text = text.Remove(i, 4);
                        i--;
                        text = text.Remove(num2, 3);
                        i -= 3;
                    }
                    else
                    {
                        text = text.Remove(i, 4);
                        i--;
                    }
                }
            }
            else if (text2.StartsWith("<i>"))
            {
                list.Insert(0, "<i>");
            }
            else if (text2.StartsWith("</i>"))
            {
                if (list.Count > 0 && list[0] == "<i>")
                {
                    list.RemoveAt(0);
                }
                else
                {
                    int num = 0;
                    while (num < list.Count && !(list[num] == "<i>"))
                    {
                        num++;
                    }
                    if (list.Count > num)
                    {
                        list.RemoveAt(num);
                    }
                    if (text.Remove(i).Contains("<i>"))
                    {
                        int num2 = text.Remove(i).LastIndexOf("<i>");
                        text = text.Remove(i, 4);
                        i--;
                        text = text.Remove(num2, 3);
                        i -= 3;
                    }
                    else
                    {
                        text = text.Remove(i, 4);
                        i--;
                    }
                }
            }
        }
        return text;
    }
    public static string RemoveTags(this string text)
    {
        return Regex.Replace(text, "<.*?>", String.Empty);
    }
    public static bool HasRGBAAt(this string text, int i)
    {
        string text2 = text.Substring(i);
        if (text2.StartsWith("<color="))
        {
            text2 = text2.Substring(7);
        }
        if (new Regex("\\#([a-fA-F0-9]{6})").IsMatch(text))
        {
            int num;
            if (text[i] == '#' && text2.Length > 7 && text[i + 7] == '>' && int.TryParse(text.Substring(i + 1, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(i + 3, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(i + 5, 2), NumberStyles.HexNumber, null, out num))
            {
                return true;
            }
            if (i == 0)
            {
                foreach (char c in text)
                {
                    if (text[i] == '#' && text2.Length > 7 && text[i + 7] == '>' && int.TryParse(text.Substring(i + 1, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(i + 3, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(i + 5, 2), NumberStyles.HexNumber, null, out num))
                    {
                        return true;
                    }
                }
            }
        }
        else if (text2.Contains(">") && Hexadecimal.colors.ContainsKey(text2.Remove(text2.IndexOf(">")).ToUpper()))
        {
            return true;
        }
        return false;
    }
    public static bool HasRGBA(this string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '#')
            {
                int num;
                if (text.Substring(i).Length > 7 && text[i + 7] == '>' && int.TryParse(text.Substring(i + 1, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(i + 3, 2), NumberStyles.HexNumber, null, out num) && int.TryParse(text.Substring(i + 5, 2), NumberStyles.HexNumber, null, out num))
                {
                    return true;
                }
            }
            else
            {
                string text2 = text.Substring(i);
                if (text2.Contains(">") && Hexadecimal.colors.ContainsKey(text2.Remove(text2.IndexOf(">")).ToUpper()))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public static string StripRGBA(this string text)
    {
        return Regex.Replace(text, "((<color=(#?)(((\\w+|\\d+)?>)?))|(</color>)|(<b>)|(</b>)|(<i>)|(</i>)|(<size=(\\w*)?>?)|(<\\/size>))?", string.Empty);
    }
    public static string FixHex(this string value)
    {
        string result;
        try
        {
            string text = value;
            int i = 0;
            while (text.Contains("[-]"))
            {
                i--;
                text = text.Remove(text.IndexOf("[-]"), 3);
            }
            for (int j = text.IndexOf("[", 0, text.Length, System.StringComparison.CurrentCulture); j >= 0; j = text.IndexOf("[", j, text.Length - j, System.StringComparison.CurrentCulture))
            {
                if (j > text.Length)
                {
                    break;
                }
                if (text.HasHexAt(j))
                {
                    i++;
                    text = text.Remove(j, 8);
                }
                else
                {
                    j++;
                }
            }
            while (i > 0)
            {
                value += "[-]";
                i--;
            }
            result = value;
        }
        catch
        {
            result = value;
        }
        return result;
    }
    internal static bool TryGetValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, out TValue value, params TKey[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            TKey key = inputs[i];
            if (dictionary.TryGetValue(key, out value))
            {
                return true;
            }
        }
        value = default(TValue);
        return false;
    }
    public static bool IsNullOrWhiteSpace(this string value)
    {
        if (value == null)
        {
            return true;
        }
        for (int i = 0; i < value.Length; i++)
        {
            if (!char.IsWhiteSpace(value[i]))
            {
                return false;
            }
        }
        return true;
    }
    public static string ToEngFormat<T>(this IEnumerable<T> collection, string word = "and")
    {
        Func<T, string> func = null;
        List<string> list = new List<string>();
        if (typeof(T).IsValueType)
        {
            foreach (T current in collection)
            {
                string item3;
                if (!string.IsNullOrEmpty(item3 = current.ToString()))
                {
                    list.Add(item3);
                }
            }
            if (func == null)
            {
                func = ((T item) => item.ToString());
            }
            list = new List<string>(Enumerable.Select<T, string>(collection, func));
        }
        else
        {
            foreach (T current2 in collection)
            {
                string item2;
                if (current2 != null && !string.IsNullOrEmpty(item2 = current2.ToString()))
                {
                    list.Add(item2);
                }
            }
        }
        return RCextensions.HandleEngFormat(word, list);
    }
    public static string ToEngFormat<T, T2>(this System.Collections.Generic.IEnumerable<T> collection, string word, System.Func<T, T2> select, System.Func<T2, bool> where, System.Func<T2, string> select2)
    {
        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
        if (typeof(T).IsValueType)
        {
            using (System.Collections.Generic.IEnumerator<T> enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    T current = enumerator.Current;
                    T2 arg = select(current);
                    string item;
                    if (where(arg) && !string.IsNullOrEmpty(item = select2(arg)))
                    {
                        list.Add(item);
                    }
                }
                goto IL_CE;
            }
        }
        foreach (T current2 in collection)
        {
            T2 arg;
            string item;
            if (current2 != null && (arg = select(current2)) != null && where(arg) && !string.IsNullOrEmpty(item = select2(arg)))
            {
                list.Add(item);
            }
        }
    IL_CE:
        return RCextensions.HandleEngFormat(word, list);
    }
    private static string HandleEngFormat(string word, System.Collections.Generic.List<string> list)
    {
        switch (list.Count)
        {
            case 0:
                return string.Empty;
            case 1:
                return list[0];
            case 2:
                return string.Concat(new string[]
                {
                    list[0],
                    " ",
                    word,
                    " ",
                    list[1]
                });
            default:
                {
                    if (list.Count < 0)
                    {
                        throw new System.Exception("count is less than 0.");
                    }
                    int index = list.Count - 1;
                    list[index] = word + " " + list[index];
                    string text = list[0];
                    for (int i = 1; i < list.Count; i++)
                    {
                        text = text + ", " + list[i];
                    }
                    return text;
                }
        }
    }
    public static string Formats(this string format, params object[] args)
    {
        return RCextensions.Formats(null, format, args);
    }
    private static string Formats(System.IFormatProvider provider, string format, params object[] args)
    {
        if (format == null || args == null)
        {
            throw new System.ArgumentNullException((format == null) ? "format" : "args");
        }
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(format.Length + args.Length * 8);
        stringBuilder.AppendFormat(provider, format, args);
        return stringBuilder.ToString();
    }
    public static string returnStringFromObject(object obj)
    {
        if (obj != null)
        {
            string str = obj as string;
            if (str != null)
            {
                return str;
            }
        }
        return string.Empty;
    }
    public static string isString(this object obj)
    {
        if (obj != null)
        {
            string text = obj as string;
            if (text != null)
            {
                return text;
            }
        }
        return string.Empty;
    }
}

