using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SmartPOS.Classes;
namespace SmartPOS.Forms
{
    public partial class MainOptions : Form
    {
        public MainOptions()
        {
            InitializeComponent();
        }
        public SqlDataAdapter adapter;
        private DataTable dataTable;
        private DataRow Row;
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainOptions_Load(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("Select Top 1 * From Options",adoClass.sqlcn);

            dataTable = new DataTable();
            try
            {
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    Row = dataTable.Rows[0];
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        txtRestName.Text = dataTable.Rows[i]["RestName"].ToString();
                        txtRestAddress1.Text = dataTable.Rows[i]["RestAddress1"].ToString();
                        txtRestAddress2.Text = dataTable.Rows[i]["RestAddress2"].ToString();
                        txtPhone.Text = dataTable.Rows[i]["telephone"].ToString();
                        txtPrinter.Text = dataTable.Rows[i]["PrinterName"].ToString();
                        txtReceiptLine1.Text = dataTable.Rows[i]["receiptLine1"].ToString();
                        txtReceiptLine2.Text = dataTable.Rows[i]["receiptLine2"].ToString();
                        if (dataTable.Rows[i]["logo"] != DBNull.Value)
                        {
                            pictureBox1.BackgroundImage = Helper.ByteToImage(dataTable.Rows[i]["logo"]);
                        }
                     }
                }
                else
                {
                    Row = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Save New Data ","?",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) 
            {
                SaveData();
            }

        }
        private void SaveData()
        {
            if (txtRestName.Text == string.Empty)
            {
                MessageBox.Show("PLease Enter Your Name");
                txtRestName.Focus();
                return;
            }
            if (txtPhone.Text == string.Empty)
            {
                MessageBox.Show("PLease Enter Your Phone Number");
                txtPhone.Focus();
                return;
            }
            if (Row == null)
            {
                Row = dataTable.NewRow();
                dataFillRow();
                dataTable.Rows.Add(Row);
            }
            else
            {
                Row.BeginEdit();
                dataFillRow();
                Row.EndEdit();
            }
            try 
            {
                adoClass.builder = new SqlCommandBuilder(adapter);
                adapter.Update(dataTable);
                MessageBox.Show("Data has been updated :)");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataFillRow()
        {
            Row["RestName"] = txtRestName.Text;
            Row["RestAddress1"] = txtRestAddress1.Text;
            Row["RestAddress2"] = txtRestAddress2.Text;
            Row["telephone"] = txtPhone.Text;
            Row["PrinterName"] =txtPrinter.Text;
            Row["receiptLine1"] =txtReceiptLine1.Text;
            Row["receiptLine2"] =txtReceiptLine2.Text;  
            if(pictureBox1.BackgroundImage != null)
            {
                Row["logo"] = Helper.ImageToByte(pictureBox1.BackgroundImage);
            }
        }

        private void btnSelectPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.png";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtPic.Text = openFileDialog.FileName;
                pictureBox1.BackgroundImage = new Bitmap(txtPic.Text);
            }
        }
    }
}
