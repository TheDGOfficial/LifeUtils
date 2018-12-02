#region Header

// 
//        LifeUtils - LifeUtils - Kleenean.cs
//                  03.12.2018 02:12

#endregion

namespace LifeUtils
{
    #region Imports

    using System;

    #endregion

    /// <inheritdoc cref="IEquatable{T}" />
    /// <summary>
    ///     Three based version of boolean.
    ///     True, false and unknown. Null equals to unknown.
    /// </summary>
    public struct Kleenean : IEquatable<Kleenean>
    {
        /// <summary>
        ///     The true value of Kleenean.
        /// </summary>
        public static readonly Kleenean True = new Kleenean(true);

        /// <summary>
        ///     The false value of Kleenean.
        /// </summary>
        public static readonly Kleenean False = new Kleenean(false);

        /// <summary>
        ///     The unknown (null) value of Kleenean.
        /// </summary>
        public static readonly Kleenean Unknown = new Kleenean(null);

        /// <summary>
        ///     The backing nullable boolean.
        /// </summary>
        private readonly bool? _back;

        /// <summary>
        ///     Creates a new Kleenean from a nullable boolean.
        /// </summary>
        /// <param name="value">The nullable boolean.</param>
        public Kleenean(bool? value) => _back = value;

        /// <summary>
        ///     Converts a nullable boolean value to a Kleenean.
        /// </summary>
        /// <param name="value">The nullable boolean value</param>
        public static implicit operator Kleenean(bool? value) => new Kleenean(value);

        /// <summary>
        ///     Converts a Kleenean to a non-null boolean.
        ///     If the value of Kleenean is unknown, then false is returned.
        /// </summary>
        /// <param name="kleenean">The kleenean to convert it.</param>
        public static implicit operator bool(Kleenean kleenean) => kleenean._back ?? false;

        /// <summary>
        ///     Returns the string representation of this Kleenean object.
        /// </summary>
        /// <returns>String representation of this Kleenean object.</returns>
        public override string ToString() => _back != null ? (bool) _back ? "true" : "false" : "unknown";

        /// <inheritdoc />
        /// <summary>
        ///     Checks if this kleenean equals to another kleenean.
        /// </summary>
        /// <param name="other">The other kleenean to check.</param>
        /// <returns>True if the given kleenean equals to this kleenean.</returns>
        public bool Equals(Kleenean other) => _back == other._back;

        /// <summary>
        ///     Checks if this kleenean equals to another object.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if the given object is a kleenean and equals with this kleenean.</returns>
        public override bool Equals(object obj) => !(obj is null) && obj is Kleenean other && Equals(other);

        /// <summary>
        ///     Gets the hash code of this kleenean.
        /// </summary>
        /// <returns>The hash code of this kleenean.</returns>
        public override int GetHashCode() => _back.GetHashCode();

        /// <summary>
        ///     Checks if two kleenean values are same.
        /// </summary>
        /// <param name="left">The left kleenean.</param>
        /// <param name="right">The right kleenean.</param>
        /// <returns>True if the two kleenean values are same.</returns>
        public static bool operator ==(Kleenean left, Kleenean right) => left.Equals(right);

        /// <summary>
        ///     Checks if the two kleenean values are not same.
        /// </summary>
        /// <param name="left">The left kleenean.</param>
        /// <param name="right">The right kleenean.</param>
        /// <returns>True if the two kleenean values are not same.</returns>
        public static bool operator !=(Kleenean left, Kleenean right) => !left.Equals(right);
    }
}