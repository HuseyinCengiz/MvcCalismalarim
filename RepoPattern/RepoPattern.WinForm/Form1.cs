using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepoPattern.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UrunServiceReference.ServiceOf_UrunDTOClient client = new UrunServiceReference.ServiceOf_UrunDTOClient();
            dataGridView1.DataSource = client.Liste();
        }
    }
}
