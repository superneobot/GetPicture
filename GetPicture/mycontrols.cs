using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetPicture
{
    public class MButton : Button 
    {
        public MButton()
        {
            BackColor = Color.FromArgb(56, 56, 56);
            ForeColor = Color.Black;
            this.FlatStyle = FlatStyle.Flat;
            this.SetBounds(0, 0, 125, 28);
            this.UseVisualStyleBackColor = true;
            this.Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular, GraphicsUnit.Pixel);
            
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            BackColor = Color.FromArgb(10, 10, 10);
            ForeColor = Color.FromArgb(44, 44, 44);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = Color.FromArgb(56, 56, 56);
            ForeColor = Color.Black;
            base.OnMouseLeave(e);
        }
    }
}
