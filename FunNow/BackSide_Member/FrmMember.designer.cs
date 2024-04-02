namespace prjFunNowMember
{
    partial class FrmMember
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.fbBirth = new prjComponentDemo.usercontrol.FieldBoxLike();
            this.fbPhone = new prjComponentDemo.usercontrol.FieldBoxLike();
            this.fbPassword = new prjComponentDemo.usercontrol.FieldBoxLike();
            this.fbEmail = new prjComponentDemo.usercontrol.FieldBoxLike();
            this.fbName = new prjComponentDemo.usercontrol.FieldBoxLike();
            this.fbId = new prjComponentDemo.usercontrol.FieldBoxLike();
            this.fbRoleId = new prjComponentDemo.usercontrol.FieldBoxLike();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(783, 542);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 65);
            this.button2.TabIndex = 13;
            this.button2.Text = "確定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(649, 542);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 65);
            this.button1.TabIndex = 12;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(694, 74);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(217, 23);
            this.comboBox1.TabIndex = 16;
            // 
            // fbBirth
            // 
            this.fbBirth.fieldName = "生日";
            this.fbBirth.fieldValue = "";
            this.fbBirth.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.fbBirth.Location = new System.Drawing.Point(41, 473);
            this.fbBirth.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fbBirth.Name = "fbBirth";
            this.fbBirth.Size = new System.Drawing.Size(547, 95);
            this.fbBirth.TabIndex = 15;
            // 
            // fbPhone
            // 
            this.fbPhone.fieldName = "電話";
            this.fbPhone.fieldValue = "";
            this.fbPhone.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.fbPhone.Location = new System.Drawing.Point(41, 372);
            this.fbPhone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fbPhone.Name = "fbPhone";
            this.fbPhone.Size = new System.Drawing.Size(547, 95);
            this.fbPhone.TabIndex = 14;
            // 
            // fbPassword
            // 
            this.fbPassword.fieldName = "密碼";
            this.fbPassword.fieldValue = "";
            this.fbPassword.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.fbPassword.Location = new System.Drawing.Point(41, 271);
            this.fbPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fbPassword.Name = "fbPassword";
            this.fbPassword.Size = new System.Drawing.Size(547, 95);
            this.fbPassword.TabIndex = 10;
            // 
            // fbEmail
            // 
            this.fbEmail.fieldName = "電子郵件";
            this.fbEmail.fieldValue = "";
            this.fbEmail.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.fbEmail.Location = new System.Drawing.Point(41, 170);
            this.fbEmail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fbEmail.Name = "fbEmail";
            this.fbEmail.Size = new System.Drawing.Size(547, 95);
            this.fbEmail.TabIndex = 9;
            // 
            // fbName
            // 
            this.fbName.fieldName = "會員姓名";
            this.fbName.fieldValue = "";
            this.fbName.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.fbName.Location = new System.Drawing.Point(41, 69);
            this.fbName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fbName.Name = "fbName";
            this.fbName.Size = new System.Drawing.Size(547, 95);
            this.fbName.TabIndex = 8;
            // 
            // fbId
            // 
            this.fbId.fieldName = "會員編號";
            this.fbId.fieldValue = "";
            this.fbId.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.fbId.Location = new System.Drawing.Point(304, 23);
            this.fbId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fbId.Name = "fbId";
            this.fbId.Size = new System.Drawing.Size(302, 74);
            this.fbId.TabIndex = 17;
            // 
            // fbRoleId
            // 
            this.fbRoleId.fieldName = "角色編號";
            this.fbRoleId.fieldValue = "";
            this.fbRoleId.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.fbRoleId.Location = new System.Drawing.Point(618, 353);
            this.fbRoleId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fbRoleId.Name = "fbRoleId";
            this.fbRoleId.Size = new System.Drawing.Size(302, 74);
            this.fbRoleId.TabIndex = 18;
            // 
            // FrmMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 642);
            this.Controls.Add(this.fbRoleId);
            this.Controls.Add(this.fbId);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.fbBirth);
            this.Controls.Add(this.fbPhone);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fbPassword);
            this.Controls.Add(this.fbEmail);
            this.Controls.Add(this.fbName);
            this.Name = "FrmMember";
            this.Text = "FrmMember";
            this.Load += new System.EventHandler(this.FrmMember_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private prjComponentDemo.usercontrol.FieldBoxLike fbPassword;
        private prjComponentDemo.usercontrol.FieldBoxLike fbEmail;
        private prjComponentDemo.usercontrol.FieldBoxLike fbName;
        private prjComponentDemo.usercontrol.FieldBoxLike fbPhone;
        private prjComponentDemo.usercontrol.FieldBoxLike fbBirth;
        private System.Windows.Forms.ComboBox comboBox1;
        private prjComponentDemo.usercontrol.FieldBoxLike fbId;
        private prjComponentDemo.usercontrol.FieldBoxLike fbRoleId;
    }
}