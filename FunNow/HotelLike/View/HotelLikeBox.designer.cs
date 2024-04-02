namespace prjFunNowMember.View
{
    partial class HotelLikeBox
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotelLikeBox));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lbCityName = new System.Windows.Forms.Label();
            this.buttonHeart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(17, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 208);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(46, 344);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 55);
            this.button1.TabIndex = 18;
            this.button1.Text = "查看住宿";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("微軟正黑體", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblName.Location = new System.Drawing.Point(41, 271);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(163, 35);
            this.lblName.TabIndex = 19;
            this.lblName.Text = "HotelName";
            // 
            // lbCityName
            // 
            this.lbCityName.AutoSize = true;
            this.lbCityName.Font = new System.Drawing.Font("微軟正黑體", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbCityName.Location = new System.Drawing.Point(41, 306);
            this.lbCityName.Name = "lbCityName";
            this.lbCityName.Size = new System.Drawing.Size(143, 35);
            this.lbCityName.TabIndex = 20;
            this.lbCityName.Text = "CityName";
            // 
            // buttonHeart
            // 
            this.buttonHeart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonHeart.Image = ((System.Drawing.Image)(resources.GetObject("buttonHeart.Image")));
            this.buttonHeart.Location = new System.Drawing.Point(196, -2);
            this.buttonHeart.Name = "buttonHeart";
            this.buttonHeart.Size = new System.Drawing.Size(52, 50);
            this.buttonHeart.TabIndex = 21;
            this.buttonHeart.UseVisualStyleBackColor = true;
            this.buttonHeart.Click += new System.EventHandler(this.RemoveHotelFromFavorites);
            // 
            // HotelLikeBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.buttonHeart);
            this.Controls.Add(this.lbCityName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "HotelLikeBox";
            this.Size = new System.Drawing.Size(246, 402);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lbCityName;
        private System.Windows.Forms.Button buttonHeart;
    }
}
