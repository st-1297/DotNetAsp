using System;
using System.Windows.Forms;
using ASPDotNetApiProxy;
using ASPDotNetApiProxy.Proxies;
using ASPDotNetWebApi.Models;

namespace ASPDotNetClient.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region イベント

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
            var product = new Product { Name = "Name", Category = "Category", Price = int.Parse(txtId.Text) };
            var response = await HttpClientProxy.CreateProductAsync(product);

            txtLog.Text += response.ID + "\t" + response.Name + "\t" + response.Category + "\t" + response.Price;
        }

        private void btnPut_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
