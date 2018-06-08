namespace PUC_AFB_Config
{
    partial class PUCAFBConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PUCAFBConfig));
            this.destinationTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.passwordTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.userNameTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.setDestinationBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.updateIntervalTxtBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.setSourceBtn = new System.Windows.Forms.Button();
            this.sourceTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // destinationTxtBox
            // 
            this.destinationTxtBox.Location = new System.Drawing.Point(9, 43);
            this.destinationTxtBox.Name = "destinationTxtBox";
            this.destinationTxtBox.Size = new System.Drawing.Size(311, 20);
            this.destinationTxtBox.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.passwordTxtBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.userNameTxtBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.setDestinationBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.destinationTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(354, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 276);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Destination Configuration";
            // 
            // passwordTxtBox
            // 
            this.passwordTxtBox.Location = new System.Drawing.Point(9, 180);
            this.passwordTxtBox.Name = "passwordTxtBox";
            this.passwordTxtBox.Size = new System.Drawing.Size(197, 20);
            this.passwordTxtBox.TabIndex = 6;
            this.passwordTxtBox.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Enter Your Password (if required):";
            // 
            // userNameTxtBox
            // 
            this.userNameTxtBox.Location = new System.Drawing.Point(9, 129);
            this.userNameTxtBox.Name = "userNameTxtBox";
            this.userNameTxtBox.Size = new System.Drawing.Size(198, 20);
            this.userNameTxtBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter Your UserName (if required):";
            // 
            // setDestinationBtn
            // 
            this.setDestinationBtn.Location = new System.Drawing.Point(9, 69);
            this.setDestinationBtn.Name = "setDestinationBtn";
            this.setDestinationBtn.Size = new System.Drawing.Size(75, 23);
            this.setDestinationBtn.TabIndex = 2;
            this.setDestinationBtn.Text = "Change";
            this.setDestinationBtn.UseVisualStyleBackColor = true;
            this.setDestinationBtn.Click += new System.EventHandler(this.setDestinationBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select the location where the files should be copied to:";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(611, 313);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 0;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.updateIntervalTxtBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.setSourceBtn);
            this.groupBox2.Controls.Add(this.sourceTxtBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 276);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Source Configuration";
            // 
            // updateIntervalTxtBox
            // 
            this.updateIntervalTxtBox.Location = new System.Drawing.Point(9, 129);
            this.updateIntervalTxtBox.Name = "updateIntervalTxtBox";
            this.updateIntervalTxtBox.Size = new System.Drawing.Size(100, 20);
            this.updateIntervalTxtBox.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(245, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "How often should files be backed up (in minutes)? ";
            // 
            // setSourceBtn
            // 
            this.setSourceBtn.Location = new System.Drawing.Point(9, 69);
            this.setSourceBtn.Name = "setSourceBtn";
            this.setSourceBtn.Size = new System.Drawing.Size(75, 23);
            this.setSourceBtn.TabIndex = 4;
            this.setSourceBtn.Text = "Change";
            this.setSourceBtn.UseVisualStyleBackColor = true;
            this.setSourceBtn.Click += new System.EventHandler(this.setSourceBtn_Click);
            // 
            // sourceTxtBox
            // 
            this.sourceTxtBox.Location = new System.Drawing.Point(9, 43);
            this.sourceTxtBox.Name = "sourceTxtBox";
            this.sourceTxtBox.Size = new System.Drawing.Size(311, 20);
            this.sourceTxtBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Select the files you want backed up:";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.Green;
            this.statusLabel.Location = new System.Drawing.Point(297, 313);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(114, 20);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Text = "Save Success!";
            // 
            // PUCAFBConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 348);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PUCAFBConfig";
            this.Text = "PUC AFB Configuration";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox destinationTxtBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button setDestinationBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox userNameTxtBox;
        private System.Windows.Forms.TextBox passwordTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox updateIntervalTxtBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button setSourceBtn;
        private System.Windows.Forms.TextBox sourceTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label statusLabel;
    }
}

