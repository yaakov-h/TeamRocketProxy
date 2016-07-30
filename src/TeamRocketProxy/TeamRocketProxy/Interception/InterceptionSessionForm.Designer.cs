namespace TeamRocketProxy.Interception
{
    partial class InterceptionSessionForm
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
            System.Windows.Forms.MenuStrip menuStrip1;
            System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
            System.Windows.Forms.SplitContainer splitContainer;
            System.Windows.Forms.GroupBox filterOptionsGroupBox;
            System.Windows.Forms.Label filterDirectionLabel;
            this.filterDirectionAnyRadioButton = new System.Windows.Forms.RadioButton();
            this.filterDirectionInRadioButton = new System.Windows.Forms.RadioButton();
            this.filterDirectionOutRadioButton = new System.Windows.Forms.RadioButton();
            this.filterUserTextTextBox = new System.Windows.Forms.TextBox();
            this.messagesListView = new System.Windows.Forms.ListView();
            this.messageIDColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.directionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.messageNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            splitContainer = new System.Windows.Forms.SplitContainer();
            filterOptionsGroupBox = new System.Windows.Forms.GroupBox();
            filterDirectionLabel = new System.Windows.Forms.Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(splitContainer)).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.SuspendLayout();
            filterOptionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            fileToolStripMenuItem});
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(904, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            exitToolStripMenuItem});
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += new System.EventHandler(this.OnExitMenuItemClicked);
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.Location = new System.Drawing.Point(0, 24);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(this.messagesListView);
            splitContainer.Panel1.Controls.Add(filterOptionsGroupBox);
            splitContainer.Size = new System.Drawing.Size(904, 437);
            splitContainer.SplitterDistance = 301;
            splitContainer.TabIndex = 1;
            // 
            // filterOptionsGroupBox
            // 
            filterOptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            filterOptionsGroupBox.Controls.Add(this.filterUserTextTextBox);
            filterOptionsGroupBox.Controls.Add(this.filterDirectionOutRadioButton);
            filterOptionsGroupBox.Controls.Add(this.filterDirectionInRadioButton);
            filterOptionsGroupBox.Controls.Add(this.filterDirectionAnyRadioButton);
            filterOptionsGroupBox.Controls.Add(filterDirectionLabel);
            filterOptionsGroupBox.Location = new System.Drawing.Point(13, 4);
            filterOptionsGroupBox.Name = "filterOptionsGroupBox";
            filterOptionsGroupBox.Size = new System.Drawing.Size(285, 68);
            filterOptionsGroupBox.TabIndex = 0;
            filterOptionsGroupBox.TabStop = false;
            filterOptionsGroupBox.Text = "Filter";
            // 
            // filterDirectionLabel
            // 
            filterDirectionLabel.AutoSize = true;
            filterDirectionLabel.Location = new System.Drawing.Point(7, 21);
            filterDirectionLabel.Name = "filterDirectionLabel";
            filterDirectionLabel.Size = new System.Drawing.Size(52, 13);
            filterDirectionLabel.TabIndex = 0;
            filterDirectionLabel.Text = "Direction:";
            // 
            // filterDirectionAnyRadioButton
            // 
            this.filterDirectionAnyRadioButton.AutoSize = true;
            this.filterDirectionAnyRadioButton.Location = new System.Drawing.Point(65, 19);
            this.filterDirectionAnyRadioButton.Name = "filterDirectionAnyRadioButton";
            this.filterDirectionAnyRadioButton.Size = new System.Drawing.Size(56, 17);
            this.filterDirectionAnyRadioButton.TabIndex = 1;
            this.filterDirectionAnyRadioButton.TabStop = true;
            this.filterDirectionAnyRadioButton.Text = "In/Out";
            this.filterDirectionAnyRadioButton.UseVisualStyleBackColor = true;
            this.filterDirectionAnyRadioButton.CheckedChanged += new System.EventHandler(this.OnFilterRadioButtonCheckedChanged);
            // 
            // filterDirectionInRadioButton
            // 
            this.filterDirectionInRadioButton.AutoSize = true;
            this.filterDirectionInRadioButton.Location = new System.Drawing.Point(128, 19);
            this.filterDirectionInRadioButton.Name = "filterDirectionInRadioButton";
            this.filterDirectionInRadioButton.Size = new System.Drawing.Size(34, 17);
            this.filterDirectionInRadioButton.TabIndex = 2;
            this.filterDirectionInRadioButton.TabStop = true;
            this.filterDirectionInRadioButton.Text = "In";
            this.filterDirectionInRadioButton.UseVisualStyleBackColor = true;
            this.filterDirectionInRadioButton.CheckedChanged += new System.EventHandler(this.OnFilterRadioButtonCheckedChanged);
            // 
            // filterDirectionOutRadioButton
            // 
            this.filterDirectionOutRadioButton.AutoSize = true;
            this.filterDirectionOutRadioButton.Location = new System.Drawing.Point(169, 19);
            this.filterDirectionOutRadioButton.Name = "filterDirectionOutRadioButton";
            this.filterDirectionOutRadioButton.Size = new System.Drawing.Size(42, 17);
            this.filterDirectionOutRadioButton.TabIndex = 3;
            this.filterDirectionOutRadioButton.TabStop = true;
            this.filterDirectionOutRadioButton.Text = "Out";
            this.filterDirectionOutRadioButton.UseVisualStyleBackColor = true;
            this.filterDirectionOutRadioButton.CheckedChanged += new System.EventHandler(this.OnFilterRadioButtonCheckedChanged);
            // 
            // filterUserTextTextBox
            // 
            this.filterUserTextTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.filterUserTextTextBox.Location = new System.Drawing.Point(6, 42);
            this.filterUserTextTextBox.Name = "filterUserTextTextBox";
            this.filterUserTextTextBox.Size = new System.Drawing.Size(273, 20);
            this.filterUserTextTextBox.TabIndex = 4;
            this.filterUserTextTextBox.Text = "Search...";
            this.filterUserTextTextBox.TextChanged += new System.EventHandler(this.OnFilterUserTextChanged);
            this.filterUserTextTextBox.Enter += new System.EventHandler(this.OnFilterUserTextTextBoxEnter);
            this.filterUserTextTextBox.Leave += new System.EventHandler(this.OnFilterUserTextTextBoxLeave);
            // 
            // messagesListView
            // 
            this.messagesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemNameColumnHeader,
            this.messageIDColumn,
            this.directionColumnHeader,
            this.messageNameColumnHeader});
            this.messagesListView.FullRowSelect = true;
            this.messagesListView.GridLines = true;
            this.messagesListView.Location = new System.Drawing.Point(13, 79);
            this.messagesListView.MultiSelect = false;
            this.messagesListView.Name = "messagesListView";
            this.messagesListView.Size = new System.Drawing.Size(285, 346);
            this.messagesListView.TabIndex = 1;
            this.messagesListView.UseCompatibleStateImageBehavior = false;
            this.messagesListView.View = System.Windows.Forms.View.Details;
            // 
            // messageIDColumn
            // 
            this.messageIDColumn.Text = "#";
            this.messageIDColumn.Width = 80;
            // 
            // directionColumnHeader
            // 
            this.directionColumnHeader.Text = "Direction";
            this.directionColumnHeader.Width = 54;
            // 
            // messageNameColumnHeader
            // 
            this.messageNameColumnHeader.Text = "Message";
            this.messageNameColumnHeader.Width = 140;
            // 
            // itemNameColumnHeader
            // 
            this.itemNameColumnHeader.Width = 0;
            // 
            // InterceptionSessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 461);
            this.Controls.Add(splitContainer);
            this.Controls.Add(menuStrip1);
            this.MainMenuStrip = menuStrip1;
            this.Name = "InterceptionSessionForm";
            this.Text = "Team Rocket Proxy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer)).EndInit();
            splitContainer.ResumeLayout(false);
            filterOptionsGroupBox.ResumeLayout(false);
            filterOptionsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton filterDirectionOutRadioButton;
        private System.Windows.Forms.RadioButton filterDirectionInRadioButton;
        private System.Windows.Forms.RadioButton filterDirectionAnyRadioButton;
        private System.Windows.Forms.ListView messagesListView;
        private System.Windows.Forms.ColumnHeader itemNameColumnHeader;
        private System.Windows.Forms.ColumnHeader directionColumnHeader;
        private System.Windows.Forms.ColumnHeader messageNameColumnHeader;
        private System.Windows.Forms.TextBox filterUserTextTextBox;
        private System.Windows.Forms.ColumnHeader messageIDColumn;
    }
}