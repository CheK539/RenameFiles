using RenameFiles.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace RenameFiles
{
    public partial class CrossMarkControl : UserControl
    {
        public CrossMarkControl()
        {
            InitializeComponent();
            var picture = new PictureBox
            {
                BackgroundImage = Resources.CrossMark,
                BackgroundImageLayout = ImageLayout.Stretch,
                Size = new Size(20, 20)
            };
            ClientSize = picture.Size;
            BackColor = Color.Transparent;
            
            Controls.Add(picture);
        }
    }
}