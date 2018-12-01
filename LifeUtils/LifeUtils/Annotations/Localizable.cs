#region Header

// 
//        LifeUtils - LifeUtils - Localizable.cs
//                  01.12.2018 05:24

#endregion

namespace LifeUtils.Annotations
{
    #region Imports

    using System;
    using System.Reflection;

    #endregion

    /// <inheritdoc />
    /// <summary>
    ///     Represents a localizable field or property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class LocalizableAttribute : Attribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Generates a new localizable field / property.
        /// </summary>
        /// <param name="key">The key, showed in config.</param>
        /// <param name="value">The default value of the field / property.</param>
        public LocalizableAttribute(string key = "", string value = "")
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        ///     The key of this option.
        ///     Shows in config file.
        /// </summary>
        private string Key { get; set; }

        /// <summary>
        ///     The default value of this option.
        ///     Shows in config file.
        /// </summary>
        private string Value { get; }

        /// <summary>
        ///     You must call this before accessing any localizable field / property.
        ///     This method sets fields / properties to configured / localized versions.
        /// </summary>
        /// <param name="type">The type to get all properties & fields.</param>
        /// <param name="config">The config file to write or get fields / properties.</param>
        public static void Init(Type type, IConfigFile config)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            const BindingFlags flags =
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            const string section =
                "Localization";

            var fieldInfos =
                type.GetFields(flags);

            var propertyInfos =
                type.GetProperties(flags);

            foreach (var fieldInfo in fieldInfos)
            foreach (var attr in fieldInfo.GetCustomAttributes<LocalizableAttribute>(true))
            {
                if (attr.Key.Equals("")) attr.Key = fieldInfo.Name;
                if (!config.ContainsKey(attr.Key, section))
                    config.Write(attr.Key, attr.Value.Equals("") ? fieldInfo.GetValue(null).ToString() : attr.Value,
                        section);
                else
                    fieldInfo.SetValue(null, config.Read(attr.Key, section));
                break;
            }

            foreach (var propertyInfo in propertyInfos)
            foreach (var attr in propertyInfo.GetCustomAttributes<LocalizableAttribute>(true))
            {
                if (attr.Key.Equals("")) attr.Key = propertyInfo.Name;
                if (!config.ContainsKey(attr.Key, section))
                    config.Write(attr.Key, attr.Value, section);
                else
                    propertyInfo.SetValue(null, config.Read(attr.Key, section));
                break;
            }
        }
    }
}