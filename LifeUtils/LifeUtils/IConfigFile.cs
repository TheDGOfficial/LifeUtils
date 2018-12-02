#region Header

// 
//        LifeUtils - LifeUtils - IConfigFile.cs
//                  03.12.2018 02:12

#endregion

namespace LifeUtils
{
    public interface IConfigFile
    {
        /// <summary>
        ///     Reads a key from the config file, from the specified section.
        /// </summary>
        /// <param name="key">The key to read.</param>
        /// <param name="section">The section.</param>
        /// <returns>The value of the key.</returns>
        string Read(string key, string section);

        /// <summary>
        ///     Writes a key into the config file, at the specified section.
        /// </summary>
        /// <param name="key">The key to write.</param>
        /// <param name="value">The value of the key to write.</param>
        /// <param name="section">The section to write.</param>
        void Write(string key, string value, string section);

        /// <summary>
        ///     Deletes a key from the config file, from the specified section.
        /// </summary>
        /// <param name="key">The key to delete.</param>
        /// <param name="section">The section to delete key from it.</param>
        void DeleteKey(string key, string section);

        /// <summary>
        ///     Checks if the specified key exists in the specified section.
        /// </summary>
        /// <param name="key">The key to check if exists.</param>
        /// <param name="section">The section to check key from it.</param>
        /// <returns>True if the config file contains specified key in the specified section.</returns>
        bool ContainsKey(string key, string section);
    }
}