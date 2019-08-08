using System;
using System.Data;
using System.Windows.Forms;
using ASPDotNetApiProxy;
using ASPDotNetApiProxy.DbModels;
using ASPDotNetApiProxy.Proxies;
using ASPDotNetApiProxy.Repositories;
using ASPDotNetWebApi.Models;

namespace ASPDotNetClient.Forms
{
    public partial class MainForm : Form
    {
        public DataTable DtTable { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            DtTable = this.dataGridViewBooks.DataSource as DataTable;

        }

        #region イベント

        /// <summary>
        /// dataGridViewのセルからフォーカスが外れた時に動作します
        /// </summary>
        private void dataGridViewBooks_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridViewBooks.EndEdit();
        }

        /// <summary>
        /// 直前の操作を取り消します
        /// </summary>
        private void btnUndo_Click(object sender, EventArgs e)
        {
            this.dataGridViewBooks.CancelEdit();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            var result = await ProductApi.GetAllProductsAsync(null);

            if (result != null)
            {
                this.dataGridViewBooks.DataSource = result;

            }

        }

        private async void btnGet_Click(object sender, EventArgs e)
        {
            var result = await ProductApi.GetAllProductsAsync(null);

            if (result != null)
            {
                foreach (var item in result)
                {
                    txtLog.Text += item.ID + "\t" + item.Name + "\t" + item.Category + "\t" + item.Price;
                    txtLog.Text += "\r\n";
                }

            }
        }

        private async void btnGetById_Click(object sender, EventArgs e)
        {
            var id = int.Parse(txtId.Text);
            var item = await HttpClientProxy.GetProductByIdAsync(id);
            txtLog.Text += item.ID + "\t" + item.Name + "\t" + item.Category + "\t" + item.Price;
        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
            var product = new PRODUCT { Name = "Name", Category = "Category", Price = int.Parse(txtId.Text) };
            var response = await HttpClientProxy.CreateProductAsync(product);

            txtLog.Text += response.ID + "\t" + response.Name + "\t" + response.Category + "\t" + response.Price;
        }

        private void btnPut_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private async void  btnConnection_Click(object sender, EventArgs e)
        {
            int id = string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text);
            var result = await TestRepository.GetAll(id);
            if(result != null)
            {
                foreach(var item in result)
                {
                    txtLog.Text += item.Name + Environment.NewLine;
                }
            }
            
        }

        #endregion

    }
}
