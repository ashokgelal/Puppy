namespace PuppyFramework.Helpers
{
    public static class StringHelpers
    {
        public static string Ellipsize(this string mainWord)
        {
            return mainWord == null ? null : string.Format("{0}…", mainWord);
        }
    }
}