﻿using System;
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
    }
}
