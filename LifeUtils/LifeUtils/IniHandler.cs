#region Header

// 
//        LifeUtils - LifeUtils - IniHandler.cs
//                  03.12.2018 02:12

#endregion

namespace LifeUtils
{
    #region Imports

    using System;
    using System.IO;
    using System.Text;

    #endregion

    /// <inheritdoc cref="IConfigFile" />
    /// <summary>
    ///     The .ini file API for C#.
    /// </summary>
    public struct IniHandler : IConfigFile, IEquatable<IniHandler>
    {
        /// <summary>
        ///     The .ini file's path / name.
        /// </summary>
        private readonly string _path;

        /// <summary>
        ///     Generates a new INIHandler with the given ini file name / path.
        /// </summary>
        /// <param name="iniPath">The ini file's name / path.</param>
        public IniHandler(string iniPath) => _path = new FileInfo(iniPath).FullName;

        /// <inheritdoc />
        /// <summary>
        ///     Reads a key from the ini file, from the specified section.
        /// </summary>
        /// <param name="key">The key to read.</param>
        /// <param name="section">The section.</param>
        /// <returns>The value of the key.</returns>
        public string Read(string key, string section)
        {
            var retVal = new StringBuilder(4096);
            SafeNativeMethods.GetPrivateProfileString(section, key, "", retVal, 4096, _path);
            return retVal.ToString();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Writes a key into the ini file, at the specified section.
        /// </summary>
        /// <param name="key">The key to write.</param>
        /// <param name="value">The value of the key to write.</param>
        /// <param name="section">The section to write.</param>
        public void Write(string key, string value, string section) =>
            SafeNativeMethods.WritePrivateProfileString(section, key, value, _path);

        /// <inheritdoc />
        /// <summary>
        ///     Deletes a key from the ini file, from the specified section.
        /// </summary>
        /// <param name="key">The key to delete.</param>
        /// <param name="section">The section to delete key from it.</param>
        public void DeleteKey(string key, string section) => Write(key, null, section);

        /// <inheritdoc />
        /// <summary>
        ///     Checks if the specified key exists in the specified section.
        /// </summary>
        /// <param name="key">The key to check if exists.</param>
        /// <param name="section">The section to check key from it.</param>
        /// <returns>True if the ini file contains specified key in the specified section.</returns>
        public bool ContainsKey(string key, string section)
        {
            var value = Read(key, section);
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        ///     Deletes a section from the ini file.
        /// </summary>
        /// <param name="section">The section to delete.</param>
        public void DeleteSection(string section) => Write(null, null, section);

        /// <summary>
        ///     Checks this ini handler equals another ini handler.
        /// </summary>
        /// <param name="obj">The possible ini handler to check.</param>
        /// <returns>True if object is a ini handler and equals to this object.</returns>
        public override bool Equals(object obj) => !(obj is null) && obj is IniHandler other && Equals(other);

        /// <summary>
        ///     Gets the hash code of this ini handler.
        /// </summary>
        /// <returns>The hash code of this ini handler.</returns>
        public override int GetHashCode() => _path != null ? StringComparer.CurrentCulture.GetHashCode(_path) : 0;

        /// <inheritdoc />
        /// <summary>
        ///     Checks if this ini handler equals to another ini handler.
        /// </summary>
        /// <param name="other">The other ini handler to check.</param>
        /// <returns>True if the two ini handlers are same.</returns>
        public bool Equals(IniHandler other) => string.Equals(_path, other._path, StringComparison.CurrentCulture);

        /// <summary>
        ///     Checks if the two ini handlers are same.
        /// </summary>
        /// <param name="left">The left ini handler.</param>
        /// <param name="right">The right ini handler.</param>
        /// <returns>True if the two ini handlers are same.</returns>
        public static bool operator ==(IniHandler left, IniHandler right) => left.Equals(right);

        /// <summary>
        ///     Checks if the two ini handlers are not same.
        /// </summary>
        /// <param name="left">The left ini handler.</param>
        /// <param name="right">The right ini handler.</param>
        /// <returns>True if the two ini handlers are not same.</returns>
        public static bool operator !=(IniHandler left, IniHandler right) => !left.Equals(right);
    }
}