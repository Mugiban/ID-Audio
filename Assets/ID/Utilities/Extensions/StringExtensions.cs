using UnityEngine;

namespace ID.Runtime.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string WithColor(this string text, Color color)
        {
            var hexadecimalColor = ColorUtility.ToHtmlStringRGBA(color);
            return "<color=#" + hexadecimalColor + ">" + text + "</color>";
        }

        public static string Bold(this string text)
        {
            return "<b>" + text + "</b>";
        }
        
        public static string Italic(this string text)
        {
            return "<i>" + text + "</i>";
        }

        public static string Size(this string text, int size)
        {
            return "<size=" + size + ">" + text + "</size>";
        }
    }
}