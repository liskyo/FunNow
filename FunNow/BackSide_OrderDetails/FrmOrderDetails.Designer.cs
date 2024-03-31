namespace FunNow.BackSide_OrderDetails
{
    partial class FrmOrderDetails
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
            this.fbCheckOutDate = new System.Windows.Forms.DateTimePicker();
            this.fbCheckInDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.fbMemberID = new prjComponentDemo.usercontrol.FieldBox();
            this.fbRoomID = new prjComponentDemo.usercontrol.FieldBox();
            this.fbOrderDetailID = new prjComponentDemo.usercontrol.FieldBox();
            this.SuspendLayout();
            // 
            // fbCheckOutDate
            // 
            this.fbCheckOutDate.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.fbCheckOutDate.Location = new System.Drawing.Point(181, 268);
            this.fbCheckOutDate.Margin = new System.Windows.Forms.Padding(2);
            this.fbCheckOutDate.Name = "fbCheckOutDate";
            this.fbCheckOutDate.Size = new System.Drawing.Size(152, 27);
            this.fbCheckOutDate.TabIndex = 22;
            this.fbCheckOutDate.ValueChanged += new System.EventHandler(this.fbCheckOutDate_ValueChanged);
            // 
            // fbCheckInDate
            // 
            this.fbCheckInDate.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.fbCheckInDate.Location = new System.Drawing.Point(181, 211);
            this.fbCheckInDate.Margin = new System.Windows.Forms.Padding(2);
            this.fbCheckInDate.Name = "fbCheckInDate";
            this.fbCheckInDate.Size = new System.Drawing.Size(152, 27);
            this.fbCheckInDate.TabIndex = 21;
            this.fbCheckInDate.ValueChanged += new System.EventHandler(this.fbCheckInDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(141, 382);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(518, 2);
            this.label1.TabIndex = 19;
            this.label1.Text = "label1";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(471, 394);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 30);
            this.button2.TabIndex = 17;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(560, 394);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 18;
            this.button1.Text = "確定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fbMemberID
            // 
            this.fbMemberID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbMemberID.fieldName = "會員編號";
            this.fbMemberID.fieldValue = "2";
            this.fbMemberID.Location = new System.Drawing.Point(182, 92);
            this.fbMemberID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fbMemberID.Name = "fbMemberID";
            this.fbMemberID.Size = new System.Drawing.Size(151, 50);
            this.fbMemberID.TabIndex = 23;
            // 
            // fbRoomID
            // 
            this.fbRoomID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbRoomID.fieldName = "房間編號";
            this.fbRoomID.fieldValue = "0";
            this.fbRoomID.Location = new System.Drawing.Point(181, 146);
            this.fbRoomID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fbRoomID.Name = "fbRoomID";
            this.fbRoomID.Size = new System.Drawing.Size(151, 50);
            this.fbRoomID.TabIndex = 20;
            // 
            // fbOrderDetailID
            // 
            this.fbOrderDetailID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fbOrderDetailID.fieldName = "編號";
            this.fbOrderDetailID.fieldValue = "0";
            this.fbOrderDetailID.Location = new System.Drawing.Point(181, 26);
            this.fbOrderDetailID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fbOrderDetailID.Name = "fbOrderDetailID";
            this.fbOrderDetailID.Size = new System.Drawing.Size(151, 50);
            this.fbOrderDetailID.TabIndex = 16;
            // 
            // FrmOrderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fbMemberID);
            this.Controls.Add(this.fbCheckOutDate);
            this.Controls.Add(this.fbCheckInDate);
            this.Controls.Add(this.fbRoomID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fbOrderDetailID);
            this.Name = "FrmOrderDetails";
            this.Text = "FrmOrderDetails";
            this.Load += new System.EventHandler(this.FrmOrderDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker fbCheckOutDate;
        private System.Windows.Forms.DateTimePicker fbCheckInDate;
        private prjComponentDemo.usercontrol.FieldBox fbRoomID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private prjComponentDemo.usercontrol.FieldBox fbOrderDetailID;
        private prjComponentDemo.usercontrol.FieldBox fbMemberID;
    }
}