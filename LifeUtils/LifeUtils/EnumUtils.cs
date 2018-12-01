#region Header

// 
//        LifeUtils - LifeUtils - EnumUtils.cs
//                  01.12.2018 05:24

#endregion

namespace LifeUtils
{
    #region Imports

    using System;

    #endregion

    /// <summary>
    ///     Enum utilities for C#.
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        ///     Gets all enum values as array.
        /// </summary>
        /// <typeparam name="T">The enum type to get values.</typeparam>
        /// <returns>The array of the all enum values in given type.</returns>
        public static T[] GetValues<T>(this T e) where T : Enum => (T[]) e.GetType().GetEnumValues();

        /// <summary>
        ///     Gets name of the specific enum value.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="e">The enum value to get name.</param>
        /// <returns>The name of the enum value.</returns>
        public static string GetName<T>(this T e) where T : Enum => Enum.GetName(e.GetType(), e);

        /// <summary>
        ///     Gets all enum names as string array.
        /// </summary>
        /// <typeparam name="T">The enum type to get names.</typeparam>
        /// <returns>The string array containing all enum names.</returns>
        public static string[] GetNames<T>(this T e) where T : Enum => e.GetType().GetEnumNames();

        /// <summary>
        ///     Parses a string into a enum value.
        /// </summary>
        /// <typeparam name="T">The type of the enum to parse.</typeparam>
        /// <param name="e">The enum. Used only for making a extension method.</param>
        /// <param name="value">The enum name to parse it.</param>
        /// <param name="ignoreCase">Should we ignore case?</param>
        /// <returns>The parsed enum.</returns>
        public static T Parse<T>(this T e, string value, bool ignoreCase = false) where T : Enum =>
            (T) Enum.Parse(e.GetType(), value, ignoreCase);
    }
}