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
            BackButton = new Button();
            BookingPageButton = new Button();
            LoginPageButton = new Button();
            ShowBookingPageButton = new Button();
            rightPanel = new Panel();
            leftPanel.SuspendLayout();
            SuspendLayout();
            // 
            // leftPanel
            // 
            leftPanel.BackgroundImage = (Image)resources.GetObject("leftPanel.BackgroundImage");
            leftPanel.Controls.Add(BackButton);
            leftPanel.Controls.Add(BookingPageButton);
            leftPanel.Controls.Add(LoginPageButton);
            leftPanel.Controls.Add(ShowBookingPageButton);
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Location = new Point(0, 0);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(225, 594);
            leftPanel.TabIndex = 0;
            // 
            // BackButton
            // 
            BackButton.Location = new Point(11, 8);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(112, 34);
            BackButton.TabIndex = 4;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = true;
            // 
            // BookingPageButton
            // 
            BookingPageButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BookingPageButton.Location = new Point(50, 100);
            BookingPageButton.Name = "BookingPageButton";
            BookingPageButton.Size = new Size(112, 34);
            BookingPageButton.TabIndex = 3;
            BookingPageButton.Text = "BookingPage";
            BookingPageButton.UseVisualStyleBackColor = true;
            BookingPageButton.Click += BookingPage_Click;
            // 
            // LoginPageButton
            // 
            LoginPageButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            LoginPageButton.Location = new Point(50, 50);
            LoginPageButton.Name = "LoginPageButton";
            LoginPageButton.Size = new Size(112, 34);
            LoginPageButton.TabIndex = 2;
            LoginPageButton.Text = "LoginPage";
            LoginPageButton.UseVisualStyleBackColor = true;
            LoginPageButton.Click += button1_Click_1;
            // 
            // ShowBookingPageButton
            // 
            ShowBookingPageButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ShowBookingPageButton.Location = new Point(50, 150);
            ShowBookingPageButton.Name = "ShowBookingPageButton";
            ShowBookingPageButton.Size = new Size(112, 34);
            ShowBookingPageButton.TabIndex = 4;
            ShowBookingPageButton.Text = "ShowBooking";
            ShowBookingPageButton.UseVisualStyleBackColor = true;
            // 
            // rightPanel
            // 
            rightPanel.BackColor = SystemColors.AppWorkspace;
            rightPanel.BackgroundImage = (Image)resources.GetObject("rightPanel.BackgroundImage");
            rightPanel.BackgroundImageLayout = ImageLayout.Stretch;
            rightPanel.Dock = DockStyle.Right;
            rightPanel.Location = new Point(245, 0);
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
        private Button LoginPageButton;
        private Button BookingPageButton;
        private Button BackButton;
        private Button ShowBookingPageButton;
    }
}
