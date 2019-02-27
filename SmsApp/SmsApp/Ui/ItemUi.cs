using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmsApp.Models;

namespace SmsApp.Ui
{
    public partial class ItemUi : Form
    {
        private string conString = " Server=ROKO\\SQLEXPRESS; Database=SmsDb;  Integrated Security=True";
        SqlConnection con;
        Category aCategory = new Category();
        Company aCompany = new Company();
        Item aItem = new Item();

        public ItemUi()
        {
            InitializeComponent();
            categoryComboBox.DataSource = GetCategoryValue();
            companyComboBox.DataSource = GetCompanyValue(aCompany);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            aItem.CategoryId = Convert.ToInt32(categoryComboBox.SelectedValue);
            aItem.CategoryId = Convert.ToInt32(companyComboBox.SelectedValue);
            aItem.Name = nameTextBox.Text;
            aItem.ReorderLevel = Convert.ToInt32(reorderLevelTextBox.Text);
            try
            {
                bool isSaved = addItems(aItem);
                if (isSaved)
                {
                    confirmLabel.Text = "Saved Successfully";
                    confirmLabel.ForeColor = Color.Green;
                }

                else
                {
                    confirmLabel.Text = "Saved Failed";
                    confirmLabel.ForeColor = Color.Red;
                }
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        private DataTable GetCategoryValue()
        {
            con = new SqlConnection(conString);

            string query = "select * From Category ";

            SqlCommand command = new SqlCommand(query, con);

            con.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable aTable = new DataTable();
            dataAdapter.Fill(aTable);

            con.Close();
            return aTable;

        }

        private DataTable GetCompanyValue(Company aCompany)
        {
            con = new SqlConnection(conString);

            string query = "select Id, Name From Company WHERE Id= "+aCompany.Id+"";

            SqlCommand command = new SqlCommand(query, con);

            con.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable aTable = new DataTable();
            dataAdapter.Fill(aTable);

            con.Close();
            return aTable;

        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            aCompany.Id = Convert.ToInt32(categoryComboBox.SelectedValue);
            companyComboBox.DataSource = GetCompanyValue(aCompany);
        }

        private bool addItems(Item aItem)
        {
            bool isSuccess = false;
            con = new SqlConnection(conString);
            string query = "Insert Into Items Values('" + aItem.CategoryId + "','"+aItem.CompanyId+"','"+aItem.Name+"','"+aItem.ReorderLevel+"')";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();

            int isExecuted = command.ExecuteNonQuery();
            if (isExecuted > 0)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            con.Close();
            return isSuccess;
        }

    }
}
