using RenameFiles.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RenameFiles
{
    public partial class MainWindow : Form
    {
        private readonly Label selectFilesLabel = new Label();
        private readonly PictureBox openFileButton = new PictureBox();
        private readonly Label selectNameLabel = new Label();
        private readonly PictureBox line = new PictureBox();
        private readonly PictureBox acceptNameButton = new PictureBox();
        private readonly OpenFileDialog openFileDialog = new OpenFileDialog();
        private readonly Renamer renamer = new Renamer();
        private readonly TextBox textBox = new TextBox();
        private string[] files;
        private readonly DoneControl doneControl = new DoneControl();
        private readonly CrossMarkControl crossMarkControl = new CrossMarkControl();

        public MainWindow()
        {
            InitializeComponent();
            ClientSize = new Size(450, 250);
            BackgroundImage = Resources.LittleGirl;
            BackgroundImageLayout = ImageLayout.Stretch;
            CenterToScreen();

            selectFilesLabel.Font = new Font("Montserrat", 10);
            selectFilesLabel.AutoSize = true;
            selectFilesLabel.Text = "Select files:";
            selectFilesLabel.BackColor = Color.Transparent;
            selectFilesLabel.ForeColor = Color.Red;

            openFileButton.BackgroundImage = Resources.OpenDialogButton;
            openFileButton.BackgroundImageLayout = ImageLayout.Stretch;
            openFileButton.BackColor = Color.Transparent;
            openFileButton.Click += OpenFileButtonClick;

            selectNameLabel.Font = new Font("Montserrat", 10);
            selectNameLabel.AutoSize = true;
            selectNameLabel.Text = "Enter new name:";
            selectNameLabel.BackColor = Color.Transparent;
            selectNameLabel.ForeColor = Color.Red;

            textBox.Font = new Font("Montserrat", 8);
            textBox.BorderStyle = BorderStyle.None;
            textBox.ForeColor = Color.Red;

            line.Image = Resources.Line;

            acceptNameButton.BackgroundImage = Resources.AcceptButton;
            acceptNameButton.BackgroundImageLayout = ImageLayout.Stretch;
            acceptNameButton.BackColor = Color.Transparent;
            acceptNameButton.Click += AcceptNameButtonClick;

            Controls.Add(selectFilesLabel);
            Controls.Add(openFileButton);
            Controls.Add(selectNameLabel);
            Controls.Add(textBox);
            Controls.Add(line);
            Controls.Add(acceptNameButton);
            Controls.Add(doneControl);
            Controls.Add(crossMarkControl);

            doneControl.Hide();
            crossMarkControl.Hide();

            SizeChanged += SetSize;
            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Icon = Resources.IconImage;
            Text = "Rename Files";
            DoubleBuffered = true;
        }

        private void SetSize(object sender, EventArgs args)
        {
            var graphics = CreateGraphics();

            openFileButton.Size = openFileButton.BackgroundImage.Size;
            textBox.Size = new Size(100, 30);
            line.Size = new Size(100, 2);
            acceptNameButton.Size = acceptNameButton.BackgroundImage.Size;

            var filesLabelWidth = ClientSize.Width / 2 - (selectFilesLabel.Size.Width + openFileButton.Size.Width) / 2;
            selectFilesLabel.Location = new Point(filesLabelWidth, ClientSize.Height / 2 - 79);
            doneControl.Location = new Point(selectFilesLabel.Location.X - doneControl.Size.Width,
                selectFilesLabel.Location.Y);
            crossMarkControl.Location = new Point(doneControl.Location.X - 2, doneControl.Location.Y);
            openFileButton.Location = new Point(selectFilesLabel.Location.X + selectFilesLabel.Size.Width,
                selectFilesLabel.Location.Y + (int) Math.Round(6 * graphics.DpiY / 100));
            var nameLabelWidth = ClientSize.Width / 2 -
                                 (selectNameLabel.Size.Width + textBox.Size.Width + acceptNameButton.Size.Width + 4) /
                                 2;
            selectNameLabel.Location = new Point(nameLabelWidth,
                selectFilesLabel.Location.Y + selectFilesLabel.Size.Height + 10);
            textBox.Location = new Point(selectNameLabel.Location.X + selectNameLabel.Size.Width,
                selectNameLabel.Location.Y + 5);
            line.Location = new Point(textBox.Location.X, textBox.Location.Y + textBox.Size.Height);
            acceptNameButton.Location =
                new Point(textBox.Location.X + textBox.Size.Width + 4, selectNameLabel.Location.Y + 5);
        }

        private void OpenFileButtonClick(object sender, EventArgs args)
        {
            openFileDialog.Multiselect = true;
            var result = openFileDialog.ShowDialog();
            switch (result)
            {
                case DialogResult.OK:
                    crossMarkControl.Hide();
                    doneControl.Show();
                    break;
                case DialogResult.Cancel:
                    doneControl.Hide();
                    crossMarkControl.Show();
                    break;
            }

            files = openFileDialog.FileNames;
        }

        private void AcceptNameButtonClick(object sender, EventArgs args)
        {
            if (textBox.Text == string.Empty)
            {
                MessageBox.Show("Enter new name first!", "Error", MessageBoxButtons.OK);
                return;
            }

            if (files == null)
            {
                MessageBox.Show("Select files first!", "Error", MessageBoxButtons.OK);
                return;
            }

            textBox.ReadOnly = true;
            renamer.Rename(files, textBox.Text);
            if (MessageBox.Show("Rename is complete!", "", MessageBoxButtons.OK) == DialogResult.OK)
                InitialState();
        }

        private void InitialState()
        {
            doneControl.Hide();
            textBox.ReadOnly = false;
            textBox.Text = "";
        }
    }
}