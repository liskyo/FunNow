namespace FunNow.BackSide_Member
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
            this.fbMemberID = new prjComponentDemo.usercontrol.FieldBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.fbEail = new prjComponentDemo.usercontrol.FieldBox();
            this.fbPhone = new prjComponentDemo.usercontrol.FieldBox();
            this.fbName = new prjComponentDemo.usercontrol.FieldBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // fbMemberID
            // 
            this.fbMemberID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbMemberID.fieldName = "會員編號";
            this.fbMemberID.fieldValue = "111";
            this.fbMemberID.Location = new System.Drawing.Point(55, 41);
            this.fbMemberID.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.fbMemberID.Name = "fbMemberID";
            this.fbMemberID.Size = new System.Drawing.Size(151, 49);
            this.fbMemberID.TabIndex = 24;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(233, 104);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(277, 218);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(16, 373);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(517, 1);
            this.label1.TabIndex = 21;
            this.label1.Text = "label1";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(346, 383);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 31);
            this.button2.TabIndex = 19;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(434, 383);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 20;
            this.button1.Text = "確定";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // fbEail
            // 
            this.fbEail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbEail.fieldName = "電子郵件";
            this.fbEail.fieldValue = "測試";
            this.fbEail.Location = new System.Drawing.Point(55, 146);
            this.fbEail.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.fbEail.Name = "fbEail";
            this.fbEail.Size = new System.Drawing.Size(151, 49);
            this.fbEail.TabIndex = 16;
            // 
            // fbPhone
            // 
            this.fbPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbPhone.fieldName = "電話";
            this.fbPhone.fieldValue = "5555";
            this.fbPhone.Location = new System.Drawing.Point(55, 201);
            this.fbPhone.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.fbPhone.Name = "fbPhone";
            this.fbPhone.Size = new System.Drawing.Size(151, 49);
            this.fbPhone.TabIndex = 17;
            // 
            // fbName
            // 
            this.fbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbName.fieldName = "姓名";
            this.fbName.fieldValue = "111";
            this.fbName.Location = new System.Drawing.Point(55, 91);
            this.fbName.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.fbName.Name = "fbName";
            this.fbName.Size = new System.Drawing.Size(151, 49);
            this.fbName.TabIndex = 18;
            // 
            // FrmMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 455);
            this.Controls.Add(this.fbMemberID);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fbEail);
            this.Controls.Add(this.fbPhone);
            this.Controls.Add(this.fbName);
            this.Name = "FrmMember";
            this.Text = "FrmMember";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private prjComponentDemo.usercontrol.FieldBox fbMemberID;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private prjComponentDemo.usercontrol.FieldBox fbEail;
        private prjComponentDemo.usercontrol.FieldBox fbPhone;
        private prjComponentDemo.usercontrol.FieldBox fbName;
    }
}