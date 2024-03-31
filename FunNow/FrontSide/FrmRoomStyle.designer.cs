namespace prjFunNow
{
    partial class FrmRoomStyle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRoomStyle));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.fbName = new prjComponentDemo.usercontrol.FieldBox();
            this.fbInfo = new prjComponentDemo.usercontrol.FieldBox();
            this.fieldBox3 = new prjComponentDemo.usercontrol.FieldBox();
            this.fieldBox4 = new prjComponentDemo.usercontrol.FieldBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(496, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(266, 240);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(515, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 53);
            this.button1.TabIndex = 1;
            this.button1.Text = "立即預訂";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(515, 367);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 53);
            this.button2.TabIndex = 2;
            this.button2.Text = "加入購物車";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // fbName
            // 
            this.fbName.fieldName = "房型";
            this.fbName.fieldValue = "";
            this.fbName.Location = new System.Drawing.Point(38, 26);
            this.fbName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fbName.Name = "fbName";
            this.fbName.Size = new System.Drawing.Size(260, 68);
            this.fbName.TabIndex = 3;
            // 
            // fbInfo
            // 
            this.fbInfo.fieldName = "房間資訊";
            this.fbInfo.fieldValue = "";
            this.fbInfo.Location = new System.Drawing.Point(38, 98);
            this.fbInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fbInfo.Name = "fbInfo";
            this.fbInfo.Size = new System.Drawing.Size(260, 68);
            this.fbInfo.TabIndex = 4;
            // 
            // fieldBox3
            // 
            this.fieldBox3.fieldName = "label1";
            this.fieldBox3.fieldValue = "";
            this.fieldBox3.Location = new System.Drawing.Point(38, 170);
            this.fieldBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fieldBox3.Name = "fieldBox3";
            this.fieldBox3.Size = new System.Drawing.Size(260, 68);
            this.fieldBox3.TabIndex = 5;
            // 
            // fieldBox4
            // 
            this.fieldBox4.fieldName = "label1";
            this.fieldBox4.fieldValue = "";
            this.fieldBox4.Location = new System.Drawing.Point(38, 242);
            this.fieldBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fieldBox4.Name = "fieldBox4";
            this.fieldBox4.Size = new System.Drawing.Size(260, 68);
            this.fieldBox4.TabIndex = 6;
            // 
            // FomRoomStyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fieldBox4);
            this.Controls.Add(this.fieldBox3);
            this.Controls.Add(this.fbInfo);
            this.Controls.Add(this.fbName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FomRoomStyle";
            this.Text = "FomRoomStylecs";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private prjComponentDemo.usercontrol.FieldBox fbName;
        private prjComponentDemo.usercontrol.FieldBox fbInfo;
        private prjComponentDemo.usercontrol.FieldBox fieldBox3;
        private prjComponentDemo.usercontrol.FieldBox fieldBox4;
    }
}