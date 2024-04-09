namespace FunNow.BackSide_Order
{
    partial class FrmOrder
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OrderIDcomboBox = new System.Windows.Forms.ComboBox();
            this.demobutton1 = new System.Windows.Forms.Button();
            this.demobutton2 = new System.Windows.Forms.Button();
            this.isOrderedBox = new FunNow.BackSide_Hotel.HotelOrderBox();
            this.CheckOutDateBox = new FunNow.BackSide_Hotel.HotelOrderBox();
            this.CheckInDateBox = new FunNow.BackSide_Hotel.HotelOrderBox();
            this.RoomIDBox = new FunNow.BackSide_Hotel.HotelOrderBox();
            this.CreatedAtBox = new FunNow.BackSide_Hotel.HotelOrderBox();
            this.CouponIDBox = new FunNow.BackSide_Hotel.HotelOrderBox();
            this.TotalPriceBox = new FunNow.BackSide_Hotel.HotelOrderBox();
            this.PaymentStatusIDBox = new FunNow.BackSide_Hotel.HotelOrderBox();
            this.OrderStatusIDBox = new FunNow.BackSide_Hotel.HotelOrderBox();
            this.MemberIDBox = new FunNow.BackSide_Hotel.HotelOrderBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(516, 485);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 41);
            this.button1.TabIndex = 7;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(653, 485);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 41);
            this.button2.TabIndex = 8;
            this.button2.Text = "確定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(11, 453);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(786, 2);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(60, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "OrderID";
            this.label2.Visible = false;
            // 
            // OrderIDcomboBox
            // 
            this.OrderIDcomboBox.FormattingEnabled = true;
            this.OrderIDcomboBox.Location = new System.Drawing.Point(64, 47);
            this.OrderIDcomboBox.Name = "OrderIDcomboBox";
            this.OrderIDcomboBox.Size = new System.Drawing.Size(195, 20);
            this.OrderIDcomboBox.TabIndex = 15;
            this.OrderIDcomboBox.Visible = false;
            this.OrderIDcomboBox.SelectedIndexChanged += new System.EventHandler(this.OrderIDcomboBox_SelectedIndexChanged_1);
            // 
            // demobutton1
            // 
            this.demobutton1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.demobutton1.Location = new System.Drawing.Point(64, 485);
            this.demobutton1.Name = "demobutton1";
            this.demobutton1.Size = new System.Drawing.Size(112, 41);
            this.demobutton1.TabIndex = 16;
            this.demobutton1.Text = "demo";
            this.demobutton1.UseVisualStyleBackColor = true;
            this.demobutton1.Click += new System.EventHandler(this.demobutton1_Click_1);
            // 
            // demobutton2
            // 
            this.demobutton2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.demobutton2.Location = new System.Drawing.Point(64, 485);
            this.demobutton2.Name = "demobutton2";
            this.demobutton2.Size = new System.Drawing.Size(112, 41);
            this.demobutton2.TabIndex = 17;
            this.demobutton2.Text = "demo";
            this.demobutton2.UseVisualStyleBackColor = true;
            this.demobutton2.Click += new System.EventHandler(this.demobutton2_Click_1);
            // 
            // isOrderedBox
            // 
            this.isOrderedBox.filedName = "isOrdered";
            this.isOrderedBox.fileValue = "";
            this.isOrderedBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.isOrderedBox.Location = new System.Drawing.Point(361, 270);
            this.isOrderedBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.isOrderedBox.Name = "isOrderedBox";
            this.isOrderedBox.Size = new System.Drawing.Size(195, 86);
            this.isOrderedBox.TabIndex = 13;
            // 
            // CheckOutDateBox
            // 
            this.CheckOutDateBox.filedName = "CheckOutDate";
            this.CheckOutDateBox.fileValue = "";
            this.CheckOutDateBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CheckOutDateBox.Location = new System.Drawing.Point(361, 212);
            this.CheckOutDateBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CheckOutDateBox.Name = "CheckOutDateBox";
            this.CheckOutDateBox.Size = new System.Drawing.Size(195, 86);
            this.CheckOutDateBox.TabIndex = 12;
            // 
            // CheckInDateBox
            // 
            this.CheckInDateBox.filedName = "CheckInDate";
            this.CheckInDateBox.fileValue = "";
            this.CheckInDateBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CheckInDateBox.Location = new System.Drawing.Point(361, 146);
            this.CheckInDateBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CheckInDateBox.Name = "CheckInDateBox";
            this.CheckInDateBox.Size = new System.Drawing.Size(195, 86);
            this.CheckInDateBox.TabIndex = 11;
            // 
            // RoomIDBox
            // 
            this.RoomIDBox.filedName = "RoomID";
            this.RoomIDBox.fileValue = "";
            this.RoomIDBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RoomIDBox.Location = new System.Drawing.Point(361, 87);
            this.RoomIDBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RoomIDBox.Name = "RoomIDBox";
            this.RoomIDBox.Size = new System.Drawing.Size(195, 86);
            this.RoomIDBox.TabIndex = 10;
            // 
            // CreatedAtBox
            // 
            this.CreatedAtBox.filedName = "CreatedAt";
            this.CreatedAtBox.fileValue = "";
            this.CreatedAtBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CreatedAtBox.Location = new System.Drawing.Point(64, 388);
            this.CreatedAtBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CreatedAtBox.Name = "CreatedAtBox";
            this.CreatedAtBox.Size = new System.Drawing.Size(195, 86);
            this.CreatedAtBox.TabIndex = 6;
            // 
            // CouponIDBox
            // 
            this.CouponIDBox.filedName = "CouponID";
            this.CouponIDBox.fileValue = "";
            this.CouponIDBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CouponIDBox.Location = new System.Drawing.Point(64, 329);
            this.CouponIDBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CouponIDBox.Name = "CouponIDBox";
            this.CouponIDBox.Size = new System.Drawing.Size(195, 86);
            this.CouponIDBox.TabIndex = 5;
            // 
            // TotalPriceBox
            // 
            this.TotalPriceBox.filedName = "TotalPrice";
            this.TotalPriceBox.fileValue = "";
            this.TotalPriceBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TotalPriceBox.Location = new System.Drawing.Point(64, 266);
            this.TotalPriceBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TotalPriceBox.Name = "TotalPriceBox";
            this.TotalPriceBox.Size = new System.Drawing.Size(195, 86);
            this.TotalPriceBox.TabIndex = 4;
            // 
            // PaymentStatusIDBox
            // 
            this.PaymentStatusIDBox.filedName = "PaymentStatusID";
            this.PaymentStatusIDBox.fileValue = "";
            this.PaymentStatusIDBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PaymentStatusIDBox.Location = new System.Drawing.Point(64, 205);
            this.PaymentStatusIDBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PaymentStatusIDBox.Name = "PaymentStatusIDBox";
            this.PaymentStatusIDBox.Size = new System.Drawing.Size(195, 86);
            this.PaymentStatusIDBox.TabIndex = 3;
            // 
            // OrderStatusIDBox
            // 
            this.OrderStatusIDBox.filedName = "OrderStatusID";
            this.OrderStatusIDBox.fileValue = "";
            this.OrderStatusIDBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OrderStatusIDBox.Location = new System.Drawing.Point(64, 144);
            this.OrderStatusIDBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OrderStatusIDBox.Name = "OrderStatusIDBox";
            this.OrderStatusIDBox.Size = new System.Drawing.Size(195, 86);
            this.OrderStatusIDBox.TabIndex = 2;
            // 
            // MemberIDBox
            // 
            this.MemberIDBox.filedName = "MemberID";
            this.MemberIDBox.fileValue = "";
            this.MemberIDBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MemberIDBox.Location = new System.Drawing.Point(64, 87);
            this.MemberIDBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MemberIDBox.Name = "MemberIDBox";
            this.MemberIDBox.Size = new System.Drawing.Size(195, 86);
            this.MemberIDBox.TabIndex = 1;
            // 
            // FrmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.demobutton2);
            this.Controls.Add(this.demobutton1);
            this.Controls.Add(this.OrderIDcomboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.isOrderedBox);
            this.Controls.Add(this.CheckOutDateBox);
            this.Controls.Add(this.CheckInDateBox);
            this.Controls.Add(this.RoomIDBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CreatedAtBox);
            this.Controls.Add(this.CouponIDBox);
            this.Controls.Add(this.TotalPriceBox);
            this.Controls.Add(this.PaymentStatusIDBox);
            this.Controls.Add(this.OrderStatusIDBox);
            this.Controls.Add(this.MemberIDBox);
            this.Name = "FrmOrder";
            this.Text = "FrmOrder";
            this.Load += new System.EventHandler(this.FrmOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private BackSide_Hotel.HotelOrderBox MemberIDBox;
        private BackSide_Hotel.HotelOrderBox OrderStatusIDBox;
        private BackSide_Hotel.HotelOrderBox PaymentStatusIDBox;
        private BackSide_Hotel.HotelOrderBox TotalPriceBox;
        private BackSide_Hotel.HotelOrderBox CouponIDBox;
        public BackSide_Hotel.HotelOrderBox CreatedAtBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private BackSide_Hotel.HotelOrderBox RoomIDBox;
        private BackSide_Hotel.HotelOrderBox CheckInDateBox;
        private BackSide_Hotel.HotelOrderBox CheckOutDateBox;
        private BackSide_Hotel.HotelOrderBox isOrderedBox;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox OrderIDcomboBox;
        public System.Windows.Forms.Button demobutton1;
        public System.Windows.Forms.Button demobutton2;
    }
}