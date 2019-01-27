using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASPDotNetWebApi01.Models;
using System.Collections.Generic;
using WindowsFormsDotNet01.Logic;

namespace WindowsFormsDotNet01
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void ShowProduct(Product product)
        {
            var productInfo = new StringBuilder();
            productInfo.Append($"Name: {product.Name}");
            productInfo.Append("\t");
            productInfo.Append($"Price: {product.Price}");
            productInfo.Append("\t");
            productInfo.Append($"Category: {product.Category}");

            this.txtResponse.Text = productInfo.ToString();
        }

        //public async Task<IEnumerable<Product>> GetProductAsync(string path)
        //{
        //    List<Product> product = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        product = await response.Content.ReadAsAsync<List<Product>>();
        //    }
        //    return product?.ToArray();
        //}

        //public async Task<Uri> CreateProductAsync(Product product)
        //{
        //    HttpResponseMessage response = await client.PostAsJsonAsync(
        //        "api/products", product);
        //    response.EnsureSuccessStatusCode();

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}

        //public async Task<Product> UpdateProductAsync(Product product)
        //{
        //    HttpResponseMessage response = await client.PutAsJsonAsync(
        //        $"api/products/{product.Id}", product);
        //    response.EnsureSuccessStatusCode();

        //    // Deserialize the updated product from the response body.
        //    product = await response.Content.ReadAsAsync<Product>();
        //    return product;
        //}

        //public async Task<HttpStatusCode> DeleteProductAsync(int id)
        //{
        //    HttpResponseMessage response = await client.DeleteAsync(
        //        $"api/products/{id}");
        //    return response.StatusCode;
        //}

        //static void Main()
        //{
        //    RunAsync().GetAwaiter().GetResult();
        //}

        public async Task RunAsync()
        {
            try
            {
                // Create a new product
                Product product = new Product
                {
                    Name = "Gizmo",
                    Price = 100,
                    Category = "Widgets"
                };

                var url = await HttpClientManager.CreateProductAsync(product);
                this.txtLog.Text = $"Created at {url}";
                Console.WriteLine($"Created at {url}");

                //// Get the product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                //// Update the product
                //this.txtLog.Text = "Updating price...";
                //Console.WriteLine("Updating price...");
                //product.Price = 80;
                //await UpdateProductAsync(product);

                //// Get the updated product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                // Delete the product
                var statusCode = await HttpClientManager.DeleteProductAsync(product.Id);
                this.txtLog.Text = $"Deleted (HTTP Status = {(int)statusCode})";
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                this.txtLog.Text = e.Message;
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }



        #region イベント

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private async void btnGet_Click(object sender, EventArgs e)
        {
            var result = await HttpClientManager.GetProductAsync();

            if (result != null)
            {
                foreach (var item in result)
                {
                    ShowProduct(item);
                    //this.txtResponse.Text += item.Id + "\t" + item.Name + "\t" + item.Category + "\t" + item.Price;
                    //this.txtResponse.Text += "\r\n";
                }
            }
        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
            // Create a new product
            Product product = new Product
            {
                Name = "Gizmo",
                Price = 100,
                Category = "Widgets"
            };

            var result = await HttpClientManager.CreateProductAsync(product);
        }

        private async void btnPut_Click(object sender, EventArgs e)
        {
            // Create a new product
            Product product = new Product
            {
                Name = "Gizmo",
                Price = 100,
                Category = "Widgets"
            };

            var result = await HttpClientManager.UpdateProductAsync(product);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            // Create a new product
            Product product = new Product
            {
                Name = "Gizmo",
                Price = 100,
                Category = "Widgets"
            };

            var result = await HttpClientManager.DeleteProductAsync(product.Id);
        }

        #endregion

    }
}
