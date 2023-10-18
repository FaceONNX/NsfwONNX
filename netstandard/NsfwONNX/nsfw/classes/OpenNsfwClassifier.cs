using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UMapx.Core;
using UMapx.Imaging;

namespace NsfwONNX
{
    /// <summary>
    /// Defines open nsfw classifier.
    /// </summary>
    /// <remarks>
    /// Implementation of the https://github.com/yahoo/open_nsfw.
    /// </remarks>
    public class OpenNsfwClassifier : INsfwClassifier
    {
        #region Private data

        /// <summary>
        /// Inference session.
        /// </summary>
        private readonly InferenceSession _session;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes open nsfw classifier.
        /// </summary>
        public OpenNsfwClassifier()
        {
            _session = new InferenceSession(Resources.resnet_50_1by2_nsfw);
        }

        /// <summary>
        /// Initializes open nsfw classifier.
        /// </summary>
        /// <param name="options">Session options</param>
        public OpenNsfwClassifier(SessionOptions options)
        {
            _session = new InferenceSession(Resources.resnet_50_1by2_nsfw, options);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the classifier labels.
        /// </summary>
        public static readonly string[] Labels = new string[] { "Safe", "Nsfw" };

        #endregion

        #region Methods

        /// <inheritdoc/>
        public float[] Forward(Bitmap image)
        {
            var rgb = image.ToRGB(false);
            return Forward(rgb);
        }

        /// <inheritdoc/>
        public float[] Forward(float[][,] image)
        {
            if (image.Length != 3)
                throw new ArgumentException("Image must be in BGR terms");

            var size = new Size(224, 224);
            var resized = new float[3][,];

            for (int i = 0; i < image.Length; i++)
            {
                resized[i] = image[i].Resize(size.Height, size.Width);
            }

            var inputMeta = _session.InputMetadata;
            var name = inputMeta.Keys.ToArray()[0];

            // pre-processing
            var dimentions = new int[] { 1, 3, size.Height, size.Width };
            var tensors = resized.ToFloatTensor(false);
            tensors.Compute(new float[] { 104, 117, 123 }, Matrice.Sub);
            var inputData = tensors.Merge(true);

            // session run
            var t = new DenseTensor<float>(inputData, dimentions);
            var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor(name, t) };
            using var outputs = _session.Run(inputs);
            var results = outputs.ToArray();
            var length = results.Length;
            var confidences = results[length - 1].AsTensor<float>().ToArray();

            return confidences;
        }

        #endregion

        #region IDisposable

        private bool _disposed;

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _session?.Dispose();
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Destructor of the open nsfw classifier.
        /// </summary>
        ~OpenNsfwClassifier()
        {
            Dispose(false);
        }

        #endregion
    }
}