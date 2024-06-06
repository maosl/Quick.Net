namespace SignalR.Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rthMessage = new TextBox();
            txtMsg1 = new TextBox();
            txtMsg2 = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // rthMessage
            // 
            rthMessage.Location = new Point(67, 36);
            rthMessage.Multiline = true;
            rthMessage.Name = "rthMessage";
            rthMessage.Size = new Size(422, 324);
            rthMessage.TabIndex = 0;
            // 
            // txtMsg1
            // 
            txtMsg1.Location = new Point(513, 59);
            txtMsg1.Name = "txtMsg1";
            txtMsg1.Size = new Size(248, 23);
            txtMsg1.TabIndex = 1;
            // 
            // txtMsg2
            // 
            txtMsg2.Location = new Point(513, 118);
            txtMsg2.Name = "txtMsg2";
            txtMsg2.Size = new Size(248, 23);
            txtMsg2.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(519, 187);
            button1.Name = "button1";
            button1.Size = new Size(242, 23);
            button1.TabIndex = 3;
            button1.Text = "发送消息";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(txtMsg2);
            Controls.Add(txtMsg1);
            Controls.Add(rthMessage);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox rthMessage;
        private TextBox txtMsg1;
        private TextBox txtMsg2;
        private Button button1;
    }
}
