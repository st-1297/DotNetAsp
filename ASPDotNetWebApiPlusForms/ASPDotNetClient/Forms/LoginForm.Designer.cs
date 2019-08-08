namespace ASPDotNetClient.Forms
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.lblStaffID = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.lblCommodity = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.txtUserPswd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStaffID
            // 
            this.lblStaffID.AutoSize = true;
            this.lblStaffID.Location = new System.Drawing.Point(12, 15);
            this.lblStaffID.Name = "lblStaffID";
            this.lblStaffID.Size = new System.Drawing.Size(56, 12);
            this.lblStaffID.TabIndex = 1;
            this.lblStaffID.Text = "ユーザーID";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(70, 12);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(130, 19);
            this.txtUserId.TabIndex = 0;
            // 
            // lblCommodity
            // 
            this.lblCommodity.AutoSize = true;
            this.lblCommodity.Location = new System.Drawing.Point(12, 63);
            this.lblCommodity.Name = "lblCommodity";
            this.lblCommodity.Size = new System.Drawing.Size(13, 12);
            this.lblCommodity.TabIndex = 3;
            this.lblCommodity.Text = "  ";
            this.lblCommodity.Visible = false;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(70, 62);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(80, 30);
            this.btnExecute.TabIndex = 2;
            this.btnExecute.Text = "ログイン";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // txtUserPswd
            // 
            this.txtUserPswd.Location = new System.Drawing.Point(70, 37);
            this.txtUserPswd.Name = "txtUserPswd";
            this.txtUserPswd.PasswordChar = '*';
            this.txtUserPswd.Size = new System.Drawing.Size(130, 19);
            this.txtUserPswd.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "パスワード";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnExecute;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 97);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.txtUserPswd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCommodity);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.lblStaffID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ログイン";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblStaffID;
        internal System.Windows.Forms.TextBox txtUserId;
        internal System.Windows.Forms.Label lblCommodity;
        internal System.Windows.Forms.Button btnExecute;
        internal System.Windows.Forms.TextBox txtUserPswd;
        internal System.Windows.Forms.Label label1;
    }
}