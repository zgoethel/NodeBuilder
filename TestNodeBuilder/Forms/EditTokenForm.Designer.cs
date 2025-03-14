namespace TestNodeBuilder.Forms
{
    partial class EditTokenForm
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
            testRegexGroup = new GroupBox();
            matchField = new TextBox();
            exactCheck = new CheckBox();
            testInputField = new TextBox();
            saveButton = new Button();
            regexFieldLabel = new Label();
            regexField = new TextBox();
            validationLabel = new Label();
            cancelButton = new Button();
            minimizeButton = new Button();
            testRegexGroup.SuspendLayout();
            SuspendLayout();
            // 
            // testRegexGroup
            // 
            testRegexGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            testRegexGroup.Controls.Add(matchField);
            testRegexGroup.Controls.Add(exactCheck);
            testRegexGroup.Controls.Add(testInputField);
            testRegexGroup.Location = new Point(12, 79);
            testRegexGroup.Name = "testRegexGroup";
            testRegexGroup.Size = new Size(308, 85);
            testRegexGroup.TabIndex = 1;
            testRegexGroup.TabStop = false;
            testRegexGroup.Text = "Test Match";
            // 
            // matchField
            // 
            matchField.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            matchField.Location = new Point(6, 51);
            matchField.Name = "matchField";
            matchField.ReadOnly = true;
            matchField.Size = new Size(237, 23);
            matchField.TabIndex = 5;
            // 
            // exactCheck
            // 
            exactCheck.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            exactCheck.AutoSize = true;
            exactCheck.Location = new Point(249, 26);
            exactCheck.Name = "exactCheck";
            exactCheck.Size = new Size(53, 19);
            exactCheck.TabIndex = 4;
            exactCheck.Text = "Exact";
            exactCheck.UseVisualStyleBackColor = true;
            exactCheck.CheckedChanged += exactCheck_CheckedChanged;
            // 
            // testInputField
            // 
            testInputField.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            testInputField.Location = new Point(6, 22);
            testInputField.Name = "testInputField";
            testInputField.Size = new Size(237, 23);
            testInputField.TabIndex = 3;
            testInputField.TextChanged += testInputField_TextChanged;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            saveButton.Location = new Point(12, 180);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(308, 23);
            saveButton.TabIndex = 6;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // regexFieldLabel
            // 
            regexFieldLabel.AutoSize = true;
            regexFieldLabel.Location = new Point(12, 9);
            regexFieldLabel.Name = "regexFieldLabel";
            regexFieldLabel.Size = new Size(69, 15);
            regexFieldLabel.TabIndex = 2;
            regexFieldLabel.Text = "Regex (BRE)";
            // 
            // regexField
            // 
            regexField.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            regexField.Location = new Point(12, 27);
            regexField.Name = "regexField";
            regexField.Size = new Size(227, 23);
            regexField.TabIndex = 1;
            regexField.TextChanged += regexField_TextChanged;
            // 
            // validationLabel
            // 
            validationLabel.AutoSize = true;
            validationLabel.Location = new Point(12, 53);
            validationLabel.Name = "validationLabel";
            validationLabel.Size = new Size(32, 15);
            validationLabel.TabIndex = 4;
            validationLabel.Text = "Valid";
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cancelButton.Location = new Point(12, 209);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(308, 23);
            cancelButton.TabIndex = 7;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.Enabled = false;
            minimizeButton.Location = new Point(245, 27);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.Size = new Size(75, 23);
            minimizeButton.TabIndex = 2;
            minimizeButton.Text = "Minimize";
            minimizeButton.UseVisualStyleBackColor = true;
            minimizeButton.Click += minimizeButton_Click;
            // 
            // EditTokenForm
            // 
            AcceptButton = saveButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(332, 250);
            Controls.Add(minimizeButton);
            Controls.Add(cancelButton);
            Controls.Add(validationLabel);
            Controls.Add(regexField);
            Controls.Add(regexFieldLabel);
            Controls.Add(saveButton);
            Controls.Add(testRegexGroup);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "EditTokenForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Token";
            Load += EditTokenForm_Load;
            testRegexGroup.ResumeLayout(false);
            testRegexGroup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox testRegexGroup;
        private Button saveButton;
        private Label regexFieldLabel;
        private TextBox regexField;
        private Label validationLabel;
        private TextBox matchField;
        private CheckBox exactCheck;
        private TextBox testInputField;
        private Button cancelButton;
        private Button minimizeButton;
    }
}