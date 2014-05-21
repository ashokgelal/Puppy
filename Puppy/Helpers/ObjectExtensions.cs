#region usings

using System;
using System.Runtime.CompilerServices;
using PuppyFramework.Properties;

#endregion

namespace PuppyFramework.Helpers
{
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Throws an <see cref="ArgumentNullException" /> if the passed parameter is null.
        /// </summary>
        /// <param name="obj">The object to be checked for null.</param>
        /// <param name="parameterName">Name of the parameter to be used in exception message.</param>
        /// <param name="message">
        ///     Message to be used in exception message.
        ///     This message will be concatenated with the Caller Info.
        /// </param>
        /// <param name="memberName">The member logging this log. The caller should almost never pass this value.</param>
        /// <param name="sourceFilePath">The source file path logging this log. The caller should almost never pass this value.</param>
        /// <param name="sourceLineNumber">
        ///     The source line number from where this log is being logged. The caller should almost
        ///     never pass this value.
        /// </param>
        public static void EnsureParameterNotNull(
            this object obj,
            string parameterName,
            string message = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (obj != null) return;

            var fullMessage = string.Format(Resources._parameterNullException, message ?? string.Empty, sourceLineNumber,
                memberName, sourceFilePath);
            throw new ArgumentNullException(parameterName, fullMessage);
        }

        /// <summary>
        ///     Throws an <see cref="InvalidOperationException" /> if the passed value is null.
        /// </summary>
        /// <param name="obj">The object to be checked for null.</param>
        /// <param name="message">
        ///     Message to be used in exception message.
        ///     This message will be concatenated with the Caller Info.
        /// </param>
        /// <param name="memberName">The member logging this log. The caller should almost never pass this value.</param>
        /// <param name="sourceFilePath">The source file path logging this log. The caller should almost never pass this value.</param>
        /// <param name="sourceLineNumber">
        ///     The source line number from where this log is being logged. The caller should almost
        ///     never pass this value.
        /// </param>
        public static void EnsureValueNotNull(
            this object obj,
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (obj != null) return;

            var fullMsg = string.Format(Resources._valueNullExceptionPattern, message, sourceLineNumber, memberName,
                sourceFilePath);
            throw new InvalidOperationException(fullMsg);
        }

        /// <summary>
        ///     Throws an <see cref="InvalidOperationException" /> if the passed string is null or whitespace.
        /// </summary>
        /// <param name="obj">The string to be checked for null or whitespace.</param>
        /// <param name="parameterName">Name of the parameter to be used in exception message.</param>
        /// <param name="message">
        ///     Message to be used in exception message.
        ///     This message will be concatenated with the Caller Info.
        /// </param>
        /// <param name="memberName">The member logging this log. The caller should almost never pass this value.</param>
        /// <param name="sourceFilePath">The source file path logging this log. The caller should almost never pass this value.</param>
        /// <param name="sourceLineNumber">
        ///     The source line number from where this log is being logged. The caller should almost
        ///     never pass this value.
        /// </param>
        public static void EnsureStringNotNullOrWhitespace(
            this string obj,
            string parameterName,
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!string.IsNullOrWhiteSpace(obj)) return;

            var fullMsg = string.Format(Resources._valueNullExceptionPattern, message, sourceLineNumber,
                memberName,
                sourceFilePath);
            throw new ArgumentException(parameterName, fullMsg);
        }
    }
}
