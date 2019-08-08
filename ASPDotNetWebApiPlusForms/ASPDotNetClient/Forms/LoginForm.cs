using ASPDotNetApiProxy.ApiModels;
using ASPDotNetApiProxy.Proxies;
using System;
using System.Windows.Forms;

namespace ASPDotNetClient.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // バージョン情報表示
            this.Text += " v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        private void LoginForm_Load(object sender, EventArgs e) { }

        /// <summary>
        /// 開始ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.btnExecute.Enabled = false;

                int id;
                if (!int.TryParse(this.txtUserId.Text, out id))
                {
                    MessageBox.Show("ユーザーIDは数字です");
                    return;
                }
                var password = this.txtUserPswd.Text;


                var result = await AuthApi.AuthenticateAsync(new UserAuthInfo(id, password));

                if (!result)
                {
                    MessageBox.Show("認証に失敗しました。\nIDおよびパスワードを再度確認してください。");

                    return;
                }

                //--- 使用頻度の高いデータをキャッシュ
                //await this.GetMasterDataCacheAsync();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
                this.btnExecute.Enabled = true;
            }
        }

        /// <summary>
        /// 使用頻度の高いデータを事前にキャッシュします。
        /// </summary>
        /// <returns></returns>
        //private async Task GetMasterDataCacheAsync()
        //{
        //    //--- ユーザー情報
        //    ModelCache.Instance.SigninUser = await EmployeeApi.GetAsync(int.Parse(this.txtStaffID.Text)).ConfigureAwait(false);
        //}
    }
}
