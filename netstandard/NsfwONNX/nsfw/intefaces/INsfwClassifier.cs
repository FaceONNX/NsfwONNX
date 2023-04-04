using System;
using System.Drawing;

namespace NsfwONNX
{
    /// <summary>
    /// Defines nsfw classifier interface.
    /// </summary>
    public interface INsfwClassifier : IDisposable
    {
        #region Interface

        /// <summary>
        /// Returns the classifier labels.
        /// </summary>
        string[] Labels { get; }

        /// <summary>
        /// Returns nsfw recognition results.
        /// </summary>
        /// <param name="image">Bitmap</param>
        /// <returns>Array</returns>
        float[] Forward(Bitmap image);

        /// <summary>
        /// Returns nsfw recognition results.
        /// </summary>
        /// <param name="image">Image in BGR terms</param>
        /// <returns>Array</returns>
        float[] Forward(float[][,] image);

        #endregion
    }
}
