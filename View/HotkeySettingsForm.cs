﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrayShell.Hotkeys;
using XrayShell.Model;
using XrayShell.Properties;
using XrayShell.Services;

namespace XrayShell.View
{
    public partial class HotkeySettingsForm : Form
    {
        private readonly XrayShellController _controller;

        // this is a copy of configuration that we are working on
        private HotkeyConfig _modifiedConfig;

        private readonly IEnumerable<TextBox> _allTextBoxes;

        public HotkeySettingsForm(XrayShellController controller)
        {
            InitializeComponent();
            UpdateTexts();
            Icon = Resources.xray;

            _controller = controller;
            _controller.ConfigChanged += controller_ConfigChanged;

            LoadCurrentConfiguration();

            // get all textboxes belong to this form
            _allTextBoxes = tableLayoutPanel1.GetChildControls<TextBox>();
            if (!_allTextBoxes.Any()) throw new Exception("Cannot get all textboxes");
        }

        private void controller_ConfigChanged(object sender, EventArgs e)
        {
            LoadCurrentConfiguration();
        }

        private void LoadCurrentConfiguration()
        {
            _modifiedConfig = _controller.GetConfigurationCopy().hotkey;
            LoadConfiguration(_modifiedConfig);
        }

        private void LoadConfiguration(HotkeyConfig config)
        {
            SwitchSystemProxyTextBox.Text = config.SwitchSystemProxy;
            SwitchSystemProxyModeTextBox.Text = config.SwitchSystemProxyMode;
            SwitchAllowLanTextBox.Text = config.SwitchAllowLan;
            ShowLogsTextBox.Text = config.ShowLogs;
            ServerMoveUpTextBox.Text = config.ServerMoveUp;
            ServerMoveDownTextBox.Text = config.ServerMoveDown;
            AddCurrentChromeURLtoPACTextBox.Text = config.AddCurrentChromeURLtoPAC;
            AddCurrentChromeDomaintoPACTextBox.Text = config.AddCurrentChromeDomaintoPAC;
            ScanQRTextBox.Text = config.ScanQR;
        }

        private void UpdateTexts()
        {
            Font = Global.Font;
            // I18N stuff
            SwitchSystemProxyLabel.Text = I18N.GetString("Switch system proxy");
            SwitchSystemProxyModeLabel.Text = I18N.GetString("Switch system proxy mode");
            SwitchAllowLanLabel.Text = I18N.GetString("Switch share over LAN");
            ShowLogsLabel.Text = I18N.GetString("Show Logs");
            ServerMoveUpLabel.Text = I18N.GetString("Switch to prev server");
            ServerMoveDownLabel.Text = I18N.GetString("Switch to next server");
            AddCurrentChromeDomaintoPACLabel.Text = I18N.GetString("Add current Chrome domain to PAC");
            AddCurrentChromeURLtoPACLabel.Text = I18N.GetString("Add current Chrome url to PAC");
            ScanQRLabel.Text = I18N.GetString("Scan QRCode");
            btnOK.Text = I18N.GetString("OK");
            btnCancel.Text = I18N.GetString("Cancel");
            btnRegisterAll.Text = I18N.GetString("Reg All");
            Text = I18N.GetString("Edit Hotkeys...");
        }

        /// <summary>
        /// Capture hotkey - Press key
        /// </summary>
        private void HotkeyDown(object sender, KeyEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            //Combination key only
            if (e.Modifiers != 0)
            {
                // XXX: Hotkey parsing depends on the sequence, more specifically, ModifierKeysConverter.
                // Windows key is reserved by operating system, we deny this key.
                if (e.Control)
                {
                    sb.Append("Ctrl+");
                }
                if (e.Alt)
                {
                    sb.Append("Alt+");
                }
                if (e.Shift)
                {
                    sb.Append("Shift+");
                }

                Keys keyvalue = (Keys)e.KeyValue;
                if ((keyvalue >= Keys.PageUp && keyvalue <= Keys.Down) ||
                    (keyvalue >= Keys.A && keyvalue <= Keys.Z) ||
                    (keyvalue >= Keys.F1 && keyvalue <= Keys.F12))
                {
                    sb.Append(e.KeyCode);
                }
                else if (keyvalue >= Keys.D0 && keyvalue <= Keys.D9)
                {
                    sb.Append('D').Append((char)e.KeyValue);
                }
                else if (keyvalue >= Keys.NumPad0 && keyvalue <= Keys.NumPad9)
                {
                    sb.Append("NumPad").Append((char)(e.KeyValue - 48));
                }
            }
            ((TextBox)sender).Text = sb.ToString();
        }

        /// <summary>
        /// Capture hotkey - Release key
        /// </summary>
        private void HotkeyUp(object sender, KeyEventArgs e)
        {
            var tb = (TextBox)sender;
            var content = tb.Text.TrimEnd();
            if (content.Length >= 1 && content[content.Length - 1] == '+')
            {
                tb.Text = "";
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;

            if (tb.Text == "")
            {
                // unreg
                UnregHotkey(tb);
            }
        }

        private void UnregHotkey(TextBox tb)
        {
            HotKeys.HotKeyCallBackHandler callBack;
            Label lb;

            PrepareForHotkey(tb, out callBack, out lb);

            UnregPrevHotkey(callBack);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void OKButton_Click(object sender, EventArgs e)
        {
            // try to register, notify to change settings if failed
            foreach (var tb in _allTextBoxes)
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    continue;
                }
                if (!TryRegHotkey(tb))
                {
                    MessageBox.Show(I18N.GetString("Register hotkey failed"));
                    return;
                }
            }

            // All check passed, saving
            await SaveConfig();
            Close();
        }

        private void RegisterAllButton_Click(object sender, EventArgs e)
        {
            foreach (var tb in _allTextBoxes)
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    continue;
                }
                TryRegHotkey(tb);
            }
        }

        private bool TryRegHotkey(TextBox tb)
        {
            var hotkey = HotKeys.Str2HotKey(tb.Text);
            if (hotkey == null)
            {
                MessageBox.Show(string.Format(I18N.GetString("Cannot parse hotkey: {0}"), tb.Text));
                tb.Clear();
                return false;
            }

            HotKeys.HotKeyCallBackHandler callBack;
            Label lb;

            PrepareForHotkey(tb, out callBack, out lb);

            UnregPrevHotkey(callBack);

            // try to register keys
            // if already registered by other progs
            // notify to change

            // use the corresponding label color to indicate
            // reg result.
            // Green: not occupied by others and operation succeed
            // Yellow: already registered by other program and need action: disable by clear the content
            //         or change to another one
            // Transparent without color: first run or empty config

            bool regResult = HotKeys.Regist(hotkey, callBack);
            lb.BackColor = regResult ? Color.Green : Color.Yellow;
            return regResult;
        }

        private static void UnregPrevHotkey(HotKeys.HotKeyCallBackHandler cb)
        {
            HotKey prevHotKey;
            if (HotKeys.IsCallbackExists(cb, out prevHotKey))
            {
                // unregister previous one
                HotKeys.UnRegist(prevHotKey);
            }
        }

        private async Task SaveConfig()
        {
            _modifiedConfig.SwitchSystemProxy = SwitchSystemProxyTextBox.Text;
            _modifiedConfig.SwitchSystemProxyMode = SwitchSystemProxyModeTextBox.Text;
            _modifiedConfig.SwitchAllowLan = SwitchAllowLanTextBox.Text;
            _modifiedConfig.ShowLogs = ShowLogsTextBox.Text;
            _modifiedConfig.ServerMoveUp = ServerMoveUpTextBox.Text;
            _modifiedConfig.ServerMoveDown = ServerMoveDownTextBox.Text;
            _modifiedConfig.AddCurrentChromeDomaintoPAC = AddCurrentChromeDomaintoPACTextBox.Text;
            _modifiedConfig.AddCurrentChromeURLtoPAC = AddCurrentChromeURLtoPACTextBox.Text;
            _modifiedConfig.ScanQR = ScanQRTextBox.Text;
            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            await _controller.SaveHotkeyConfig(_modifiedConfig);
        }



        #region Prepare hotkey

        /// <summary>
        /// Find correct callback and corresponding label
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="cb"></param>
        /// <param name="lb"></param>
        private void PrepareForHotkey(TextBox tb, out HotKeys.HotKeyCallBackHandler cb, out Label lb)
        {
            /*
             * XXX: The labelName, TextBoxName and callbackName
             *      must follow this rule to make use of reflection
             *
             *      <BaseName><Control-Type-Name>
             */
            if (tb == null)
                throw new ArgumentNullException(nameof(tb));

            var pos = tb.Name.LastIndexOf("TextBox", StringComparison.OrdinalIgnoreCase);
            var rawName = tb.Name.Substring(0, pos);
            var labelName = rawName + "Label";
            var callbackName = rawName + "Callback";

            var callback = HotkeyCallbacks.GetCallback(callbackName);
            cb = callback as HotKeys.HotKeyCallBackHandler ?? throw new Exception($"{callbackName} not found");

            var label = GetFieldViaName(GetType(), labelName, this);
            lb = label as Label ?? throw new Exception($"{labelName} not found");
        }

        /// <summary>
        ///fuck
        /// </summary>
        /// <param name="type">from which type</param>
        /// <param name="name">field name</param>
        /// <param name="obj">pass null if static field</param>
        /// <returns></returns>
        private static object GetFieldViaName(Type type, string name, object obj)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));
            // In general, TextBoxes and Labels are private
            FieldInfo fi = type.GetField(name,
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Static);
            return fi == null ? null : fi.GetValue(obj);
        }

        #endregion
    }
}
