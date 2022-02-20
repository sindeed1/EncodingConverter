using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncodingConverter.Controls
{
    internal class InputEncodingToolStrip : ToolStrip
    {
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripDropDownButton tsddInputEncoding;

        #region ...ctor...

        public InputEncodingToolStrip()
        {
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsddInputEncoding = new System.Windows.Forms.ToolStripDropDownButton();

            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsddInputEncoding});

            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(91, 22);
            this.toolStripLabel1.Text = "Input Encoding:";
            // 
            // tsddInputEncoding
            // 
            this.tsddInputEncoding.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            //this.tsddInputEncoding.Image = ((System.Drawing.Image)(resources.GetObject("tsddInputEncoding.Image")));
            //this.tsddInputEncoding.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddInputEncoding.Name = "tsddInputEncoding";
            this.tsddInputEncoding.Size = new System.Drawing.Size(114, 22);
            this.tsddInputEncoding.Text = "Arabic (Windows)";
        }
        #endregion

        private Encoding _Encoding;

        public Encoding Encoding
        {
            get { return _Encoding; }
            set { _Encoding = value; }
        }


    }
}
