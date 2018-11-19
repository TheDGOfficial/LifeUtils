#region Header

// 
//        LifeUtils - LifeUtils - Kleenean.cs
//                  19.11.2018 06:12

#endregion

namespace LifeUtils
{
    /// <summary>
    ///     Three based version of boolean.
    ///     True, false and unknown. Null equals to unknown.
    /// </summary>
    public struct Kleenean
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
    }
}