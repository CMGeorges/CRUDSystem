using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRUDSystem.Entities;

namespace CRUDSystem
{
    public partial class Form1 : Form
    {

        Detail MyDetail = new Detail();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopGridView();
        }

        private void PopGridView()
        {

            using(var MyModelEntities = new MyModel())
            {
                dataGridViewResult.DataSource = MyModelEntities.Details.ToList<Detail>();
            }

        }

        /// <summary>
        /// Save Detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Set Data
            MyDetail.Fname = txtFirstName.Text;
            MyDetail.Lname = txtLastName.Text;
            MyDetail.Address = txtAddress.Text;
            MyDetail.Age= Convert.ToInt32(txtAge.Text);
            MyDetail.DateOfBirth= Convert.ToDateTime(dateTimePickerBirthDate.Text);

            using(var MydbENtities = new MyModel())
            {
                MydbENtities.Details.Add(MyDetail);//save new details
                MydbENtities.SaveChanges();
            }

            PopGridView();

        }
    }
}
