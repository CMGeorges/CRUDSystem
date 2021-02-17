using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;


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

        private async void Form1_Load(object sender, EventArgs e)//making the metgod async
        {
            await PopGridView();
        }

        private async Task PopGridView()//making the metgod async
        {

            using(var MyModelEntities = new MyModel())
            {
                dataGridViewResult.DataSource = await MyModelEntities.Details.ToListAsync<Detail>();
            }

        }

        /// <summary>
        /// Save Detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSave_Click(object sender, EventArgs e)//async make  the method as a asynchrone method
        {
            //Set Data
            MyDetail.Fname = txtFirstName.Text;
            MyDetail.Lname = txtLastName.Text;
            MyDetail.Address = txtAddress.Text;
            MyDetail.Age= Convert.ToInt32(txtAge.Text);
            MyDetail.DateOfBirth= Convert.ToDateTime(dateTimePickerBirthDate.Text);

            using(var MydbENtities = new MyModel())
            {


                if(MyDetail.ID == 0)
                {
                    MydbENtities.Details.Add(MyDetail);//save new details
                    await MydbENtities.SaveChangesAsync();//complete the async method


                    MessageBox.Show("Information has been Saved.","Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);//Show Message as the detail is saved.
                }
                else
                {
                    MydbENtities.Entry(MyDetail).State = EntityState.Modified;//update a detail
                    await MydbENtities.SaveChangesAsync();
                    btnSave.Text = "Save";
                    MyDetail.ID = 0;

                    MessageBox.Show("Information has been Updated.", "Modified", MessageBoxButtons.OK, MessageBoxIcon.Information);//Show Message as the detail is updated.
                }
             

            }

            await PopGridView();

        }



        /// <summary>
        /// to set the fields on doubleclicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void dataGridViewResult_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewResult.CurrentRow.Index != -1)
            {
                MyDetail.ID = Convert.ToInt32(dataGridViewResult.CurrentRow.Cells[0].Value);
                using(var MyDBEntities = new MyModel())
                {
                    MyDetail =  await MyDBEntities.Details.Where(x => x.ID == MyDetail.ID).FirstOrDefaultAsync();
                    txtFirstName.Text = MyDetail.Fname;
                    txtLastName.Text = MyDetail.Lname;
                    txtAge.Text = MyDetail.Age.ToString();
                    txtAddress.Text = MyDetail.Address;
                    dateTimePickerBirthDate.Text = MyDetail.DateOfBirth.ToString();

                    btnSave.Text = "Update";
                   
                }
                
                
            }
        }


        /// <summary>
        /// Remove some
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDelete_Click(object sender, EventArgs e)//making the metgod async
        {

            if (MessageBox.Show("Are you sure you want to delete this  information? ","Please Confirmed",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var MyDbEntites = new MyModel())
                {

                    var entry = MyDbEntites.Entry(MyDetail);
                    if (entry.State == EntityState.Detached)
                    {
                        MyDbEntites.Details.Attach(MyDetail);


                        MyDbEntites.Details.Remove(MyDetail);
                        await MyDbEntites.SaveChangesAsync();
                        await PopGridView();
                        ClearFields();
                        MessageBox.Show("Information has been Deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);//Show Message as the detail is deleted.
                    }

                }

            }

           
        }



        /// <summary>
        /// To clear the fields
        /// </summary>
        void ClearFields()
        {

            txtFirstName.Text ="";
            txtLastName.Text = "";
            txtAge.Text = "";
            txtAddress.Text = "";
            btnSave.Text = "Save";
            dateTimePickerBirthDate.Text = DateTime.Now.ToString();

        }

        private void BirthDate_ValueChanged(object sender, EventArgs e)
        {
            int dateDiff = DateTime.Now.Year - dateTimePickerBirthDate.Value.Year;

            txtAge.Text = dateDiff.ToString();
        }


        /// <summary>
        /// To Refresh the table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnRefresh_Click(object sender, EventArgs e)//making the metgod async
        {
            await PopGridView();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

    }
}
