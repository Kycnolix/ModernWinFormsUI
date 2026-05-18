namespace ModernWinFormsUI.Demo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            lblTitle = new Label();
            lblDescription = new Label();
            cardButtons = new ModernWinFormsUI.Controls.MwCard();
            btnGhostDemo = new ModernWinFormsUI.Controls.MwButton();
            btnDangerDemo = new ModernWinFormsUI.Controls.MwButton();
            btnSecondaryDemo = new ModernWinFormsUI.Controls.MwButton();
            btnPrimaryDemo = new ModernWinFormsUI.Controls.MwButton();
            cardInputs = new ModernWinFormsUI.Controls.MwCard();
            inputErrorDemo = new ModernWinFormsUI.Controls.MwTextBox();
            inputStudentNo = new ModernWinFormsUI.Controls.MwTextBox();
            cardBadges = new ModernWinFormsUI.Controls.MwCard();
            badgeInfo = new ModernWinFormsUI.Controls.MwBadge();
            badgeDanger = new ModernWinFormsUI.Controls.MwBadge();
            badgeWarning = new ModernWinFormsUI.Controls.MwBadge();
            badgeSuccess = new ModernWinFormsUI.Controls.MwBadge();
            badgeNeutral = new ModernWinFormsUI.Controls.MwBadge();
            cardAlerts = new ModernWinFormsUI.Controls.MwCard();
            mwAlert2 = new ModernWinFormsUI.Controls.MwAlert();
            mwAlert1 = new ModernWinFormsUI.Controls.MwAlert();
            alertWarningDemo = new ModernWinFormsUI.Controls.MwAlert();
            alertSuccessDemo = new ModernWinFormsUI.Controls.MwAlert();
            lblFooter = new Label();
            lblVersion = new Label();
            segmentedByNumber = new ModernWinFormsUI.Controls.MwSegmentedButton();
            SegmentedButtons = new ModernWinFormsUI.Controls.MwCard();
            segmentedByName = new ModernWinFormsUI.Controls.MwSegmentedButton();
            cardPictureBox = new ModernWinFormsUI.Controls.MwCard();
            mwPictureBox1 = new ModernWinFormsUI.Controls.MwPictureBox();
            cardComboBox = new ModernWinFormsUI.Controls.MwCard();
            comboPrinter = new ModernWinFormsUI.Controls.MwComboBox();
            cardStudents = new ModernWinFormsUI.Controls.MwCard();
            gridStudents = new ModernWinFormsUI.Controls.MwDataGridView();
            cardButtons.SuspendLayout();
            cardInputs.SuspendLayout();
            cardBadges.SuspendLayout();
            cardAlerts.SuspendLayout();
            SegmentedButtons.SuspendLayout();
            cardPictureBox.SuspendLayout();
            cardComboBox.SuspendLayout();
            cardStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridStudents).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(246, 32);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "ModernWinFormsUI";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblDescription.ForeColor = Color.FromArgb(75, 85, 99);
            lblDescription.Location = new Point(18, 46);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(462, 17);
            lblDescription.TabIndex = 5;
            lblDescription.Text = "A lightweight modern UI component library for .NET 8 WinForms applications.";
            // 
            // cardButtons
            // 
            cardButtons.BackColor = Color.Transparent;
            cardButtons.BorderColor = Color.FromArgb(218, 224, 235);
            cardButtons.CardBackColor = Color.FromArgb(255, 255, 255);
            cardButtons.Controls.Add(btnGhostDemo);
            cardButtons.Controls.Add(btnDangerDemo);
            cardButtons.Controls.Add(btnSecondaryDemo);
            cardButtons.Controls.Add(btnPrimaryDemo);
            cardButtons.Location = new Point(18, 81);
            cardButtons.Margin = new Padding(8);
            cardButtons.Name = "cardButtons";
            cardButtons.Padding = new Padding(16, 64, 16, 16);
            cardButtons.Radius = 14;
            cardButtons.Size = new Size(499, 155);
            cardButtons.SubTitle = "Action buttons with variants, sizes and focus states.\n";
            cardButtons.TabIndex = 6;
            cardButtons.Title = "Buttons";
            // 
            // btnGhostDemo
            // 
            btnGhostDemo.ButtonSize = ModernWinFormsUI.Controls.MwButtonSize.Medium;
            btnGhostDemo.FlatAppearance.BorderSize = 0;
            btnGhostDemo.FlatStyle = FlatStyle.Flat;
            btnGhostDemo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnGhostDemo.Location = new Point(367, 90);
            btnGhostDemo.Name = "btnGhostDemo";
            btnGhostDemo.Padding = new Padding(16, 0, 16, 0);
            btnGhostDemo.Radius = 10;
            btnGhostDemo.Size = new Size(110, 38);
            btnGhostDemo.TabIndex = 3;
            btnGhostDemo.Text = "Ghost";
            btnGhostDemo.UseVisualStyleBackColor = false;
            btnGhostDemo.Variant = ModernWinFormsUI.Controls.MwButtonVariant.Ghost;
            // 
            // btnDangerDemo
            // 
            btnDangerDemo.ButtonSize = ModernWinFormsUI.Controls.MwButtonSize.Medium;
            btnDangerDemo.FlatAppearance.BorderSize = 0;
            btnDangerDemo.FlatStyle = FlatStyle.Flat;
            btnDangerDemo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDangerDemo.Location = new Point(251, 90);
            btnDangerDemo.Name = "btnDangerDemo";
            btnDangerDemo.Padding = new Padding(16, 0, 16, 0);
            btnDangerDemo.Radius = 10;
            btnDangerDemo.Size = new Size(110, 38);
            btnDangerDemo.TabIndex = 2;
            btnDangerDemo.Text = "Danger";
            btnDangerDemo.UseVisualStyleBackColor = false;
            btnDangerDemo.Variant = ModernWinFormsUI.Controls.MwButtonVariant.Danger;
            // 
            // btnSecondaryDemo
            // 
            btnSecondaryDemo.ButtonSize = ModernWinFormsUI.Controls.MwButtonSize.Medium;
            btnSecondaryDemo.FlatAppearance.BorderSize = 0;
            btnSecondaryDemo.FlatStyle = FlatStyle.Flat;
            btnSecondaryDemo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSecondaryDemo.Location = new Point(135, 90);
            btnSecondaryDemo.Name = "btnSecondaryDemo";
            btnSecondaryDemo.Padding = new Padding(16, 0, 16, 0);
            btnSecondaryDemo.Radius = 10;
            btnSecondaryDemo.Size = new Size(110, 38);
            btnSecondaryDemo.TabIndex = 1;
            btnSecondaryDemo.Text = "Secondary";
            btnSecondaryDemo.UseVisualStyleBackColor = false;
            btnSecondaryDemo.Variant = ModernWinFormsUI.Controls.MwButtonVariant.Secondary;
            // 
            // btnPrimaryDemo
            // 
            btnPrimaryDemo.ButtonSize = ModernWinFormsUI.Controls.MwButtonSize.Medium;
            btnPrimaryDemo.FlatAppearance.BorderSize = 0;
            btnPrimaryDemo.FlatStyle = FlatStyle.Flat;
            btnPrimaryDemo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPrimaryDemo.IconGap = 0;
            btnPrimaryDemo.IconSize = 20;
            btnPrimaryDemo.Location = new Point(19, 90);
            btnPrimaryDemo.Name = "btnPrimaryDemo";
            btnPrimaryDemo.Padding = new Padding(16, 0, 16, 0);
            btnPrimaryDemo.Radius = 10;
            btnPrimaryDemo.Size = new Size(110, 38);
            btnPrimaryDemo.TabIndex = 0;
            btnPrimaryDemo.Text = "Primary";
            btnPrimaryDemo.UseVisualStyleBackColor = false;
            btnPrimaryDemo.Variant = ModernWinFormsUI.Controls.MwButtonVariant.Primary;
            btnPrimaryDemo.Click += btnPrimaryDemo_Click;
            // 
            // cardInputs
            // 
            cardInputs.BackColor = Color.Transparent;
            cardInputs.BorderColor = Color.FromArgb(218, 224, 235);
            cardInputs.CardBackColor = Color.FromArgb(255, 255, 255);
            cardInputs.Controls.Add(inputErrorDemo);
            cardInputs.Controls.Add(inputStudentNo);
            cardInputs.Location = new Point(18, 252);
            cardInputs.Margin = new Padding(8);
            cardInputs.Name = "cardInputs";
            cardInputs.Padding = new Padding(16, 64, 16, 16);
            cardInputs.Radius = 14;
            cardInputs.Size = new Size(499, 268);
            cardInputs.SubTitle = "Text fields with label, helper text and error state.\n";
            cardInputs.TabIndex = 7;
            cardInputs.Title = "Inputs";
            // 
            // inputErrorDemo
            // 
            inputErrorDemo.BackColor = Color.Transparent;
            inputErrorDemo.ErrorText = "Card ID is required.\n";
            inputErrorDemo.Font = new Font("Segoe UI", 9.5F);
            inputErrorDemo.LabelText = "Card ID";
            inputErrorDemo.Location = new Point(251, 124);
            inputErrorDemo.MinimumSize = new Size(120, 38);
            inputErrorDemo.Name = "inputErrorDemo";
            inputErrorDemo.PlaceholderText = "Enter card ID";
            inputErrorDemo.Size = new Size(226, 90);
            inputErrorDemo.TabIndex = 1;
            // 
            // inputStudentNo
            // 
            inputStudentNo.BackColor = Color.Transparent;
            inputStudentNo.Font = new Font("Segoe UI", 9.5F);
            inputStudentNo.LabelText = "Student Number";
            inputStudentNo.Location = new Point(19, 124);
            inputStudentNo.MinimumSize = new Size(120, 38);
            inputStudentNo.Name = "inputStudentNo";
            inputStudentNo.PlaceholderText = "e.g. 202400123";
            inputStudentNo.Size = new Size(226, 66);
            inputStudentNo.TabIndex = 0;
            // 
            // cardBadges
            // 
            cardBadges.BackColor = Color.Transparent;
            cardBadges.BorderColor = Color.FromArgb(218, 224, 235);
            cardBadges.CardBackColor = Color.FromArgb(255, 255, 255);
            cardBadges.Controls.Add(badgeInfo);
            cardBadges.Controls.Add(badgeDanger);
            cardBadges.Controls.Add(badgeWarning);
            cardBadges.Controls.Add(badgeSuccess);
            cardBadges.Controls.Add(badgeNeutral);
            cardBadges.Location = new Point(578, 81);
            cardBadges.Margin = new Padding(8);
            cardBadges.Name = "cardBadges";
            cardBadges.Padding = new Padding(16, 64, 16, 16);
            cardBadges.Radius = 14;
            cardBadges.Size = new Size(499, 155);
            cardBadges.SubTitle = "Small status indicators for tables, cards and forms.\n";
            cardBadges.TabIndex = 7;
            cardBadges.Title = "Badges";
            // 
            // badgeInfo
            // 
            badgeInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            badgeInfo.Location = new Point(402, 99);
            badgeInfo.Name = "badgeInfo";
            badgeInfo.Size = new Size(57, 26);
            badgeInfo.TabIndex = 4;
            badgeInfo.TabStop = false;
            badgeInfo.Text = "New";
            badgeInfo.Variant = ModernWinFormsUI.Controls.MwBadgeVariant.Info;
            // 
            // badgeDanger
            // 
            badgeDanger.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            badgeDanger.Location = new Point(305, 99);
            badgeDanger.Name = "badgeDanger";
            badgeDanger.Size = new Size(78, 26);
            badgeDanger.TabIndex = 3;
            badgeDanger.TabStop = false;
            badgeDanger.Text = "Blocked";
            badgeDanger.Variant = ModernWinFormsUI.Controls.MwBadgeVariant.Danger;
            // 
            // badgeWarning
            // 
            badgeWarning.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            badgeWarning.Location = new Point(202, 99);
            badgeWarning.Name = "badgeWarning";
            badgeWarning.Size = new Size(81, 26);
            badgeWarning.TabIndex = 2;
            badgeWarning.TabStop = false;
            badgeWarning.Text = "Pending";
            badgeWarning.Variant = ModernWinFormsUI.Controls.MwBadgeVariant.Warning;
            // 
            // badgeSuccess
            // 
            badgeSuccess.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            badgeSuccess.Location = new Point(112, 99);
            badgeSuccess.Name = "badgeSuccess";
            badgeSuccess.Size = new Size(68, 26);
            badgeSuccess.TabIndex = 1;
            badgeSuccess.TabStop = false;
            badgeSuccess.Text = "Active";
            badgeSuccess.Variant = ModernWinFormsUI.Controls.MwBadgeVariant.Success;
            // 
            // badgeNeutral
            // 
            badgeNeutral.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            badgeNeutral.Location = new Point(19, 99);
            badgeNeutral.Name = "badgeNeutral";
            badgeNeutral.Size = new Size(75, 26);
            badgeNeutral.TabIndex = 0;
            badgeNeutral.TabStop = false;
            badgeNeutral.Text = "Passive";
            // 
            // cardAlerts
            // 
            cardAlerts.BackColor = Color.Transparent;
            cardAlerts.BorderColor = Color.FromArgb(218, 224, 235);
            cardAlerts.CardBackColor = Color.FromArgb(255, 255, 255);
            cardAlerts.Controls.Add(mwAlert2);
            cardAlerts.Controls.Add(mwAlert1);
            cardAlerts.Controls.Add(alertWarningDemo);
            cardAlerts.Controls.Add(alertSuccessDemo);
            cardAlerts.Location = new Point(578, 252);
            cardAlerts.Margin = new Padding(8);
            cardAlerts.Name = "cardAlerts";
            cardAlerts.Padding = new Padding(16, 64, 16, 16);
            cardAlerts.Radius = 14;
            cardAlerts.Size = new Size(499, 484);
            cardAlerts.SubTitle = "Inline feedback messages for success, warning and error states.\n";
            cardAlerts.TabIndex = 8;
            cardAlerts.Title = "Alerts";
            // 
            // mwAlert2
            // 
            mwAlert2.Font = new Font("Segoe UI", 9.5F);
            mwAlert2.Location = new Point(19, 375);
            mwAlert2.Message = "Please checkout your printer device";
            mwAlert2.MinimumSize = new Size(220, 56);
            mwAlert2.Name = "mwAlert2";
            mwAlert2.Size = new Size(460, 73);
            mwAlert2.TabIndex = 3;
            mwAlert2.TabStop = false;
            mwAlert2.Title = "Printer error";
            mwAlert2.Variant = ModernWinFormsUI.Controls.MwAlertVariant.Danger;
            // 
            // mwAlert1
            // 
            mwAlert1.Font = new Font("Segoe UI", 9.5F);
            mwAlert1.Location = new Point(19, 165);
            mwAlert1.Message = "Please select a document for printing";
            mwAlert1.MinimumSize = new Size(220, 56);
            mwAlert1.Name = "mwAlert1";
            mwAlert1.Size = new Size(460, 73);
            mwAlert1.TabIndex = 2;
            mwAlert1.TabStop = false;
            mwAlert1.Title = "Printer selected";
            mwAlert1.Variant = ModernWinFormsUI.Controls.MwAlertVariant.Success;
            // 
            // alertWarningDemo
            // 
            alertWarningDemo.Font = new Font("Segoe UI", 9.5F);
            alertWarningDemo.Location = new Point(19, 267);
            alertWarningDemo.Message = "Please select a printer before printing the card.\n";
            alertWarningDemo.MinimumSize = new Size(220, 56);
            alertWarningDemo.Name = "alertWarningDemo";
            alertWarningDemo.Size = new Size(460, 90);
            alertWarningDemo.TabIndex = 1;
            alertWarningDemo.TabStop = false;
            alertWarningDemo.Title = "Printer not selected";
            alertWarningDemo.Variant = ModernWinFormsUI.Controls.MwAlertVariant.Warning;
            // 
            // alertSuccessDemo
            // 
            alertSuccessDemo.Font = new Font("Segoe UI", 9.5F);
            alertSuccessDemo.Location = new Point(19, 63);
            alertSuccessDemo.Message = "Student card was printed successfully.\n";
            alertSuccessDemo.MinimumSize = new Size(220, 56);
            alertSuccessDemo.Name = "alertSuccessDemo";
            alertSuccessDemo.Size = new Size(460, 90);
            alertSuccessDemo.TabIndex = 0;
            alertSuccessDemo.TabStop = false;
            alertSuccessDemo.Title = "Print completed";
            // 
            // lblFooter
            // 
            lblFooter.AutoSize = true;
            lblFooter.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblFooter.ForeColor = Color.FromArgb(107, 114, 128);
            lblFooter.Location = new Point(18, 960);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(411, 15);
            lblFooter.TabIndex = 9;
            lblFooter.Text = "ModernWinFormsUI Demo  •  .NET 8 WinForms  •  Designer-friendly controls";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblVersion.ForeColor = Color.FromArgb(75, 85, 99);
            lblVersion.Location = new Point(1803, 966);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(41, 15);
            lblVersion.TabIndex = 10;
            lblVersion.Text = "v0.4.0";
            // 
            // segmentedByNumber
            // 
            segmentedByNumber.FlatAppearance.BorderSize = 0;
            segmentedByNumber.FlatStyle = FlatStyle.Flat;
            segmentedByNumber.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            segmentedByNumber.Location = new Point(19, 85);
            segmentedByNumber.Name = "segmentedByNumber";
            segmentedByNumber.Padding = new Padding(16, 0, 16, 0);
            segmentedByNumber.Selected = true;
            segmentedByNumber.Size = new Size(226, 40);
            segmentedByNumber.TabIndex = 11;
            segmentedByNumber.Text = "Student NO";
            segmentedByNumber.UseVisualStyleBackColor = false;
            // 
            // SegmentedButtons
            // 
            SegmentedButtons.BackColor = Color.Transparent;
            SegmentedButtons.BorderColor = Color.FromArgb(218, 224, 235);
            SegmentedButtons.CardBackColor = Color.FromArgb(255, 255, 255);
            SegmentedButtons.Controls.Add(segmentedByName);
            SegmentedButtons.Controls.Add(segmentedByNumber);
            SegmentedButtons.Location = new Point(18, 546);
            SegmentedButtons.Margin = new Padding(8);
            SegmentedButtons.Name = "SegmentedButtons";
            SegmentedButtons.Padding = new Padding(16, 64, 16, 16);
            SegmentedButtons.Radius = 14;
            SegmentedButtons.Size = new Size(499, 190);
            SegmentedButtons.SubTitle = "";
            SegmentedButtons.TabIndex = 12;
            SegmentedButtons.Title = "SegmentedButtons";
            // 
            // segmentedByName
            // 
            segmentedByName.FlatAppearance.BorderSize = 0;
            segmentedByName.FlatStyle = FlatStyle.Flat;
            segmentedByName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            segmentedByName.Location = new Point(251, 85);
            segmentedByName.Name = "segmentedByName";
            segmentedByName.Padding = new Padding(16, 0, 16, 0);
            segmentedByName.Size = new Size(226, 40);
            segmentedByName.TabIndex = 12;
            segmentedByName.Text = "Name / Surname";
            segmentedByName.UseVisualStyleBackColor = false;
            // 
            // cardPictureBox
            // 
            cardPictureBox.BackColor = Color.Transparent;
            cardPictureBox.BorderColor = Color.FromArgb(218, 224, 235);
            cardPictureBox.CardBackColor = Color.FromArgb(255, 255, 255);
            cardPictureBox.Controls.Add(mwPictureBox1);
            cardPictureBox.Location = new Point(578, 752);
            cardPictureBox.Margin = new Padding(8);
            cardPictureBox.Name = "cardPictureBox";
            cardPictureBox.Padding = new Padding(16, 64, 16, 16);
            cardPictureBox.Radius = 14;
            cardPictureBox.Size = new Size(499, 190);
            cardPictureBox.SubTitle = "Image preview container with blank, text and initials states.\n";
            cardPictureBox.TabIndex = 13;
            cardPictureBox.Title = "Picture Box";
            // 
            // mwPictureBox1
            // 
            mwPictureBox1.BackColor = Color.Transparent;
            mwPictureBox1.BorderColor = Color.FromArgb(218, 224, 235);
            mwPictureBox1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            mwPictureBox1.Image = (Image)resources.GetObject("mwPictureBox1.Image");
            mwPictureBox1.Location = new Point(202, 60);
            mwPictureBox1.MinimumSize = new Size(48, 48);
            mwPictureBox1.Name = "mwPictureBox1";
            mwPictureBox1.PictureBackColor = Color.FromArgb(255, 255, 255);
            mwPictureBox1.Size = new Size(99, 111);
            mwPictureBox1.TabIndex = 0;
            mwPictureBox1.TabStop = false;
            mwPictureBox1.Text = "mwPictureBox1";
            // 
            // cardComboBox
            // 
            cardComboBox.BackColor = Color.Transparent;
            cardComboBox.BorderColor = Color.FromArgb(218, 224, 235);
            cardComboBox.CardBackColor = Color.FromArgb(255, 255, 255);
            cardComboBox.Controls.Add(comboPrinter);
            cardComboBox.Location = new Point(18, 752);
            cardComboBox.Margin = new Padding(8);
            cardComboBox.Name = "cardComboBox";
            cardComboBox.Padding = new Padding(16, 64, 16, 16);
            cardComboBox.Radius = 14;
            cardComboBox.Size = new Size(499, 190);
            cardComboBox.SubTitle = "Native selection behavior with modern label, helper and error states.\n";
            cardComboBox.TabIndex = 14;
            cardComboBox.Title = "Combo Box";
            // 
            // comboPrinter
            // 
            comboPrinter.BackColor = Color.Transparent;
            comboPrinter.Font = new Font("Segoe UI", 9.5F);
            comboPrinter.HelperText = "Select the student's department.\n";
            comboPrinter.LabelText = "Department";
            comboPrinter.Location = new Point(19, 77);
            comboPrinter.MinimumSize = new Size(120, 42);
            comboPrinter.Name = "comboPrinter";
            comboPrinter.PlaceholderText = "Select a printer";
            comboPrinter.Size = new Size(342, 96);
            comboPrinter.TabIndex = 0;
            // 
            // cardStudents
            // 
            cardStudents.BackColor = Color.Transparent;
            cardStudents.BorderColor = Color.FromArgb(218, 224, 235);
            cardStudents.CardBackColor = Color.FromArgb(255, 255, 255);
            cardStudents.Controls.Add(gridStudents);
            cardStudents.Location = new Point(1136, 81);
            cardStudents.Margin = new Padding(8);
            cardStudents.Name = "cardStudents";
            cardStudents.Padding = new Padding(16, 64, 16, 16);
            cardStudents.Radius = 14;
            cardStudents.Size = new Size(708, 409);
            cardStudents.SubTitle = "Modern table styling with configurable colors, lines and row states.\n";
            cardStudents.TabIndex = 15;
            cardStudents.Title = "Data Grid View";
            cardStudents.Paint += cardStudents_Paint;
            // 
            // gridStudents
            // 
            gridStudents.AllowUserToAddRows = false;
            gridStudents.AllowUserToDeleteRows = false;
            gridStudents.AllowUserToResizeRows = false;
            gridStudents.AlternateRowBackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(17, 24, 39);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(17, 24, 39);
            gridStudents.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            gridStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridStudents.BackgroundColor = Color.White;
            gridStudents.BorderStyle = BorderStyle.None;
            gridStudents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gridStudents.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(17, 24, 39);
            dataGridViewCellStyle2.Padding = new Padding(14, 0, 14, 0);
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(17, 24, 39);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            gridStudents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            gridStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(17, 24, 39);
            dataGridViewCellStyle3.Padding = new Padding(14, 0, 14, 0);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(17, 24, 39);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            gridStudents.DefaultCellStyle = dataGridViewCellStyle3;
            gridStudents.EmptyBackColor = Color.White;
            gridStudents.EnableHeadersVisualStyles = false;
            gridStudents.Font = new Font("Segoe UI", 9.5F);
            gridStudents.GridColor = Color.FromArgb(226, 232, 240);
            gridStudents.GridLineColor = Color.FromArgb(226, 232, 240);
            gridStudents.HeaderBackColor = Color.White;
            gridStudents.HeaderForeColor = Color.FromArgb(17, 24, 39);
            gridStudents.Location = new Point(19, 67);
            gridStudents.MultiSelect = false;
            gridStudents.Name = "gridStudents";
            gridStudents.OuterBorderColor = Color.FromArgb(226, 232, 240);
            gridStudents.ReadOnly = true;
            gridStudents.RowBackColor = Color.White;
            gridStudents.RowForeColor = Color.FromArgb(17, 24, 39);
            gridStudents.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(17, 24, 39);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(17, 24, 39);
            gridStudents.RowsDefaultCellStyle = dataGridViewCellStyle4;
            gridStudents.RowTemplate.Height = 50;
            gridStudents.SelectionBackColor = Color.FromArgb(239, 246, 255);
            gridStudents.SelectionForeColor = Color.FromArgb(17, 24, 39);
            gridStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridStudents.Size = new Size(670, 314);
            gridStudents.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1861, 987);
            Controls.Add(cardStudents);
            Controls.Add(cardComboBox);
            Controls.Add(cardPictureBox);
            Controls.Add(SegmentedButtons);
            Controls.Add(lblVersion);
            Controls.Add(lblFooter);
            Controls.Add(cardAlerts);
            Controls.Add(cardBadges);
            Controls.Add(cardInputs);
            Controls.Add(cardButtons);
            Controls.Add(lblDescription);
            Controls.Add(lblTitle);
            Name = "MainForm";
            ShowIcon = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ModernWinFormsUI Demo";
            Load += MainForm_Load;
            cardButtons.ResumeLayout(false);
            cardInputs.ResumeLayout(false);
            cardBadges.ResumeLayout(false);
            cardAlerts.ResumeLayout(false);
            SegmentedButtons.ResumeLayout(false);
            cardPictureBox.ResumeLayout(false);
            cardComboBox.ResumeLayout(false);
            cardStudents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridStudents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblTitle;
        private Label lblDescription;
        private Controls.MwCard cardButtons;
        private Controls.MwButton btnGhostDemo;
        private Controls.MwButton btnDangerDemo;
        private Controls.MwButton btnSecondaryDemo;
        private Controls.MwButton btnPrimaryDemo;
        private Controls.MwCard cardInputs;
        private Controls.MwTextBox inputErrorDemo;
        private Controls.MwTextBox inputStudentNo;
        private Controls.MwCard cardBadges;
        private Controls.MwBadge badgeNeutral;
        private Controls.MwCard cardAlerts;
        private Controls.MwBadge badgeDanger;
        private Controls.MwBadge badgeWarning;
        private Controls.MwBadge badgeSuccess;
        private Controls.MwBadge badgeInfo;
        private Controls.MwAlert alertWarningDemo;
        private Controls.MwAlert alertSuccessDemo;
        private Label lblFooter;
        private Label lblVersion;
        private Controls.MwSegmentedButton segmentedByNumber;
        private Controls.MwCard SegmentedButtons;
        private Controls.MwSegmentedButton segmentedByName;
        private Controls.MwCard cardPictureBox;
        private Controls.MwPictureBox mwPictureBox1;
        private Controls.MwCard cardComboBox;
        private Controls.MwComboBox comboPrinter;
        private Controls.MwAlert mwAlert2;
        private Controls.MwAlert mwAlert1;
        private Controls.MwCard cardStudents;
        private Controls.MwDataGridView gridStudents;
    }
}
