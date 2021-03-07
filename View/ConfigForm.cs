using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrayShell.Model;
using XrayShell.Properties;
using XrayShell.Services;

namespace XrayShell.View
{
    public partial class ConfigForm : Form
    {
        private XrayShellController controller;

        // this is a copy of configuration that we are working on
        private Configuration _modifiedConfiguration;
        private int _lastSelectedIndex = -1;

        public ConfigForm(XrayShellController controller)
        {
            InitializeComponent();
            PerformLayout();
            UpdateTexts();
            Icon = Resources.xray;
            this.controller = controller;

            ProtocolSelect.DataSource = Xray.OutProtocols;
            FlowSelect.DataSource = Xray.Flows;
            NetworkSelect.DataSource = new[] { "tcp", "kcp", "ws", "http", "domainsocket", "quic" };
            CTSelect.DataSource = new[] { "none", "srtp", "utp", "wechat-video", "dtls", "wireguard" };
            TlsSelect.DataSource = new[] { "none", "tls", "xtls" };

            controller.ConfigChanged += (s, e) =>
            {
                LoadCurrentConfiguration();
            };
            LoadCurrentConfiguration();
        }

        private void UpdateControls()
        {
            ProtocolSelect.Enabled = AddressTextBox.Enabled = ServerPortText.Enabled = IDTextBox.Enabled = RemarksTextBox.Enabled = true;
            switch (ProtocolSelect.SelectedItem.ToString())
            {
                case "vless":
                    PathTextBox.Enabled = HostTextBox.Enabled = TlsSelect.Enabled = FlowSelect.Enabled = SNITextBox.Enabled = SecuritySelect.Enabled = NetworkSelect.Enabled = true;
                    AlterIdText.Enabled = CTSelect.Enabled = false;
                    IDLabel.Text = I18N.GetString("ID");
                    SecurityLabel.Text = I18N.GetString("Security");
                    SecuritySelect.DataSource = new[] { "none" };
                    SecuritySelect.SelectedItem = "none";
                    break;
                case "vmess":
                    PathTextBox.Enabled = AlterIdText.Enabled = CTSelect.Enabled = HostTextBox.Enabled = TlsSelect.Enabled = SecuritySelect.Enabled = NetworkSelect.Enabled = true;
                    FlowSelect.Enabled = SNITextBox.Enabled = false;
                    IDLabel.Text = I18N.GetString("ID");
                    SecurityLabel.Text = I18N.GetString("Security");
                    SecuritySelect.DataSource = Xray.VMessSecurity;
                    SecuritySelect.SelectedItem = "auto";
                    break;
                case "shadowsocks":
                    PathTextBox.Enabled = FlowSelect.Enabled = SNITextBox.Enabled = AlterIdText.Enabled = CTSelect.Enabled = HostTextBox.Enabled = TlsSelect.Enabled = NetworkSelect.Enabled = false;
                    SecuritySelect.Enabled = true;
                    IDLabel.Text = I18N.GetString("Password");
                    SecurityLabel.Text = I18N.GetString("Method");
                    SecuritySelect.DataSource = Xray.SSMethods;
                    break;
                case "trojan":
                    PathTextBox.Enabled = SecuritySelect.Enabled = FlowSelect.Enabled = SNITextBox.Enabled = AlterIdText.Enabled = CTSelect.Enabled = HostTextBox.Enabled = TlsSelect.Enabled = NetworkSelect.Enabled = false;
                    IDLabel.Text = I18N.GetString("Password");
                    SecurityLabel.Text = I18N.GetString("Security");
                    break;
                case "trojan-go":
                    SecuritySelect.Enabled = FlowSelect.Enabled = AlterIdText.Enabled = CTSelect.Enabled = TlsSelect.Enabled = NetworkSelect.Enabled = false;
                    SNITextBox.Enabled = HostTextBox.Enabled = PathTextBox.Enabled = true;
                    IDLabel.Text = I18N.GetString("Password");
                    SecurityLabel.Text = I18N.GetString("Security");
                    break;
            }
        }

        private void UpdateTexts()
        {
            Font = Global.Font;
            AddButton.Text = I18N.GetString("&Add");
            DeleteButton.Text = I18N.GetString("&Delete");
            DuplicateButton.Text = I18N.GetString("Dupli&cate");
            ClipboardButton.Text = I18N.GetString("Cli&pboard");

            ProtocolLabel.Text = I18N.GetString("Protocol");
            AddressLabel.Text = I18N.GetString("Server Address");
            ServerPortLabel.Text = I18N.GetString("Server Port");
            IDLabel.Text = I18N.GetString("ID");
            NetworkLabel.Text = I18N.GetString("Transport Protocol");
            SecurityLabel.Text = I18N.GetString("Security");
            AlterIdLabel.Text = I18N.GetString("AlterId");
            PathLabel.Text = I18N.GetString("Camouflage Path");
            SNILabel.Text = I18N.GetString("TLS Domain");
            HostLabel.Text = I18N.GetString("Camouflage Host");

            CTLabel.Text = I18N.GetString("Camouflage Type");
            TlsLabel.Text = I18N.GetString("Tls");
            FlowLabel.Text = I18N.GetString("Flow");
            RemarksLabel.Text = I18N.GetString("Remarks");

            ProxyPortLabel.Text = I18N.GetString("Proxy Port");
            CorePortLabel.Text = I18N.GetString("Core Port");

            ServerGroupBox.Text = I18N.GetString("Server");

            OKButton.Text = I18N.GetString("OK");
            MyCancelButton.Text = I18N.GetString("Cancel");
            MoveUpButton.Text = I18N.GetString("Move &Up");
            MoveDownButton.Text = I18N.GetString("Move D&own");
            this.Text = I18N.GetString("Edit Servers");
        }

        private void LoadCurrentConfiguration()
        {
            _modifiedConfiguration = controller.GetConfigurationCopy();
            LoadConfiguration(_modifiedConfiguration);
            _lastSelectedIndex = _modifiedConfiguration.index;
            if (_lastSelectedIndex < 0 || _lastSelectedIndex >= ServersListBox.Items.Count)
            {
                _lastSelectedIndex = 0;
            }
            ServersListBox.SelectedIndex = _lastSelectedIndex;
            UpdateMoveUpAndDownButton();
            LoadSelectedServer();
        }

        private void UpdateMoveUpAndDownButton()
        {
            MoveUpButton.Enabled = ServersListBox.SelectedIndex != 0;
            MoveDownButton.Enabled = ServersListBox.SelectedIndex != ServersListBox.Items.Count - 1;
        }

        private void LoadSelectedServer()
        {
            if (ServersListBox.SelectedIndex >= 0 && ServersListBox.SelectedIndex < _modifiedConfiguration.configs.Count)
            {
                var server = _modifiedConfiguration.configs[ServersListBox.SelectedIndex];

                LocalPortNum.Value = _modifiedConfiguration.localPort;
                CorePortNum.Value = _modifiedConfiguration.corePort;

                ProtocolSelect.SelectedItem = server.protocol;

                AddressTextBox.Text = server.address;
                ServerPortText.Text = server.port.ToString();
                IDTextBox.Text = server.id;
                NetworkSelect.SelectedItem = server.network;
                SecuritySelect.SelectedItem = server.security ?? (server.protocol == "vmess" ? "aauto" : "none");
                AlterIdText.Text = server.alterId.ToString();
                PathTextBox.Text = server.path;
                SNITextBox.Text = server.sni;
                HostTextBox.Text = server.host;
                CTSelect.SelectedItem = server.type;
                TlsSelect.SelectedItem = server.tls;
                FlowSelect.SelectedItem = server.flow;
                RemarksTextBox.Text = server.ps;
            }
        }

        private void MoveConfigItem(int step)
        {
            int index = ServersListBox.SelectedIndex;
            var server = _modifiedConfiguration.configs[index];
            object item = ServersListBox.Items[index];

            _modifiedConfiguration.configs.Remove(server);
            _modifiedConfiguration.configs.Insert(index + step, server);
            _modifiedConfiguration.index += step;

            ServersListBox.BeginUpdate();
            ServersListBox.Enabled = false;
            _lastSelectedIndex = index + step;
            ServersListBox.Items.Remove(item);
            ServersListBox.Items.Insert(index + step, item);
            ServersListBox.Enabled = true;
            ServersListBox.SelectedIndex = index + step;
            ServersListBox.EndUpdate();

            UpdateMoveUpAndDownButton();
        }

        private bool SaveOldSelectedServer()
        {
            try
            {
                if (_lastSelectedIndex == -1 || _lastSelectedIndex >= _modifiedConfiguration.configs.Count)
                {
                    return true;
                }
                var server = new ServerObject();

                //if (Uri.CheckHostName(server.add = IPTextBox.Text.Trim()) == UriHostNameType.Unknown)
                //{
                //    MessageBox.Show(I18N.GetString("Invalid server address"));
                //    IPTextBox.Focus();
                //    return false;
                //}

                var old = _modifiedConfiguration.configs[_lastSelectedIndex];

                server.protocol = ProtocolSelect.SelectedItem.ToString();
                server.address = AddressTextBox.Text;
                if (int.TryParse(ServerPortText.Text, out var port) && port > 0 && port < 65536)
                {
                    server.port = port;
                }
                else
                {
                    MessageBox.Show(I18N.GetString("Port out of range"));
                    ServerPortText.Focus();
                    return false;
                }
                server.id = IDTextBox.Text;
                server.ps = RemarksTextBox.Text;

                switch (server.protocol)
                {
                    case "vless":
                        server.path = PathTextBox.Text;
                        server.host = HostTextBox.Text;
                        server.tls = TlsSelect.SelectedItem.ToString();
                        server.flow = FlowSelect.SelectedItem.ToString();
                        server.sni = SNITextBox.Text;
                        server.security = SecuritySelect.SelectedItem.ToString();
                        server.network = NetworkSelect.SelectedItem.ToString();
                        break;
                    case "vmess":
                        if (int.TryParse(AlterIdText.Text, out var alterid) && alterid >= 0 && alterid < 65536)
                        {
                            server.alterId = alterid;
                        }
                        else
                        {
                            MessageBox.Show(I18N.GetString("AlterId out of range"));
                            ServerPortText.Focus();
                            return false;
                        }
                        server.type = CTSelect.SelectedItem.ToString();
                        server.path = PathTextBox.Text;
                        server.host = HostTextBox.Text;
                        server.tls = TlsSelect.SelectedItem.ToString();
                        server.security = SecuritySelect.SelectedItem?.ToString()??"auto";
                        server.network = NetworkSelect.SelectedItem.ToString();
                        break;
                    case "shadowsocks":
                        server.security = SecuritySelect.SelectedItem.ToString();
                        break;
                    case "trojan":
                        break;
                    case "trojan-go":
                        server.path = PathTextBox.Text;
                        server.host = HostTextBox.Text;
                        server.sni = SNITextBox.Text;
                        break;
                }


                var localPort = (int) LocalPortNum.Value;
                var corePort = (int) CorePortNum.Value;
                //Configuration.CheckServer(server.add);
                Configuration.CheckLocalPort(localPort);

                if (old != null) server.group = old.group;

                _modifiedConfiguration.configs[_lastSelectedIndex] = server;
                _modifiedConfiguration.localPort = localPort;
                _modifiedConfiguration.corePort = corePort;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void LoadConfiguration(Configuration configuration)
        {
            ServersListBox.Items.Clear();
            foreach (var server in _modifiedConfiguration.configs)
            {
                ServersListBox.Items.Add(server.ps);
            }
        }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClipboardButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            var txt = Clipboard.GetText(TextDataFormat.Text);
            if (ServerObject.TryParse(txt, out var saba))
            {
                saba.group = null;
                _modifiedConfiguration.configs.Add(saba);
                LoadConfiguration(_modifiedConfiguration);
                ServersListBox.SelectedIndex = _modifiedConfiguration.configs.Count - 1;
                _lastSelectedIndex = ServersListBox.SelectedIndex;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            _lastSelectedIndex = ServersListBox.SelectedIndex;
            if (_lastSelectedIndex >= 0 && _lastSelectedIndex < _modifiedConfiguration.configs.Count)
            {
                _modifiedConfiguration.configs.RemoveAt(_lastSelectedIndex);
            }
            if (_lastSelectedIndex >= _modifiedConfiguration.configs.Count)
            {
                _lastSelectedIndex = _modifiedConfiguration.configs.Count - 1;
            }
            ServersListBox.SelectedIndex = _lastSelectedIndex;
            LoadConfiguration(_modifiedConfiguration);
            ServersListBox.SelectedIndex = _lastSelectedIndex;
            LoadSelectedServer();
        }

        private void ServersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ServersListBox.CanSelect)
            {
                return;
            }
            if (_lastSelectedIndex == ServersListBox.SelectedIndex)
            {
                // we are moving back to oldSelectedIndex or doing a force move
                return;
            }
            if (!SaveOldSelectedServer())
            {
                // why this won't cause stack overflow?
                ServersListBox.SelectedIndex = _lastSelectedIndex;
                return;
            }
            if (_lastSelectedIndex >= 0)
            {
                ServersListBox.Items[_lastSelectedIndex] = _modifiedConfiguration.configs[_lastSelectedIndex].ps;
            }
            UpdateMoveUpAndDownButton();
            LoadSelectedServer();
            _lastSelectedIndex = ServersListBox.SelectedIndex;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            var server = Configuration.GetDefaultServer();
            _modifiedConfiguration.configs.Add(server);
            LoadConfiguration(_modifiedConfiguration);
            ServersListBox.SelectedIndex = _modifiedConfiguration.configs.Count - 1;
            _lastSelectedIndex = ServersListBox.SelectedIndex;
        }

        private void DuplicateButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            var currServer = _modifiedConfiguration.configs[_lastSelectedIndex];
            var currIndex = _modifiedConfiguration.configs.IndexOf(currServer);
            _modifiedConfiguration.configs.Insert(currIndex + 1, currServer);
            LoadConfiguration(_modifiedConfiguration);
            ServersListBox.SelectedIndex = currIndex + 1;
            _lastSelectedIndex = ServersListBox.SelectedIndex;
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            if (ServersListBox.SelectedIndex > 0)
            {
                MoveConfigItem(-1);  // -1 means move backward
            }
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            if (ServersListBox.SelectedIndex < ServersListBox.Items.Count - 1)
            {
                MoveConfigItem(+1);  // +1 means move forward
            }
        }

        private async void OKButton_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            if (_modifiedConfiguration.configs.Count == 0)
            {
                MessageBox.Show(I18N.GetString("Please add at least one server"));
                return;
            }

            OKButton.Enabled = false;
            OKButton.Text = I18N.GetString("Busy...");
            controller.SaveServers(_modifiedConfiguration.configs, _modifiedConfiguration.localPort,_modifiedConfiguration.corePort);
            await controller.SelectServerIndex(ServersListBox.SelectedIndex);
            Close();
        }

        private void OnlyAllowDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ProtocolSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void TlsSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TlsSelect.SelectedItem?.ToString() == "xtls")
            {
                FlowSelect.Enabled = true;
                FlowSelect.DataSource = Xray.Flows;
            }
            else
            {
                FlowSelect.DataSource = new[] {""};
                FlowSelect.Enabled = false;
            }
        }
    }
}
