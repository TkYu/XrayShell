﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrayShell.Model;
using XrayShell.Properties;
using XrayShell.Services;

namespace XrayShell.View
{
public partial class SubscribeConfigForm : Form
    {
        private XrayShellController _controller;

        // this is a copy of configuration that we are working on
        private Configuration _modifiedConfiguration;
        private int _lastSelectedIndex = -1;
        public SubscribeConfigForm()
        {
            InitializeComponent();
        }

        public SubscribeConfigForm(XrayShellController controller)
        {
            InitializeComponent();
            UpdateTexts();
            Icon = Resources.xray;
            _controller = controller;
            _controller.ConfigChanged += controller_ConfigChanged;
            LoadCurrentConfiguration();
        }

        private void UpdateTexts()
        {
            Font = Global.Font;
            AddButton.Text = I18N.GetString("&Add");
            DeleteButton.Text = I18N.GetString("&Delete");
            SubscribeGroupBox.Text = I18N.GetString("Subscribe");
            NameLabel.Text = I18N.GetString("Subscribe Name");
            UrlLabel.Text = I18N.GetString("Url");
            UseProxyCheckBox.Text = I18N.GetString("UseProxy");
            OKButton.Text = I18N.GetString("OK");
            MyCancelButton.Text = I18N.GetString("Cancel");
            Text = I18N.GetString("Edit Subscriptions");
        }

        private void controller_ConfigChanged(object sender, EventArgs e)
        {
            LoadCurrentConfiguration();
        }

        private void LoadCurrentConfiguration()
        {
            _modifiedConfiguration = _controller.GetConfigurationCopy();
            LoadConfiguration(_modifiedConfiguration);
            _lastSelectedIndex = _modifiedConfiguration.subscribes.Count-1;
            if (_lastSelectedIndex < 0 || _lastSelectedIndex >= SubscribeListBox.Items.Count)
            {
                AddButton_Click(this,null);
                _lastSelectedIndex = 0;
            }
            SubscribeListBox.SelectedIndex = _lastSelectedIndex;
            LoadSelectedItem();
        }
        private void LoadConfiguration(Configuration configuration)
        {
            SubscribeListBox.Items.Clear();
            foreach (SubscribeConfig server in _modifiedConfiguration.subscribes)
            {
                SubscribeListBox.Items.Add(server.name);
            }
        }

        Dictionary<int,string> oldNames = new Dictionary<int, string>();
        private bool SaveOld()
        {
            try
            {
                if (_lastSelectedIndex == -1 || _lastSelectedIndex >= _modifiedConfiguration.subscribes.Count)
                {
                    return true;
                }
                var item = new SubscribeConfig{name = NameTextBox.Text,url = UrlTextBox.Text,useProxy = UseProxyCheckBox.Checked};
                if (!oldNames.ContainsKey(_lastSelectedIndex))
                    oldNames.Add(_lastSelectedIndex, _modifiedConfiguration.subscribes[_lastSelectedIndex].name);
                else
                    oldNames[_lastSelectedIndex] = _modifiedConfiguration.subscribes[_lastSelectedIndex].name;
                _modifiedConfiguration.subscribes[_lastSelectedIndex] = item;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void LoadSelectedItem()
        {
            if (SubscribeListBox.SelectedIndex >= 0 && SubscribeListBox.SelectedIndex < _modifiedConfiguration.subscribes.Count)
            {
                var item = _modifiedConfiguration.subscribes[SubscribeListBox.SelectedIndex];
                NameTextBox.Text = item.name;
                UrlTextBox.Text = item.url;
                UseProxyCheckBox.Checked = item.useProxy;
            }
        }

        private async void OKButton_Click(object sender, EventArgs e)
        {
            if (!SaveOld())
            {
                return;
            }

            OKButton.Enabled = false;
            OKButton.Text = I18N.GetString("Busy...");

            try
            {
                var currentSvc = _controller.GetCurrentServer();
                _controller.SaveSubscribes(_modifiedConfiguration.subscribes);
                if (_lastSelectedIndex > -1)
                {
                    var item = _modifiedConfiguration.subscribes[_lastSelectedIndex];
                    var wc = new WebClient();
                    if (item.useProxy) wc.Proxy = new WebProxy(IPAddress.Loopback.ToString(), _modifiedConfiguration.localPort);
                    var cts = new System.Threading.CancellationTokenSource();
                    // ReSharper disable once AccessToDisposedClosure
                    cts.Token.Register(() => wc?.CancelAsync());
                    cts.CancelAfter(10000);
                    var downloadString = await wc.DownloadStringTaskAsync(item.url);
                    wc.Dispose();
                    var lst = new List<ServerObject>();
                    if (downloadString.Contains("vmess://"))
                    {
                        var mcg = Regex.Matches(downloadString, @"vmess://(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{4})", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
                        foreach (Match s in mcg)
                        {
                            if (ServerObject.TryParse(s.Value, out ServerObject svc))
                            {
                                svc.@group = item.name;
                                lst.Add(svc);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            var debase64 = downloadString.DeBase64();
                            var split = debase64.Split('\r', '\n');
                            foreach (var s in split)
                            {
                                if (ServerObject.TryParse(s, out ServerObject svc))
                                {
                                    svc.@group = item.name;
                                    lst.Add(svc);
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show(I18N.GetString("Invalid Base64 string."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (lst.Any())
                    {
                        _modifiedConfiguration.configs.RemoveAll(c => c.@group == oldNames[_lastSelectedIndex]);
                        _modifiedConfiguration.configs.AddRange(lst);
                    }
                }
                _controller.SaveServers(_modifiedConfiguration.configs, _modifiedConfiguration.localPort, _modifiedConfiguration.corePort);
                var newIdx = _modifiedConfiguration.configs.IndexOf(currentSvc);
                if (newIdx == -1)
                    await _controller.SelectServerIndex(0);
                else if (newIdx != _modifiedConfiguration.index)
                    await _controller.SelectServerIndex(newIdx);
            }
            catch (Exception exception)
            {
                Logging.LogUsefulException(exception);
            }
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var item = new SubscribeConfig{name = I18N.GetString("Subscribe Name")};
            _modifiedConfiguration.subscribes.Add(item);
            LoadConfiguration(_modifiedConfiguration);
            SubscribeListBox.SelectedIndex = _modifiedConfiguration.subscribes.Count - 1;
            _lastSelectedIndex = SubscribeListBox.SelectedIndex;
        }

        private void SubscribeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!SubscribeListBox.CanSelect)
            {
                return;
            }
            if (_lastSelectedIndex == SubscribeListBox.SelectedIndex)
            {
                return;
            }
            if (!SaveOld())
            {
                SubscribeListBox.SelectedIndex = _lastSelectedIndex;
                return;
            }
            if (_lastSelectedIndex >= 0)
            {
                SubscribeListBox.Items[_lastSelectedIndex] = _modifiedConfiguration.subscribes[_lastSelectedIndex].name;
            }
            LoadSelectedItem();
            _lastSelectedIndex = SubscribeListBox.SelectedIndex;
        }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            _lastSelectedIndex = SubscribeListBox.SelectedIndex;
            if (_lastSelectedIndex >= 0 && _lastSelectedIndex < _modifiedConfiguration.subscribes.Count)
            {
                var current = _modifiedConfiguration.subscribes[_lastSelectedIndex];
                _modifiedConfiguration.configs.RemoveAll(c => c.@group == current.name);
                _modifiedConfiguration.subscribes.RemoveAt(_lastSelectedIndex);

            }
            if (_lastSelectedIndex >= _modifiedConfiguration.subscribes.Count)
            {
                _lastSelectedIndex = _modifiedConfiguration.subscribes.Count - 1;
            }
            SubscribeListBox.SelectedIndex = _lastSelectedIndex;
            LoadConfiguration(_modifiedConfiguration);
            SubscribeListBox.SelectedIndex = _lastSelectedIndex;
            LoadSelectedItem();
        }
    }
}
