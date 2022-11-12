﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Supermarket_Management_System_In_csharp
{
    public partial class UC_categories : UserControl
    {
        public UC_categories()
        {
            InitializeComponent();
        }

        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        private void auto()
        {
            txt_categorieid.Text = "" + GetUniqueKey(5);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_categorieid.Text == "" || txt_categoriename.Text == "")
                {
                    MessageBox.Show("Please , Insert all Information ... ", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\hp\documents\visual studio 2010\Projects\Supermarket Management System In csharp\Supermarket Management System In csharp\SMS.mdf;Integrated Security=True;User Instance=True");

                    con.Open();

                    String str = "Insert Into db_categories (catid,catname)Values('" + txt_categorieid.Text + "','" + txt_categoriename.Text + "')";

                    SqlCommand cmd = new SqlCommand(str, con);

                    cmd.ExecuteNonQuery();

                    String str2 = "Select max(catid) From db_categories";

                    SqlCommand cmd2 = new SqlCommand(str2, con);

                    SqlDataReader dr = cmd2.ExecuteReader();

                    if (dr.Read())
                    {
                        showdata();

                        MessageBox.Show("Categories Created Successfull ....", "Thank You", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        clear();

                        auto();

                        con.Close();

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear()
        {
            txt_categorieid.Clear();
            txt_categoriename.Clear();
        }

        private void showdata()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\hp\documents\visual studio 2010\Projects\Supermarket Management System In csharp\Supermarket Management System In csharp\SMS.mdf;Integrated Security=True;User Instance=True");

            con.Open();

            String str = "Select * From db_categories";

            SqlCommand cmd = new SqlCommand(str, con);

            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            db_categoriesDataGridView.DataSource = dt;

            con.Close();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_categorieid.Text == "" || txt_categoriename.Text == "")
                {
                    MessageBox.Show("Please , Insert all Information ... ", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\hp\documents\visual studio 2010\Projects\Supermarket Management System In csharp\Supermarket Management System In csharp\SMS.mdf;Integrated Security=True;User Instance=True");

                    con.Open();

                    String str = "Update db_categories Set catname = '" + txt_categoriename.Text + "' Where catid = '" + txt_categorieid.Text + "'";

                    SqlCommand cmd = new SqlCommand(str, con);

                    cmd.ExecuteNonQuery();

                    String str2 = "Select max(catid) From db_categories";

                    SqlCommand cmd2 = new SqlCommand(str2, con);

                    SqlDataReader dr = cmd2.ExecuteReader();

                    if (dr.Read())
                    {
                        showdata();

                        MessageBox.Show("Categories Updated Successfull ....", "Thank You", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        clear();

                        auto();

                        con.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {

            try
            {
                if (txt_categorieid.Text == "" || txt_categoriename.Text == "")
                {
                    MessageBox.Show("Please , Insert all Information ... ", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\hp\documents\visual studio 2010\Projects\Supermarket Management System In csharp\Supermarket Management System In csharp\SMS.mdf;Integrated Security=True;User Instance=True");

                    con.Open();

                    String str = "Delete From db_categories Where catid = '" + txt_categorieid.Text + "'";

                    SqlCommand cmd = new SqlCommand(str, con);

                    cmd.ExecuteNonQuery();

                    showdata();

                    MessageBox.Show("User Delete Successfull ....", "Thank You", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clear();

                    auto();

                    con.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       
        }

        private void pcb_serch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\hp\documents\visual studio 2010\Projects\Supermarket Management System In csharp\Supermarket Management System In csharp\SMS.mdf;Integrated Security=True;User Instance=True");

            con.Open();

            String str = "Select catid,catname From db_categories Where catid = '" + txt_categorieid.Text + "'";

            SqlCommand cmd = new SqlCommand(str, con);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txt_categorieid.Text = dr.GetValue(0).ToString();
                txt_categoriename.Text = dr.GetValue(1).ToString();
            }

            con.Close();
        }

        private void pcb_search1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\hp\documents\visual studio 2010\Projects\Supermarket Management System In csharp\Supermarket Management System In csharp\SMS.mdf;Integrated Security=True;User Instance=True");
            
            con.Open();

            String str = "Select catid,catname From db_categories Where catname = '" + txt_categoriename.Text + "'";

            SqlCommand cmd = new SqlCommand(str, con);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txt_categorieid.Text = dr.GetValue(0).ToString();
                txt_categoriename.Text = dr.GetValue(1).ToString();
            }

            con.Close();

        }

        private void db_categoriesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.db_categoriesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.db_categories);

        }

        private void UC_categories_Load(object sender, EventArgs e)
        {
            auto();
            showdata();
        }
    }
}
