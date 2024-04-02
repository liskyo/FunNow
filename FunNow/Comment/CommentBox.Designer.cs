namespace FunNow.Comment.Model
{
    partial class CommentBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommentBox));
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbRating = new System.Windows.Forms.Label();
            this.lbCommentTxt = new System.Windows.Forms.Label();
            this.lbCommentTime = new System.Windows.Forms.Label();
            this.lbCheckInDate = new System.Windows.Forms.Label();
            this.lbRoomName = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(19, 109);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 26);
            this.pictureBox3.TabIndex = 18;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(19, 142);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 26);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 74);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 26);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // lbRating
            // 
            this.lbRating.AutoSize = true;
            this.lbRating.Font = new System.Drawing.Font("微軟正黑體", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbRating.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(97)))));
            this.lbRating.Location = new System.Drawing.Point(13, 23);
            this.lbRating.Name = "lbRating";
            this.lbRating.Size = new System.Drawing.Size(54, 35);
            this.lbRating.TabIndex = 15;
            this.lbRating.Text = "0.0";
            // 
            // lbCommentTxt
            // 
            this.lbCommentTxt.AutoEllipsis = true;
            this.lbCommentTxt.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbCommentTxt.Location = new System.Drawing.Point(249, 82);
            this.lbCommentTxt.Name = "lbCommentTxt";
            this.lbCommentTxt.Size = new System.Drawing.Size(405, 86);
            this.lbCommentTxt.TabIndex = 14;
            this.lbCommentTxt.Text = "CommentTxt";
            // 
            // lbCommentTime
            // 
            this.lbCommentTime.AutoSize = true;
            this.lbCommentTime.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbCommentTime.ForeColor = System.Drawing.Color.DimGray;
            this.lbCommentTime.Location = new System.Drawing.Point(250, 54);
            this.lbCommentTime.Name = "lbCommentTime";
            this.lbCommentTime.Size = new System.Drawing.Size(75, 17);
            this.lbCommentTime.TabIndex = 13;
            this.lbCommentTime.Text = "CreateAt：";
            // 
            // lbCheckInDate
            // 
            this.lbCheckInDate.AutoSize = true;
            this.lbCheckInDate.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbCheckInDate.Location = new System.Drawing.Point(48, 148);
            this.lbCheckInDate.Name = "lbCheckInDate";
            this.lbCheckInDate.Size = new System.Drawing.Size(84, 17);
            this.lbCheckInDate.TabIndex = 12;
            this.lbCheckInDate.Text = "CheckIntime";
            // 
            // lbRoomName
            // 
            this.lbRoomName.AutoSize = true;
            this.lbRoomName.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbRoomName.Location = new System.Drawing.Point(48, 115);
            this.lbRoomName.Name = "lbRoomName";
            this.lbRoomName.Size = new System.Drawing.Size(71, 17);
            this.lbRoomName.TabIndex = 11;
            this.lbRoomName.Text = "Roomtype";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbName.Location = new System.Drawing.Point(48, 82);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(96, 17);
            this.lbName.TabIndex = 10;
            this.lbName.Text = "MemberName";
            // 
            // CommentBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbRating);
            this.Controls.Add(this.lbCommentTxt);
            this.Controls.Add(this.lbCommentTime);
            this.Controls.Add(this.lbCheckInDate);
            this.Controls.Add(this.lbRoomName);
            this.Controls.Add(this.lbName);
            this.Margin = new System.Windows.Forms.Padding(2, 15, 2, 2);
            this.Name = "CommentBox";
            this.Size = new System.Drawing.Size(681, 190);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbRating;
        private System.Windows.Forms.Label lbCommentTxt;
        private System.Windows.Forms.Label lbCommentTime;
        private System.Windows.Forms.Label lbCheckInDate;
        private System.Windows.Forms.Label lbRoomName;
        private System.Windows.Forms.Label lbName;
    }
}
