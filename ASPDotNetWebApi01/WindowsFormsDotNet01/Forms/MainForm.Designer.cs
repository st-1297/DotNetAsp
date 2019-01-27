namespace WindowsFormsDotNet01
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.lblText = new System.Windows.Forms.Label();
            this.btnGet = new System.Windows.Forms.Button();
            this.lblButton = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnPost = new System.Windows.Forms.Button();
            this.btnPut = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(91, 25);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponse.Size = new System.Drawing.Size(459, 40);
            this.txtResponse.TabIndex = 0;
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(32, 32);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(41, 12);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "テキスト";
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(91, 107);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 2;
            this.btnGet.Text = "GET";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // lblButton
            // 
            this.lblButton.AutoSize = true;
            this.lblButton.Location = new System.Drawing.Point(32, 107);
            this.lblButton.Name = "lblButton";
            this.lblButton.Size = new System.Drawing.Size(32, 12);
            this.lblButton.TabIndex = 3;
            this.lblButton.Text = "ボタン";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(91, 163);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(459, 40);
            this.txtLog.TabIndex = 4;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(32, 163);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(23, 12);
            this.lblLog.TabIndex = 5;
            this.lblLog.Text = "ログ";
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(189, 107);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(75, 23);
            this.btnPost.TabIndex = 6;
            this.btnPost.Text = "POST";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // btnPut
            // 
            this.btnPut.Location = new System.Drawing.Point(279, 107);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(75, 23);
            this.btnPut.TabIndex = 7;
            this.btnPut.Text = "PUT";
            this.btnPut.UseVisualStyleBackColor = true;
            this.btnPut.Click += new System.EventHandler(this.btnPut_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(373, 107);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnPut);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblButton);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.txtResponse);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Label lblButton;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnPut;
        private System.Windows.Forms.Button btnDelete;
    }
}

