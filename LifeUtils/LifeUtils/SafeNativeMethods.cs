#region Header

// 
//        LifeUtils - LifeUtils - SafeNativeMethods.cs
//                  01.12.2018 05:24

#endregion

namespace LifeUtils
{
    #region Imports

    using System.Runtime.InteropServices;
    using System.Text;

    #endregion

    /// <summary>
    ///     Includes all safe native methods used by LifeAPI.
    /// </summary>
    public static class SafeNativeMethods
    {
        /// <summary>
        ///     Native method for file I/O.
        /// </summary>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint
            WritePrivateProfileString(string section, string key, string value, string filePath);

        /// <summary>
        ///     Native method for file I/O.
        /// </summary>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint GetPrivateProfileString(string section, string key, string Default,
            StringBuilder retVal, uint size, string filePath);
    }
}