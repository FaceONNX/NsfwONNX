using UMapx.Core;
using UMapx.Imaging;

namespace NsfwONNX.WinForms
{
    public partial class Form1 : Form
    {
        private Bitmap _bitmap;
        private readonly OpenNsfwClassifier _nsfwClassifier;

        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragDrop += Form1_DragDrop;
            this.DragEnter += Form1_DragEnter;
            this.BackgroundImageLayout = ImageLayout.Zoom;
            _nsfwClassifier = new OpenNsfwClassifier();
        }

        #region Form methods

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])(e.Data.GetData(DataFormats.FileDrop, true));
            TryOpenImage(files.FirstOrDefault());
        }

        #endregion

        #region Private methods

        private void TryOpenImage(string fileName)
        {
            try
            {
                _bitmap?.Dispose();
                _bitmap = new Bitmap(fileName);
                var results = _nsfwClassifier.Forward(_bitmap);
                var prob = Matrice.Max(results, out int index);
                var label = OpenNsfwClassifier.Labels[index];
                Console.WriteLine($"{fileName} classified as {label} with {prob}");

                if (label != OpenNsfwClassifier.Labels[0])
                {
                    var blur = new BoxBlur(2 * (int)(Math.Max(_bitmap.Width, _bitmap.Height) / 40.0));
                    blur.Apply(_bitmap);
                    blur.Apply(_bitmap);
                }

                this.BackgroundImage = _bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}