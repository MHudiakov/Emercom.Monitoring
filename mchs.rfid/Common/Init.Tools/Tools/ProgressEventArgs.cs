// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressEventArgs.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Event arguments during processing of a single file or directory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Init.Tools
{
    /// <summary>
    /// Event arguments during processing of a single file or directory.
    /// </summary>
    public class ProgressEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// Initialise a new instance
        /// </summary>
        /// <param name="name">The file or directory name if known.</param>
        /// <param name="processed">The number of bytes processed so far</param>
        /// <param name="target">The total number of bytes to process, 0 if not known</param>
        public ProgressEventArgs(string name, long processed, long target)
        {
            this._name = name;
            this._processed = processed;
            this._target = target;
        }
        #endregion

        /// <summary>
        /// The name for this event if known.
        /// </summary>
        public string Name
        {
            get { return this._name; }
        }

        /// <summary>
        /// Get set a value indicating wether scanning should continue or not.
        /// </summary>
        public bool ContinueRunning
        {
            get { return this._continueRunning; }
            set { this._continueRunning = value; }
        }

        /// <summary>
        /// Get a percentage representing how much of the has been processed
        /// </summary>
        /// <value>0.0 to 100.0 percent; 0 if target is not known.</value>
        public float PercentComplete
        {
            get
            {
                float result;
                if (this._target <= 0)
                {
                    result = 0;
                }
                else
                {
                    result = (this._processed / (float)this._target) * 100.0f;
                }

                return result;
            }
        }

        #region Instance Fields

        /// <summary>
        /// The name which was used with the event.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Load process
        /// </summary>
        private readonly long _processed;

        /// <summary>
        /// A predetermined fixed target value to use with progress updates.
        /// If the value is negative the target is calculated by looking at the stream.
        /// </summary>
        private readonly long _target;

        /// <summary>
        /// Is continue running
        /// </summary>
        private bool _continueRunning = true;
        #endregion
    }

    #region ProgressHandler
    /// <summary>
    /// Delegate invoked during processing of a file or directory
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments.</param>
    public delegate void ProgressHandler(object sender, ProgressEventArgs e);
    #endregion
}