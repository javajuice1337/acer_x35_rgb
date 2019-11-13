using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Xml;

namespace acer_rgb
{
    public partial class frmMain : Form
    {
        [DllImport(@"User32", SetLastError = true,
            EntryPoint = "RegisterPowerSettingNotification",
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr RegisterPowerSettingNotification(
            IntPtr hRecipient,
            ref Guid PowerSettingGuid,
            Int32 Flags);

        [DllImport(@"User32",
            EntryPoint = "UnregisterPowerSettingNotification",
            CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnregisterPowerSettingNotification(
            IntPtr handle);

        private static Guid GUID_MONITOR_POWER_ON = new Guid(0x02731015, 0x4510, 0x4526, 0x99, 0xE6, 0xE5, 0xA1, 0x7E, 0xBD, 0x1A, 0xEA);
        
        private const int WM_POWERBROADCAST = 0x0218;
        private const int DEVICE_NOTIFY_WINDOW_HANDLE = 0x00000000;
        private const int PBT_POWERSETTINGCHANGE = 0x8013; // DPPE

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct POWERBROADCAST_SETTING
        {
            public Guid PowerSetting;
            public uint DataLength;
            public byte Data;
        }

        private const int X35_DEFAULT_R = 0;
        private const int X35_DEFAULT_G = 200;
        private const int X35_DEFAULT_B = 255;
        private const int MAX_COLORS = 8;
        
        private IntPtr hw_ret = IntPtr.Zero;
        private Thread workerThread = null;
        private int lastToastCount = 0;
        private bool isInit = false;
        private bool isLoaded = false;
        private bool isHidden = false;
        private bool isSilent = false;
        private bool isDebug = false;

        private Dictionary<String, List<ColorItem>> colorList = new Dictionary<String, List<ColorItem>>();
        private List<Color> colorDefault = new List<Color>
        {
            Color.FromArgb(X35_DEFAULT_R, X35_DEFAULT_G, X35_DEFAULT_B),
            Color.Red,
            Color.Lime,
            Color.Blue,
            Color.Orange,
            Color.Purple,
            Color.Yellow,
            Color.Black
        };

        private int cboStatic_Color1_DropDownWidth = 0;
        private int cboStatic_Alarm_DropDownWidth = 0;
        private int cboBreathing_Color1_DropDownWidth = 0;
        private int cboBreathing_Color2_DropDownWidth = 0;
        private int cboFlashing_Color1_DropDownWidth = 0;
        private int cboFlashing_Color2_DropDownWidth = 0;
        private int cboPhasing_Color1_DropDownWidth = 0;
        private int cboPhasing_Color2_DropDownWidth = 0;

        private int UserOption_LightingEffect = -1;
        private int UserOption_Static_Color1 = 0;
        private int UserOption_Static_Alarm = 0;
        private int UserOption_Breathing_Color1 = 0;
        private int UserOption_Breathing_Color2 = 0;
        private int UserOption_Breathing_Speed = 0;
        private int UserOption_Flashing_Color1 = 0;
        private int UserOption_Flashing_Color2 = 0;
        private int UserOption_Flashing_Speed = 0;
        private int UserOption_Phasing_Color1 = 0;
        private int UserOption_Phasing_Color2 = 0;
        private int UserOption_Phasing_Speed = 0;
        private int UserOption_SpectrumBreathing_Speed = 0;
        private int UserOption_SpectrumFlashing_Speed = 0;
        private bool UserOption_AutoOff = false;
        private bool UserOption_AutoAlarm = false;

        private const string XML_FILENAME = "acer_x35_rgb_cfg.xml";

        private const String CMDLINEARG_EFFECT = "/effect";
        private const String CMDLINEARG_SILENT = "/silent";
        private string[] appArgs;

        private delegate void DisplayColorFunc(Color c);

        public frmMain(string[] args)
        {
            InitializeComponent();
            appArgs = args;
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // Power status event triggered
            if (m.Msg == WM_POWERBROADCAST)
            {
                // Machine is trying to enter suspended state
                if (m.WParam.ToInt32() == PBT_POWERSETTINGCHANGE)
                {
                    if (isInit)
                    {
                        // Extract Data From Msg
                        POWERBROADCAST_SETTING ps = (POWERBROADCAST_SETTING)Marshal.PtrToStructure(m.LParam, typeof(POWERBROADCAST_SETTING));

                        if (ps.PowerSetting == GUID_MONITOR_POWER_ON)
                        {
                            if (chkAutoOff.Checked)
                            {
                                if (ps.Data == 0) // monitor off
                                {
                                    StopWorker();

                                    workerThread = new Thread(WorkerStatic);
                                    workerThread.IsBackground = true;
                                    workerThread.Start(new object[] { Color.Black.ToArgb() });

                                    if (chkActivateAlarm.Checked)
                                    {
                                        lastToastCount = WindowsToastUtils.GetToastCount();
                                        tmrToastDetector.Start();
                                    }
                                }
                                else if (ps.Data == 1) // monitor on
                                {
                                    if (tmrToastDetector.Enabled) tmrToastDetector.Stop();
                                    ApplyChanges();
                                }
                            }
                        }
                    }
                    else
                    {
                        isInit = true;
                    }
                }
                return;
            }

            base.WndProc(ref m);
        }

        private Color ColorBlend(Color color, Color backColor, double amount)
        {
            byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
            byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
            byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));
            return Color.FromArgb(r, g, b);
        }

        private void StopWorker()
        {
            if (workerThread != null)
            {
                try
                {
                    workerThread.Abort();
                    workerThread = null;

                    Thread.Sleep(50);
                }
                catch { }
            }
        }

        private void WorkerStatic(object o)
        {
            object[] _o = (object[])o;
            Color color1 = Color.FromArgb((int)_o[0]);
            const int delay = 1000;

            try
            {
                while (workerThread == Thread.CurrentThread)
                {
                    ApplyColor(color1);

                    Thread.Sleep(delay);
                }
            }
            catch (System.AccessViolationException) { }
        }

        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        private void WorkerBreathing(object o)
        {
            object[] _o = (object[])o;
            Color color1 = Color.FromArgb((int)_o[0]);
            Color color2 = Color.FromArgb((int)_o[1]);
            Color c = Color.Black;
            int delay = 10 + (UserOption_Breathing_Speed * 10);
            int mod = 1;
            double val = 0;
            double val2 = 0;

            const double min = 0;
            const double max = 1;
            const double step = 0.05;
            const double hang = 5;

            try
            {
                while (workerThread == Thread.CurrentThread)
                {
                    if (val2 > 0)
                    {
                        val2--;
                    }
                    else
                    {
                        if (val == min)
                        {
                            c = color1;
                            val2 = hang;
                        }
                        else if (val == max)
                        {
                            c = color2;
                            val2 = hang;
                        }
                        else
                        {
                            c = ColorBlend(color1, color2, 1 - val);
                        }

                        ApplyColor(c);

                        val += step * mod;
                        if (val < min)
                        {
                            val = min;
                            mod = 1;
                        }
                        else if (val > max)
                        {
                            val = max;
                            mod = -1;
                        }
                    }

                    Thread.Sleep(delay);
                }
            }
            catch (System.AccessViolationException) { }
        }

        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        private void WorkerFlashing(object o)
        {
            object[] _o = (object[])o;
            Color color1 = Color.FromArgb((int)_o[0]);
            Color color2 = Color.FromArgb((int)_o[1]);
            Color c = Color.Black;
            int delay = 100 + (UserOption_Flashing_Speed * 20);
            bool val = true;

            try
            {
                while (workerThread == Thread.CurrentThread)
                {
                    if (val)
                    {
                        c = color1;
                    }
                    else
                    {
                        c = color2;
                    }

                    ApplyColor(c);
                    val = !val;

                    Thread.Sleep(delay);
                }
            }
            catch (System.AccessViolationException) { }
        }

        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        private void WorkerPhasing(object o)
        {
            object[] _o = (object[])o;
            Color color1 = Color.FromArgb((int)_o[0]);
            Color color2 = Color.FromArgb((int)_o[1]);
            Color c = Color.Black;
            int delay = 10 + (UserOption_Phasing_Speed * 10);
            int mod = 1;
            double val = 0;
            double val2 = 0;
            double val3 = 0;

            const double min = 0;
            const double max = 1;
            const double step = 0.05;
            const double phase = 5;

            Color _color1 = color1;
            Color _color2 = color2;
            Random r = new Random();

            try
            {
                while (workerThread == Thread.CurrentThread)
                {
                    if (val == min)
                    {
                        if (val2 > 0)
                        {
                            _color1 = ColorBlend(_color1, _color2, 1 - (((val2 - 1) / phase) - ((1 / phase) * ((double)r.Next(0, 100) / 100))));
                            val2--;
                        }
                        else
                        {
                            _color1 = color1;
                            val2 = r.Next(0, (int)phase) + 1;
                        }

                        c = _color1;
                    }
                    else if (val == max)
                    {
                        if (val3 > 0)
                        {
                            _color2 = ColorBlend(_color2, _color1, 1 - (((val3 - 1) / phase) - ((1 / phase) * ((double)r.Next(0, 100) / 100))));
                            val3--;
                        }
                        else
                        {
                            _color2 = color2;
                            val3 = r.Next(0, (int)phase) + 1;
                        }

                        c = _color2;
                    }
                    else
                    {
                        c = ColorBlend(_color1, _color2, 1 - val);
                    }

                    ApplyColor(c);

                    val += step * mod;
                    if (val < min)
                    {
                        val = min;
                        mod = 1;
                    }
                    else if (val > max)
                    {
                        val = max;
                        mod = -1;
                    }

                    Thread.Sleep(delay);
                }
            }
            catch (System.AccessViolationException) { }
        }

        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        private void WorkerSpectrumBreathing(object o)
        {
            //object[] _o = (object[])o;
            Color color1 = Color.Black;
            Color color2 = Color.Black;
            Color c = Color.Black;
            int delay = 10 + (UserOption_SpectrumBreathing_Speed * 10);
            int mod = 1;
            double val = 0;
            double val2 = 0;
            Random r = new Random();

            const double min = 0;
            const double max = 1;
            const double step = 0.05;
            const double hang = 5;

            try
            {
                while (workerThread == Thread.CurrentThread)
                {
                    if (val2 > 0)
                    {
                        val2--;
                    }
                    else
                    {
                        if (val == min)
                        {
                            color2 = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                            c = color1;
                            val2 = hang;
                        }
                        else if (val == max)
                        {
                            color1 = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                            c = color2;
                            val2 = hang;
                        }
                        else
                        {
                            c = ColorBlend(color1, color2, 1 - val);
                        }

                        ApplyColor(c);

                        val += step * mod;
                        if (val < min)
                        {
                            val = min;
                            mod = 1;
                        }
                        else if (val > max)
                        {
                            val = max;
                            mod = -1;
                        }
                    }

                    Thread.Sleep(delay);
                }
            }
            catch (System.AccessViolationException) { }
        }

        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        private void WorkerSpectrumFlashing(object o)
        {
            //object[] _o = (object[])o;
            int delay = 100 + (UserOption_SpectrumFlashing_Speed * 20);
            Random r = new Random();

            try
            {
                while (workerThread == Thread.CurrentThread)
                {
                    ApplyColor(Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256)));

                    Thread.Sleep(delay);
                }
            }
            catch (System.AccessViolationException) { }
        }
        
        private void ApplyChanges()
        {
            if (!isLoaded) return;

            if (cboStatic_Color1.SelectedIndex >= 0) UserOption_Static_Color1 = ColorTranslator.FromHtml(cboStatic_Color1.Text).ToArgb();
            if (cboStatic_Alarm.SelectedIndex >= 0) UserOption_Static_Alarm = ColorTranslator.FromHtml(cboStatic_Alarm.Text).ToArgb();
            if (cboBreathing_Color1.SelectedIndex >= 0) UserOption_Breathing_Color1 = ColorTranslator.FromHtml(cboBreathing_Color1.Text).ToArgb();
            if (cboBreathing_Color2.SelectedIndex >= 0) UserOption_Breathing_Color2 = ColorTranslator.FromHtml(cboBreathing_Color2.Text).ToArgb();
            UserOption_Breathing_Speed = trkBreathing_Speed.Maximum - trkBreathing_Speed.Value;
            if (cboFlashing_Color1.SelectedIndex >= 0) UserOption_Flashing_Color1 = ColorTranslator.FromHtml(cboFlashing_Color1.Text).ToArgb();
            if (cboFlashing_Color2.SelectedIndex >= 0) UserOption_Flashing_Color2 = ColorTranslator.FromHtml(cboFlashing_Color2.Text).ToArgb();
            UserOption_Flashing_Speed = trkFlashing_Speed.Maximum - trkFlashing_Speed.Value;
            if (cboPhasing_Color1.SelectedIndex >= 0) UserOption_Phasing_Color1 = ColorTranslator.FromHtml(cboPhasing_Color1.Text).ToArgb();
            if (cboPhasing_Color2.SelectedIndex >= 0) UserOption_Phasing_Color2 = ColorTranslator.FromHtml(cboPhasing_Color2.Text).ToArgb();
            UserOption_Phasing_Speed = trkPhasing_Speed.Maximum - trkPhasing_Speed.Value;
            UserOption_SpectrumBreathing_Speed = trkSpectrumBreathing_Speed.Maximum - trkSpectrumBreathing_Speed.Value;
            UserOption_SpectrumFlashing_Speed = trkSpectrumFlashing_Speed.Maximum - trkSpectrumFlashing_Speed.Value;

            StopWorker();

            if (UserOption_LightingEffect == 0)
            {
                workerThread = new Thread(WorkerStatic);
                workerThread.IsBackground = true;
                workerThread.Start(new object[] { UserOption_Static_Color1 });
            }
            else if (UserOption_LightingEffect == 1)
            {
                workerThread = new Thread(WorkerBreathing);
                workerThread.IsBackground = true;
                workerThread.Start(new object[] { UserOption_Breathing_Color1, UserOption_Breathing_Color2 });
            }
            else if (UserOption_LightingEffect == 2)
            {
                workerThread = new Thread(WorkerFlashing);
                workerThread.IsBackground = true;
                workerThread.Start(new object[] { UserOption_Flashing_Color1, UserOption_Flashing_Color2 });
            }
            else if (UserOption_LightingEffect == 3)
            {
                workerThread = new Thread(WorkerPhasing);
                workerThread.IsBackground = true;
                workerThread.Start(new object[] { UserOption_Phasing_Color1, UserOption_Phasing_Color2 });
            }
            else if (UserOption_LightingEffect == 4)
            {
                workerThread = new Thread(WorkerSpectrumBreathing);
                workerThread.IsBackground = true;
                workerThread.Start();
            }
            else if (UserOption_LightingEffect == 5)
            {
                workerThread = new Thread(WorkerSpectrumFlashing);
                workerThread.IsBackground = true;
                workerThread.Start();
            }
        }

        private void ApplyColor(Color c)
        {
#if (!DEBUG)
            LED_Control_API.Acer_Apply_X35_Two_Region_RGB(c.R, c.G, c.B, c.R, c.G, c.B, 0);
#endif
            if (!isHidden) DisplayColor(c);
        }
        
        private void DisplayColor(Color c)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new DisplayColorFunc(DisplayColor), c);
                }
                else
                {
                    picLine.BackColor = c;
                }
            }
            catch { }
        }
        
        private void LoadComboList(ComboBox cbo, List<ColorItem> cil)
        {
            cbo.Items.Clear();
            foreach (ColorItem item in cil)
            {
                cbo.Items.Add(item.hex);
            }
        }

        private void LoadComboList(ComboBox cbo, List<ColorItem> cil, Color selected)
        {
            LoadComboList(cbo, cil);
            for (int i = 0; i < cil.Count; i++)
            {
                if (cil[i].color.R == selected.R && cil[i].color.G == selected.G && cil[i].color.B == selected.B)
                {
                    cbo.SelectedIndex = i;
                    break;
                }
            }
        }

        private void LoadComboList(ComboBox cbo, List<ColorItem> cil, int selected)
        {
            LoadComboList(cbo, cil, Color.FromArgb(selected));
        }

        private void LoadColorList(List<Color> cl, List<ColorItem> cil, Color insert)
        {
            for (int i = 0; i < cl.Count; i++)
            {
                if (cl[i].R == insert.R && cl[i].G == insert.G && cl[i].B == insert.B)
                {
                    cl.RemoveAt(i);
                    break;
                }
            }
            cl.Insert(0, insert);
            int cnt = 0;
            foreach (Color c in cl)
            {
                cil.Add(new ColorItem(c));
                cnt++;
                if (cnt >= MAX_COLORS) break;
            }
        }

        private void UpdateColorList(Color c, List<ColorItem> cil)
        {
            for (int i = 0; i < cil.Count; i++)
            {
                if (cil[i].color.R == c.R && cil[i].color.G == c.G && cil[i].color.B == c.B)
                {
                    cil.RemoveAt(i);
                    break;
                }
            }
            cil.Insert(0, new ColorItem(c));
            if (cil.Count > MAX_COLORS)
            {
                int delta = cil.Count - MAX_COLORS;
                cil.RemoveRange(cil.Count - delta, delta);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
#if (DEBUG)
            isDebug = true;
#endif

            // build the color lists

            colorList.Add("Static_Color1", new List<ColorItem>());
            colorList.Add("Static_Alarm", new List<ColorItem>());
            colorList.Add("Breathing_Color1", new List<ColorItem>());
            colorList.Add("Breathing_Color2", new List<ColorItem>());
            colorList.Add("Flashing_Color1", new List<ColorItem>());
            colorList.Add("Flashing_Color2", new List<ColorItem>());
            colorList.Add("Phasing_Color1", new List<ColorItem>());
            colorList.Add("Phasing_Color2", new List<ColorItem>());

            // load the default options

            Color X35_DEFAULT = Color.FromArgb(X35_DEFAULT_R, X35_DEFAULT_G, X35_DEFAULT_B);
            UserOption_Static_Color1 = X35_DEFAULT.ToArgb();
            UserOption_Static_Alarm = Color.Red.ToArgb();
            UserOption_Breathing_Color1 = X35_DEFAULT.ToArgb();
            UserOption_Breathing_Color2 = Color.Black.ToArgb();
            UserOption_Breathing_Speed = 5;
            UserOption_Flashing_Color1 = X35_DEFAULT.ToArgb();
            UserOption_Flashing_Color2 = Color.Black.ToArgb();
            UserOption_Flashing_Speed = 5;
            UserOption_Phasing_Color1 = X35_DEFAULT.ToArgb();
            UserOption_Phasing_Color2 = Color.Black.ToArgb();
            UserOption_Phasing_Speed = 5;
            UserOption_SpectrumBreathing_Speed = 5;
            UserOption_SpectrumFlashing_Speed = 5;
            UserOption_AutoOff = true;
            UserOption_AutoAlarm = true;

            // load the options from xml

            Rectangle winBounds = new Rectangle(this.Location, this.Size);

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(XML_FILENAME);

                XmlNode parentNode = xmlDoc.SelectSingleNode(Program.appName);
                if (parentNode != null)
                {
                    XmlNode thisNode = parentNode.SelectSingleNode("options");
                    if (thisNode != null)
                    {
                        if (thisNode.Attributes.GetNamedItem("autooff") != null)
                        {
                            UserOption_AutoOff = bool.Parse(thisNode.Attributes.GetNamedItem("autooff").Value);
                        }

                        if (thisNode.Attributes.GetNamedItem("autoalarm") != null)
                        {
                            UserOption_AutoAlarm = bool.Parse(thisNode.Attributes.GetNamedItem("autoalarm").Value);
                        }

                        if (thisNode.Attributes.GetNamedItem("locX") != null)
                        {
                            winBounds.X = int.Parse(thisNode.Attributes.GetNamedItem("locX").Value);
                        }

                        if (thisNode.Attributes.GetNamedItem("locY") != null)
                        {
                            winBounds.Y = int.Parse(thisNode.Attributes.GetNamedItem("locY").Value);
                        }
                    }

                    thisNode = parentNode.SelectSingleNode("effects");
                    if (thisNode != null)
                    {
                        if (thisNode.Attributes.GetNamedItem("selected") != null)
                        {
                            UserOption_LightingEffect = int.Parse(thisNode.Attributes.GetNamedItem("selected").Value);
                            if (UserOption_LightingEffect < 0 || UserOption_LightingEffect >= lstEffect.Items.Count) UserOption_LightingEffect = 0;
                        }

                        foreach (XmlNode thisEntry in thisNode.ChildNodes)
                        {
                            if (thisEntry.Attributes.GetNamedItem("id") != null)
                            {
                                int id = int.Parse(thisEntry.Attributes.GetNamedItem("id").Value);

                                if (id == 0 &&
                                    thisEntry.Attributes.GetNamedItem("color") != null)
                                {
                                    UserOption_Static_Color1 = int.Parse(thisEntry.Attributes.GetNamedItem("color").Value);
                                }
                                else if (id == 1 &&
                                    thisEntry.Attributes.GetNamedItem("color1") != null &&
                                    thisEntry.Attributes.GetNamedItem("color2") != null &&
                                    thisEntry.Attributes.GetNamedItem("speed") != null)
                                {
                                    UserOption_Breathing_Color1 = int.Parse(thisEntry.Attributes.GetNamedItem("color1").Value);
                                    UserOption_Breathing_Color2 = int.Parse(thisEntry.Attributes.GetNamedItem("color2").Value);
                                    UserOption_Breathing_Speed = int.Parse(thisEntry.Attributes.GetNamedItem("speed").Value);
                                }
                                else if (id == 2 &&
                                    thisEntry.Attributes.GetNamedItem("color1") != null &&
                                    thisEntry.Attributes.GetNamedItem("color2") != null &&
                                    thisEntry.Attributes.GetNamedItem("speed") != null)
                                {
                                    UserOption_Flashing_Color1 = int.Parse(thisEntry.Attributes.GetNamedItem("color1").Value);
                                    UserOption_Flashing_Color2 = int.Parse(thisEntry.Attributes.GetNamedItem("color2").Value);
                                    UserOption_Flashing_Speed = int.Parse(thisEntry.Attributes.GetNamedItem("speed").Value);
                                }
                                else if (id == 3 &&
                                    thisEntry.Attributes.GetNamedItem("color1") != null &&
                                    thisEntry.Attributes.GetNamedItem("color2") != null &&
                                    thisEntry.Attributes.GetNamedItem("speed") != null)
                                {
                                    UserOption_Phasing_Color1 = int.Parse(thisEntry.Attributes.GetNamedItem("color1").Value);
                                    UserOption_Phasing_Color2 = int.Parse(thisEntry.Attributes.GetNamedItem("color2").Value);
                                    UserOption_Phasing_Speed = int.Parse(thisEntry.Attributes.GetNamedItem("speed").Value);
                                }
                                else if (id == 4 &&
                                    thisEntry.Attributes.GetNamedItem("speed") != null)
                                {
                                    UserOption_SpectrumBreathing_Speed = int.Parse(thisEntry.Attributes.GetNamedItem("speed").Value);
                                }
                                else if (id == 5 &&
                                    thisEntry.Attributes.GetNamedItem("speed") != null)
                                {
                                    UserOption_SpectrumFlashing_Speed = int.Parse(thisEntry.Attributes.GetNamedItem("speed").Value);
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception(string.Format("Missing the '{0}' tag in xml.", Program.appName));
                }
            }
            catch
            {
            }

            if (SystemInformation.VirtualScreen.Contains(winBounds))
            {
                // set the window bounds
                this.Left = winBounds.Left;
                this.Top = winBounds.Top;
            }

            // check for a cmdline parameter
            for (int i = 0; i < appArgs.Length; i++)
            {
                if (appArgs[i] == CMDLINEARG_EFFECT && i < appArgs.Length - 1)
                {
                    // use the specified effect as default
                    if (int.TryParse(appArgs[i + 1], out UserOption_LightingEffect))
                    {
                        if (UserOption_LightingEffect < 0 || UserOption_LightingEffect >= lstEffect.Items.Count) UserOption_LightingEffect = 0;
                    }
                }
                else if (appArgs[i] == CMDLINEARG_SILENT)
                {
                    isSilent = true;
                }
            }
            
            // prepare the color lists

            LoadColorList(colorDefault, colorList["Static_Color1"], Color.FromArgb(UserOption_Static_Color1));
            LoadColorList(colorDefault, colorList["Static_Alarm"], Color.FromArgb(UserOption_Static_Alarm));
            LoadColorList(colorDefault, colorList["Breathing_Color1"], Color.FromArgb(UserOption_Breathing_Color1));
            LoadColorList(colorDefault, colorList["Breathing_Color2"], Color.FromArgb(UserOption_Breathing_Color2));
            LoadColorList(colorDefault, colorList["Flashing_Color1"], Color.FromArgb(UserOption_Flashing_Color1));
            LoadColorList(colorDefault, colorList["Flashing_Color2"], Color.FromArgb(UserOption_Flashing_Color2));
            LoadColorList(colorDefault, colorList["Phasing_Color1"], Color.FromArgb(UserOption_Phasing_Color1));
            LoadColorList(colorDefault, colorList["Phasing_Color2"], Color.FromArgb(UserOption_Phasing_Color2));
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            Acer_Detect_Lighting_Device.Do_All_Lighting_Device_Detection();
            if (Acer_Detect_Lighting_Device.IsConnected_X35 || isDebug)
            {
#if (!DEBUG)
                IntPtr hWnd = this.Handle;
                hw_ret = RegisterPowerSettingNotification(hWnd, ref GUID_MONITOR_POWER_ON, DEVICE_NOTIFY_WINDOW_HANDLE);

                bool denied = false;
                if (!WindowsToastUtils.RequestAccess(out denied) || denied) chkActivateAlarm.Enabled = false;
#endif
                LoadComboList(cboStatic_Color1, colorList["Static_Color1"], UserOption_Static_Color1);
                LoadComboList(cboStatic_Alarm, colorList["Static_Alarm"], UserOption_Static_Alarm);
                LoadComboList(cboBreathing_Color1, colorList["Breathing_Color1"], UserOption_Breathing_Color1);
                LoadComboList(cboBreathing_Color2, colorList["Breathing_Color2"], UserOption_Breathing_Color2);
                LoadComboList(cboFlashing_Color1, colorList["Flashing_Color1"], UserOption_Flashing_Color1);
                LoadComboList(cboFlashing_Color2, colorList["Flashing_Color2"], UserOption_Flashing_Color2);
                LoadComboList(cboPhasing_Color1, colorList["Phasing_Color1"], UserOption_Phasing_Color1);
                LoadComboList(cboPhasing_Color2, colorList["Phasing_Color2"], UserOption_Phasing_Color2);

                if (UserOption_Breathing_Speed >= trkBreathing_Speed.Minimum && UserOption_Breathing_Speed <= trkBreathing_Speed.Maximum)
                {
                    trkBreathing_Speed.Value = trkBreathing_Speed.Maximum - UserOption_Breathing_Speed;
                }
                if (UserOption_Flashing_Speed >= trkFlashing_Speed.Minimum && UserOption_Flashing_Speed <= trkFlashing_Speed.Maximum)
                {
                    trkFlashing_Speed.Value = trkFlashing_Speed.Maximum - UserOption_Flashing_Speed;
                }
                if (UserOption_Phasing_Speed >= trkPhasing_Speed.Minimum && UserOption_Phasing_Speed <= trkPhasing_Speed.Maximum)
                {
                    trkPhasing_Speed.Value = trkPhasing_Speed.Maximum - UserOption_Phasing_Speed;
                }
                if (UserOption_SpectrumBreathing_Speed >= trkSpectrumBreathing_Speed.Minimum && UserOption_SpectrumBreathing_Speed <= trkSpectrumBreathing_Speed.Maximum)
                {
                    trkSpectrumBreathing_Speed.Value = trkSpectrumBreathing_Speed.Maximum - UserOption_SpectrumBreathing_Speed;
                }
                if (UserOption_SpectrumFlashing_Speed >= trkSpectrumFlashing_Speed.Minimum && UserOption_SpectrumFlashing_Speed <= trkSpectrumFlashing_Speed.Maximum)
                {
                    trkSpectrumFlashing_Speed.Value = trkSpectrumFlashing_Speed.Maximum - UserOption_SpectrumFlashing_Speed;
                }

                isLoaded = true;

                chkEnable.Enabled = true;
                chkEnable.Checked = true;
            }
            else
            {
                lblWarning.Visible = true;
            }

            chkAutoOff.Checked = UserOption_AutoOff;
            chkActivateAlarm.Checked = UserOption_AutoAlarm;

            if (isSilent) this.WindowState = FormWindowState.Minimized;
        }

        private void frmMain_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            // save the options to xml

            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                XmlNode parentNode = xmlDoc.AppendChild(xmlDoc.CreateElement(Program.appName));
                if (parentNode != null)
                {
                    XmlAttribute thisAttribute = xmlDoc.CreateAttribute("version");
                    thisAttribute.Value = Application.ProductVersion;

                    parentNode.Attributes.Append(thisAttribute);

                    XmlNode thisNode = parentNode.AppendChild(xmlDoc.CreateElement("options"));
                    if (thisNode != null)
                    {
                        thisAttribute = xmlDoc.CreateAttribute("autooff");
                        thisAttribute.Value = UserOption_AutoOff.ToString();

                        thisNode.Attributes.Append(thisAttribute);

                        thisAttribute = xmlDoc.CreateAttribute("autoalarm");
                        thisAttribute.Value = UserOption_AutoAlarm.ToString();

                        thisNode.Attributes.Append(thisAttribute);
                        
                        thisAttribute = xmlDoc.CreateAttribute("locX");
                        thisAttribute.Value = this.Bounds.Left.ToString();

                        thisNode.Attributes.Append(thisAttribute);

                        thisAttribute = xmlDoc.CreateAttribute("locY");
                        thisAttribute.Value = this.Bounds.Top.ToString();

                        thisNode.Attributes.Append(thisAttribute);
                    }
                    else
                    {
                        throw new Exception("Unable to create the 'options' tag in xml.");
                    }

                    thisNode = parentNode.AppendChild(xmlDoc.CreateElement("effects"));
                    if (thisNode != null)
                    {
                        XmlAttribute attribSelected = xmlDoc.CreateAttribute("selected");
                        attribSelected.Value = UserOption_LightingEffect.ToString();

                        thisNode.Attributes.Append(attribSelected);

                        for (int i = 0; i < lstEffect.Items.Count; i++)
                        {
                            if (i == 0)
                            {
                                XmlAttribute attribID = xmlDoc.CreateAttribute("id");
                                attribID.Value = i.ToString();

                                XmlAttribute attribColor = xmlDoc.CreateAttribute("color");
                                attribColor.Value = UserOption_Static_Color1.ToString();

                                XmlNode childNode = thisNode.AppendChild(xmlDoc.CreateElement("effect"));
                                if (childNode != null)
                                {
                                    childNode.Attributes.Append(attribID);
                                    childNode.Attributes.Append(attribColor);
                                }
                            }
                            else if (i == 1)
                            {
                                XmlAttribute attribID = xmlDoc.CreateAttribute("id");
                                attribID.Value = i.ToString();

                                XmlAttribute attribColor1 = xmlDoc.CreateAttribute("color1");
                                attribColor1.Value = UserOption_Breathing_Color1.ToString();

                                XmlAttribute attribColor2 = xmlDoc.CreateAttribute("color2");
                                attribColor2.Value = UserOption_Breathing_Color2.ToString();

                                XmlAttribute attribSpeed = xmlDoc.CreateAttribute("speed");
                                attribSpeed.Value = UserOption_Breathing_Speed.ToString();

                                XmlNode childNode = thisNode.AppendChild(xmlDoc.CreateElement("effect"));
                                if (childNode != null)
                                {
                                    childNode.Attributes.Append(attribID);
                                    childNode.Attributes.Append(attribColor1);
                                    childNode.Attributes.Append(attribColor2);
                                    childNode.Attributes.Append(attribSpeed);
                                }
                            }
                            else if (i == 2)
                            {
                                XmlAttribute attribID = xmlDoc.CreateAttribute("id");
                                attribID.Value = i.ToString();

                                XmlAttribute attribColor1 = xmlDoc.CreateAttribute("color1");
                                attribColor1.Value = UserOption_Flashing_Color1.ToString();

                                XmlAttribute attribColor2 = xmlDoc.CreateAttribute("color2");
                                attribColor2.Value = UserOption_Flashing_Color2.ToString();

                                XmlAttribute attribSpeed = xmlDoc.CreateAttribute("speed");
                                attribSpeed.Value = UserOption_Flashing_Speed.ToString();

                                XmlNode childNode = thisNode.AppendChild(xmlDoc.CreateElement("effect"));
                                if (childNode != null)
                                {
                                    childNode.Attributes.Append(attribID);
                                    childNode.Attributes.Append(attribColor1);
                                    childNode.Attributes.Append(attribColor2);
                                    childNode.Attributes.Append(attribSpeed);
                                }
                            }
                            else if (i == 3)
                            {
                                XmlAttribute attribID = xmlDoc.CreateAttribute("id");
                                attribID.Value = i.ToString();

                                XmlAttribute attribColor1 = xmlDoc.CreateAttribute("color1");
                                attribColor1.Value = UserOption_Phasing_Color1.ToString();

                                XmlAttribute attribColor2 = xmlDoc.CreateAttribute("color2");
                                attribColor2.Value = UserOption_Phasing_Color2.ToString();

                                XmlAttribute attribSpeed = xmlDoc.CreateAttribute("speed");
                                attribSpeed.Value = UserOption_Phasing_Speed.ToString();

                                XmlNode childNode = thisNode.AppendChild(xmlDoc.CreateElement("effect"));
                                if (childNode != null)
                                {
                                    childNode.Attributes.Append(attribID);
                                    childNode.Attributes.Append(attribColor1);
                                    childNode.Attributes.Append(attribColor2);
                                    childNode.Attributes.Append(attribSpeed);
                                }
                            }
                            else if (i == 4)
                            {
                                XmlAttribute attribID = xmlDoc.CreateAttribute("id");
                                attribID.Value = i.ToString();

                                XmlAttribute attribSpeed = xmlDoc.CreateAttribute("speed");
                                attribSpeed.Value = UserOption_SpectrumBreathing_Speed.ToString();

                                XmlNode childNode = thisNode.AppendChild(xmlDoc.CreateElement("effect"));
                                if (childNode != null)
                                {
                                    childNode.Attributes.Append(attribID);
                                    childNode.Attributes.Append(attribSpeed);
                                }
                            }
                            else if (i == 5)
                            {
                                XmlAttribute attribID = xmlDoc.CreateAttribute("id");
                                attribID.Value = i.ToString();

                                XmlAttribute attribSpeed = xmlDoc.CreateAttribute("speed");
                                attribSpeed.Value = UserOption_SpectrumFlashing_Speed.ToString();

                                XmlNode childNode = thisNode.AppendChild(xmlDoc.CreateElement("effect"));
                                if (childNode != null)
                                {
                                    childNode.Attributes.Append(attribID);
                                    childNode.Attributes.Append(attribSpeed);
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Unable to create the 'effects' tag in xml.");
                    }
                }
                else
                {
                    throw new Exception(string.Format("Unable to create the '{0}' tag in xml.", Program.appName));
                }

                xmlDoc.Save(XML_FILENAME);
            }
            catch
            {
            }

            if (hw_ret != IntPtr.Zero)
            {
                if (tmrToastDetector.Enabled) tmrToastDetector.Stop();

                try
                {
                    UnregisterPowerSettingNotification(hw_ret);
                }
                catch { }
            }

            if (chkEnable.Checked) chkEnable.Checked = false;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            // check for windowstate change
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                isHidden = true;
            }
        }

        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkEnable.Checked)
                {
                    LED_Control_API.Acer_Connect_to_X35_Controller();
                    Thread.Sleep(50);
                    LED_Control_API.Acer_Apply_X35_Enable_Two_Region_Control(0);
                    Thread.Sleep(50);
                    ApplyColor(Color.Black);

                    lstEffect.Enabled = true;
                    tabEffect.Enabled = true;
                    grpOptions.Enabled = true;

                    if (lstEffect.SelectedIndex == -1) lstEffect.SelectedIndex = UserOption_LightingEffect;
                    tabEffect_SelectedIndexChanged(null, null);
                }
                else
                {
                    lstEffect.Enabled = false;
                    tabEffect.Enabled = false;
                    grpOptions.Enabled = false;

                    StopWorker();

                    try
                    {
                        ApplyColor(Color.Black);
                        Thread.Sleep(50);
                        LED_Control_API.Acer_Close_X35_Control();
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                chkEnable.Checked = false;
            }
        }

        private void lstEffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserOption_LightingEffect = lstEffect.SelectedIndex;
            tabEffect.SelectedIndex = lstEffect.SelectedIndex;
        }

        private void tabEffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserOption_LightingEffect = lstEffect.SelectedIndex;
            lstEffect.SelectedIndex = tabEffect.SelectedIndex;

            ApplyChanges();
        }

        private void cboStatic_Color1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void cboStatic_Color1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1)
            {
                Bitmap img = colorList["Static_Color1"][e.Index].img;
                e.Graphics.DrawImage(img, new PointF(e.Bounds.X, e.Bounds.Y));

                using (Brush b = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(cboStatic_Color1.Items[e.Index].ToString(), e.Font, b,
                        new RectangleF(e.Bounds.X + img.Width + 3, e.Bounds.Y + 2, cboStatic_Color1_DropDownWidth, cboStatic_Color1.ItemHeight));
                }
            }

            e.DrawFocusRectangle();
        }

        private void cboStatic_Color1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            int maxWidth = 0;

            foreach (object obj in cboStatic_Color1.Items)
            {
                int width = (int)e.Graphics.MeasureString(obj.ToString(), cboStatic_Color1.Font).Width;
                if (width > maxWidth) maxWidth = width;
            }

            cboStatic_Color1_DropDownWidth = maxWidth + 20;
        }

        private void btnStatic_Color1_Click(object sender, EventArgs e)
        {
            if (cboStatic_Color1.SelectedIndex >= 0)
            {
                dlgColor.Color = ColorTranslator.FromHtml(cboStatic_Color1.Text);
            }

            if (dlgColor.ShowDialog(this) == DialogResult.OK)
            {
                UpdateColorList(dlgColor.Color, colorList["Static_Color1"]);
                LoadComboList(cboStatic_Color1, colorList["Static_Color1"]);
                cboStatic_Color1.SelectedIndex = 0;
            }
        }

        private void cboStatic_Alarm_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void cboStatic_Alarm_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1)
            {
                Bitmap img = colorList["Static_Alarm"][e.Index].img;
                e.Graphics.DrawImage(img, new PointF(e.Bounds.X, e.Bounds.Y));

                using (Brush b = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(cboStatic_Alarm.Items[e.Index].ToString(), e.Font, b,
                        new RectangleF(e.Bounds.X + img.Width + 3, e.Bounds.Y + 2, cboStatic_Alarm_DropDownWidth, cboStatic_Alarm.ItemHeight));
                }
            }

            e.DrawFocusRectangle();
        }

        private void cboStatic_Alarm_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            int maxWidth = 0;

            foreach (object obj in cboStatic_Alarm.Items)
            {
                int width = (int)e.Graphics.MeasureString(obj.ToString(), cboStatic_Alarm.Font).Width;
                if (width > maxWidth) maxWidth = width;
            }

            cboStatic_Alarm_DropDownWidth = maxWidth + 20;
        }

        private void btnStatic_Alarm_Click(object sender, EventArgs e)
        {
            if (cboStatic_Alarm.SelectedIndex >= 0)
            {
                dlgColor.Color = ColorTranslator.FromHtml(cboStatic_Alarm.Text);
            }

            if (dlgColor.ShowDialog(this) == DialogResult.OK)
            {
                UpdateColorList(dlgColor.Color, colorList["Static_Alarm"]);
                LoadComboList(cboStatic_Alarm, colorList["Static_Alarm"]);
                cboStatic_Alarm.SelectedIndex = 0;
            }
        }
        
        private void cboBreathing_Color1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void cboBreathing_Color1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1)
            {
                Bitmap img = colorList["Breathing_Color1"][e.Index].img;
                e.Graphics.DrawImage(img, new PointF(e.Bounds.X, e.Bounds.Y));

                using (Brush b = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(cboBreathing_Color1.Items[e.Index].ToString(), e.Font, b,
                        new RectangleF(e.Bounds.X + img.Width + 3, e.Bounds.Y + 2, cboBreathing_Color1_DropDownWidth, cboBreathing_Color1.ItemHeight));
                }
            }

            e.DrawFocusRectangle();
        }

        private void cboBreathing_Color1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            int maxWidth = 0;

            foreach (object obj in cboBreathing_Color1.Items)
            {
                int width = (int)e.Graphics.MeasureString(obj.ToString(), cboBreathing_Color1.Font).Width;
                if (width > maxWidth) maxWidth = width;
            }

            cboBreathing_Color1_DropDownWidth = maxWidth + 20;
        }

        private void btnBreathing_Color1_Click(object sender, EventArgs e)
        {
            if (cboBreathing_Color1.SelectedIndex >= 0)
            {
                dlgColor.Color = ColorTranslator.FromHtml(cboBreathing_Color1.Text);
            }

            if (dlgColor.ShowDialog(this) == DialogResult.OK)
            {
                UpdateColorList(dlgColor.Color, colorList["Breathing_Color1"]);
                LoadComboList(cboBreathing_Color1, colorList["Breathing_Color1"]);
                cboBreathing_Color1.SelectedIndex = 0;
            }
        }

        private void cboBreathing_Color2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void cboBreathing_Color2_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1)
            {
                Bitmap img = colorList["Breathing_Color2"][e.Index].img;
                e.Graphics.DrawImage(img, new PointF(e.Bounds.X, e.Bounds.Y));

                using (Brush b = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(cboBreathing_Color2.Items[e.Index].ToString(), e.Font, b,
                        new RectangleF(e.Bounds.X + img.Width + 3, e.Bounds.Y + 2, cboBreathing_Color2_DropDownWidth, cboBreathing_Color2.ItemHeight));
                }
            }

            e.DrawFocusRectangle();
        }

        private void cboBreathing_Color2_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            int maxWidth = 0;

            foreach (object obj in cboBreathing_Color2.Items)
            {
                int width = (int)e.Graphics.MeasureString(obj.ToString(), cboBreathing_Color2.Font).Width;
                if (width > maxWidth) maxWidth = width;
            }

            cboBreathing_Color2_DropDownWidth = maxWidth + 20;
        }

        private void btnBreathing_Color2_Click(object sender, EventArgs e)
        {
            if (cboBreathing_Color2.SelectedIndex >= 0)
            {
                dlgColor.Color = ColorTranslator.FromHtml(cboBreathing_Color2.Text);
            }

            if (dlgColor.ShowDialog(this) == DialogResult.OK)
            {
                UpdateColorList(dlgColor.Color, colorList["Breathing_Color2"]);
                LoadComboList(cboBreathing_Color2, colorList["Breathing_Color2"]);
                cboBreathing_Color2.SelectedIndex = 0;
            }
        }
        
        private void trkBreathing_Speed_Scroll(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void cboFlashing_Color1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void cboFlashing_Color1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1)
            {
                Bitmap img = colorList["Flashing_Color1"][e.Index].img;
                e.Graphics.DrawImage(img, new PointF(e.Bounds.X, e.Bounds.Y));

                using (Brush b = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(cboFlashing_Color1.Items[e.Index].ToString(), e.Font, b,
                        new RectangleF(e.Bounds.X + img.Width + 3, e.Bounds.Y + 2, cboFlashing_Color1_DropDownWidth, cboFlashing_Color1.ItemHeight));
                }
            }

            e.DrawFocusRectangle();
        }

        private void cboFlashing_Color1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            int maxWidth = 0;

            foreach (object obj in cboFlashing_Color1.Items)
            {
                int width = (int)e.Graphics.MeasureString(obj.ToString(), cboFlashing_Color1.Font).Width;
                if (width > maxWidth) maxWidth = width;
            }

            cboFlashing_Color1_DropDownWidth = maxWidth + 20;
        }

        private void btnFlashing_Color1_Click(object sender, EventArgs e)
        {
            if (cboFlashing_Color1.SelectedIndex >= 0)
            {
                dlgColor.Color = ColorTranslator.FromHtml(cboFlashing_Color1.Text);
            }

            if (dlgColor.ShowDialog(this) == DialogResult.OK)
            {
                UpdateColorList(dlgColor.Color, colorList["Flashing_Color1"]);
                LoadComboList(cboFlashing_Color1, colorList["Flashing_Color1"]);
                cboFlashing_Color1.SelectedIndex = 0;
            }
        }

        private void cboFlashing_Color2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void cboFlashing_Color2_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1)
            {
                Bitmap img = colorList["Flashing_Color2"][e.Index].img;
                e.Graphics.DrawImage(img, new PointF(e.Bounds.X, e.Bounds.Y));

                using (Brush b = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(cboFlashing_Color2.Items[e.Index].ToString(), e.Font, b,
                        new RectangleF(e.Bounds.X + img.Width + 3, e.Bounds.Y + 2, cboFlashing_Color2_DropDownWidth, cboFlashing_Color2.ItemHeight));
                }
            }

            e.DrawFocusRectangle();
        }

        private void cboFlashing_Color2_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            int maxWidth = 0;

            foreach (object obj in cboFlashing_Color2.Items)
            {
                int width = (int)e.Graphics.MeasureString(obj.ToString(), cboFlashing_Color2.Font).Width;
                if (width > maxWidth) maxWidth = width;
            }

            cboFlashing_Color2_DropDownWidth = maxWidth + 20;
        }

        private void btnFlashing_Color2_Click(object sender, EventArgs e)
        {
            if (cboFlashing_Color2.SelectedIndex >= 0)
            {
                dlgColor.Color = ColorTranslator.FromHtml(cboFlashing_Color2.Text);
            }

            if (dlgColor.ShowDialog(this) == DialogResult.OK)
            {
                UpdateColorList(dlgColor.Color, colorList["Flashing_Color2"]);
                LoadComboList(cboFlashing_Color2, colorList["Flashing_Color2"]);
                cboFlashing_Color2.SelectedIndex = 0;
            }
        }

        private void trkFlashing_Speed_Scroll(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void cboPhasing_Color1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void cboPhasing_Color1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1)
            {
                Bitmap img = colorList["Phasing_Color1"][e.Index].img;
                e.Graphics.DrawImage(img, new PointF(e.Bounds.X, e.Bounds.Y));

                using (Brush b = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(cboPhasing_Color1.Items[e.Index].ToString(), e.Font, b,
                        new RectangleF(e.Bounds.X + img.Width + 3, e.Bounds.Y + 2, cboPhasing_Color1_DropDownWidth, cboPhasing_Color1.ItemHeight));
                }
            }

            e.DrawFocusRectangle();
        }

        private void cboPhasing_Color1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            int maxWidth = 0;

            foreach (object obj in cboPhasing_Color1.Items)
            {
                int width = (int)e.Graphics.MeasureString(obj.ToString(), cboPhasing_Color1.Font).Width;
                if (width > maxWidth) maxWidth = width;
            }

            cboPhasing_Color1_DropDownWidth = maxWidth + 20;
        }

        private void btnPhasing_Color1_Click(object sender, EventArgs e)
        {
            if (cboPhasing_Color1.SelectedIndex >= 0)
            {
                dlgColor.Color = ColorTranslator.FromHtml(cboPhasing_Color1.Text);
            }

            if (dlgColor.ShowDialog(this) == DialogResult.OK)
            {
                UpdateColorList(dlgColor.Color, colorList["Phasing_Color1"]);
                LoadComboList(cboPhasing_Color1, colorList["Phasing_Color1"]);
                cboPhasing_Color1.SelectedIndex = 0;
            }
        }

        private void cboPhasing_Color2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void cboPhasing_Color2_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1)
            {
                Bitmap img = colorList["Phasing_Color2"][e.Index].img;
                e.Graphics.DrawImage(img, new PointF(e.Bounds.X, e.Bounds.Y));

                using (Brush b = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(cboPhasing_Color2.Items[e.Index].ToString(), e.Font, b,
                        new RectangleF(e.Bounds.X + img.Width + 3, e.Bounds.Y + 2, cboPhasing_Color2_DropDownWidth, cboPhasing_Color2.ItemHeight));
                }
            }

            e.DrawFocusRectangle();
        }

        private void cboPhasing_Color2_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            int maxWidth = 0;

            foreach (object obj in cboPhasing_Color2.Items)
            {
                int width = (int)e.Graphics.MeasureString(obj.ToString(), cboPhasing_Color2.Font).Width;
                if (width > maxWidth) maxWidth = width;
            }

            cboPhasing_Color2_DropDownWidth = maxWidth + 20;
        }

        private void btnPhasing_Color2_Click(object sender, EventArgs e)
        {
            if (cboPhasing_Color2.SelectedIndex >= 0)
            {
                dlgColor.Color = ColorTranslator.FromHtml(cboPhasing_Color2.Text);
            }

            if (dlgColor.ShowDialog(this) == DialogResult.OK)
            {
                UpdateColorList(dlgColor.Color, colorList["Phasing_Color2"]);
                LoadComboList(cboPhasing_Color2, colorList["Phasing_Color2"]);
                cboPhasing_Color2.SelectedIndex = 0;
            }
        }

        private void trkPhasing_Speed_Scroll(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void trkSpectrumBreathing_Speed_Scroll(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void trkSpectrumFlashing_Speed_Scroll(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void chkActivateAlarm_CheckedChanged(object sender, EventArgs e)
        {
            cboStatic_Alarm.Enabled = chkActivateAlarm.Checked;
            btnStatic_Alarm.Enabled = chkActivateAlarm.Checked;
            if (cboStatic_Alarm.Enabled) cboStatic_Alarm.Focus();
        }

        private void nicMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left && !SystemInformation.MouseButtonsSwapped) ||
                (e.Button == MouseButtons.Right && SystemInformation.MouseButtonsSwapped))
            {
                mnuOpen.PerformClick();
            }
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            // display the main form
            this.Show();
            this.WindowState = FormWindowState.Normal;

            isHidden = false;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tmrToastDetector_Tick(object sender, EventArgs e)
        {
            int _lastToastCount = WindowsToastUtils.GetToastCount();
            if (_lastToastCount != lastToastCount)
            {
                tmrToastDetector.Stop();
                StopWorker();

                workerThread = new Thread(WorkerStatic);
                workerThread.IsBackground = true;
                workerThread.Start(new object[] { UserOption_Static_Alarm });
            }
        }
    }
}
