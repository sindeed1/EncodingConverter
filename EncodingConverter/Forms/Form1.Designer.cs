namespace EncodingConverter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.encodingsTool1 = new AHD.SM.Controls.EncodingsTool();
            this.SuspendLayout();
            // 
            // encodingsTool1
            // 
            this.encodingsTool1.Location = new System.Drawing.Point(127, 63);
            this.encodingsTool1.Name = "encodingsTool1";
            this.encodingsTool1.SelectedEncoding = ((System.Text.Encoding)(resources.GetObject("encodingsTool1.SelectedEncoding")));
            this.encodingsTool1.Size = new System.Drawing.Size(420, 214);
            this.encodingsTool1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.encodingsTool1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private AHD.SM.Controls.EncodingsTool encodingsTool1;
    }
}

