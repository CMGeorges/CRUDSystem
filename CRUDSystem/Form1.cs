using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRUDSystem.Application.Abstractions;
using CRUDSystem.Application.Services;
using CRUDSystem.Application.Validation;
using CRUDSystem.Entities;
using CRUDSystem.Infrastructure;

namespace CRUDSystem
{
    public partial class Form1 : Form
    {
        private readonly IDetailService _detailService;
        private int _selectedDetailId;

        public Form1()
            : this(CompositionRoot.CreateDetailService())
        {
        }

        internal Form1(IDetailService detailService)
        {
            _detailService = detailService ?? throw new ArgumentNullException(nameof(detailService));
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await RefreshGridAsync();
        }

        private async Task RefreshGridAsync()
        {
            dataGridViewResult.DataSource = null;
            dataGridViewResult.DataSource = await _detailService.GetDetailsAsync();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            Detail detail;
            if (!TryBuildDetailFromForm(out detail))
            {
                return;
            }

            var isNewDetail = detail.ID == 0;
            await _detailService.SaveDetailAsync(detail);
            await RefreshGridAsync();
            ClearFields();

            MessageBox.Show(
                isNewDetail ? "Information has been saved." : "Information has been updated.",
                isNewDetail ? "Saved" : "Updated",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private async void dataGridViewResult_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewResult.CurrentRow == null || dataGridViewResult.CurrentRow.Index == -1)
            {
                return;
            }

            _selectedDetailId = Convert.ToInt32(dataGridViewResult.CurrentRow.Cells[0].Value);
            var detail = await _detailService.GetDetailAsync(_selectedDetailId);
            if (detail == null)
            {
                return;
            }

            txtFirstName.Text = detail.Fname;
            txtLastName.Text = detail.Lname;
            txtAge.Text = detail.Age.ToString();
            txtAddress.Text = detail.Address;
            dateTimePickerBirthDate.Value = detail.DateOfBirth;
            btnSave.Text = "Update";
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedDetailId == 0)
            {
                MessageBox.Show("Select a record before deleting.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this information?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            await _detailService.DeleteDetailAsync(_selectedDetailId);
            await RefreshGridAsync();
            ClearFields();
            MessageBox.Show("Information has been deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearFields()
        {
            _selectedDetailId = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtAddress.Text = string.Empty;
            btnSave.Text = "Save";
            dateTimePickerBirthDate.Value = DateTime.Now;
        }

        private void BirthDate_ValueChanged(object sender, EventArgs e)
        {
            txtAge.Text = AgeCalculator.CalculateAge(dateTimePickerBirthDate.Value.Date, DateTime.Now.Date).ToString();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshGridAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private bool TryBuildDetailFromForm(out Detail detail)
        {
            detail = null;

            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("First name and last name are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int age;
            if (!int.TryParse(txtAge.Text, out age))
            {
                MessageBox.Show("Age must be a valid number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            detail = new Detail
            {
                ID = _selectedDetailId,
                Fname = txtFirstName.Text.Trim(),
                Lname = txtLastName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Age = age,
                DateOfBirth = dateTimePickerBirthDate.Value.Date
            };

            var validationResult = DetailValidator.Validate(detail);
            if (!validationResult.IsValid)
            {
                MessageBox.Show(validationResult.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
