#region Header

// 
//        LifeUtils - LifeUtils - IConverter.cs
//                  19.11.2018 06:12

#endregion

namespace LifeUtils
{
    /// <summary>
    ///     Super type of the <see cref="IConverter{TFrom,TTo}" />
    /// </summary>
    public interface IConverter
    {
        // Ignored
    }

    /// <inheritdoc />
    /// <summary>
    ///     Converter that converts some type to another type.
    /// </summary>
    /// <typeparam name="TFrom">The type from.</typeparam>
    /// <typeparam name="TTo">The type to convert.</typeparam>
    public interface IConverter<in TFrom, out TTo> : IConverter
    {
        /// <summary>
        ///     Converts a type to another type.
        /// </summary>
        /// <param name="from">Type to convert.</param>
        /// <returns>The converted value.</returns>
        TTo Convert(TFrom from);
    }
}