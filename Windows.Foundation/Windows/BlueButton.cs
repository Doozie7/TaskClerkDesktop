using System;
using System.Drawing;
using System.Windows.Forms;

namespace BritishMicro.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public class BlueButton : Button
    {
        /// <summary>
        /// 
        /// </summary>
        public BlueButton()
        {
            Leave += new EventHandler(BlueButton_Leave);
            Enter += new EventHandler(BlueButton_Enter);
            MouseHover += new EventHandler(BlueButton_MouseHover);

            FlatAppearance.BorderColor = Color.LightSteelBlue;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.CheckedBackColor = SystemColors.InactiveCaptionText;
            FlatAppearance.MouseDownBackColor = SystemColors.InactiveCaptionText;
            FlatAppearance.MouseOverBackColor = SystemColors.InactiveCaptionText;
            FlatStyle = FlatStyle.Flat;
            ImageAlign = ContentAlignment.TopLeft;
            TextAlign = ContentAlignment.TopLeft;
            TextImageRelation = TextImageRelation.ImageBeforeText;
        }

        private void BlueButton_Enter(object sender, EventArgs e)
        {
            BackColor = SystemColors.MenuHighlight;
            FlatAppearance.BorderSize = 1;
        }

        private void BlueButton_Leave(object sender, EventArgs e)
        {
            BackColor = SystemColors.Control;
            FlatAppearance.BorderSize = 0;
        }

        private void BlueButton_MouseHover(object sender, EventArgs e)
        {
            Focus();
            BackColor = SystemColors.MenuHighlight;
        }
    }
}