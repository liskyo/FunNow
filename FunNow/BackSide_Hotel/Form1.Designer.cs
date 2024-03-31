namespace FunNow.BackSide_Hotel
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.ComboxHotelRegion = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.hotelBoxDesc = new FunNow.BackSide_Hotel.HotelBox();
            this.hotelBoxPhone = new FunNow.BackSide_Hotel.HotelBox();
            this.hotelBoxAddress = new FunNow.BackSide_Hotel.HotelBox();
            this.hotelBoxName = new FunNow.BackSide_Hotel.HotelBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(545, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "區域";
            // 
            // ComboxHotelRegion
            // 
            this.ComboxHotelRegion.FormattingEnabled = true;
            this.ComboxHotelRegion.Items.AddRange(new object[] {
            "北部",
            "中部",
            "南部",
            "東部"});
            this.ComboxHotelRegion.Location = new System.Drawing.Point(548, 157);
            this.ComboxHotelRegion.Name = "ComboxHotelRegion";
            this.ComboxHotelRegion.Size = new System.Drawing.Size(121, 26);
            this.ComboxHotelRegion.TabIndex = 5;
            this.ComboxHotelRegion.Text = "請選擇區域";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(548, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "儲存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // hotelBoxDesc
            // 
            this.hotelBoxDesc.filedName = "描述";
            this.hotelBoxDesc.fileValue = "";
            this.hotelBoxDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.hotelBoxDesc.Location = new System.Drawing.Point(354, 210);
            this.hotelBoxDesc.Name = "hotelBoxDesc";
            this.hotelBoxDesc.Size = new System.Drawing.Size(150, 68);
            this.hotelBoxDesc.TabIndex = 3;
            // 
            // hotelBoxPhone
            // 
            this.hotelBoxPhone.filedName = "電話";
            this.hotelBoxPhone.fileValue = "";
            this.hotelBoxPhone.Location = new System.Drawing.Point(354, 136);
            this.hotelBoxPhone.Name = "hotelBoxPhone";
            this.hotelBoxPhone.Size = new System.Drawing.Size(150, 68);
            this.hotelBoxPhone.TabIndex = 2;
            // 
            // hotelBoxAddress
            // 
            this.hotelBoxAddress.filedName = "飯店地址";
            this.hotelBoxAddress.fileValue = "";
            this.hotelBoxAddress.Location = new System.Drawing.Point(157, 210);
            this.hotelBoxAddress.Name = "hotelBoxAddress";
            this.hotelBoxAddress.Size = new System.Drawing.Size(150, 68);
            this.hotelBoxAddress.TabIndex = 1;
            // 
            // hotelBoxName
            // 
            this.hotelBoxName.filedName = "飯店名稱";
            this.hotelBoxName.fileValue = "";
            this.hotelBoxName.Location = new System.Drawing.Point(157, 136);
            this.hotelBoxName.Name = "hotelBoxName";
            this.hotelBoxName.Size = new System.Drawing.Size(150, 68);
            this.hotelBoxName.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(169, 323);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 52);
            this.button2.TabIndex = 7;
            this.button2.Text = "填房型";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ComboxHotelRegion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hotelBoxDesc);
            this.Controls.Add(this.hotelBoxPhone);
            this.Controls.Add(this.hotelBoxAddress);
            this.Controls.Add(this.hotelBoxName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HotelBox hotelBoxName;
        private HotelBox hotelBoxAddress;
        private HotelBox hotelBoxPhone;
        private HotelBox hotelBoxDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComboxHotelRegion;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}