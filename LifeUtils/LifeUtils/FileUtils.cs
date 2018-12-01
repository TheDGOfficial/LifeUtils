#region Header

// 
//        LifeUtils - LifeUtils - FileUtils.cs
//                  01.12.2018 05:24

#endregion

namespace LifeUtils
{
    #region Imports

    using System;
    using System.Diagnostics;
    using System.IO;
    using Microsoft.Win32;

    #endregion

    /// <summary>
    ///     File & folder utilities for C#.
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        ///     Checks if a file exists in the given path.
        ///     If path is null or empty, false is returned.
        ///     Returns false in any exception / error.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>True if a file exists in the given path.</returns>
        public static bool FileExists(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            try
            {
                return File.Exists(path);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Gets java installation directory of computer.
        ///     Return value can be null. If it was null,
        ///     we are unable to detect java installation path,
        ///     so the computer doesn't have java or the path is unknown.
        /// </summary>
        /// <returns>The java installation path.</returns>
        public static string GetJavaHome()
        {
            try
            {
                var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
                if (!string.IsNullOrEmpty(javaHome) && !string.IsNullOrWhiteSpace(javaHome))
                    return javaHome;

                var jreHome = Environment.GetEnvironmentVariable("JRE_HOME");
                if (!string.IsNullOrEmpty(jreHome) && !string.IsNullOrWhiteSpace(jreHome))
                    return jreHome;

                var jdkHome = Environment.GetEnvironmentVariable("JDK_HOME");
                if (!string.IsNullOrEmpty(jdkHome) && !string.IsNullOrWhiteSpace(jdkHome))
                    return jdkHome;

                var registryPath = GetJavaHomeFromRegistry()?.Trim();
                if (!string.IsNullOrEmpty(registryPath) && !string.IsNullOrWhiteSpace(registryPath))
                    return registryPath;

                var processPath = GetJavaHomeFromConsole();

                return !string.IsNullOrEmpty(processPath) && !string.IsNullOrWhiteSpace(processPath)
                    ? processPath
                    : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     Gets the java home from console.
        ///     It's the most comfortable solution after environment variables.
        /// </summary>
        /// <returns>The java home from console.</returns>
        public static string GetJavaHomeFromConsole()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "for %i in (java.exe) do @echo. %-$PATH:i",
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };

            process.Start();

            var javaHome = process.StandardOutput.ReadToEnd();

            try
            {
                process.Kill();
            }
            catch
            {
                /* ignored */
            }

            return javaHome;
        }

        /// <summary>
        ///     Returns the java home from registry.
        ///     Can be null. You should use GetJavaHome() instead.
        /// </summary>
        /// <returns>The java home from registry.</returns>
        public static string GetJavaHomeFromRegistry()
        {
            const string javaKey = @"SOFTWARE\JavaSoft\Java Runtime Environment\";

            try
            {
                using (var rk = Registry.LocalMachine.OpenSubKey(javaKey))
                {
                    var currentVersion = rk?.GetValue("CurrentVersion").ToString();
                    using (var key = rk?.OpenSubKey(currentVersion))
                    {
                        return key?.GetValue("JavaHome").ToString();
                    }
                }
            }
            catch
            {
                try
                {
                    using (var rk = Registry.CurrentUser.OpenSubKey(javaKey))
                    {
                        var currentVersion = rk?.GetValue("CurrentVersion").ToString();
                        using (var key = rk?.OpenSubKey(currentVersion))
                        {
                            return key?.GetValue("JavaHome").ToString();
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///     Deletes a directory in the given path.
        ///     Includes all subdirectories and files, Ignores any exceptions / errors.
        /// </summary>
        /// <param name="path">The directories path to delete it.</param>
        public static void DeleteDirectory(string path)
        {
            foreach (var directory in Directory.GetDirectories(path)) DeleteDirectory(directory);
            try
            {
                Directory.Delete(path, true);
            }
            catch (IOException)
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch
                {
                    /* ignored */
                }
            }
            catch (UnauthorizedAccessException)
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch
                {
                    /* ignored */
                }
            }
            catch
            {
                /* ignored */
            }
        }
    }
}