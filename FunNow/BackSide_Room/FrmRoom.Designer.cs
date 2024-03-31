namespace FunNow.BackSide_Room
{
    partial class FrmRoom
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.fbRoomTypeID = new prjComponentDemo.usercontrol.FieldBox();
            this.fbDescription = new prjComponentDemo.usercontrol.FieldBox();
            this.fbRoomName = new prjComponentDemo.usercontrol.FieldBox();
            this.fbRoomPrice = new prjComponentDemo.usercontrol.FieldBox();
            this.fbHotelID = new prjComponentDemo.usercontrol.FieldBox();
            this.fbRoomID = new prjComponentDemo.usercontrol.FieldBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(256, 73);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(277, 218);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(39, 342);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(517, 1);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(369, 352);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 31);
            this.button2.TabIndex = 9;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(457, 352);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 10;
            this.button1.Text = "確定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fbRoomTypeID
            // 
            this.fbRoomTypeID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbRoomTypeID.fieldName = "房間種類";
            this.fbRoomTypeID.fieldValue = "1";
            this.fbRoomTypeID.Location = new System.Drawing.Point(78, 229);
            this.fbRoomTypeID.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.fbRoomTypeID.Name = "fbRoomTypeID";
            this.fbRoomTypeID.Size = new System.Drawing.Size(151, 49);
            this.fbRoomTypeID.TabIndex = 13;
            // 
            // fbDescription
            // 
            this.fbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbDescription.fieldName = "描述";
            this.fbDescription.fieldValue = "";
            this.fbDescription.Location = new System.Drawing.Point(78, 281);
            this.fbDescription.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.fbDescription.Name = "fbDescription";
            this.fbDescription.Size = new System.Drawing.Size(453, 49);
            this.fbDescription.TabIndex = 4;
            // 
            // fbRoomName
            // 
            this.fbRoomName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbRoomName.fieldName = "名稱";
            this.fbRoomName.fieldValue = "測試";
            this.fbRoomName.Location = new System.Drawing.Point(78, 115);
            this.fbRoomName.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.fbRoomName.Name = "fbRoomName";
            this.fbRoomName.Size = new System.Drawing.Size(151, 49);
            this.fbRoomName.TabIndex = 6;
            // 
            // fbRoomPrice
            // 
            this.fbRoomPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbRoomPrice.fieldName = "價格";
            this.fbRoomPrice.fieldValue = "5555";
            this.fbRoomPrice.Location = new System.Drawing.Point(78, 170);
            this.fbRoomPrice.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.fbRoomPrice.Name = "fbRoomPrice";
            this.fbRoomPrice.Size = new System.Drawing.Size(151, 49);
            this.fbRoomPrice.TabIndex = 7;
            // 
            // fbHotelID
            // 
            this.fbHotelID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbHotelID.fieldName = "旅館編號";
            this.fbHotelID.fieldValue = "111";
            this.fbHotelID.Location = new System.Drawing.Point(78, 60);
            this.fbHotelID.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.fbHotelID.Name = "fbHotelID";
            this.fbHotelID.Size = new System.Drawing.Size(151, 49);
            this.fbHotelID.TabIndex = 8;
            // 
            // fbRoomID
            // 
            this.fbRoomID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbRoomID.fieldName = "房間編號";
            this.fbRoomID.fieldValue = "111";
            this.fbRoomID.Location = new System.Drawing.Point(78, 10);
            this.fbRoomID.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.fbRoomID.Name = "fbRoomID";
            this.fbRoomID.Size = new System.Drawing.Size(151, 49);
            this.fbRoomID.TabIndex = 14;
            // 
            // FrmRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 391);
            this.Controls.Add(this.fbRoomID);
            this.Controls.Add(this.fbRoomTypeID);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fbDescription);
            this.Controls.Add(this.fbRoomName);
            this.Controls.Add(this.fbRoomPrice);
            this.Controls.Add(this.fbHotelID);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmRoom";
            this.Text = "FrmRoom";
            this.Load += new System.EventHandler(this.FrmRoom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private prjComponentDemo.usercontrol.FieldBox fbDescription;
        private prjComponentDemo.usercontrol.FieldBox fbRoomName;
        private prjComponentDemo.usercontrol.FieldBox fbRoomPrice;
        private prjComponentDemo.usercontrol.FieldBox fbHotelID;
        private prjComponentDemo.usercontrol.FieldBox fbRoomTypeID;
        private prjComponentDemo.usercontrol.FieldBox fbRoomID;
    }
}