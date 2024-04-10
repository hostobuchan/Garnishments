using System.Drawing;

namespace System.Windows.Forms
{
    public partial class ImageButton : PictureBox
    {
        private Enums.ButtonState bState = Enums.ButtonState.ButtonUp;
        private Enums.MouseCapture bMouse = Enums.MouseCapture.Outside;

        public string TextDisplayed { get { return this.Text; } set { this.Text = value; } }

        public Font TextFont { get { return this.Font; } set { this.Font = value; } }

        public Color TextColor { get { return this.ForeColor; } set { this.ForeColor = value; } }

        public ImageButton()
        {
            InitializeComponent();
        }

        private void ImageButton_MouseEnter(object sender, EventArgs e)
        {
            bMouse = Enums.MouseCapture.Inside;
            Refresh();
        }

        private void ImageButton_MouseDown(object sender, MouseEventArgs e)
        {
            bState = Enums.ButtonState.ButtonDown;
            Refresh();
        }

        private void ImageButton_MouseLeave(object sender, EventArgs e)
        {
            bState = Enums.ButtonState.ButtonUp;
            bMouse = Enums.MouseCapture.Outside;
            Refresh();
        }

        private void ImageButton_MouseUp(object sender, MouseEventArgs e)
        {
            bState = Enums.ButtonState.ButtonUp;
            Refresh();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawOverlay(e.Graphics);
            DrawText(e.Graphics);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        private void DrawOverlay(Graphics gfx)
        {
            if (this.bMouse == Enums.MouseCapture.Inside && this.bState == Enums.ButtonState.ButtonUp)
                using (SolidBrush s = new SolidBrush(Color.FromArgb(32, 255, 255, 255)))
                {
                    gfx.FillRectangle(s, this.ClientRectangle);
                }
            if (this.bMouse == Enums.MouseCapture.Inside && this.bState == Enums.ButtonState.ButtonDown)
                using (SolidBrush s = new SolidBrush(Color.FromArgb(32, 0, 0, 0)))
                {
                    gfx.FillRectangle(s, this.ClientRectangle);
                }
        }

        private void DrawText(Graphics gfx)
        {
            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                gfx.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), this.ClientRectangle, format);
            }
        }
    }
}
