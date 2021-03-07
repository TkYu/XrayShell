namespace XrayShell.View
{
    partial class ConfigForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.OKButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.ServerGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CTSelect = new System.Windows.Forms.ComboBox();
            this.AlterIdLabel = new System.Windows.Forms.Label();
            this.RemarksTextBox = new System.Windows.Forms.TextBox();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.ServerPortLabel = new System.Windows.Forms.Label();
            this.IDLabel = new System.Windows.Forms.Label();
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.NetworkLabel = new System.Windows.Forms.Label();
            this.NetworkSelect = new System.Windows.Forms.ComboBox();
            this.SecurityLabel = new System.Windows.Forms.Label();
            this.CTLabel = new System.Windows.Forms.Label();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.PathLabel = new System.Windows.Forms.Label();
            this.SecuritySelect = new System.Windows.Forms.ComboBox();
            this.ServerPortText = new System.Windows.Forms.TextBox();
            this.AlterIdText = new System.Windows.Forms.TextBox();
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.RemarksLabel = new System.Windows.Forms.Label();
            this.FlowLabel = new System.Windows.Forms.Label();
            this.FlowSelect = new System.Windows.Forms.ComboBox();
            this.SNILabel = new System.Windows.Forms.Label();
            this.HostLabel = new System.Windows.Forms.Label();
            this.TlsLabel = new System.Windows.Forms.Label();
            this.ProtocolLabel = new System.Windows.Forms.Label();
            this.ProtocolSelect = new System.Windows.Forms.ComboBox();
            this.SNITextBox = new System.Windows.Forms.TextBox();
            this.HostTextBox = new System.Windows.Forms.TextBox();
            this.TlsSelect = new System.Windows.Forms.ComboBox();
            this.ServersListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.MoveDownButton = new System.Windows.Forms.Button();
            this.MoveUpButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.ProxyPortLabel = new System.Windows.Forms.Label();
            this.LocalPortNum = new System.Windows.Forms.NumericUpDown();
            this.CorePortLabel = new System.Windows.Forms.Label();
            this.CorePortNum = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.DuplicateButton = new System.Windows.Forms.Button();
            this.ClipboardButton = new System.Windows.Forms.Button();
            this.ServerGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocalPortNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CorePortNum)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Location = new System.Drawing.Point(423, 374);
            this.panel2.Margin = new System.Windows.Forms.Padding(6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0, 0);
            this.panel2.TabIndex = 1;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.OKButton.Location = new System.Drawing.Point(6, 6);
            this.OKButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(150, 46);
            this.OKButton.TabIndex = 23;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.MyCancelButton.Location = new System.Drawing.Point(168, 6);
            this.MyCancelButton.Margin = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(150, 46);
            this.MyCancelButton.TabIndex = 24;
            this.MyCancelButton.Text = "Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            this.MyCancelButton.Click += new System.EventHandler(this.MyCancelButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.DeleteButton.Location = new System.Drawing.Point(172, 12);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(6, 12, 0, 6);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(160, 46);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "&Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.AddButton.Location = new System.Drawing.Point(0, 12);
            this.AddButton.Margin = new System.Windows.Forms.Padding(0, 12, 6, 6);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(160, 46);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "&Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ServerGroupBox
            // 
            this.ServerGroupBox.AutoSize = true;
            this.ServerGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ServerGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.ServerGroupBox.Location = new System.Drawing.Point(356, 0);
            this.ServerGroupBox.Margin = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.ServerGroupBox.Name = "ServerGroupBox";
            this.ServerGroupBox.Padding = new System.Windows.Forms.Padding(6);
            this.ServerGroupBox.Size = new System.Drawing.Size(671, 686);
            this.ServerGroupBox.TabIndex = 0;
            this.ServerGroupBox.TabStop = false;
            this.ServerGroupBox.Text = "Server";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.CTSelect, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.AlterIdLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.RemarksTextBox, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.AddressLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ServerPortLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.IDLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.AddressTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.NetworkLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.NetworkSelect, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.SecurityLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.CTLabel, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.PathTextBox, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.PathLabel, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.SecuritySelect, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.ServerPortText, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.AlterIdText, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.IDTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.RemarksLabel, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.FlowLabel, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.FlowSelect, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.SNILabel, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.HostLabel, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.TlsLabel, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.ProtocolLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ProtocolSelect, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.SNITextBox, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.HostTextBox, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.TlsSelect, 1, 11);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 30);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(654, 626);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // CTSelect
            // 
            this.CTSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CTSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CTSelect.FormattingEnabled = true;
            this.CTSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CTSelect.ItemHeight = 25;
            this.CTSelect.Location = new System.Drawing.Point(205, 448);
            this.CTSelect.Margin = new System.Windows.Forms.Padding(6);
            this.CTSelect.Name = "CTSelect";
            this.CTSelect.Size = new System.Drawing.Size(437, 33);
            this.CTSelect.TabIndex = 17;
            // 
            // AlterIdLabel
            // 
            this.AlterIdLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AlterIdLabel.AutoSize = true;
            this.AlterIdLabel.Location = new System.Drawing.Point(120, 279);
            this.AlterIdLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.AlterIdLabel.Name = "AlterIdLabel";
            this.AlterIdLabel.Size = new System.Drawing.Size(73, 25);
            this.AlterIdLabel.TabIndex = 107;
            this.AlterIdLabel.Text = "AlterId";
            // 
            // RemarksTextBox
            // 
            this.RemarksTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RemarksTextBox.Location = new System.Drawing.Point(205, 583);
            this.RemarksTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.RemarksTextBox.MaxLength = 32;
            this.RemarksTextBox.Name = "RemarksTextBox";
            this.RemarksTextBox.Size = new System.Drawing.Size(437, 31);
            this.RemarksTextBox.TabIndex = 20;
            this.RemarksTextBox.WordWrap = false;
            // 
            // AddressLabel
            // 
            this.AddressLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Location = new System.Drawing.Point(33, 60);
            this.AddressLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(160, 25);
            this.AddressLabel.TabIndex = 102;
            this.AddressLabel.Text = "Server Address";
            // 
            // ServerPortLabel
            // 
            this.ServerPortLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ServerPortLabel.AutoSize = true;
            this.ServerPortLabel.Location = new System.Drawing.Point(73, 103);
            this.ServerPortLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ServerPortLabel.Name = "ServerPortLabel";
            this.ServerPortLabel.Size = new System.Drawing.Size(120, 25);
            this.ServerPortLabel.TabIndex = 103;
            this.ServerPortLabel.Text = "Server Port";
            // 
            // IDLabel
            // 
            this.IDLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.IDLabel.AutoSize = true;
            this.IDLabel.Location = new System.Drawing.Point(161, 146);
            this.IDLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(32, 25);
            this.IDLabel.TabIndex = 104;
            this.IDLabel.Text = "ID";
            this.IDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.AddressTextBox.Location = new System.Drawing.Point(205, 57);
            this.AddressTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.AddressTextBox.MaxLength = 512;
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(437, 31);
            this.AddressTextBox.TabIndex = 8;
            this.AddressTextBox.WordWrap = false;
            // 
            // NetworkLabel
            // 
            this.NetworkLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.NetworkLabel.AutoSize = true;
            this.NetworkLabel.Location = new System.Drawing.Point(103, 190);
            this.NetworkLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.NetworkLabel.Name = "NetworkLabel";
            this.NetworkLabel.Size = new System.Drawing.Size(90, 25);
            this.NetworkLabel.TabIndex = 105;
            this.NetworkLabel.Text = "Network";
            // 
            // NetworkSelect
            // 
            this.NetworkSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.NetworkSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NetworkSelect.FormattingEnabled = true;
            this.NetworkSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.NetworkSelect.ItemHeight = 25;
            this.NetworkSelect.Location = new System.Drawing.Point(205, 186);
            this.NetworkSelect.Margin = new System.Windows.Forms.Padding(6);
            this.NetworkSelect.Name = "NetworkSelect";
            this.NetworkSelect.Size = new System.Drawing.Size(437, 33);
            this.NetworkSelect.TabIndex = 11;
            // 
            // SecurityLabel
            // 
            this.SecurityLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SecurityLabel.AutoSize = true;
            this.SecurityLabel.Location = new System.Drawing.Point(103, 235);
            this.SecurityLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.SecurityLabel.Name = "SecurityLabel";
            this.SecurityLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SecurityLabel.Size = new System.Drawing.Size(90, 25);
            this.SecurityLabel.TabIndex = 106;
            this.SecurityLabel.Text = "Security";
            // 
            // CTLabel
            // 
            this.CTLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CTLabel.AutoSize = true;
            this.CTLabel.Location = new System.Drawing.Point(12, 452);
            this.CTLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.CTLabel.Name = "CTLabel";
            this.CTLabel.Size = new System.Drawing.Size(181, 25);
            this.CTLabel.TabIndex = 111;
            this.CTLabel.Text = "Camouflage Type";
            // 
            // PathTextBox
            // 
            this.PathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PathTextBox.Location = new System.Drawing.Point(205, 319);
            this.PathTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.PathTextBox.MaxLength = 512;
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(437, 31);
            this.PathTextBox.TabIndex = 14;
            this.PathTextBox.WordWrap = false;
            // 
            // PathLabel
            // 
            this.PathLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PathLabel.AutoSize = true;
            this.PathLabel.Location = new System.Drawing.Point(137, 322);
            this.PathLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(56, 25);
            this.PathLabel.TabIndex = 108;
            this.PathLabel.Text = "Path";
            // 
            // SecuritySelect
            // 
            this.SecuritySelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SecuritySelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SecuritySelect.FormattingEnabled = true;
            this.SecuritySelect.Location = new System.Drawing.Point(205, 231);
            this.SecuritySelect.Margin = new System.Windows.Forms.Padding(6);
            this.SecuritySelect.Name = "SecuritySelect";
            this.SecuritySelect.Size = new System.Drawing.Size(437, 33);
            this.SecuritySelect.TabIndex = 12;
            // 
            // ServerPortText
            // 
            this.ServerPortText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerPortText.Location = new System.Drawing.Point(205, 100);
            this.ServerPortText.Margin = new System.Windows.Forms.Padding(6);
            this.ServerPortText.Name = "ServerPortText";
            this.ServerPortText.Size = new System.Drawing.Size(437, 31);
            this.ServerPortText.TabIndex = 9;
            this.ServerPortText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyAllowDigit_KeyPress);
            // 
            // AlterIdText
            // 
            this.AlterIdText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.AlterIdText.Location = new System.Drawing.Point(205, 276);
            this.AlterIdText.Margin = new System.Windows.Forms.Padding(6);
            this.AlterIdText.Name = "AlterIdText";
            this.AlterIdText.Size = new System.Drawing.Size(437, 31);
            this.AlterIdText.TabIndex = 13;
            this.AlterIdText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyAllowDigit_KeyPress);
            // 
            // IDTextBox
            // 
            this.IDTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.IDTextBox.Location = new System.Drawing.Point(205, 143);
            this.IDTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(437, 31);
            this.IDTextBox.TabIndex = 10;
            // 
            // RemarksLabel
            // 
            this.RemarksLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RemarksLabel.AutoSize = true;
            this.RemarksLabel.Location = new System.Drawing.Point(107, 586);
            this.RemarksLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.RemarksLabel.Name = "RemarksLabel";
            this.RemarksLabel.Size = new System.Drawing.Size(86, 25);
            this.RemarksLabel.TabIndex = 114;
            this.RemarksLabel.Text = "Remark";
            // 
            // FlowLabel
            // 
            this.FlowLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FlowLabel.AutoSize = true;
            this.FlowLabel.Location = new System.Drawing.Point(136, 542);
            this.FlowLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.FlowLabel.Name = "FlowLabel";
            this.FlowLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FlowLabel.Size = new System.Drawing.Size(57, 25);
            this.FlowLabel.TabIndex = 113;
            this.FlowLabel.Text = "Flow";
            // 
            // FlowSelect
            // 
            this.FlowSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FlowSelect.FormattingEnabled = true;
            this.FlowSelect.Location = new System.Drawing.Point(205, 538);
            this.FlowSelect.Margin = new System.Windows.Forms.Padding(6);
            this.FlowSelect.Name = "FlowSelect";
            this.FlowSelect.Size = new System.Drawing.Size(437, 33);
            this.FlowSelect.TabIndex = 19;
            // 
            // SNILabel
            // 
            this.SNILabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SNILabel.AutoSize = true;
            this.SNILabel.Location = new System.Drawing.Point(147, 365);
            this.SNILabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.SNILabel.Name = "SNILabel";
            this.SNILabel.Size = new System.Drawing.Size(46, 25);
            this.SNILabel.TabIndex = 109;
            this.SNILabel.Text = "SNI";
            // 
            // HostLabel
            // 
            this.HostLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.HostLabel.AutoSize = true;
            this.HostLabel.Location = new System.Drawing.Point(137, 408);
            this.HostLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.HostLabel.Name = "HostLabel";
            this.HostLabel.Size = new System.Drawing.Size(56, 25);
            this.HostLabel.TabIndex = 110;
            this.HostLabel.Text = "Host";
            // 
            // TlsLabel
            // 
            this.TlsLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TlsLabel.AutoSize = true;
            this.TlsLabel.Location = new System.Drawing.Point(152, 497);
            this.TlsLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.TlsLabel.Name = "TlsLabel";
            this.TlsLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TlsLabel.Size = new System.Drawing.Size(41, 25);
            this.TlsLabel.TabIndex = 112;
            this.TlsLabel.Text = "Tls";
            // 
            // ProtocolLabel
            // 
            this.ProtocolLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ProtocolLabel.AutoSize = true;
            this.ProtocolLabel.Location = new System.Drawing.Point(102, 16);
            this.ProtocolLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ProtocolLabel.Name = "ProtocolLabel";
            this.ProtocolLabel.Size = new System.Drawing.Size(91, 25);
            this.ProtocolLabel.TabIndex = 101;
            this.ProtocolLabel.Text = "Protocol";
            // 
            // ProtocolSelect
            // 
            this.ProtocolSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ProtocolSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProtocolSelect.FormattingEnabled = true;
            this.ProtocolSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ProtocolSelect.ItemHeight = 25;
            this.ProtocolSelect.Items.AddRange(new object[] {
            "vless",
            "vmess",
            "shadowsocks",
            "trojan",
            "trojan-go"});
            this.ProtocolSelect.Location = new System.Drawing.Point(205, 12);
            this.ProtocolSelect.Margin = new System.Windows.Forms.Padding(6);
            this.ProtocolSelect.Name = "ProtocolSelect";
            this.ProtocolSelect.Size = new System.Drawing.Size(437, 33);
            this.ProtocolSelect.TabIndex = 7;
            this.ProtocolSelect.SelectedIndexChanged += new System.EventHandler(this.ProtocolSelect_SelectedIndexChanged);
            // 
            // SNITextBox
            // 
            this.SNITextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SNITextBox.Location = new System.Drawing.Point(205, 362);
            this.SNITextBox.Margin = new System.Windows.Forms.Padding(6);
            this.SNITextBox.MaxLength = 512;
            this.SNITextBox.Name = "SNITextBox";
            this.SNITextBox.Size = new System.Drawing.Size(437, 31);
            this.SNITextBox.TabIndex = 15;
            this.SNITextBox.WordWrap = false;
            // 
            // HostTextBox
            // 
            this.HostTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.HostTextBox.Location = new System.Drawing.Point(205, 405);
            this.HostTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.HostTextBox.MaxLength = 512;
            this.HostTextBox.Name = "HostTextBox";
            this.HostTextBox.Size = new System.Drawing.Size(437, 31);
            this.HostTextBox.TabIndex = 16;
            this.HostTextBox.WordWrap = false;
            // 
            // TlsSelect
            // 
            this.TlsSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TlsSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TlsSelect.FormattingEnabled = true;
            this.TlsSelect.Location = new System.Drawing.Point(205, 493);
            this.TlsSelect.Margin = new System.Windows.Forms.Padding(6);
            this.TlsSelect.Name = "TlsSelect";
            this.TlsSelect.Size = new System.Drawing.Size(437, 33);
            this.TlsSelect.TabIndex = 18;
            this.TlsSelect.SelectedIndexChanged += new System.EventHandler(this.TlsSelect_SelectedIndexChanged);
            // 
            // ServersListBox
            // 
            this.ServersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServersListBox.FormattingEnabled = true;
            this.ServersListBox.IntegralHeight = false;
            this.ServersListBox.ItemHeight = 25;
            this.ServersListBox.Location = new System.Drawing.Point(0, 0);
            this.ServersListBox.Margin = new System.Windows.Forms.Padding(0);
            this.ServersListBox.Name = "ServersListBox";
            this.ServersListBox.Size = new System.Drawing.Size(332, 690);
            this.ServersListBox.TabIndex = 0;
            this.ServersListBox.SelectedIndexChanged += new System.EventHandler(this.ServersListBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.ServersListBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ServerGroupBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1027, 882);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.MoveDownButton, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.MoveUpButton, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 818);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(332, 64);
            this.tableLayoutPanel6.TabIndex = 10;
            // 
            // MoveDownButton
            // 
            this.MoveDownButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.MoveDownButton.Location = new System.Drawing.Point(172, 12);
            this.MoveDownButton.Margin = new System.Windows.Forms.Padding(6, 12, 0, 6);
            this.MoveDownButton.Name = "MoveDownButton";
            this.MoveDownButton.Size = new System.Drawing.Size(160, 46);
            this.MoveDownButton.TabIndex = 6;
            this.MoveDownButton.Text = "Move D&own";
            this.MoveDownButton.UseVisualStyleBackColor = true;
            this.MoveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // MoveUpButton
            // 
            this.MoveUpButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.MoveUpButton.Location = new System.Drawing.Point(0, 12);
            this.MoveUpButton.Margin = new System.Windows.Forms.Padding(0, 12, 6, 6);
            this.MoveUpButton.Name = "MoveUpButton";
            this.MoveUpButton.Size = new System.Drawing.Size(160, 46);
            this.MoveUpButton.TabIndex = 5;
            this.MoveUpButton.Text = "Move &Up";
            this.MoveUpButton.UseVisualStyleBackColor = true;
            this.MoveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.ProxyPortLabel, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.LocalPortNum, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.CorePortLabel, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.CorePortNum, 1, 1);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(765, 690);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(262, 128);
            this.tableLayoutPanel5.TabIndex = 9;
            // 
            // ProxyPortLabel
            // 
            this.ProxyPortLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ProxyPortLabel.AutoSize = true;
            this.ProxyPortLabel.Location = new System.Drawing.Point(12, 29);
            this.ProxyPortLabel.Margin = new System.Windows.Forms.Padding(6, 20, 6, 20);
            this.ProxyPortLabel.Name = "ProxyPortLabel";
            this.ProxyPortLabel.Size = new System.Drawing.Size(112, 25);
            this.ProxyPortLabel.TabIndex = 115;
            this.ProxyPortLabel.Text = "Proxy Port";
            // 
            // LocalPortNum
            // 
            this.LocalPortNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LocalPortNum.Location = new System.Drawing.Point(133, 26);
            this.LocalPortNum.Margin = new System.Windows.Forms.Padding(3, 20, 3, 20);
            this.LocalPortNum.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.LocalPortNum.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.LocalPortNum.Name = "LocalPortNum";
            this.LocalPortNum.Size = new System.Drawing.Size(120, 31);
            this.LocalPortNum.TabIndex = 21;
            this.LocalPortNum.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // CorePortLabel
            // 
            this.CorePortLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CorePortLabel.AutoSize = true;
            this.CorePortLabel.Location = new System.Drawing.Point(24, 87);
            this.CorePortLabel.Name = "CorePortLabel";
            this.CorePortLabel.Size = new System.Drawing.Size(103, 25);
            this.CorePortLabel.TabIndex = 116;
            this.CorePortLabel.Text = "Core Port";
            // 
            // CorePortNum
            // 
            this.CorePortNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CorePortNum.Location = new System.Drawing.Point(133, 84);
            this.CorePortNum.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CorePortNum.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.CorePortNum.Name = "CorePortNum";
            this.CorePortNum.Size = new System.Drawing.Size(120, 31);
            this.CorePortNum.TabIndex = 22;
            this.CorePortNum.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.Controls.Add(this.MyCancelButton, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.OKButton, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(709, 824);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(6, 6, 0, 6);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(318, 52);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.DuplicateButton, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.DeleteButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.AddButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.ClipboardButton, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 690);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(332, 128);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // DuplicateButton
            // 
            this.DuplicateButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.DuplicateButton.Location = new System.Drawing.Point(0, 76);
            this.DuplicateButton.Margin = new System.Windows.Forms.Padding(0, 12, 6, 6);
            this.DuplicateButton.Name = "DuplicateButton";
            this.DuplicateButton.Size = new System.Drawing.Size(160, 46);
            this.DuplicateButton.TabIndex = 3;
            this.DuplicateButton.Text = "Dupli&cate";
            this.DuplicateButton.UseVisualStyleBackColor = true;
            this.DuplicateButton.Click += new System.EventHandler(this.DuplicateButton_Click);
            // 
            // ClipboardButton
            // 
            this.ClipboardButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ClipboardButton.Location = new System.Drawing.Point(172, 76);
            this.ClipboardButton.Margin = new System.Windows.Forms.Padding(6, 12, 0, 6);
            this.ClipboardButton.Name = "ClipboardButton";
            this.ClipboardButton.Size = new System.Drawing.Size(160, 46);
            this.ClipboardButton.TabIndex = 4;
            this.ClipboardButton.Text = "Clipboard";
            this.ClipboardButton.UseVisualStyleBackColor = true;
            this.ClipboardButton.Click += new System.EventHandler(this.ClipboardButton_Click);
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(1167, 1065);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.Padding = new System.Windows.Forms.Padding(24, 24, 24, 18);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Servers";
            this.ServerGroupBox.ResumeLayout(false);
            this.ServerGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocalPortNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CorePortNum)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.GroupBox ServerGroupBox;
        private System.Windows.Forms.ListBox ServersListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label ProxyPortLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button MoveDownButton;
        private System.Windows.Forms.Button MoveUpButton;
        private System.Windows.Forms.Button DuplicateButton;
        private System.Windows.Forms.Button ClipboardButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label AlterIdLabel;
        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.Label ServerPortLabel;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.TextBox AddressTextBox;
        private System.Windows.Forms.Label NetworkLabel;
        private System.Windows.Forms.ComboBox NetworkSelect;
        private System.Windows.Forms.Label CTLabel;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.ComboBox CTSelect;
        private System.Windows.Forms.NumericUpDown LocalPortNum;
        private System.Windows.Forms.Label CorePortLabel;
        private System.Windows.Forms.NumericUpDown CorePortNum;
        private System.Windows.Forms.TextBox ServerPortText;
        private System.Windows.Forms.TextBox AlterIdText;
        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.Label SecurityLabel;
        private System.Windows.Forms.ComboBox SecuritySelect;
        private System.Windows.Forms.Label FlowLabel;
        private System.Windows.Forms.ComboBox FlowSelect;
        private System.Windows.Forms.Label SNILabel;
        private System.Windows.Forms.Label HostLabel;
        private System.Windows.Forms.Label TlsLabel;
        private System.Windows.Forms.Label ProtocolLabel;
        private System.Windows.Forms.ComboBox ProtocolSelect;
        private System.Windows.Forms.TextBox SNITextBox;
        private System.Windows.Forms.TextBox HostTextBox;
        private System.Windows.Forms.ComboBox TlsSelect;
        private System.Windows.Forms.TextBox RemarksTextBox;
        private System.Windows.Forms.Label RemarksLabel;
    }
}