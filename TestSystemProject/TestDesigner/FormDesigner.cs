using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDesigner
{
    public partial class FormDesigner : Form
    {
        public FormDesigner()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                radioListBox_Answers.Items.Add(i);
            }
            
        }

    }
}
