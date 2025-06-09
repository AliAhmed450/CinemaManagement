namespace ProjectWinForm
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
        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            leftPanel = new Panel();
            BookingPage = new Button();
            button1 = new Button();
            rightPanel = new Panel();
            leftPanel.SuspendLayout();
            SuspendLayout();
            // 
            // leftPanel
            // 
            leftPanel.BackgroundImage = (Image)resources.GetObject("leftPanel.BackgroundImage");
            leftPanel.Controls.Add(BookingPage);
            leftPanel.Controls.Add(button1);
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Location = new Point(0, 0);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(225, 594);
            leftPanel.TabIndex = 0;
            // 
            // BookingPage
            // 
            BookingPage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BookingPage.Location = new Point(50, 98);
            BookingPage.Name = "BookingPage";
            BookingPage.Size = new Size(112, 34);
            BookingPage.TabIndex = 3;
            BookingPage.Text = "BookingPage";
            BookingPage.UseVisualStyleBackColor = true;
            BookingPage.Click += BookingPage_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.Location = new Point(50, 29);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 2;
            button1.Text = "LoginPage";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // rightPanel
            // 
            rightPanel.BackColor = SystemColors.AppWorkspace;
            rightPanel.Dock = DockStyle.Right;
            rightPanel.Location = new Point(224, 0);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new Size(797, 594);
            rightPanel.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1021, 594);
            Controls.Add(rightPanel);
            Controls.Add(leftPanel);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            leftPanel.ResumeLayout(false);
            ResumeLayout(false);
        }


        private Panel leftPanel;
        private Panel rightPanel;
        private Button button1;
        private Button BookingPage;
    }
}
