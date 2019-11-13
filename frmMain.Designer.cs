namespace acer_rgb
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tmrToastDetector = new System.Windows.Forms.Timer(this.components);
            this.picMain = new System.Windows.Forms.PictureBox();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.lblWarning = new System.Windows.Forms.Label();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.btnStatic_Alarm = new System.Windows.Forms.Button();
            this.chkActivateAlarm = new System.Windows.Forms.CheckBox();
            this.cboStatic_Alarm = new System.Windows.Forms.ComboBox();
            this.chkAutoOff = new System.Windows.Forms.CheckBox();
            this.lblLighting = new System.Windows.Forms.Label();
            this.lstEffect = new System.Windows.Forms.ListBox();
            this.tabEffect = new System.Windows.Forms.TabControl();
            this.tpgStatic = new System.Windows.Forms.TabPage();
            this.btnStatic_Color1 = new System.Windows.Forms.Button();
            this.cboStatic_Color1 = new System.Windows.Forms.ComboBox();
            this.lblStatic_Color1 = new System.Windows.Forms.Label();
            this.tpgBreathing = new System.Windows.Forms.TabPage();
            this.lblBreathing_Speed = new System.Windows.Forms.Label();
            this.trkBreathing_Speed = new System.Windows.Forms.TrackBar();
            this.btnBreathing_Color2 = new System.Windows.Forms.Button();
            this.cboBreathing_Color2 = new System.Windows.Forms.ComboBox();
            this.lblBreathing_Color2 = new System.Windows.Forms.Label();
            this.btnBreathing_Color1 = new System.Windows.Forms.Button();
            this.cboBreathing_Color1 = new System.Windows.Forms.ComboBox();
            this.lblBreathing_Color1 = new System.Windows.Forms.Label();
            this.tpgFlasing = new System.Windows.Forms.TabPage();
            this.lblFlashing_Speed = new System.Windows.Forms.Label();
            this.trkFlashing_Speed = new System.Windows.Forms.TrackBar();
            this.btnFlashing_Color2 = new System.Windows.Forms.Button();
            this.cboFlashing_Color2 = new System.Windows.Forms.ComboBox();
            this.lblFlashing_Color2 = new System.Windows.Forms.Label();
            this.btnFlashing_Color1 = new System.Windows.Forms.Button();
            this.cboFlashing_Color1 = new System.Windows.Forms.ComboBox();
            this.lblFlashing_Color1 = new System.Windows.Forms.Label();
            this.tpgPhasing = new System.Windows.Forms.TabPage();
            this.lblPhasing_Speed = new System.Windows.Forms.Label();
            this.trkPhasing_Speed = new System.Windows.Forms.TrackBar();
            this.btnPhasing_Color2 = new System.Windows.Forms.Button();
            this.cboPhasing_Color2 = new System.Windows.Forms.ComboBox();
            this.lblPhasing_Color2 = new System.Windows.Forms.Label();
            this.btnPhasing_Color1 = new System.Windows.Forms.Button();
            this.cboPhasing_Color1 = new System.Windows.Forms.ComboBox();
            this.lblPhasing_Color1 = new System.Windows.Forms.Label();
            this.tpgSpectrumBreathing = new System.Windows.Forms.TabPage();
            this.lblSpectrumBreathing_NoColor = new System.Windows.Forms.Label();
            this.lblSpectrumBreathing_Speed = new System.Windows.Forms.Label();
            this.trkSpectrumBreathing_Speed = new System.Windows.Forms.TrackBar();
            this.tpgSpectrumFlashing = new System.Windows.Forms.TabPage();
            this.lblSpectrumFlashing_NoColor = new System.Windows.Forms.Label();
            this.lblSpectrumFlashing_Speed = new System.Windows.Forms.Label();
            this.trkSpectrumFlashing_Speed = new System.Windows.Forms.TrackBar();
            this.picLine = new System.Windows.Forms.PictureBox();
            this.nicMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.mnuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.grpMain.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.tabEffect.SuspendLayout();
            this.tpgStatic.SuspendLayout();
            this.tpgBreathing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkBreathing_Speed)).BeginInit();
            this.tpgFlasing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkFlashing_Speed)).BeginInit();
            this.tpgPhasing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkPhasing_Speed)).BeginInit();
            this.tpgSpectrumBreathing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpectrumBreathing_Speed)).BeginInit();
            this.tpgSpectrumFlashing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpectrumFlashing_Speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLine)).BeginInit();
            this.mnuTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrToastDetector
            // 
            this.tmrToastDetector.Tick += new System.EventHandler(this.tmrToastDetector_Tick);
            // 
            // picMain
            // 
            this.picMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picMain.Image = ((System.Drawing.Image)(resources.GetObject("picMain.Image")));
            this.picMain.Location = new System.Drawing.Point(0, 0);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(663, 253);
            this.picMain.TabIndex = 2;
            this.picMain.TabStop = false;
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.lblWarning);
            this.grpMain.Controls.Add(this.chkEnable);
            this.grpMain.Controls.Add(this.grpOptions);
            this.grpMain.Controls.Add(this.lblLighting);
            this.grpMain.Controls.Add(this.lstEffect);
            this.grpMain.Controls.Add(this.tabEffect);
            this.grpMain.Location = new System.Drawing.Point(12, 266);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(636, 261);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Acer Predator X35 RGB Control";
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(287, 0);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblWarning.Size = new System.Drawing.Size(297, 13);
            this.lblWarning.TabIndex = 5;
            this.lblWarning.Text = "No X35 device found; make sure the USB cable is plugged in";
            this.lblWarning.Visible = false;
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Enabled = false;
            this.chkEnable.Location = new System.Drawing.Point(169, 0);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.chkEnable.Size = new System.Drawing.Size(72, 17);
            this.chkEnable.TabIndex = 0;
            this.chkEnable.Text = "&Enabled";
            this.chkEnable.UseVisualStyleBackColor = true;
            this.chkEnable.CheckedChanged += new System.EventHandler(this.chkEnable_CheckedChanged);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.btnStatic_Alarm);
            this.grpOptions.Controls.Add(this.chkActivateAlarm);
            this.grpOptions.Controls.Add(this.cboStatic_Alarm);
            this.grpOptions.Controls.Add(this.chkAutoOff);
            this.grpOptions.Enabled = false;
            this.grpOptions.Location = new System.Drawing.Point(17, 171);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(599, 74);
            this.grpOptions.TabIndex = 4;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Lighting Options";
            // 
            // btnStatic_Alarm
            // 
            this.btnStatic_Alarm.Location = new System.Drawing.Point(505, 40);
            this.btnStatic_Alarm.Name = "btnStatic_Alarm";
            this.btnStatic_Alarm.Size = new System.Drawing.Size(30, 23);
            this.btnStatic_Alarm.TabIndex = 3;
            this.btnStatic_Alarm.Text = "...";
            this.btnStatic_Alarm.UseVisualStyleBackColor = true;
            this.btnStatic_Alarm.Click += new System.EventHandler(this.btnStatic_Alarm_Click);
            // 
            // chkActivateAlarm
            // 
            this.chkActivateAlarm.AutoSize = true;
            this.chkActivateAlarm.Checked = true;
            this.chkActivateAlarm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivateAlarm.Location = new System.Drawing.Point(18, 44);
            this.chkActivateAlarm.Name = "chkActivateAlarm";
            this.chkActivateAlarm.Size = new System.Drawing.Size(358, 17);
            this.chkActivateAlarm.TabIndex = 1;
            this.chkActivateAlarm.Text = "A&ctivate the Alarm Color when receiving a toast while monitor sleeps:";
            this.chkActivateAlarm.UseVisualStyleBackColor = true;
            this.chkActivateAlarm.CheckedChanged += new System.EventHandler(this.chkActivateAlarm_CheckedChanged);
            // 
            // cboStatic_Alarm
            // 
            this.cboStatic_Alarm.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboStatic_Alarm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatic_Alarm.FormattingEnabled = true;
            this.cboStatic_Alarm.Location = new System.Drawing.Point(378, 40);
            this.cboStatic_Alarm.Name = "cboStatic_Alarm";
            this.cboStatic_Alarm.Size = new System.Drawing.Size(121, 22);
            this.cboStatic_Alarm.TabIndex = 2;
            this.cboStatic_Alarm.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboStatic_Alarm_DrawItem);
            this.cboStatic_Alarm.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cboStatic_Alarm_MeasureItem);
            this.cboStatic_Alarm.SelectedIndexChanged += new System.EventHandler(this.cboStatic_Alarm_SelectedIndexChanged);
            // 
            // chkAutoOff
            // 
            this.chkAutoOff.AutoSize = true;
            this.chkAutoOff.Checked = true;
            this.chkAutoOff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoOff.Location = new System.Drawing.Point(18, 21);
            this.chkAutoOff.Name = "chkAutoOff";
            this.chkAutoOff.Size = new System.Drawing.Size(287, 17);
            this.chkAutoOff.TabIndex = 0;
            this.chkAutoOff.Text = "&Automatically turn off the lighting when monitor sleeps";
            this.chkAutoOff.UseVisualStyleBackColor = true;
            // 
            // lblLighting
            // 
            this.lblLighting.AutoSize = true;
            this.lblLighting.Location = new System.Drawing.Point(14, 23);
            this.lblLighting.Name = "lblLighting";
            this.lblLighting.Size = new System.Drawing.Size(80, 13);
            this.lblLighting.TabIndex = 1;
            this.lblLighting.Text = "&Lighting Effect:";
            // 
            // lstEffect
            // 
            this.lstEffect.Enabled = false;
            this.lstEffect.FormattingEnabled = true;
            this.lstEffect.Items.AddRange(new object[] {
            "Static",
            "Breathing",
            "Flashing",
            "Phasing",
            "Spectrum (Breathing)",
            "Spectrum (Flashing)"});
            this.lstEffect.Location = new System.Drawing.Point(17, 40);
            this.lstEffect.Name = "lstEffect";
            this.lstEffect.Size = new System.Drawing.Size(143, 121);
            this.lstEffect.TabIndex = 2;
            this.lstEffect.SelectedIndexChanged += new System.EventHandler(this.lstEffect_SelectedIndexChanged);
            // 
            // tabEffect
            // 
            this.tabEffect.Controls.Add(this.tpgStatic);
            this.tabEffect.Controls.Add(this.tpgBreathing);
            this.tabEffect.Controls.Add(this.tpgFlasing);
            this.tabEffect.Controls.Add(this.tpgPhasing);
            this.tabEffect.Controls.Add(this.tpgSpectrumBreathing);
            this.tabEffect.Controls.Add(this.tpgSpectrumFlashing);
            this.tabEffect.Enabled = false;
            this.tabEffect.Location = new System.Drawing.Point(175, 23);
            this.tabEffect.Name = "tabEffect";
            this.tabEffect.SelectedIndex = 0;
            this.tabEffect.Size = new System.Drawing.Size(445, 142);
            this.tabEffect.TabIndex = 3;
            this.tabEffect.SelectedIndexChanged += new System.EventHandler(this.tabEffect_SelectedIndexChanged);
            // 
            // tpgStatic
            // 
            this.tpgStatic.Controls.Add(this.btnStatic_Color1);
            this.tpgStatic.Controls.Add(this.cboStatic_Color1);
            this.tpgStatic.Controls.Add(this.lblStatic_Color1);
            this.tpgStatic.Location = new System.Drawing.Point(4, 22);
            this.tpgStatic.Name = "tpgStatic";
            this.tpgStatic.Padding = new System.Windows.Forms.Padding(3);
            this.tpgStatic.Size = new System.Drawing.Size(437, 116);
            this.tpgStatic.TabIndex = 0;
            this.tpgStatic.Text = "Static";
            this.tpgStatic.UseVisualStyleBackColor = true;
            // 
            // btnStatic_Color1
            // 
            this.btnStatic_Color1.Location = new System.Drawing.Point(194, 15);
            this.btnStatic_Color1.Name = "btnStatic_Color1";
            this.btnStatic_Color1.Size = new System.Drawing.Size(30, 23);
            this.btnStatic_Color1.TabIndex = 2;
            this.btnStatic_Color1.Text = "...";
            this.btnStatic_Color1.UseVisualStyleBackColor = true;
            this.btnStatic_Color1.Click += new System.EventHandler(this.btnStatic_Color1_Click);
            // 
            // cboStatic_Color1
            // 
            this.cboStatic_Color1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboStatic_Color1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatic_Color1.FormattingEnabled = true;
            this.cboStatic_Color1.Location = new System.Drawing.Point(67, 15);
            this.cboStatic_Color1.Name = "cboStatic_Color1";
            this.cboStatic_Color1.Size = new System.Drawing.Size(121, 22);
            this.cboStatic_Color1.TabIndex = 1;
            this.cboStatic_Color1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboStatic_Color1_DrawItem);
            this.cboStatic_Color1.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cboStatic_Color1_MeasureItem);
            this.cboStatic_Color1.SelectedIndexChanged += new System.EventHandler(this.cboStatic_Color1_SelectedIndexChanged);
            // 
            // lblStatic_Color1
            // 
            this.lblStatic_Color1.AutoSize = true;
            this.lblStatic_Color1.Location = new System.Drawing.Point(16, 20);
            this.lblStatic_Color1.Name = "lblStatic_Color1";
            this.lblStatic_Color1.Size = new System.Drawing.Size(36, 13);
            this.lblStatic_Color1.TabIndex = 0;
            this.lblStatic_Color1.Text = "Color:";
            // 
            // tpgBreathing
            // 
            this.tpgBreathing.Controls.Add(this.lblBreathing_Speed);
            this.tpgBreathing.Controls.Add(this.trkBreathing_Speed);
            this.tpgBreathing.Controls.Add(this.btnBreathing_Color2);
            this.tpgBreathing.Controls.Add(this.cboBreathing_Color2);
            this.tpgBreathing.Controls.Add(this.lblBreathing_Color2);
            this.tpgBreathing.Controls.Add(this.btnBreathing_Color1);
            this.tpgBreathing.Controls.Add(this.cboBreathing_Color1);
            this.tpgBreathing.Controls.Add(this.lblBreathing_Color1);
            this.tpgBreathing.Location = new System.Drawing.Point(4, 22);
            this.tpgBreathing.Name = "tpgBreathing";
            this.tpgBreathing.Padding = new System.Windows.Forms.Padding(3);
            this.tpgBreathing.Size = new System.Drawing.Size(437, 116);
            this.tpgBreathing.TabIndex = 1;
            this.tpgBreathing.Text = "Breathing";
            this.tpgBreathing.UseVisualStyleBackColor = true;
            // 
            // lblBreathing_Speed
            // 
            this.lblBreathing_Speed.AutoSize = true;
            this.lblBreathing_Speed.Location = new System.Drawing.Point(253, 19);
            this.lblBreathing_Speed.Name = "lblBreathing_Speed";
            this.lblBreathing_Speed.Size = new System.Drawing.Size(41, 13);
            this.lblBreathing_Speed.TabIndex = 6;
            this.lblBreathing_Speed.Text = "Speed:";
            // 
            // trkBreathing_Speed
            // 
            this.trkBreathing_Speed.LargeChange = 2;
            this.trkBreathing_Speed.Location = new System.Drawing.Point(256, 42);
            this.trkBreathing_Speed.Name = "trkBreathing_Speed";
            this.trkBreathing_Speed.Size = new System.Drawing.Size(153, 45);
            this.trkBreathing_Speed.TabIndex = 7;
            this.trkBreathing_Speed.Value = 5;
            this.trkBreathing_Speed.Scroll += new System.EventHandler(this.trkBreathing_Speed_Scroll);
            // 
            // btnBreathing_Color2
            // 
            this.btnBreathing_Color2.Location = new System.Drawing.Point(194, 43);
            this.btnBreathing_Color2.Name = "btnBreathing_Color2";
            this.btnBreathing_Color2.Size = new System.Drawing.Size(30, 23);
            this.btnBreathing_Color2.TabIndex = 5;
            this.btnBreathing_Color2.Text = "...";
            this.btnBreathing_Color2.UseVisualStyleBackColor = true;
            this.btnBreathing_Color2.Click += new System.EventHandler(this.btnBreathing_Color2_Click);
            // 
            // cboBreathing_Color2
            // 
            this.cboBreathing_Color2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboBreathing_Color2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBreathing_Color2.FormattingEnabled = true;
            this.cboBreathing_Color2.Location = new System.Drawing.Point(67, 43);
            this.cboBreathing_Color2.Name = "cboBreathing_Color2";
            this.cboBreathing_Color2.Size = new System.Drawing.Size(121, 22);
            this.cboBreathing_Color2.TabIndex = 4;
            this.cboBreathing_Color2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboBreathing_Color2_DrawItem);
            this.cboBreathing_Color2.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cboBreathing_Color2_MeasureItem);
            this.cboBreathing_Color2.SelectedIndexChanged += new System.EventHandler(this.cboBreathing_Color2_SelectedIndexChanged);
            // 
            // lblBreathing_Color2
            // 
            this.lblBreathing_Color2.AutoSize = true;
            this.lblBreathing_Color2.Location = new System.Drawing.Point(16, 48);
            this.lblBreathing_Color2.Name = "lblBreathing_Color2";
            this.lblBreathing_Color2.Size = new System.Drawing.Size(45, 13);
            this.lblBreathing_Color2.TabIndex = 3;
            this.lblBreathing_Color2.Text = "Color 2:";
            // 
            // btnBreathing_Color1
            // 
            this.btnBreathing_Color1.Location = new System.Drawing.Point(194, 15);
            this.btnBreathing_Color1.Name = "btnBreathing_Color1";
            this.btnBreathing_Color1.Size = new System.Drawing.Size(30, 23);
            this.btnBreathing_Color1.TabIndex = 2;
            this.btnBreathing_Color1.Text = "...";
            this.btnBreathing_Color1.UseVisualStyleBackColor = true;
            this.btnBreathing_Color1.Click += new System.EventHandler(this.btnBreathing_Color1_Click);
            // 
            // cboBreathing_Color1
            // 
            this.cboBreathing_Color1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboBreathing_Color1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBreathing_Color1.FormattingEnabled = true;
            this.cboBreathing_Color1.Location = new System.Drawing.Point(67, 15);
            this.cboBreathing_Color1.Name = "cboBreathing_Color1";
            this.cboBreathing_Color1.Size = new System.Drawing.Size(121, 22);
            this.cboBreathing_Color1.TabIndex = 1;
            this.cboBreathing_Color1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboBreathing_Color1_DrawItem);
            this.cboBreathing_Color1.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cboBreathing_Color1_MeasureItem);
            this.cboBreathing_Color1.SelectedIndexChanged += new System.EventHandler(this.cboBreathing_Color1_SelectedIndexChanged);
            // 
            // lblBreathing_Color1
            // 
            this.lblBreathing_Color1.AutoSize = true;
            this.lblBreathing_Color1.Location = new System.Drawing.Point(16, 20);
            this.lblBreathing_Color1.Name = "lblBreathing_Color1";
            this.lblBreathing_Color1.Size = new System.Drawing.Size(45, 13);
            this.lblBreathing_Color1.TabIndex = 0;
            this.lblBreathing_Color1.Text = "Color 1:";
            // 
            // tpgFlasing
            // 
            this.tpgFlasing.Controls.Add(this.lblFlashing_Speed);
            this.tpgFlasing.Controls.Add(this.trkFlashing_Speed);
            this.tpgFlasing.Controls.Add(this.btnFlashing_Color2);
            this.tpgFlasing.Controls.Add(this.cboFlashing_Color2);
            this.tpgFlasing.Controls.Add(this.lblFlashing_Color2);
            this.tpgFlasing.Controls.Add(this.btnFlashing_Color1);
            this.tpgFlasing.Controls.Add(this.cboFlashing_Color1);
            this.tpgFlasing.Controls.Add(this.lblFlashing_Color1);
            this.tpgFlasing.Location = new System.Drawing.Point(4, 22);
            this.tpgFlasing.Name = "tpgFlasing";
            this.tpgFlasing.Size = new System.Drawing.Size(437, 116);
            this.tpgFlasing.TabIndex = 2;
            this.tpgFlasing.Text = "Flashing";
            this.tpgFlasing.UseVisualStyleBackColor = true;
            // 
            // lblFlashing_Speed
            // 
            this.lblFlashing_Speed.AutoSize = true;
            this.lblFlashing_Speed.Location = new System.Drawing.Point(253, 19);
            this.lblFlashing_Speed.Name = "lblFlashing_Speed";
            this.lblFlashing_Speed.Size = new System.Drawing.Size(41, 13);
            this.lblFlashing_Speed.TabIndex = 6;
            this.lblFlashing_Speed.Text = "Speed:";
            // 
            // trkFlashing_Speed
            // 
            this.trkFlashing_Speed.LargeChange = 2;
            this.trkFlashing_Speed.Location = new System.Drawing.Point(256, 42);
            this.trkFlashing_Speed.Name = "trkFlashing_Speed";
            this.trkFlashing_Speed.Size = new System.Drawing.Size(153, 45);
            this.trkFlashing_Speed.TabIndex = 7;
            this.trkFlashing_Speed.Value = 5;
            this.trkFlashing_Speed.Scroll += new System.EventHandler(this.trkFlashing_Speed_Scroll);
            // 
            // btnFlashing_Color2
            // 
            this.btnFlashing_Color2.Location = new System.Drawing.Point(194, 43);
            this.btnFlashing_Color2.Name = "btnFlashing_Color2";
            this.btnFlashing_Color2.Size = new System.Drawing.Size(30, 23);
            this.btnFlashing_Color2.TabIndex = 5;
            this.btnFlashing_Color2.Text = "...";
            this.btnFlashing_Color2.UseVisualStyleBackColor = true;
            this.btnFlashing_Color2.Click += new System.EventHandler(this.btnFlashing_Color2_Click);
            // 
            // cboFlashing_Color2
            // 
            this.cboFlashing_Color2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboFlashing_Color2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFlashing_Color2.FormattingEnabled = true;
            this.cboFlashing_Color2.Location = new System.Drawing.Point(67, 43);
            this.cboFlashing_Color2.Name = "cboFlashing_Color2";
            this.cboFlashing_Color2.Size = new System.Drawing.Size(121, 22);
            this.cboFlashing_Color2.TabIndex = 4;
            this.cboFlashing_Color2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboFlashing_Color2_DrawItem);
            this.cboFlashing_Color2.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cboFlashing_Color2_MeasureItem);
            this.cboFlashing_Color2.SelectedIndexChanged += new System.EventHandler(this.cboFlashing_Color2_SelectedIndexChanged);
            // 
            // lblFlashing_Color2
            // 
            this.lblFlashing_Color2.AutoSize = true;
            this.lblFlashing_Color2.Location = new System.Drawing.Point(16, 48);
            this.lblFlashing_Color2.Name = "lblFlashing_Color2";
            this.lblFlashing_Color2.Size = new System.Drawing.Size(45, 13);
            this.lblFlashing_Color2.TabIndex = 3;
            this.lblFlashing_Color2.Text = "Color 2:";
            // 
            // btnFlashing_Color1
            // 
            this.btnFlashing_Color1.Location = new System.Drawing.Point(194, 15);
            this.btnFlashing_Color1.Name = "btnFlashing_Color1";
            this.btnFlashing_Color1.Size = new System.Drawing.Size(30, 23);
            this.btnFlashing_Color1.TabIndex = 2;
            this.btnFlashing_Color1.Text = "...";
            this.btnFlashing_Color1.UseVisualStyleBackColor = true;
            this.btnFlashing_Color1.Click += new System.EventHandler(this.btnFlashing_Color1_Click);
            // 
            // cboFlashing_Color1
            // 
            this.cboFlashing_Color1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboFlashing_Color1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFlashing_Color1.FormattingEnabled = true;
            this.cboFlashing_Color1.Location = new System.Drawing.Point(67, 15);
            this.cboFlashing_Color1.Name = "cboFlashing_Color1";
            this.cboFlashing_Color1.Size = new System.Drawing.Size(121, 22);
            this.cboFlashing_Color1.TabIndex = 1;
            this.cboFlashing_Color1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboFlashing_Color1_DrawItem);
            this.cboFlashing_Color1.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cboFlashing_Color1_MeasureItem);
            this.cboFlashing_Color1.SelectedIndexChanged += new System.EventHandler(this.cboFlashing_Color1_SelectedIndexChanged);
            // 
            // lblFlashing_Color1
            // 
            this.lblFlashing_Color1.AutoSize = true;
            this.lblFlashing_Color1.Location = new System.Drawing.Point(16, 20);
            this.lblFlashing_Color1.Name = "lblFlashing_Color1";
            this.lblFlashing_Color1.Size = new System.Drawing.Size(45, 13);
            this.lblFlashing_Color1.TabIndex = 0;
            this.lblFlashing_Color1.Text = "Color 1:";
            // 
            // tpgPhasing
            // 
            this.tpgPhasing.Controls.Add(this.lblPhasing_Speed);
            this.tpgPhasing.Controls.Add(this.trkPhasing_Speed);
            this.tpgPhasing.Controls.Add(this.btnPhasing_Color2);
            this.tpgPhasing.Controls.Add(this.cboPhasing_Color2);
            this.tpgPhasing.Controls.Add(this.lblPhasing_Color2);
            this.tpgPhasing.Controls.Add(this.btnPhasing_Color1);
            this.tpgPhasing.Controls.Add(this.cboPhasing_Color1);
            this.tpgPhasing.Controls.Add(this.lblPhasing_Color1);
            this.tpgPhasing.Location = new System.Drawing.Point(4, 22);
            this.tpgPhasing.Name = "tpgPhasing";
            this.tpgPhasing.Size = new System.Drawing.Size(437, 116);
            this.tpgPhasing.TabIndex = 3;
            this.tpgPhasing.Text = "Phasing";
            this.tpgPhasing.UseVisualStyleBackColor = true;
            // 
            // lblPhasing_Speed
            // 
            this.lblPhasing_Speed.AutoSize = true;
            this.lblPhasing_Speed.Location = new System.Drawing.Point(253, 19);
            this.lblPhasing_Speed.Name = "lblPhasing_Speed";
            this.lblPhasing_Speed.Size = new System.Drawing.Size(41, 13);
            this.lblPhasing_Speed.TabIndex = 6;
            this.lblPhasing_Speed.Text = "Speed:";
            // 
            // trkPhasing_Speed
            // 
            this.trkPhasing_Speed.LargeChange = 2;
            this.trkPhasing_Speed.Location = new System.Drawing.Point(256, 42);
            this.trkPhasing_Speed.Name = "trkPhasing_Speed";
            this.trkPhasing_Speed.Size = new System.Drawing.Size(153, 45);
            this.trkPhasing_Speed.TabIndex = 7;
            this.trkPhasing_Speed.Value = 5;
            this.trkPhasing_Speed.Scroll += new System.EventHandler(this.trkPhasing_Speed_Scroll);
            // 
            // btnPhasing_Color2
            // 
            this.btnPhasing_Color2.Location = new System.Drawing.Point(194, 43);
            this.btnPhasing_Color2.Name = "btnPhasing_Color2";
            this.btnPhasing_Color2.Size = new System.Drawing.Size(30, 23);
            this.btnPhasing_Color2.TabIndex = 5;
            this.btnPhasing_Color2.Text = "...";
            this.btnPhasing_Color2.UseVisualStyleBackColor = true;
            this.btnPhasing_Color2.Click += new System.EventHandler(this.btnPhasing_Color2_Click);
            // 
            // cboPhasing_Color2
            // 
            this.cboPhasing_Color2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboPhasing_Color2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhasing_Color2.FormattingEnabled = true;
            this.cboPhasing_Color2.Location = new System.Drawing.Point(67, 43);
            this.cboPhasing_Color2.Name = "cboPhasing_Color2";
            this.cboPhasing_Color2.Size = new System.Drawing.Size(121, 22);
            this.cboPhasing_Color2.TabIndex = 4;
            this.cboPhasing_Color2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboPhasing_Color2_DrawItem);
            this.cboPhasing_Color2.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cboPhasing_Color2_MeasureItem);
            this.cboPhasing_Color2.SelectedIndexChanged += new System.EventHandler(this.cboPhasing_Color2_SelectedIndexChanged);
            // 
            // lblPhasing_Color2
            // 
            this.lblPhasing_Color2.AutoSize = true;
            this.lblPhasing_Color2.Location = new System.Drawing.Point(16, 48);
            this.lblPhasing_Color2.Name = "lblPhasing_Color2";
            this.lblPhasing_Color2.Size = new System.Drawing.Size(45, 13);
            this.lblPhasing_Color2.TabIndex = 3;
            this.lblPhasing_Color2.Text = "Color 2:";
            // 
            // btnPhasing_Color1
            // 
            this.btnPhasing_Color1.Location = new System.Drawing.Point(194, 15);
            this.btnPhasing_Color1.Name = "btnPhasing_Color1";
            this.btnPhasing_Color1.Size = new System.Drawing.Size(30, 23);
            this.btnPhasing_Color1.TabIndex = 2;
            this.btnPhasing_Color1.Text = "...";
            this.btnPhasing_Color1.UseVisualStyleBackColor = true;
            this.btnPhasing_Color1.Click += new System.EventHandler(this.btnPhasing_Color1_Click);
            // 
            // cboPhasing_Color1
            // 
            this.cboPhasing_Color1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboPhasing_Color1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhasing_Color1.FormattingEnabled = true;
            this.cboPhasing_Color1.Location = new System.Drawing.Point(67, 15);
            this.cboPhasing_Color1.Name = "cboPhasing_Color1";
            this.cboPhasing_Color1.Size = new System.Drawing.Size(121, 22);
            this.cboPhasing_Color1.TabIndex = 1;
            this.cboPhasing_Color1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboPhasing_Color1_DrawItem);
            this.cboPhasing_Color1.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cboPhasing_Color1_MeasureItem);
            this.cboPhasing_Color1.SelectedIndexChanged += new System.EventHandler(this.cboPhasing_Color1_SelectedIndexChanged);
            // 
            // lblPhasing_Color1
            // 
            this.lblPhasing_Color1.AutoSize = true;
            this.lblPhasing_Color1.Location = new System.Drawing.Point(16, 20);
            this.lblPhasing_Color1.Name = "lblPhasing_Color1";
            this.lblPhasing_Color1.Size = new System.Drawing.Size(45, 13);
            this.lblPhasing_Color1.TabIndex = 0;
            this.lblPhasing_Color1.Text = "Color 1:";
            // 
            // tpgSpectrumBreathing
            // 
            this.tpgSpectrumBreathing.Controls.Add(this.lblSpectrumBreathing_NoColor);
            this.tpgSpectrumBreathing.Controls.Add(this.lblSpectrumBreathing_Speed);
            this.tpgSpectrumBreathing.Controls.Add(this.trkSpectrumBreathing_Speed);
            this.tpgSpectrumBreathing.Location = new System.Drawing.Point(4, 22);
            this.tpgSpectrumBreathing.Name = "tpgSpectrumBreathing";
            this.tpgSpectrumBreathing.Size = new System.Drawing.Size(437, 116);
            this.tpgSpectrumBreathing.TabIndex = 4;
            this.tpgSpectrumBreathing.Text = "Spectrum (Breathing)";
            this.tpgSpectrumBreathing.UseVisualStyleBackColor = true;
            // 
            // lblSpectrumBreathing_NoColor
            // 
            this.lblSpectrumBreathing_NoColor.AutoSize = true;
            this.lblSpectrumBreathing_NoColor.Enabled = false;
            this.lblSpectrumBreathing_NoColor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpectrumBreathing_NoColor.Location = new System.Drawing.Point(19, 21);
            this.lblSpectrumBreathing_NoColor.Name = "lblSpectrumBreathing_NoColor";
            this.lblSpectrumBreathing_NoColor.Size = new System.Drawing.Size(129, 13);
            this.lblSpectrumBreathing_NoColor.TabIndex = 0;
            this.lblSpectrumBreathing_NoColor.Text = "No color options available";
            // 
            // lblSpectrumBreathing_Speed
            // 
            this.lblSpectrumBreathing_Speed.AutoSize = true;
            this.lblSpectrumBreathing_Speed.Location = new System.Drawing.Point(253, 19);
            this.lblSpectrumBreathing_Speed.Name = "lblSpectrumBreathing_Speed";
            this.lblSpectrumBreathing_Speed.Size = new System.Drawing.Size(41, 13);
            this.lblSpectrumBreathing_Speed.TabIndex = 1;
            this.lblSpectrumBreathing_Speed.Text = "Speed:";
            // 
            // trkSpectrumBreathing_Speed
            // 
            this.trkSpectrumBreathing_Speed.LargeChange = 2;
            this.trkSpectrumBreathing_Speed.Location = new System.Drawing.Point(256, 42);
            this.trkSpectrumBreathing_Speed.Name = "trkSpectrumBreathing_Speed";
            this.trkSpectrumBreathing_Speed.Size = new System.Drawing.Size(153, 45);
            this.trkSpectrumBreathing_Speed.TabIndex = 2;
            this.trkSpectrumBreathing_Speed.Value = 5;
            this.trkSpectrumBreathing_Speed.Scroll += new System.EventHandler(this.trkSpectrumBreathing_Speed_Scroll);
            // 
            // tpgSpectrumFlashing
            // 
            this.tpgSpectrumFlashing.Controls.Add(this.lblSpectrumFlashing_NoColor);
            this.tpgSpectrumFlashing.Controls.Add(this.lblSpectrumFlashing_Speed);
            this.tpgSpectrumFlashing.Controls.Add(this.trkSpectrumFlashing_Speed);
            this.tpgSpectrumFlashing.Location = new System.Drawing.Point(4, 22);
            this.tpgSpectrumFlashing.Name = "tpgSpectrumFlashing";
            this.tpgSpectrumFlashing.Size = new System.Drawing.Size(437, 116);
            this.tpgSpectrumFlashing.TabIndex = 5;
            this.tpgSpectrumFlashing.Text = "Spectrum (Flashing)";
            this.tpgSpectrumFlashing.UseVisualStyleBackColor = true;
            // 
            // lblSpectrumFlashing_NoColor
            // 
            this.lblSpectrumFlashing_NoColor.AutoSize = true;
            this.lblSpectrumFlashing_NoColor.Enabled = false;
            this.lblSpectrumFlashing_NoColor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpectrumFlashing_NoColor.Location = new System.Drawing.Point(19, 21);
            this.lblSpectrumFlashing_NoColor.Name = "lblSpectrumFlashing_NoColor";
            this.lblSpectrumFlashing_NoColor.Size = new System.Drawing.Size(129, 13);
            this.lblSpectrumFlashing_NoColor.TabIndex = 0;
            this.lblSpectrumFlashing_NoColor.Text = "No color options available";
            // 
            // lblSpectrumFlashing_Speed
            // 
            this.lblSpectrumFlashing_Speed.AutoSize = true;
            this.lblSpectrumFlashing_Speed.Location = new System.Drawing.Point(253, 19);
            this.lblSpectrumFlashing_Speed.Name = "lblSpectrumFlashing_Speed";
            this.lblSpectrumFlashing_Speed.Size = new System.Drawing.Size(41, 13);
            this.lblSpectrumFlashing_Speed.TabIndex = 1;
            this.lblSpectrumFlashing_Speed.Text = "Speed:";
            // 
            // trkSpectrumFlashing_Speed
            // 
            this.trkSpectrumFlashing_Speed.LargeChange = 2;
            this.trkSpectrumFlashing_Speed.Location = new System.Drawing.Point(256, 42);
            this.trkSpectrumFlashing_Speed.Name = "trkSpectrumFlashing_Speed";
            this.trkSpectrumFlashing_Speed.Size = new System.Drawing.Size(153, 45);
            this.trkSpectrumFlashing_Speed.TabIndex = 2;
            this.trkSpectrumFlashing_Speed.Value = 5;
            this.trkSpectrumFlashing_Speed.Scroll += new System.EventHandler(this.trkSpectrumFlashing_Speed_Scroll);
            // 
            // picLine
            // 
            this.picLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picLine.BackColor = System.Drawing.Color.Black;
            this.picLine.Location = new System.Drawing.Point(0, 250);
            this.picLine.Name = "picLine";
            this.picLine.Size = new System.Drawing.Size(660, 10);
            this.picLine.TabIndex = 4;
            this.picLine.TabStop = false;
            // 
            // nicMain
            // 
            this.nicMain.ContextMenuStrip = this.mnuTray;
            this.nicMain.Icon = ((System.Drawing.Icon)(resources.GetObject("nicMain.Icon")));
            this.nicMain.Text = "Acer Predator X35 RGB Control";
            this.nicMain.Visible = true;
            this.nicMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nicMain_MouseDoubleClick);
            // 
            // mnuTray
            // 
            this.mnuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.mnuSep1,
            this.mnuExit});
            this.mnuTray.Name = "mnuTray";
            this.mnuTray.Size = new System.Drawing.Size(104, 54);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(103, 22);
            this.mnuOpen.Text = "&Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuSep1
            // 
            this.mnuSep1.Name = "mnuSep1";
            this.mnuSep1.Size = new System.Drawing.Size(100, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(103, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // dlgColor
            // 
            this.dlgColor.Color = System.Drawing.Color.White;
            this.dlgColor.FullOpen = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 541);
            this.Controls.Add(this.picLine);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.picMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Acer Predator X35 RGB Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.tabEffect.ResumeLayout(false);
            this.tpgStatic.ResumeLayout(false);
            this.tpgStatic.PerformLayout();
            this.tpgBreathing.ResumeLayout(false);
            this.tpgBreathing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkBreathing_Speed)).EndInit();
            this.tpgFlasing.ResumeLayout(false);
            this.tpgFlasing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkFlashing_Speed)).EndInit();
            this.tpgPhasing.ResumeLayout(false);
            this.tpgPhasing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkPhasing_Speed)).EndInit();
            this.tpgSpectrumBreathing.ResumeLayout(false);
            this.tpgSpectrumBreathing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpectrumBreathing_Speed)).EndInit();
            this.tpgSpectrumFlashing.ResumeLayout(false);
            this.tpgSpectrumFlashing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpectrumFlashing_Speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLine)).EndInit();
            this.mnuTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.Timer tmrToastDetector;
        private System.Windows.Forms.PictureBox picMain;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.TabControl tabEffect;
        private System.Windows.Forms.TabPage tpgStatic;
        private System.Windows.Forms.TabPage tpgBreathing;
        private System.Windows.Forms.ListBox lstEffect;
        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkActivateAlarm;
        private System.Windows.Forms.CheckBox chkAutoOff;
        private System.Windows.Forms.Label lblLighting;
        private System.Windows.Forms.PictureBox picLine;
        private System.Windows.Forms.TabPage tpgFlasing;
        private System.Windows.Forms.TabPage tpgPhasing;
        private System.Windows.Forms.NotifyIcon nicMain;
        private System.Windows.Forms.Button btnStatic_Color1;
        private System.Windows.Forms.ComboBox cboStatic_Color1;
        private System.Windows.Forms.Label lblStatic_Color1;
        private System.Windows.Forms.ColorDialog dlgColor;
        private System.Windows.Forms.Button btnStatic_Alarm;
        private System.Windows.Forms.ComboBox cboStatic_Alarm;
        private System.Windows.Forms.Button btnBreathing_Color2;
        private System.Windows.Forms.ComboBox cboBreathing_Color2;
        private System.Windows.Forms.Label lblBreathing_Color2;
        private System.Windows.Forms.Button btnBreathing_Color1;
        private System.Windows.Forms.ComboBox cboBreathing_Color1;
        private System.Windows.Forms.Label lblBreathing_Color1;
        private System.Windows.Forms.Label lblBreathing_Speed;
        private System.Windows.Forms.TrackBar trkBreathing_Speed;
        private System.Windows.Forms.TabPage tpgSpectrumBreathing;
        private System.Windows.Forms.TabPage tpgSpectrumFlashing;
        private System.Windows.Forms.Label lblFlashing_Speed;
        private System.Windows.Forms.TrackBar trkFlashing_Speed;
        private System.Windows.Forms.Button btnFlashing_Color2;
        private System.Windows.Forms.ComboBox cboFlashing_Color2;
        private System.Windows.Forms.Label lblFlashing_Color2;
        private System.Windows.Forms.Button btnFlashing_Color1;
        private System.Windows.Forms.ComboBox cboFlashing_Color1;
        private System.Windows.Forms.Label lblFlashing_Color1;
        private System.Windows.Forms.Label lblPhasing_Speed;
        private System.Windows.Forms.TrackBar trkPhasing_Speed;
        private System.Windows.Forms.Button btnPhasing_Color2;
        private System.Windows.Forms.ComboBox cboPhasing_Color2;
        private System.Windows.Forms.Label lblPhasing_Color2;
        private System.Windows.Forms.Button btnPhasing_Color1;
        private System.Windows.Forms.ComboBox cboPhasing_Color1;
        private System.Windows.Forms.Label lblPhasing_Color1;
        private System.Windows.Forms.Label lblSpectrumBreathing_Speed;
        private System.Windows.Forms.TrackBar trkSpectrumBreathing_Speed;
        private System.Windows.Forms.Label lblSpectrumFlashing_Speed;
        private System.Windows.Forms.TrackBar trkSpectrumFlashing_Speed;
        private System.Windows.Forms.Label lblSpectrumBreathing_NoColor;
        private System.Windows.Forms.Label lblSpectrumFlashing_NoColor;
        private System.Windows.Forms.ContextMenuStrip mnuTray;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripSeparator mnuSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.Label lblWarning;
    }
}

