using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrayShell.Model;
using XrayShell.Properties;

namespace XrayShell.Services
{
    class XrayRunner
    {
        private static readonly string _uniqueConfigFile;
        private static readonly Job _xrayJob;
        private Process _process;
        static XrayRunner()
        {
            try
            {
                _uniqueConfigFile = $"XrayShell_{Global.PathHash}.json";
                _xrayJob = new Job();
            }
            catch (IOException e)
            {
                Logging.LogUsefulException(e);
            }
        }

        public static void KillAll()
        {
            var existingPrivoxy = Process.GetProcessesByName(Xray.XRAY_CORE_WITHOUTEXT);
            foreach (var p in existingPrivoxy.Where(IsChildProcess)) p.KillProcess();
        }

        public void Start(Configuration configuration)
        {
            if (_process != null) return;
            KillAll();
            var config = Utils.GetTempPath(_uniqueConfigFile);
            File.WriteAllText(config,Xray.GenerateVNextConf(configuration.GetCurrentServer(),configuration.corePort,configuration.shareOverLan ? "0.0.0.0" : "127.0.0.1"));
            _process = new Process
            {
                StartInfo =
                {
                    FileName = Xray.XRAY_CORE,
                    Arguments = $"run -c {config}",
                    WorkingDirectory = Global.AppPath,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = true,
                    CreateNoWindow = true
                }
            };
            _process.Start();
            _xrayJob.AddProcess(_process.Handle);
        }

        public void Stop()
        {
            if (_process == null) return;
            _process.KillProcess();
            _process.Dispose();
            _process = null;
        }


        private static bool IsChildProcess(Process process)
        {
            try
            {
                var cmd = process.GetCommandLine();
                return cmd.Contains(_uniqueConfigFile);
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
                return false;
            }
        }
    }
}
