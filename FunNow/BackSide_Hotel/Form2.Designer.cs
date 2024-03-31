namespace FunNow.BackSide_Hotel
{
    partial class Form2
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
            this.comboBoxHotel = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Desc = new FunNow.BackSide_Hotel.HotelBox();
            this.Price = new FunNow.BackSide_Hotel.HotelBox();
            this.RoomCapacity = new FunNow.BackSide_Hotel.HotelBox();
            this.RoomType = new FunNow.BackSide_Hotel.HotelBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxHotel
            // 
            this.comboBoxHotel.FormattingEnabled = true;
            this.comboBoxHotel.Location = new System.Drawing.Point(76, 49);
            this.comboBoxHotel.Name = "comboBoxHotel";
            this.comboBoxHotel.Size = new System.Drawing.Size(150, 26);
            this.comboBoxHotel.TabIndex = 0;
           
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(386, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(318, 254);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(386, 322);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 57);
            this.button1.TabIndex = 6;
            this.button1.Text = "儲存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(72, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "飯店";
            // 
            // Desc
            // 
            this.Desc.filedName = "描述";
            this.Desc.fileValue = "";
            this.Desc.Location = new System.Drawing.Point(76, 315);
            this.Desc.Name = "Desc";
            this.Desc.Size = new System.Drawing.Size(150, 68);
            this.Desc.TabIndex = 4;
            // 
            // Price
            // 
            this.Price.filedName = "價格";
            this.Price.fileValue = "";
            this.Price.Location = new System.Drawing.Point(76, 241);
            this.Price.Name = "Price";
            this.Price.Size = new System.Drawing.Size(150, 68);
            this.Price.TabIndex = 3;
            // 
            // RoomCapacity
            // 
            this.RoomCapacity.filedName = "容納人數";
            this.RoomCapacity.fileValue = "";
            this.RoomCapacity.Location = new System.Drawing.Point(76, 163);
            this.RoomCapacity.Name = "RoomCapacity";
            this.RoomCapacity.Size = new System.Drawing.Size(150, 68);
            this.RoomCapacity.TabIndex = 2;
            // 
            // RoomType
            // 
            this.RoomType.filedName = "房型";
            this.RoomType.fileValue = "";
            this.RoomType.Location = new System.Drawing.Point(76, 85);
            this.RoomType.Name = "RoomType";
            this.RoomType.Size = new System.Drawing.Size(150, 68);
            this.RoomType.TabIndex = 1;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Desc);
            this.Controls.Add(this.Price);
            this.Controls.Add(this.RoomCapacity);
            this.Controls.Add(this.RoomType);
            this.Controls.Add(this.comboBoxHotel);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxHotel;
        private HotelBox RoomType;
        private HotelBox RoomCapacity;
        private HotelBox Price;
        private HotelBox Desc;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}