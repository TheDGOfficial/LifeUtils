#region Header

// 
//        LifeUtils - LifeUtils - Annotations.cs
//                  01.12.2018 05:24

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