using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XrayShell.Model;

namespace XrayShell.Services
{
    public class UpdateChecker
    {

        public const string XRAY_URL = "https://github.com/XTLS/Xray-core/releases/latest";
        public const string SHELL_URL = "https://github.com/TkYu/XrayShell/releases/latest";
        public const string XRAY_CNPMJS_URL = "https://github.com.cnpmjs.org/XTLS/Xray-core/releases/latest";
        //public const string XRAY_FASTGIT_URL = "https://hub.fastgit.org/XTLS/Xray-core/releases/latest";
        public const string XRAY_WUYANZHESHUI_URL = "https://github.wuyanzheshui.workers.dev/XTLS/Xray-core/releases/latest";
        public const string SHELL_CNPMJS_URL = "https://github.com.cnpmjs.org/TkYu/XrayShell/releases/latest";
        public const string SHELL_WUYANZHESHUI_URL = "https://github.wuyanzheshui.workers.dev/TkYu/XrayShell/releases/latest";

        static UpdateChecker()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public static async Task<string> GetCoreVersion(string proxy = null)
        {
            if (!string.IsNullOrEmpty(proxy))
                return await GetVersionFromLocation(XRAY_URL, proxy);
            var ret = await GetVersionFromLocation(XRAY_URL, null);
            //if (string.IsNullOrEmpty(ret))
            //    return await GetVersionFromLocation(XRAY_FASTGIT_URL, null);
            if (string.IsNullOrEmpty(ret))
                return await GetVersionFromLocation(XRAY_WUYANZHESHUI_URL, null);
            return ret;
        }

        public static async Task<string> GetVersion(string proxy = null)
        {
            if (!string.IsNullOrEmpty(proxy))
                return await GetVersionFromLocation(SHELL_URL, proxy);
            var ret = await GetVersionFromLocation(SHELL_URL, null);
            if (string.IsNullOrEmpty(ret))
                return await GetVersionFromLocation(SHELL_CNPMJS_URL, null);
            if (string.IsNullOrEmpty(ret))
                return await GetVersionFromLocation(SHELL_WUYANZHESHUI_URL, null);
            return ret;
        }

        private static async Task<string> GetCoreVersionInternal()
        {
            var ret = await GetVersionFromLocation(XRAY_CNPMJS_URL, null);
            if (string.IsNullOrEmpty(ret))
                return await GetVersionFromLocation(XRAY_WUYANZHESHUI_URL, null);
            if (string.IsNullOrEmpty(ret))
                return await GetVersionFromLocation(XRAY_URL, null);
            return ret;
        }

        private static async Task<string> GetVersionInternal()
        {
            var ret = await GetVersionFromLocation(SHELL_CNPMJS_URL, null);
            if (string.IsNullOrEmpty(ret))
                return await GetVersionFromLocation(SHELL_WUYANZHESHUI_URL, null);
            if (string.IsNullOrEmpty(ret))
                return await GetVersionFromLocation(SHELL_URL, null);
            return ret;
        }

        private static async Task<string> GetVersionFromLocation(string entry, string proxy)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(entry);
                if (!string.IsNullOrEmpty(proxy)) request.Proxy = new WebProxy(new Uri(proxy));
                request.Timeout = 5000;
                request.AllowAutoRedirect = false;
                var response = await request.GetResponseAsync();
                return response.Headers["Location"].Split('/').Last().TrimStart('v');
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                return null;
            }
        }


        public async Task<Tuple<bool, bool, string, string>> CheckUpdate(Configuration config)
        {
            var rayupdate = false;
            var rayversion = await GetCoreVersionInternal();
            //if (string.IsNullOrEmpty(rayversion)) rayversion = await GetCoreVersion($"http://127.0.0.1:{config.localPort}");
            if (!string.IsNullOrEmpty(rayversion) && CompareVersion(rayversion, Xray.Version.ToString()) > 0) rayupdate = true;
            var versionupdate = false;
            var version = await GetVersionInternal();
            //if (string.IsNullOrEmpty(version)) version = await GetVersion($"http://127.0.0.1:{config.localPort}");
            if (!string.IsNullOrEmpty(version) && CompareVersion(version, Global.Version) > 0) versionupdate = true;
            return new Tuple<bool, bool, string, string>(versionupdate, rayupdate, version, rayversion);
        }

        public static int CompareVersion(string l, string r)
        {
            var ls = l.Split('.');
            var rs = r.Split('.');
            for (int i = 0; i < Math.Max(ls.Length, rs.Length); i++)
            {
                int lp = (i < ls.Length) ? int.Parse(ls[i]) : 0;
                int rp = (i < rs.Length) ? int.Parse(rs[i]) : 0;
                if (lp != rp)
                {
                    return lp - rp;
                }
            }

            return 0;
        }
    }
}