using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter.Forms
{
    internal class MainFormLogic
    {
        public static Color InputTextErrorColor = Color.Red;
        public static Color InputTextNoErrorColor = Color.Black;

        public event EventHandler InputTextColorChanged;


        private Color _InputTextColor;
        string _InputText;

        public MainFormLogic()
        {
            Program.ECC.InputTextChanged += ECC_InputTextChanged;
        }

        private void ECC_InputTextChanged(object sender, EventArgs e)
        {
            _InputText = null;
        }

        public string InputText
        {
            get
            {
                //To save processing time, InputText will be processed one time and stored
                //in the _InputText. If it was null then we should read the file; Otherwise
                //we can just return the text:
                if (_InputText != null)
                    return _InputText;

                Exception ex;
                _InputText = Program.ECC.GetInputTextSafe(out ex);

                if (ex != null)
                {
                    //if (ex is OutOfMemoryException)
                    //{
                    //    _InputText = ex.Message;
                    //}
                    _InputText = $"An error occurred while reading input file:{Environment.NewLine}" + ex.Message;
                    this.InputTextColor = InputTextErrorColor;
                }
                else
                {
                    this.InputTextColor = Color.Black;
                }
                return _InputText;
            }
        }


        public Color InputTextColor
        {
            get { return _InputTextColor; }
            private set
            {
                if (_InputTextColor == value)
                    return;

                _InputTextColor = value;

                InputTextColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }


    }
}
