namespace WindowsFormsApplication1
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboBaudrate = new System.Windows.Forms.ComboBox();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpRW = new System.Windows.Forms.GroupBox();
            this.btnReadAnalog = new System.Windows.Forms.Button();
            this.btnReadHolding = new System.Windows.Forms.Button();
            this.txtValue2 = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReg1 = new System.Windows.Forms.TextBox();
            this.txtReg = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerMail = new System.Windows.Forms.Timer(this.components);
            this.grpRW.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(120, 21);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(121, 20);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "COM3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Baudrate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Parity";
            // 
            // cboBaudrate
            // 
            this.cboBaudrate.FormattingEnabled = true;
            this.cboBaudrate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "115200"});
            this.cboBaudrate.Location = new System.Drawing.Point(120, 46);
            this.cboBaudrate.Name = "cboBaudrate";
            this.cboBaudrate.Size = new System.Drawing.Size(121, 21);
            this.cboBaudrate.TabIndex = 2;
            this.cboBaudrate.Text = "9600";
            // 
            // cboParity
            // 
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd",
            "Mark",
            "Space"});
            this.cboParity.Location = new System.Drawing.Point(120, 73);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(121, 21);
            this.cboParity.TabIndex = 2;
            this.cboParity.Text = "None";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(46, 111);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(125, 43);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(197, 111);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(125, 43);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 406);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Status :";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(78, 406);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(10, 13);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "-";
            // 
            // grpRW
            // 
            this.grpRW.Controls.Add(this.btnReadAnalog);
            this.grpRW.Controls.Add(this.btnReadHolding);
            this.grpRW.Controls.Add(this.txtValue2);
            this.grpRW.Controls.Add(this.txtValue);
            this.grpRW.Controls.Add(this.label6);
            this.grpRW.Controls.Add(this.txtReg1);
            this.grpRW.Controls.Add(this.txtReg);
            this.grpRW.Controls.Add(this.label5);
            this.grpRW.Enabled = false;
            this.grpRW.Location = new System.Drawing.Point(12, 160);
            this.grpRW.Name = "grpRW";
            this.grpRW.Size = new System.Drawing.Size(453, 243);
            this.grpRW.TabIndex = 6;
            this.grpRW.TabStop = false;
            this.grpRW.Text = "Read/Write";
            // 
            // btnReadAnalog
            // 
            this.btnReadAnalog.Location = new System.Drawing.Point(189, 45);
            this.btnReadAnalog.Name = "btnReadAnalog";
            this.btnReadAnalog.Size = new System.Drawing.Size(75, 50);
            this.btnReadAnalog.TabIndex = 4;
            this.btnReadAnalog.Text = "Read Analog";
            this.btnReadAnalog.UseVisualStyleBackColor = true;
            this.btnReadAnalog.Click += new System.EventHandler(this.btnReadAnalog_Click);
            // 
            // btnReadHolding
            // 
            this.btnReadHolding.Location = new System.Drawing.Point(108, 45);
            this.btnReadHolding.Name = "btnReadHolding";
            this.btnReadHolding.Size = new System.Drawing.Size(75, 50);
            this.btnReadHolding.TabIndex = 2;
            this.btnReadHolding.Text = "Read Holding";
            this.btnReadHolding.UseVisualStyleBackColor = true;
            this.btnReadHolding.Click += new System.EventHandler(this.btnReadHolding_Click);
            // 
            // txtValue2
            // 
            this.txtValue2.Location = new System.Drawing.Point(189, 100);
            this.txtValue2.Name = "txtValue2";
            this.txtValue2.Size = new System.Drawing.Size(76, 20);
            this.txtValue2.TabIndex = 1;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(108, 101);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(76, 20);
            this.txtValue.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Value";
            // 
            // txtReg1
            // 
            this.txtReg1.Location = new System.Drawing.Point(189, 18);
            this.txtReg1.Name = "txtReg1";
            this.txtReg1.Size = new System.Drawing.Size(76, 20);
            this.txtReg1.TabIndex = 1;
            this.txtReg1.Text = "1";
            // 
            // txtReg
            // 
            this.txtReg.Location = new System.Drawing.Point(108, 19);
            this.txtReg.Name = "txtReg";
            this.txtReg.Size = new System.Drawing.Size(76, 20);
            this.txtReg.TabIndex = 1;
            this.txtReg.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Register";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerMail
            // 
            this.timerMail.Tick += new System.EventHandler(this.timerMail_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 428);
            this.Controls.Add(this.grpRW);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cboParity);
            this.Controls.Add(this.cboBaudrate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Modbus RTU Master";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpRW.ResumeLayout(false);
            this.grpRW.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboBaudrate;
        private System.Windows.Forms.ComboBox cboParity;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grpRW;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtReg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnReadAnalog;
        private System.Windows.Forms.Button btnReadHolding;
        private System.Windows.Forms.TextBox txtReg1;
        private System.Windows.Forms.TextBox txtValue2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerMail;
    }
}

