using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EncodingConverter.Controls
{
    class ToolStripEncodingViewer : ToolStripControlHost
    {

        #region ...ctor...

        public ToolStripEncodingViewer() : base(new EncodingsViewer())
        {
            var panel = Control as EncodingsViewer;
            Visible = true;
            Enabled = true;
            panel.AutoSize = false;
            panel.Size = new Size(500, 300);
            panel.MinimumSize = panel.Size;
        }
        #endregion

        public EncodingsViewer EncodingsViewer { get { return (EncodingsViewer)this.Control; } }

    }
}
