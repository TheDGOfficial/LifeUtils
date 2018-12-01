#region Header

// 
//        LifeUtils - LifeUtils - Version.cs
//                  01.12.2018 05:24

#endregion

namespace LifeUtils
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    #endregion

    /// <inheritdoc cref="IComparable{T}" />
    /// <summary>
    ///     Represents a version.
    /// </summary>
    public sealed class Version : IEquatable<Version>, IComparable<Version>
    {
        /// <summary>
        ///     The regex for parsing versions.
        /// </summary>
        private static readonly Regex VersionRegex =
            new Regex(@"(\d+)\.(\d+)(?:\.(\d+))?\s*(.*)", RegexOptions.Compiled);

        /// <summary>
        ///     Everything after the version,
        ///     e.g. "alpha", "b", "rc 1", "build 2314", "-SNAPSHOT" etc. or null if nothing.
        /// </summary>
        private readonly string _suffix;

        /// <summary>
        ///     The version array that contains version data.
        /// </summary>
        private readonly int[] _version = new int[3];

        /// <summary>
        ///     Creates a new version with given numbers.
        /// </summary>
        /// <param name="version"></param>
        public Version(params int[] version)
        {
            if (version == null) throw new ArgumentNullException(nameof(version));
            if (version.Length < 1 || version.Length > _version.Length)
                throw new ArgumentOutOfRangeException(nameof(version),
                    "Versions must have a minimum of 2 and a maximum of 3 numbers (" + version.Length +
                    " numbers given)");
            for (var i = 0; i < version.Length; i++) _version[i] = version[i];
            _suffix = null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Creates a version with given numbers.
        /// </summary>
        /// <param name="major">The major version.</param>
        /// <param name="minor">The minor version.</param>
        /// <param name="revision">The revision number.</param>
        /// <param name="suffix">The suffix.</param>
        public Version(int major, int minor, int revision, string suffix) :
            this(major, minor, revision) => _suffix = suffix;

        /// <summary>
        ///     Generates a version from given string.
        /// </summary>
        /// <param name="version">The string containing version.</param>
        public Version(string version)
        {
            if (version == null) throw new ArgumentNullException(nameof(version));
            var match = VersionRegex.Match(version.Trim());
            if (match.Length < 1)
                throw new ArgumentOutOfRangeException("'" + version + "' is not a valid version string");
            for (var i = 0; i < _version.Length; i++)
                if (match.Groups.Count > i + 1)
                {
                    var v = match.Groups[i + 1].Value;
                    if (!string.IsNullOrEmpty(v))
                        _version[i] = int.Parse(v);
                }

            var sfx =
                match.Groups[match.Groups.Count].ToString();
            _suffix = string.IsNullOrEmpty(sfx) ? null : sfx;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Compares this version with another version.
        /// </summary>
        /// <param name="other">The other version to compare it.</param>
        /// <returns>The difference between two versions.</returns>
        public int CompareTo(Version other)
        {
            if (other is null)
                return 1;
            for (var i = 0; i < _version.Length; i++)
            {
                if (_version[i] > other._version[i])
                    return 1;
                if (_version[i] < other._version[i])
                    return -1;
            }

            return _suffix == null
                ? other._suffix == null ? 0 : 1
                : other._suffix == null
                    ? -1
                    : string.Compare(_suffix, other._suffix, StringComparison.CurrentCulture);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks if this version equals to another version.
        /// </summary>
        /// <param name="other">The other version to check.</param>
        /// <returns>True if the given version equals this version.</returns>
        public bool Equals(Version other) => !(other is null) && CompareTo(other) == 0;

        /// <summary>
        ///     Generates a new version from a version string.
        /// </summary>
        /// <param name="version">The created version object.</param>
        public static implicit operator Version(string version) => new Version(version);

        /// <summary>
        ///     Returns display version of a version object.
        /// </summary>
        /// <param name="version">The display version of a version object.</param>
        public static implicit operator string(Version version) => version.ToString();

        /// <summary>
        ///     Returns the string representation of this version.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => GetMajor() + "." + GetMinor() + "." + GetRevision() +
            (string.IsNullOrEmpty(_suffix) ? "" :
                _suffix.StartsWith("-") ? _suffix : " " + _suffix);

        /// <summary>
        ///     Gets the suffix of this version.
        /// </summary>
        /// <returns>The suffix of this version.</returns>
        public string GetSuffix() => _suffix;

        /// <summary>
        ///     Gets the version array of this version.
        /// </summary>
        /// <returns>The version array of this version.</returns>
        public int[] GetVersion() => _version;

        /// <summary>
        ///     Whether this is a stable version,
        ///     i.e. a simple version number without any additional details (like alpha/beta/etc.)
        /// </summary>
        /// <returns>True if this is a stable version.</returns>
        public bool IsStable() => _suffix == null;

        /// <summary>
        ///     Gets the major version of this version.
        /// </summary>
        /// <returns>The major version of this version.</returns>
        public int GetMajor() => _version.Length < 1 ? 0 : _version[0];

        /// <summary>
        ///     Gets the minor version of this version.
        /// </summary>
        /// <returns>The minor version of this version.</returns>
        public int GetMinor() => _version.Length < 2 ? 0 : _version[1];

        /// <summary>
        ///     Gets the revision version of this version.
        /// </summary>
        /// <returns>The revision version of this version.</returns>
        public int GetRevision() => _version.Length < 3 ? 0 : _version[2];

        /// <summary>
        ///     Checks if this version equals to another version.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if the given version equals this version.</returns>
        public override bool Equals(object obj) => Equals(obj as Version);

        /// <summary>
        ///     Gets the hash code of this version.
        /// </summary>
        /// <returns>The hash code of this version.</returns>
        public override int GetHashCode()
        {
            var hashCode = 1359330549;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_suffix);
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(_version);
            return hashCode;
        }

        /// <summary>
        ///     Checks if one version equals to another version.
        /// </summary>
        /// <param name="one">The first version.</param>
        /// <param name="other">The second version.</param>
        /// <returns>True if two versions are equal.</returns>
        public static bool operator ==(Version one, Version other) =>
            !(one is null) && one.CompareTo(other) == 0;

        /// <summary>
        ///     Checks if one version not equals to another version.
        /// </summary>
        /// <param name="one">The first version.</param>
        /// <param name="other">The second version.</param>
        /// <returns>True if two versions are not equal.</returns>
        public static bool operator !=(Version one, Version other) => !(one == other);

        /// <summary>
        ///     Checks if this version is smaller than a other version.
        /// </summary>
        /// <param name="other">A other version to check it.</param>
        /// <returns>True if this version is smaller than the given version.</returns>
        public bool IsSmallerThan(Version other) => CompareTo(other) < 0;

        /// <summary>
        ///     Checks if this version is higher than a other version.
        /// </summary>
        /// <param name="other">A other version to check it.</param>
        /// <returns>True if this version is higher than the given version.</returns>
        public bool IsHigherThan(Version other) => CompareTo(other) > 0;

        /// <summary>
        ///     Checks if this version is higher than a other version.
        /// </summary>
        /// <param name="one">This version.</param>
        /// <param name="other">A other version to check it.</param>
        /// <returns>True if this version is higher than the given version.</returns>
        public static bool operator >(Version one, Version other) => one.IsHigherThan(other);

        /// <summary>
        ///     Checks if this version is smaller than a other version.
        /// </summary>
        /// <param name="one">This version.</param>
        /// <param name="other">A other version to check it.</param>
        /// <returns>True if this version is smaller than the given version.</returns>
        public static bool operator <(Version one, Version other) => one.IsSmallerThan(other);
    }
}