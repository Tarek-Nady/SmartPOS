using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SmartPOS.Forms;

namespace SmartPOS
{
    public partial class MainForm : Form
    {
        private Button currentButton;
        private Form activeForm;
        public MainForm()
        {
            InitializeComponent();
        }
       private void MainForm_Load(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
            this.Text = string.Empty;
            this.ControlBox = false;
        }

        private void OpenChildForm(Form cform, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = cform;   
            ActiveButton(btnSender);
            cform.TopLevel = false;
            cform.FormBorderStyle = FormBorderStyle.None;
            cform.Dock = DockStyle.Fill;
            pnlMainForm.Controls.Add(cform);
            pnlMainForm.Tag = cform;
            cform.BringToFront();
            cform.Show();
        }
        private Color SelectTheme()
        {
            if(currentButton.Text=="Point Of Sale")
            {
                return Color.Red;
            }
            else if (currentButton.Text == "Setup")
            {
                return Color.Gray;
            }
            else if (currentButton.Text == "Reporting")
            {
                return Color.Blue;
            }
            else if (currentButton.Text == "Options")
            {
                return Color.Green;
            }
            else
            {
                return Color.White;
            }
        }

        private void ActiveButton(object sender) 
        { 
            if(sender != null)
            {
                if(currentButton != (Button)sender)
                {

                    unSelectButton();
                    currentButton = (Button)sender;
                    Color color = SelectTheme();
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new Font("Tahoma", 11F, FontStyle.Bold);
                    pnlTitle.BackColor = color;
                    lblTitle.Text= currentButton.Text;

                }
            }
        }

        private void unSelectButton()
        {
            foreach(Control ctr in pnlMenu.Controls)
            {
                if (ctr.GetType() == typeof(Button))
                {
                    ctr.BackColor = Color.DarkSalmon;
                    ctr.ForeColor = Color.White;
                    ctr.Font = new Font("Tahoma", 8F, FontStyle.Regular);

                }
            }
        }
        private void btnPOS_Click(object sender, EventArgs e)
        {
            OpenChildForm(new MainPointOfSale(),sender);
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            OpenChildForm(new MainSetup(), sender);
        }

        private void btnReporting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new MainReports(), sender);
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            OpenChildForm(new MainOptions(), sender);
            
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnlTitle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.google.com"); 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
