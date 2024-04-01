namespace FunNow.Comment
{
    partial class WriteCommentBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WriteCommentBox));
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lbHotelName = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbRoomName = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbStayedDays = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(331, 24);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(110, 110);
            this.pictureBox4.TabIndex = 108;
            this.pictureBox4.TabStop = false;
            // 
            // lbHotelName
            // 
            this.lbHotelName.AutoSize = true;
            this.lbHotelName.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbHotelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(97)))));
            this.lbHotelName.Location = new System.Drawing.Point(19, 24);
            this.lbHotelName.Name = "lbHotelName";
            this.lbHotelName.Size = new System.Drawing.Size(85, 31);
            this.lbHotelName.TabIndex = 103;
            this.lbHotelName.Text = "label3";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(25, 63);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.TabIndex = 107;
            this.pictureBox3.TabStop = false;
            // 
            // lbRoomName
            // 
            this.lbRoomName.AutoSize = true;
            this.lbRoomName.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbRoomName.Location = new System.Drawing.Point(66, 70);
            this.lbRoomName.Name = "lbRoomName";
            this.lbRoomName.Size = new System.Drawing.Size(87, 20);
            this.lbRoomName.TabIndex = 104;
            this.lbRoomName.Text = "Roomtype";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(25, 106);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.TabIndex = 106;
            this.pictureBox2.TabStop = false;
            // 
            // lbStayedDays
            // 
            this.lbStayedDays.AutoSize = true;
            this.lbStayedDays.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbStayedDays.Location = new System.Drawing.Point(66, 115);
            this.lbStayedDays.Name = "lbStayedDays";
            this.lbStayedDays.Size = new System.Drawing.Size(91, 19);
            this.lbStayedDays.TabIndex = 105;
            this.lbStayedDays.Text = "StayedDays";
            // 
            // WriteCommentBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.lbHotelName);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.lbRoomName);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lbStayedDays);
            this.Name = "WriteCommentBox";
            this.Size = new System.Drawing.Size(461, 153);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lbHotelName;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lbRoomName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lbStayedDays;
    }
}
