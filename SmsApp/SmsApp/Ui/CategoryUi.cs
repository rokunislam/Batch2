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
using SmsApp.Connection;
using SmsApp.Models;

namespace SmsApp
{
    public partial class CategoryUi : Form
    {
        private string conString = " Server=ROKO\\SQLEXPRESS; Database=SmsDb;  Integrated Security=True";
        Category aCategory = new Category();
        SqlConnection con;
        private string name;

        public CategoryUi()
        {
            InitializeComponent();
            showDataGridView.Text = "Sl\t\t|Name\n";
            showDataGridView.DataSource = GetCategoryInfo(aCategory);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            aCategory.Name = nameTextBox.Text;
            if (Exists(name))
            {
                MessageBox.Show("All ready exists");
                return;
            }
            try
            {
               

                if (!String.IsNullOrEmpty(nameTextBox.Text))
                {
                    bool isSaved = AddCategory(aCategory);
                    if (isSaved)
                    {
                        confirmLabel.Text = " Saved Successfully";
                        confirmLabel.ForeColor = Color.Green;

                       
                    }
                    else
                    {
                        confirmLabel.Text = " Saved Failed";
                        confirmLabel.ForeColor = Color.Red;
                    }
                }

                else
                {
                    confirmLabel.Text = "Plz Enter Category Name";
                    confirmLabel.ForeColor = Color.Red;
                }
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }


        private bool AddCategory( Category aCategory)
        
        {
            bool isSuccess = false;

            con = new SqlConnection(conString);
            string query = "Insert Into Category Values('" + aCategory.Name + "')";
            SqlCommand command= new SqlCommand(query,con);
            con.Open();

            int isExecuted = command.ExecuteNonQuery();
            if (isExecuted>0)
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

        private DataTable GetCategoryInfo(Category aCategory)
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
        private bool Exists(string Name)
        {

            bool isExists = false;

            try
            {

                //2
             con = new SqlConnection(conString);

                //4
                string query = @"SELECT * FROM Category WHERE Name = '" + aCategory.Name + "'";

                //5
                SqlCommand sqlCommand = new SqlCommand(query, con);
                //6
                con.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count < 0)
                {
                    isExists = false;
                }
                else
                {
                    isExists = true;
                }
                //7
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                string data = "";
                if (sqlDataReader.Read())
                {
                    data = sqlDataReader["Id"].ToString();
                }

                if (!String.IsNullOrEmpty(data))



                    con.Close();


            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }

            return isExists;
        }

        private void showDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            nameTextBox.Text = showDataGridView.Rows[rowIndex].Cells[1].Value.ToString();
        }
    }


}
