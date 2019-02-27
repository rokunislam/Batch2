using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmsApp.Ui
{
    public partial class UserUi : Form
    {
        public UserUi()
        {
            InitializeComponent();
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoryUi Category= new CategoryUi();
            Category.Show();
        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompanyUi Company= new CompanyUi();
            Company.Show();
        }

        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemUi Item= new ItemUi();
            Item.Show();
        }
    }
}
