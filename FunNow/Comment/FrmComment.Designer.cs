namespace FunNow.Comment
{
    partial class FrmComment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmComment));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbAvgRating = new System.Windows.Forms.Label();
            this.lbGuestComment = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbGroupBy = new System.Windows.Forms.Label();
            this.cbGroupBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBreakfast = new System.Windows.Forms.Button();
            this.bntRoom = new System.Windows.Forms.Button();
            this.bntLocation = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(55, 283);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(710, 324);
            this.flowLayoutPanel1.TabIndex = 52;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label1.Location = new System.Drawing.Point(182, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 17);
            this.label1.TabIndex = 50;
            this.label1.Text = "/5";
            // 
            // lbAvgRating
            // 
            this.lbAvgRating.AutoSize = true;
            this.lbAvgRating.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbAvgRating.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lbAvgRating.Location = new System.Drawing.Point(51, 167);
            this.lbAvgRating.Name = "lbAvgRating";
            this.lbAvgRating.Size = new System.Drawing.Size(65, 24);
            this.lbAvgRating.TabIndex = 49;
            this.lbAvgRating.Text = "label1";
            // 
            // lbGuestComment
            // 
            this.lbGuestComment.AutoSize = true;
            this.lbGuestComment.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbGuestComment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(97)))));
            this.lbGuestComment.Location = new System.Drawing.Point(49, 119);
            this.lbGuestComment.Name = "lbGuestComment";
            this.lbGuestComment.Size = new System.Drawing.Size(85, 31);
            this.lbGuestComment.TabIndex = 48;
            this.lbGuestComment.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-2, -1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1066, 90);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 47;
            this.pictureBox1.TabStop = false;
            // 
            // lbGroupBy
            // 
            this.lbGroupBy.AutoSize = true;
            this.lbGroupBy.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbGroupBy.Location = new System.Drawing.Point(514, 243);
            this.lbGroupBy.Name = "lbGroupBy";
            this.lbGroupBy.Size = new System.Drawing.Size(105, 24);
            this.lbGroupBy.TabIndex = 54;
            this.lbGroupBy.Text = "排序依據：";
            // 
            // cbGroupBy
            // 
            this.cbGroupBy.ForeColor = System.Drawing.Color.Gray;
            this.cbGroupBy.FormattingEnabled = true;
            this.cbGroupBy.Items.AddRange(new object[] {
            "最新",
            "最舊",
            "分數最高",
            "分數最低"});
            this.cbGroupBy.Location = new System.Drawing.Point(617, 245);
            this.cbGroupBy.Name = "cbGroupBy";
            this.cbGroupBy.Size = new System.Drawing.Size(121, 20);
            this.cbGroupBy.TabIndex = 55;
            this.cbGroupBy.Text = "選擇排序...";
            this.cbGroupBy.SelectedIndexChanged += new System.EventHandler(this.cbGroupBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(97)))));
            this.label2.Location = new System.Drawing.Point(49, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 20);
            this.label2.TabIndex = 56;
            this.label2.Text = "選擇主題以瀏覽相關評論：";
            // 
            // btnBreakfast
            // 
            this.btnBreakfast.BackColor = System.Drawing.Color.White;
            this.btnBreakfast.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBreakfast.FlatAppearance.BorderSize = 0;
            this.btnBreakfast.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnBreakfast.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(97)))));
            this.btnBreakfast.Location = new System.Drawing.Point(55, 237);
            this.btnBreakfast.Margin = new System.Windows.Forms.Padding(281, 2, 2, 2);
            this.btnBreakfast.Name = "btnBreakfast";
            this.btnBreakfast.Size = new System.Drawing.Size(79, 35);
            this.btnBreakfast.TabIndex = 96;
            this.btnBreakfast.Text = "+早餐";
            this.btnBreakfast.UseVisualStyleBackColor = false;
            this.btnBreakfast.Click += new System.EventHandler(this.btnBreakfast_Click);
            // 
            // bntRoom
            // 
            this.bntRoom.BackColor = System.Drawing.Color.White;
            this.bntRoom.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bntRoom.FlatAppearance.BorderSize = 0;
            this.bntRoom.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bntRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(97)))));
            this.bntRoom.Location = new System.Drawing.Point(142, 237);
            this.bntRoom.Margin = new System.Windows.Forms.Padding(281, 2, 2, 2);
            this.bntRoom.Name = "bntRoom";
            this.bntRoom.Size = new System.Drawing.Size(79, 35);
            this.bntRoom.TabIndex = 97;
            this.bntRoom.Text = "+房間";
            this.bntRoom.UseVisualStyleBackColor = false;
            this.bntRoom.Click += new System.EventHandler(this.bntRoom_Click);
            // 
            // bntLocation
            // 
            this.bntLocation.BackColor = System.Drawing.Color.White;
            this.bntLocation.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bntLocation.FlatAppearance.BorderSize = 0;
            this.bntLocation.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bntLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(97)))));
            this.bntLocation.Location = new System.Drawing.Point(230, 237);
            this.bntLocation.Margin = new System.Windows.Forms.Padding(281, 2, 2, 2);
            this.bntLocation.Name = "bntLocation";
            this.bntLocation.Size = new System.Drawing.Size(79, 35);
            this.bntLocation.TabIndex = 98;
            this.bntLocation.Text = "+地點";
            this.bntLocation.UseVisualStyleBackColor = false;
            this.bntLocation.Click += new System.EventHandler(this.bntLocation_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(55, 627);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(937, 180);
            this.dataGridView1.TabIndex = 99;
            // 
            // FrmComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1063, 836);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.bntLocation);
            this.Controls.Add(this.bntRoom);
            this.Controls.Add(this.btnBreakfast);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbGroupBy);
            this.Controls.Add(this.lbGroupBy);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbAvgRating);
            this.Controls.Add(this.lbGuestComment);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmComment";
            this.Text = "FrmComments";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbAvgRating;
        private System.Windows.Forms.Label lbGuestComment;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbGroupBy;
        private System.Windows.Forms.ComboBox cbGroupBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBreakfast;
        private System.Windows.Forms.Button bntRoom;
        private System.Windows.Forms.Button bntLocation;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}