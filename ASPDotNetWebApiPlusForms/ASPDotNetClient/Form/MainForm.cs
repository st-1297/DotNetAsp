using ASPDotNetClient.Logic;
using System;
using System.Windows.Forms;

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
            var result = await HttpClientManager.GetAllProductsAsync();

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
            var item = await HttpClientManager.GetProductByIdAsync(id);
            txtLog.Text += item.ID + "\t" + item.Name + "\t" + item.Category + "\t" + item.Price;
        }

        private void btnPost_Click(object sender, EventArgs e)
        {

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
