#region Using

using System;

#endregion

namespace PuppyFramework.Helpers
{
    public static class StringHelpers
    {
        public static string Ellipsize(this string mainWord)
        {
            return mainWord == null ? null : string.Format("{0}…", mainWord);
        }

        public static bool IsValidHttpUrl(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            Uri uriResult;
            return Uri.TryCreate(text, UriKind.Absolute, out uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}