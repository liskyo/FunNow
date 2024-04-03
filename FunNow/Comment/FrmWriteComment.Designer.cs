namespace FunNow.Comment
{
    partial class FrmWriteComment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWriteComment));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxScore = new System.Windows.Forms.ComboBox();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lbHotelName = new System.Windows.Forms.Label();
            this.panelWriteComment = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbRoomName = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbStayedDays = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelWriteComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, -1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1066, 90);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(97)))));
            this.label1.Location = new System.Drawing.Point(155, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 50;
            this.label1.Text = "住宿評分";
            // 
            // comboBoxScore
            // 
            this.comboBoxScore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxScore.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBoxScore.FormattingEnabled = true;
            this.comboBoxScore.IntegralHeight = false;
            this.comboBoxScore.Location = new System.Drawing.Point(158, 372);
            this.comboBoxScore.Name = "comboBoxScore";
            this.comboBoxScore.Size = new System.Drawing.Size(139, 28);
            this.comboBoxScore.TabIndex = 51;
            this.comboBoxScore.TabStop = false;
            // 
            // tbInput
            // 
            this.tbInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInput.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbInput.ForeColor = System.Drawing.Color.Black;
            this.tbInput.Location = new System.Drawing.Point(159, 477);
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(555, 151);
            this.tbInput.TabIndex = 52;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(97)))));
            this.label2.Location = new System.Drawing.Point(155, 450);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 53;
            this.label2.Text = "住宿評論";
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(97)))));
            this.btnConfirm.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(582, 661);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(281, 2, 2, 2);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(132, 48);
            this.btnConfirm.TabIndex = 95;
            this.btnConfirm.Text = "送出評論";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lbHotelName
            // 
            this.lbHotelName.AutoSize = true;
            this.lbHotelName.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbHotelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(97)))));
            this.lbHotelName.Location = new System.Drawing.Point(20, 11);
            this.lbHotelName.Name = "lbHotelName";
            this.lbHotelName.Size = new System.Drawing.Size(85, 31);
            this.lbHotelName.TabIndex = 96;
            this.lbHotelName.Text = "label3";
            // 
            // panelWriteComment
            // 
            this.panelWriteComment.BackColor = System.Drawing.Color.White;
            this.panelWriteComment.Controls.Add(this.pictureBox4);
            this.panelWriteComment.Controls.Add(this.lbHotelName);
            this.panelWriteComment.Controls.Add(this.pictureBox3);
            this.panelWriteComment.Controls.Add(this.lbRoomName);
            this.panelWriteComment.Controls.Add(this.pictureBox2);
            this.panelWriteComment.Controls.Add(this.lbStayedDays);
            this.panelWriteComment.Location = new System.Drawing.Point(159, 159);
            this.panelWriteComment.Name = "panelWriteComment";
            this.panelWriteComment.Size = new System.Drawing.Size(556, 141);
            this.panelWriteComment.TabIndex = 97;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(428, 12);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(110, 110);
            this.pictureBox4.TabIndex = 102;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(26, 50);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.TabIndex = 101;
            this.pictureBox3.TabStop = false;
            // 
            // lbRoomName
            // 
            this.lbRoomName.AutoSize = true;
            this.lbRoomName.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbRoomName.Location = new System.Drawing.Point(67, 57);
            this.lbRoomName.Name = "lbRoomName";
            this.lbRoomName.Size = new System.Drawing.Size(87, 20);
            this.lbRoomName.TabIndex = 98;
            this.lbRoomName.Text = "Roomtype";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(26, 93);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.TabIndex = 100;
            this.pictureBox2.TabStop = false;
            // 
            // lbStayedDays
            // 
            this.lbStayedDays.AutoSize = true;
            this.lbStayedDays.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbStayedDays.Location = new System.Drawing.Point(67, 102);
            this.lbStayedDays.Name = "lbStayedDays";
            this.lbStayedDays.Size = new System.Drawing.Size(91, 19);
            this.lbStayedDays.TabIndex = 99;
            this.lbStayedDays.Text = "StayedDays";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(79)))), ((int)(((byte)(97)))));
            this.label3.Location = new System.Drawing.Point(152, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 31);
            this.label3.TabIndex = 98;
            this.label3.Text = "訂單資訊";
            // 
            // FrmWriteComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 766);
            this.Controls.Add(this.panelWriteComment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.comboBoxScore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FrmWriteComment";
            this.Text = "FrmWriteComment";
            this.Load += new System.EventHandler(this.FrmWriteComment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelWriteComment.ResumeLayout(false);
            this.panelWriteComment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxScore;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lbHotelName;
        private System.Windows.Forms.Panel panelWriteComment;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lbRoomName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lbStayedDays;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label3;
        // private ShowOrder showOrder1;
    }
}