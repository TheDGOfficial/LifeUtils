#region Header

// 
//        LifeUtils - LifeUtils - Annotations.cs
//                  19.11.2018 06:12

#endregion

namespace LifeUtils.Annotations
{
    #region Imports

    using System;

    #endregion

    public static class Annotations
    {
        public static void Init(Type type, IConfigFile config)
        {
            LocalizableAttribute.Init(type, config);
            ConfigurableAttribute.Init(type, config);
        }
    }
}