using RenameFiles.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace RenameFiles
{
    public partial class DoneControl : UserControl
    {
        public DoneControl()
        {
            InitializeComponent();
            var picture = new PictureBox
            {
                BackgroundImage = Resources.CheckMark,
                BackgroundImageLayout = ImageLayout.Stretch,
                Size = new Size(20, 20)
            };
            ClientSize = picture.Size;
            BackColor = Color.Transparent;
            
            Controls.Add(picture);
        }
    }
}