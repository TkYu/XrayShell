using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace XrayShell
{
    public static class Xray
    {
        #region Props

        public static readonly string[] Flows = {
            "xtls-rprx-origin",//最初的流控模式。该模式纪念价值大于实际使用价值
            "xtls-rprx-origin-udp443",//同 xtls-rprx-origin, 但放行了目标为 443 端口的 UDP 流量
            "xtls-rprx-direct",//所有平台皆可使用的典型流控模式
            "xtls-rprx-direct-udp443",//同 xtls-rprx-direct, 但是放行了目标为 443 端口的 UDP 流量
            "xtls-rprx-splice",//Linux 平台下最建议使用的流控模式
            "xtls-rprx-splice-udp443"//同 xtls-rprx-splice, 但是放行了目标为 443 端口的 UDP 流量
        };

        public static readonly string[] VMessSecurity = {
            "aes-128-gcm",//推荐在 PC 上使用
            "chacha20-poly1305",//推荐在手机端使用
            "auto",//默认值，自动选择（运行框架为 AMD64、ARM64 或 s390x 时为 aes-128-gcm 加密方式，其他情况则为 Chacha20-Poly1305 加密方式）
            "none"//不加密
        };

        public static readonly string[] SSMethods = {
            "AES-256-GCM",
            "AES-128-GCM",
            "ChaCha20-Poly1305",
            "ChaCha20-IETF-Poly1305",//ChaCha20-Poly1305的别称
            //不推荐
            "AES-256-CFB",
            "AES-128-CFB",
            "ChaCha20",
            "ChaCha20-IETF"
            //不允许
            //"plain",
            //"none"
        };

        public static readonly string[] QuicCamouflageTypes = {
            "none",//默认值，不进行伪装，发送的数据是没有特征的数据包。
            "srtp",//伪装成 SRTP 数据包，会被识别为视频通话数据（如 FaceTime）。
            "utp",//伪装成 uTP 数据包，会被识别为 BT 下载数据。
            "wechat-video",//伪装成微信视频通话的数据包。
            "dtls",//伪装成 DTLS 1.2 数据包。
            "wireguard"//伪装成 WireGuard 数据包。（并不是真正的 WireGuard 协议）
        };

        public static readonly string[] OutProtocols = {
            //"blackhole",//Blackhole（黑洞）是一个出站数据协议，它会阻碍所有数据的出站，配合 路由配置 一起使用，可以达到禁止访问某些网站的效果。
            //"dns",//DNS 是一个出站协议，主要用于拦截和转发 DNS 查询。
            //"freedom",//Freedom 是一个出站协议，可以用来向任意网络发送（正常的） TCP 或 UDP 数据。
            //"http",//http 协议没有对传输加密，不适宜经公网中传输。
            //"socks",//socks 协议没有对传输加密，不适宜经公网中传输。
            "vless",//VLESS 是一个无状态的轻量传输协议，它分为入站和出站两部分，可以作为 Xray 客户端和服务器之间的桥梁。
            "vmess",//VMess 是一个加密传输协议，通常作为 Xray 客户端和服务器之间的桥梁。
            "trojan",//Trojan 被设计工作在正确配置的加密 TLS 隧道。
            "trojan-go",//Trojan的超集。
            "shadowsocks"//Shadowsocks 协议，兼容大部分其它版本的实现。
        };


        public const string XRAY_CORE = "xray.exe";
        public const string XRAY_CORE_WITHOUTEXT = "xray";


        private static readonly Regex _regexVersion = new Regex(@"Xray\s+(\d+).(\d+).(\d+)", RegexOptions.IgnoreCase);

        public static bool CoreExsis => File.Exists(XRAY_CORE);

        public static Version Version
        {
            get
            {
                if (!CoreExsis) return null;
                var run = Utils.StartProcess(XRAY_CORE, "version");
                if (string.IsNullOrEmpty(run)) return null;
                var match = _regexVersion.Match(run);
                return match.Success ? new Version(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value)) : null;
            }
        }
        #endregion

        #region Methods


        #endregion

        #region Template

        public static readonly string AccessLogPath = Utils.GetTempPath("XrayShell_Core_Access.log");

        public static readonly string ErrorLogPath = Utils.GetTempPath("XrayShell_Core_Error.log");

        public static string GenerateVNextConf(ServerObject svc, int listenPort, string listenAddr = "127.0.0.1", bool sniffing = true)
        {
            var sso = new StreamSettingsObject {network = svc.network, security = string.IsNullOrEmpty(svc.tls) ? "none" : svc.tls};
            switch (svc.network)
            {
                //“tcp” | “kcp” | “ws” | “http” | “domainsocket” | “quic”
                // 这里先只支持到tcp/ws/http
                case "tcp":
                    sso.tcpSettings = new TcpObject { type = svc.type ?? "none" };//先暂时不支持http伪装
                    break;
                case "ws":
                    sso.wsSettings = new WebSocketObject {path = svc.path ?? "/", headers = new Dictionary<string, string> {{"Host", string.IsNullOrEmpty(svc.host) ? svc.address : svc.host } }};
                    break;
                case "http":
                    sso.httpSettings = new HttpObject {path = svc.path ?? "/", host = svc.host.Split(',')};
                    break;
                //先暂时不支持
                //case "kcp":
                //    sso.kcpSettings = new KcpObject();
                //    break;
                //先暂时不支持
                //case "quic":
                //    sso.quicSettings = new QuicObject();
                //    break;
            }

            if (svc.security == "xtls")
                sso.xtlsSettings = new TLSObject
                {
                    serverName = string.IsNullOrEmpty(svc.sni) ? (string.IsNullOrEmpty(svc.host) ? svc.address : svc.host) : svc.sni,
                    allowInsecure = false,
                    alpn = new []{ "h2", "http/1.1" },
                    minVersion= "1.2",
                    maxVersion= "1.3"
                };
            else if (svc.security == "tls")
                sso.tlsSettings = new TLSObject
                {
                    serverName = string.IsNullOrEmpty(svc.sni) ? (string.IsNullOrEmpty(svc.host) ? svc.address : svc.host) : svc.sni,
                    allowInsecure = false,
                    alpn = new[] { "h2", "http/1.1" },
                    minVersion = "1.2",
                    maxVersion = "1.3"
                };

            var outbound = new OutboundObject
            {
                protocol = svc.protocol,
                mux = new MuxObject { enabled = false, concurrency = 8 },
                tag = "outBound_PROXY",
                streamSettings = sso
            };

            switch (svc.protocol)
            {
                case "vless":
                    outbound.settings = new VlessOutboundConfigurationObject
                    {
                        vnext = new []{new VlessConfigurationObject
                        {
                            address = svc.address,
                            port = svc.port,
                            users = new []{new VlessUser
                            {
                                id = svc.id,
                                //需要填 "none"，不能留空。该要求是为了提醒使用者没有加密，也为了以后出加密方式时，防止使用者填错属性名或填错位置导致裸奔。
                                encryption = "none",
                                //目前 XTLS 仅支持 TCP、mKCP、DomainSocket 这三种传输方式
                                flow = svc.flow,
                                level = 0
                            }}
                        }}
                    };
                    break;
                case "vmess":
                    outbound.settings = new VmessOutboundConfigurationObject
                    {
                        vnext = new []{new VmessConfigurationObject
                        {
                            address = svc.address,
                            port = svc.port,
                            users = new []{new VmessUser
                            {
                                alterId = svc.alterId,
                                id = svc.id,
                                level = 0,
                                security = svc.security
                            }}
                        }}
                    };
                    break;
                case "trojan":
                    outbound.settings = new TrojanOutboundConfigurationObject
                    {
                        servers = new[]{new TrojanConfigurationObject
                        {
                            address = svc.address,
                            port = svc.port,
                            password = svc.id,
                            level = 0
                        }}
                    };
                    outbound.streamSettings.network = "tcp";
                    outbound.streamSettings.security = "tls";
                    break;
                case "trojan-go":
                    outbound.settings = new TrojanOutboundConfigurationObject
                    {
                        servers = new []{new TrojanConfigurationObject
                        {
                            address = svc.address,
                            port = svc.port,
                            password = svc.id,
                            //email = svc.email,//可选值，先不做配置
                            flow = svc.flow,
                            level = 0
                        }}
                    };
                    break;
                case "shadowsocks":
                    outbound.settings = new ShadowsocksOutboundConfigurationObject
                    {
                        servers = new []{new ShadowsocksConfigurationObject
                        {
                            address = svc.address,
                            port = svc.port,
                            password = svc.id,
                            //email = svc.email,//可选值，先不做配置
                            method = svc.security,
                            level = 0
                        }}
                    };
                    break;
            }

            var inbound = $@"{{""port"":{listenPort},""listen"":""{listenAddr}"",""protocol"":""socks"",""settings"":{{""auth"":""noauth"",""udp"":true,""ip"":""{listenAddr}"",""clients"":null}},""streamSettings"":{{}},""sniffing"":{{""enabled"":{(sniffing ? "true" : "false")},""destOverride"":[""http"", ""tls""]}}}}";
            return $@"{{""log"":{{""access"":""{AccessLogPath.Replace("\\", @"\\")}"",""error"":""{ErrorLogPath.Replace("\\", @"\\")}"",""loglevel"":""warning""}},""inbounds"":[{inbound}],""outbounds"":[{outbound.SerializeToJson()}],""routing"":{{""domainStrategy"":""IPOnDemand"",""rules"":[{{""type"":""field"",""ip"":[""0.0.0.0/8"",""10.0.0.0/8"",""100.64.0.0/10"",""127.0.0.0/8"",""169.254.0.0/16"",""172.16.0.0/12"",""192.0.0.0/24"",""192.0.2.0/24"",""192.168.0.0/16"",""198.18.0.0/15"",""198.51.100.0/24"",""203.0.113.0/24"",""::1/128"",""fc00::/7"",""fe80::/10""],""outboundTag"":""direct""}}]}},""dns"":{{""hosts"":{{}},""servers"":[""223.5.5.5"",""114.114.114.114"",""localhost""]}},""policy"":{{""system"":{{""statsInboundUplink"":false,""statsInboundDownlink"":false}}}},""other"":{{}}}}";
        }

        #endregion
    }

    #region Config

    public class ServerObject
    {
        public ServerObject()
        {
        }

        public string ToURL()
        {
            switch (protocol)
            {
                case "vless":
                    return new Vless(this).ToString();
                case "vmess":
                    return new Vmess(this).ToString();
                case "shadowsocks":
                    return new Shadowsocks(this).ToString();
                case "trojan":
                    return new Trojan(this).ToString();
                case "trojan-go":
                    return new TrojanGo(this).ToString();
                default:
                    return string.Empty;
            }
        }

        public static List<ServerObject> GetServers(string content)
        {
            var serverUrls = content.Contains("://") ?
                content.Split('\r', '\n') :
                Encoding.UTF8.GetString(Convert.FromBase64String(content)).Split('\r', '\n');
            var servers = new List<ServerObject>();
            foreach (var serverUrl in serverUrls)
            {
                if (TryParse(serverUrl, out var saba))
                    servers.Add(saba);
            }
            return servers;
        }

        public static bool TryParse(string input, out ServerObject svc)
        {
            if (!input.Contains("://")) { svc = null; return false; }
            var proto = input.Substring(0, input.IndexOf("://", StringComparison.Ordinal)).ToLower();
            try
            {
                switch (proto)
                {
                    case "vless":
                        if (Vless.TryParse(input, out var vless))
                        {
                            svc = vless.ToOutbound();
                            return true;
                        }
                        break;
                    case "vmess":
                        if (Vmess.TryParse(input, out var vmess))
                        {
                            svc = vmess.ToOutbound();
                            return true;
                        }
                        break;
                    case "trojan":
                        if (Trojan.TryParse(input, out var trojan))
                        {
                            svc = trojan.ToOutbound();
                            return true;
                        }
                        break;
                    case "trojan-go":
                        if (TrojanGo.TryParse(input, out var trojango))
                        {
                            svc = trojango.ToOutbound();
                            return true;
                        }
                        break;
                    case "ss":
                        if (Shadowsocks.TryParse(input, out var ss))
                        {
                            svc = ss.ToOutbound();
                            return true;
                        }
                        break;
                }
                svc = null;
                return false;
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                svc = null;
                return false;
            }
        }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// UUID或密码，v2ray就指定UUID，shadowsocks在这里指定密码
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 网络协议可以改为 "tcp", "ws" 等
        /// </summary>
        public string network { get; set; }
        /// <summary>
        /// 加密方法，vmess 默认为 auto, vless 默认为 none, shadowsocks也可以在这里指定加密方法，例如:"aes-256-gcm"
        /// </summary>
        public string security { get; set; }
        /// <summary>
        /// 协议可选 "vless","vmess","shadowsocks","trojan"
        /// </summary>
        public string protocol { get; set; }
        /// <summary>
        /// 伪装主机域名，例如ws协议通过这个指定HTTP头里的host字段
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// TLS域名，不指定使用host的值，如果host也没指定就使用 address的值
        /// </summary>
        public string sni { get; set; }
        /// <summary>
        /// 值可以指定为 "","tls","xtls"
        /// </summary>
        public string tls { get; set; }
        /// <summary>
        /// 流控，xtls需要用到的字段，可以不指定使用默认值 "xtls-rprx-direct"
        /// </summary>
        public string flow { get; set; }
        /// <summary>
        /// vmess用
        /// </summary>
        public int alterId { get; set; }
        /// <summary>
        /// 伪装路径
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 伪装类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string ps { get; set; }
        ///// <summary>
        ///// 是否允许不安全连接，目前先强制写为false
        ///// </summary>
        //public bool allowInsecure { get; set; }


        /// <summary>
        /// 分组，默认空
        /// </summary>
        public string group { get; set; } = "";
    }

    public class Vless
    {
        public Vless()
        {
        }

        public Vless(ServerObject outbound)
        {
            address = outbound.address;
            id = outbound.id;
            port = outbound.port;
            network = outbound.network;
            path = outbound.path;
            host = outbound.host;
            tls = outbound.tls;
            sni = outbound.sni;
            flow = outbound.flow;
            security = outbound.security;
            ps = outbound.ps;
        }

        public static bool TryParse(string input, out Vless svc)
        {
            if (!input.StartsWith("vless://", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Invalid");
            try
            {
                var uri = new Uri(input);
                svc = new Vless { address = uri.Host, port = uri.Port, id = uri.UserInfo };
                if (!string.IsNullOrEmpty(uri.Fragment)) svc.ps = Uri.UnescapeDataString(uri.Fragment.TrimStart('#'));
                svc.network = "tcp";
                svc.tls = "tls";
                if (!string.IsNullOrEmpty(uri.Query))
                {
                    var queryParameters = System.Web.HttpUtility.ParseQueryString(uri.Query);
                    if (queryParameters["sni"] != null) svc.sni = queryParameters["sni"];
                    if (queryParameters["host"] != null) svc.host = queryParameters["host"];
                    if (queryParameters["path"] != null) svc.path = queryParameters["path"];
                    if (queryParameters["network"] != null) svc.network = queryParameters["network"];
                    if (queryParameters["security"] != null) svc.security = queryParameters["security"];
                    if (queryParameters["tls"] != null) svc.tls = queryParameters["tls"];
                    if (queryParameters["flow"] != null) svc.flow = queryParameters["flow"];
                }

                return true;
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                svc = null;
                return false;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder("vless://" + id + "@" + address + ":" + port + "/?");
            sb.Append($"allowInsecure={allowInsecure.ToString().ToLower()}&host={host}&path={path}");
            if (!string.IsNullOrEmpty(network) && network != "tcp") sb.Append($"&network={network}");
            if (!string.IsNullOrEmpty(security) && security != "none") sb.Append($"&network={network}");
            if (!string.IsNullOrEmpty(tls)) sb.Append($"&tls={tls}");
            if (!string.IsNullOrEmpty(flow)) sb.Append($"&flow={flow}");
            //if (!string.IsNullOrEmpty(alpn)) sb.Append($"&alpn={alpn}");
            //if (disableSessionResumption) sb.Append($"&disableSessionResumption={disableSessionResumption.ToString().ToLower()}");
            if (!string.IsNullOrEmpty(sni)) sb.Append($"&sni={sni}");
            if (!string.IsNullOrEmpty(ps)) sb.Append($"#{Uri.EscapeUriString(ps)}");
            return sb.ToString();
        }

        public ServerObject ToOutbound()
        {
            return new ServerObject
            {
                address = address,
                port = port,
                id = id,
                ps = ps,
                path = path,
                host = host,
                tls = tls,
                flow = flow,
                sni = sni,
                security = security,
                network = network,
                protocol = "vless"
            };
        }

        public string address { get; set; }
        public int port { get; set; }
        public string id { get; set; }
        public string network { get; set; }
        public string security { get; set; }
        public string host { get; set; }
        public string sni { get; set; }
        public string tls { get; set; }
        public string flow { get; set; }
        public string path { get; set; }
        public string ps { get; set; }
        public bool allowInsecure { get; set; }
    }

    public class Vmess
    {
        public Vmess()
        {
        }

        public Vmess(ServerObject outbound)
        {
            v = "2";
            add = outbound.address;
            aid = outbound.alterId;
            id = outbound.id;
            port = outbound.port;
            net = outbound.network;
            type = outbound.type;
            path = outbound.path;
            host = outbound.host;
            tls = outbound.tls;
            security = outbound.security;
            ps = outbound.ps;
        }

        public static bool TryParse(string input, out Vmess svc)
        {
            if (input.StartsWith("vmess://", StringComparison.OrdinalIgnoreCase))
                input = input.Remove(0, 8);
            try
            {
                var de64 = Encoding.UTF8.GetString(Convert.FromBase64String(input));
                svc = Utils.DeSerializeJsonObject<Vmess>(de64);
                return true;
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                svc = null;
                return false;
            }
        }

        public override string ToString()
        {
            return $"vmess://{Convert.ToBase64String(Encoding.UTF8.GetBytes(this.SerializeToJson()))}";
        }

        public ServerObject ToOutbound()
        {
            return new ServerObject
            {
                address = add,
                port = port,
                id = id,
                alterId = aid,
                ps = ps,
                type = type,
                path = path,
                host = host,
                tls = tls,
                network = net,
                security = security,
                protocol = "vmess"
            };
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string v { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ps { get; set; }
        /// <summary>
        /// 远程服务器地址
        /// </summary>
        public string add { get; set; }
        /// <summary>
        /// 远程服务器端口
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// 远程服务器ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 远程服务器额外ID
        /// </summary>
        public int aid { get; set; }
        /// <summary>
        /// 传输协议tcp,kcp,ws
        /// </summary>
        public string net { get; set; }
        /// <summary>
        /// 伪装类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 伪装的域名
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// path
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 底层传输安全
        /// </summary>
        public string tls { get; set; }
        /// <summary>
        /// 最好使用默认的auto
        /// </summary>
        public string security { get; set; }
    }

    public class Shadowsocks
    {
        public Shadowsocks()
        {
        }
        public Shadowsocks(ServerObject outbound)
        {
            server = outbound.address;
            server_port = outbound.port;
            password = outbound.id;
            method = outbound.security;
            remarks = outbound.ps;
        }
        private static readonly Regex
            UriFinder = new Regex(@"ss://(?<password>\w+)@(?<domain>.*?):(?<port>\d+)"),
            UrlFinder = new Regex(@"ss://(?<base64>[A-Za-z0-9+-/=_]+)(?:#(?<tag>\S+))?", RegexOptions.IgnoreCase),
            DetailsParser = new Regex(@"^((?<method>.+?):(?<password>.*)@(?<hostname>.+?):(?<port>\d+?))$", RegexOptions.IgnoreCase);
        public static bool TryParse(string input, out Shadowsocks svc)
        {
            if (!input.StartsWith("ss://", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Invalid");
            try
            {
                svc = new Shadowsocks();
                if (UriFinder.IsMatch(input))
                {//new format
                    Uri parsedUrl;
                    try
                    {
                        parsedUrl = new Uri(input);
                    }
                    catch (UriFormatException)
                    {
                        return false;
                    }
                    svc.remarks = parsedUrl.GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
                    svc.server = parsedUrl.GetComponents(UriComponents.Host, UriFormat.Unescaped);
                    svc.server_port = parsedUrl.Port;
                    var rawUserInfo = parsedUrl.GetComponents(UriComponents.UserInfo, UriFormat.Unescaped);
                    var base64 = rawUserInfo.Replace('-', '+').Replace('_', '/');    // Web-safe base64 to normal base64
                    string userInfo;
                    try
                    {
                        userInfo = base64.DeBase64();
                    }
                    catch (FormatException)
                    {
                        return false;
                    }
                    var userInfoParts = userInfo.Split(new[] { ':' }, 2);
                    if (userInfoParts.Length != 2) return false;
                    svc.method = userInfoParts[0];
                    svc.password = userInfoParts[1];
                    var queryParameters = System.Web.HttpUtility.ParseQueryString(parsedUrl.Query);
                    var pluginParts = (queryParameters["plugin"] ?? "").Split(new[] { ';' }, 2);
                    if (pluginParts.Length > 0) svc.plugin = pluginParts[0] ?? "";
                    if (pluginParts.Length > 1) svc.plugin_opts = pluginParts[1] ?? "";
                    //svc.timeout = 30;
                    return true;
                }
                else
                {
                    var matchOld = UrlFinder.Match(input);
                    if (matchOld.Success)
                    {
                        var base64 = matchOld.Groups["base64"].Value.TrimEnd('/');
                        var tag = matchOld.Groups["tag"].Value;
                        if (!string.IsNullOrEmpty(tag))
                        {
                            svc.remarks = Uri.UnescapeDataString(tag);
                        }
                        Match details;
                        try
                        {
                            details = DetailsParser.Match(base64.DeBase64());
                        }
                        catch (FormatException)
                        {
                            throw new Exception("Invalid");
                        }
                        if (!details.Success)
                            throw new Exception("Invalid");
                        svc.method = details.Groups["method"].Value;
                        svc.password = details.Groups["password"].Value;
                        svc.server = details.Groups["hostname"].Value;
                        svc.server_port = int.Parse(details.Groups["port"].Value);
                        //svc.timeout = 30;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                svc = null;
                return false;
            }
        }
        public override string ToString()
        {
            string tag = string.Empty;
            string url;
            if (string.IsNullOrWhiteSpace(plugin))
            {
                string parts = $"{method}:{password}@{server}:{server_port}";
                string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(parts));
                url = base64;
            }
            else
            {
                // SIP002
                string parts = $"{method}:{password}";
                string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(parts));
                string websafeBase64 = base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');

                string pluginPart = plugin;
                if (!string.IsNullOrWhiteSpace(plugin_opts)) pluginPart += ";" + plugin_opts;

                url = $"{websafeBase64}@{Uri.EscapeDataString(server)}:{server_port}/?plugin={Uri.EscapeDataString(pluginPart)}";
            }

            if (!string.IsNullOrEmpty(remarks))
            {
                tag = $"#{Uri.EscapeUriString(remarks)}";
            }
            return $"ss://{url}{tag}";
        }
        public ServerObject ToOutbound()
        {
            return new ServerObject
            {
                address = server,
                port = server_port,
                security = method,
                id = password,
                network = "tcp",
                ps = remarks,
                protocol = "shadowsocks"
            };
        }
        public string server { get; set; }
        public int server_port { get; set; }
        public string password { get; set; }
        public string method { get; set; }
        public string plugin { get; set; }
        public string plugin_opts { get; set; }
        public string plugin_args { get; set; }
        public string remarks { get; set; }
        public int timeout { get; set; }
    }

    public class Trojan
    {
        public Trojan()
        {
        }

        public Trojan(ServerObject outbound)
        {
            server = outbound.address;
            server_port = outbound.port;
            password = outbound.id;
            remarks = outbound.ps;
        }

        public static bool TryParse(string input, out Trojan svc)
        {
            if (!input.StartsWith("trojan://", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Invalid");
            try
            {
                var uri = new Uri(input);
                svc = new Trojan { server = uri.Host, server_port = uri.Port, password = uri.UserInfo };
                if (!string.IsNullOrEmpty(uri.Fragment)) svc.remarks = Uri.UnescapeDataString(uri.Fragment.TrimStart('#'));
                if (!string.IsNullOrEmpty(uri.Query))
                {
                    //var queryParameters = System.Web.HttpUtility.ParseQueryString(uri.Query);
                    //if (queryParameters["peer"] != null)
                    //    svc.server = queryParameters["peer"];
                }

                return true;
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                svc = null;
                return false;
            }
        }

        public override string ToString()
        {
            //[offcial](https://github.com/trojan-gfw/trojan-url): trojan://password@remote_host:remote_port
            //west: trojan://密码@节点IP:端口?allowInsecure=1&peer=节点域名#节点备注
            return $"trojan://{password}@{server}:{server_port}?allowInsecure=0#{Uri.EscapeDataString(remarks)}";//默认用域名，验证证书
        }

        public ServerObject ToOutbound()
        {
            return new ServerObject
            {
                address = server,
                port = server_port,
                id = password,
                ps = remarks,
                network = "tcp",
                protocol = "trojan"
            };
        }

        public string server { get; set; }
        public int server_port { get; set; }
        public string password { get; set; }
        public string remarks { get; set; }
        //public bool fast_open { get; set; }
    }

    public class TrojanGo
    {
        public TrojanGo()
        {
        }

        public TrojanGo(ServerObject outbound)
        {
            server = outbound.address;
            server_port = outbound.port;
            password = outbound.id;
            remarks = outbound.ps;

            if (outbound.network != "tcp") type = outbound.network;
            if (!string.IsNullOrEmpty(outbound.sni)) sni = outbound.sni;
            host = outbound.host;
            path = outbound.path;
        }

        public static bool TryParse(string input, out TrojanGo svc)
        {
            if (!input.StartsWith("trojan-go://", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Invalid");
            try
            {
                var uri = new Uri(input);
                svc = new TrojanGo { server = uri.Host, server_port = uri.Port, password = uri.UserInfo };
                if (!string.IsNullOrEmpty(uri.Fragment)) svc.remarks = Uri.UnescapeDataString(uri.Fragment.TrimStart('#'));
                if (!string.IsNullOrEmpty(uri.Query))
                {
                    var queryParameters = System.Web.HttpUtility.ParseQueryString(uri.Query);
                    if (queryParameters["peer"] != null)
                        svc.sni = queryParameters["peer"];
                    if (queryParameters["sni"] != null)
                        svc.sni = queryParameters["sni"];
                    if (queryParameters["host"] != null)
                        svc.host = queryParameters["host"];
                    if (queryParameters["path"] != null)
                        svc.path = queryParameters["path"];
                    if (queryParameters["type"] != null && queryParameters["type"] != "original")
                    {
                        svc.type = queryParameters["type"];
                        if (svc.type == "h2") svc.type = "http";
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                svc = null;
                return false;
            }
        }

        public override string ToString()
        {
            //trojan-go://password@cloudflare.com/?type=ws&path=%2Fpath&host=your-site.com
            return
                $"trojan-go://{password}@{server}:{server_port}?allowInsecure=0{(string.IsNullOrEmpty(type) ? "" : "&type=" + type)}{(string.IsNullOrEmpty(sni) ? "" : "&sni=" + sni)}{(string.IsNullOrEmpty(host) ? "" : "&host=" + host)}{(string.IsNullOrEmpty(path) ? "" : "&path=" + path)}#{Uri.EscapeDataString(remarks)}"; //默认用域名，验证证书
        }

        public ServerObject ToOutbound()
        {
            return new ServerObject
            {
                address = server,
                port = server_port,
                id = password,
                ps = remarks,
                network = "tcp",
                protocol = "trojan-go",
                sni = sni,
                host = host,
                path = path
            };
        }

        public string server { get; set; }
        public int server_port { get; set; }
        public string password { get; set; }
        public string remarks { get; set; }

        public string type { get; set; }
        public string sni { get; set; }
        public string host { get; set; }
        public string path { get; set; }
    }

    public class OutboundObject
    {
        /// <summary>
        /// 用于发送数据的 IP 地址，当主机有多个 IP 地址时有效，默认值为"0.0.0.0"。
        /// </summary>
        public string sendThrough { get; set; } = "0.0.0.0";
        public string protocol { get; set; }
        public object settings { get; set; }
        public string tag { get; set; }
        public StreamSettingsObject streamSettings { get; set; }

        //public Proxysettings proxySettings { get; set; }
        public MuxObject mux { get; set; }
    }

    public class VlessOutboundConfigurationObject
    {
        public VlessConfigurationObject[] vnext { get; set; }
    }
    public class VlessConfigurationObject
    {
        public string address { get; set; }
        public int port { get; set; }
        public VlessUser[] users { get; set; }
    }
    public class VlessUser
    {
        public string id { get; set; }
        public string encryption { get; set; }
        public string flow { get; set; }
        public int level { get; set; }
    }
    public class VmessOutboundConfigurationObject
    {
        public VmessConfigurationObject[] vnext { get; set; }
    }
    public class VmessConfigurationObject
    {
        public string address { get; set; }
        public int port { get; set; }
        public VmessUser[] users { get; set; }
    }

    public class VmessUser
    {
        public string id { get; set; }
        /// <summary>
        /// 客户端 AlterID 设置为 0 代表启用 VMessAEAD ；服务端为自动适配，可同时兼容启用和未开启 VMessAEAD 的客户端。
        /// </summary>
        public int alterId { get; set; }
        public string security { get; set; }
        public int level { get; set; }
    }

    public class TrojanOutboundConfigurationObject
    {
        public TrojanConfigurationObject[] servers { get; set; }
    }
    public class TrojanConfigurationObject
    {
        public string address { get; set; }
        public int port { get; set; }
        public string password { get; set; }
        //public string email { get; set; }
        public string flow { get; set; }
        public int level { get; set; }
    }

    public class ShadowsocksOutboundConfigurationObject
    {
        public ShadowsocksConfigurationObject[] servers { get; set; }
    }
    public class ShadowsocksConfigurationObject
    {
        public string email { get; set; }
        public string address { get; set; }
        public int port { get; set; }
        /// <summary>
        /// 推荐的加密方式：
        /// AES-256-GCM
        /// AES-128-GCM
        /// ChaCha20-Poly1305 或称 ChaCha20-IETF-Poly1305
        /// none 或 plain
        /// 不推荐的加密方式:
        /// AES-256-CFB
        /// AES-128-CFB
        /// ChaCha20
        /// ChaCha20-IETF
        /// </summary>
        public string method { get; set; }
        public string password { get; set; }
        public int level { get; set; }
    }

    public class MuxObject
    {
        /// <summary>
        /// 是否启用 Mux 转发请求
        /// </summary>
        public bool enabled { get; set; }
        /// <summary>
        /// 最大并发连接数。最小值1，最大值1024，缺省默认值8。
        /// </summary>
        public int concurrency { get; set; }
    }



    public class StreamSettingsObject
    {
        /// <summary>
        /// “tcp” | “kcp” | “ws” | “http” | “domainsocket” | “quic”
        /// </summary>
        public string network { get; set; }
        /// <summary>
        /// “none” | “tls” | “xtls”
        /// </summary>
        public string security { get; set; }
        public TLSObject tlsSettings { get; set; }
        public TLSObject xtlsSettings { get; set; }
        public TcpObject tcpSettings { get; set; }
        public KcpObject kcpSettings { get; set; }
        public WebSocketObject wsSettings { get; set; }
        public HttpObject httpSettings { get; set; }
        public QuicObject quicSettings { get; set; }

        //public object dsSettings { get; set; }
        //public object sockopt { get; set; }
    }


    public class QuicObject
    {
        /// <summary>
        /// “none” | “aes-128-gcm” | “chacha20-poly1305”
        /// </summary>
        public string security { get; set; }
        public string key { get; set; }
        public QuicHeader header { get; set; }
    }

    public class QuicHeader
    {
        public string type { get; set; }
    }


    public class HttpObject
    {
        public string[] host { get; set; }
        public string path { get; set; }
    }


    public class WebSocketObject
    {
        public string path { get; set; }
        public Dictionary<string, string> headers { get; set; }
    }


    public class KcpObject
    {
        public int mtu { get; set; }
        public int tti { get; set; }
        public int uplinkCapacity { get; set; }
        public int downlinkCapacity { get; set; }
        public bool congestion { get; set; }
        public int readBufferSize { get; set; }
        public int writeBufferSize { get; set; }
        public HeaderObject header { get; set; }
    }

    public class HeaderObject
    {
        public string type { get; set; }
    }


    public class TLSObject
    {
        public string serverName { get; set; }
        public bool allowInsecure { get; set; }
        public string[] alpn { get; set; }
        public string minVersion { get; set; }
        public string maxVersion { get; set; }
        //public bool preferServerCipherSuites { get; set; }
        //public string cipherSuites { get; set; }
        //public CertificateObject[] certificates { get; set; }
        //public bool disableSystemRoot { get; set; }
        //public bool enableSessionResumption { get; set; }
    }


    //public class CertificateObject
    //{
    //    public int ocspStapling { get; set; }
    //    public string usage { get; set; }
    //    public string certificateFile { get; set; }
    //    public string keyFile { get; set; }
    //    public string[] certificate { get; set; }
    //    public string[] key { get; set; }
    //}


    public class TcpObject
    {
        public string type { get; set; }
        public HTTPRequestObject request { get; set; }
        public HTTPResponseObject response { get; set; }
    }


    public class HTTPRequestObject
    {
        public string version { get; set; }
        public string method { get; set; }
        public string[] path { get; set; }
        public HTTPRequestHeaders headers { get; set; }
    }

    public class HTTPRequestHeaders
    {
        public string[] Host { get; set; }
        public string[] UserAgent { get; set; }
        public string[] AcceptEncoding { get; set; }
        public string[] Connection { get; set; }
        public string Pragma { get; set; }
    }



    public class HTTPResponseObject
    {
        public string version { get; set; }
        public string status { get; set; }
        public string reason { get; set; }
        public HTTPResponseHeaders headers { get; set; }
    }

    public class HTTPResponseHeaders
    {
        public string[] ContentType { get; set; }
        public string[] TransferEncoding { get; set; }
        public string[] Connection { get; set; }
        public string Pragma { get; set; }
    }

    //https://xtls.github.io/config/base/
    #endregion

}
