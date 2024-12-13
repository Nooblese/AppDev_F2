using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace _045_basseg_Alcantara_F2
{
    public partial class Form1 : Form
    {
        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/basse/Downloads/dpPirates.accdb"; 
        OleDbConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string query = "select piratename as ALIAS, givenname as Name, age as Age, pirategroup as PIRATEGROUP, bounty as BOUNTY from pirates";
            //string query = "select * from pirates";
            conn = new OleDbConnection(connStr);
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query,conn);
            adapter.Fill(dt);
            grdView.DataSource = dt;
            conn.Close();
            grdView.Columns["Age"].Visible = false;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //string query = "select piratename as ALIAS, givenname as Name, pirategroup as PIRATEGROUP, bounty as BOUNTY from pirates where piratename like '%" + txtSearch.Text + "%' or givenname like '%" + txtSearch.Text + "%' and pirategroupt like '%" + cmbSearch.Text + "%' ";
            ////string query = "select * from pirates";
            //conn = new OleDbConnection(connStr);
            //conn.Open();
            //OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            //adapter.Fill(dt);
            //grdView.DataSource = dt;
            //conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string query = "select piratename as ALIAS, givenname as Name, age as Age, pirategroup as PIRATEGROUP, bounty as BOUNTY from pirates where piratename like '%" + txtSearch.Text + "%' or givenname like '%" + txtSearch.Text + "%' and pirategroup = '" + cmbSearch.Text + "' ";
            conn = new OleDbConnection(connStr);
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            adapter.Fill(dt);
            grdView.DataSource = dt;
            conn.Close();
            grdView.Columns["Age"].Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (grdView.SelectedRows.Count > 0)
            {
                
                DataGridViewRow selectedRow = grdView.SelectedRows[0];

                // Populate the textboxes and combobox with data from the selected row
                txtAlias.Text = selectedRow.Cells["ALIAS"].Value.ToString();
                txtName.Text = selectedRow.Cells["Name"].Value.ToString();
                txtAge.Text = selectedRow.Cells["Age"].Value.ToString();
                cmbPirateGroup.SelectedItem = selectedRow.Cells["PIRATEGROUP"].Value.ToString();
                txtBounty.Text = selectedRow.Cells["BOUNTY"].Value.ToString();

                // Enable editing controls
                txtAlias.Enabled = true;
                txtName.Enabled = true;
                cmbPirateGroup.Enabled = true;
                txtBounty.Enabled = true;

                // Disable New Record button
                btnRecord.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please select a row to view details.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (grdView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = grdView.SelectedRows[0];
                int crewID = Convert.ToInt32(selectedRow.Cells["CrewID"].Value);
                string name = textBoxName.Text;
                int bounty = Convert.ToInt32(textBoxBounty.Text);

                // Validate Bounty
                if (bounty < 0)
                {
                    MessageBox.Show("Bounty cannot be negative.");
                    return;
                }

                string query = "UPDATE pirates set piratename =@alias, givenname =@name, age =@age, pirategroup = '"+cmbPirateGroup.Text+"', bounty =@bounty where ID";
            //DataTable dt = new DataTable();
            conn = new OleDbConnection(connStr);
            OleDbCommand cmd = new OleDbCommand(query, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@alias", txtAlias.Text);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@age", txtAge.Text);
            cmd.Parameters.AddWithValue("@bounty", txtBounty.Text);
            cmd.ExecuteNonQuery();
            conn.Close();


        }
    }
    
}
