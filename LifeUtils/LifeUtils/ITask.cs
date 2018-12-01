#region Header

// 
//        LifeUtils - LifeUtils - ITask.cs
//                  01.12.2018 05:24

#endregion

namespace LifeUtils
{
    /// <summary>
    ///     Represents a runnable task.
    /// </summary>
    public interface ITask
    {
        /// <summary>
        ///     Runs this task.
        /// </summary>
        void Run();
    }
}