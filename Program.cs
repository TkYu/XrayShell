using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrayShell.Services;
using XrayShell.View;

namespace XrayShell
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Utils.ReleaseMemory(true);
            using (Mutex mutex = new Mutex(false, "Global\\XrayShell_" + Global.PathHash))
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += Application_ThreadException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.ApplicationExit += Application_ApplicationExit;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var processesName = Process.GetCurrentProcess().MainModule?.ModuleName ?? "XrayShell.exe";
                var processesNameWithoutExtension = Path.GetFileNameWithoutExtension(processesName);
                if (!mutex.WaitOne(0, false))
                {
                    Process[] oldProcesses = Process.GetProcessesByName(processesNameWithoutExtension);
                    if (oldProcesses.Length > 0)
                    {
                        Process oldProcess = oldProcesses[0];
                    }
                    MessageBox.Show(I18N.GetString("Find XrayShell icon in your notify tray.") + "\n" +
                                    I18N.GetString("If you want to start multiple XrayShell, make a copy in another directory."),
                        I18N.GetString("XrayShell is already running."));
                    return;
                }

                Directory.SetCurrentDirectory(Global.AppPath);

                Logging.OpenLogFile();
                //check
                if (Xray.CoreExsis)
                {
                    if (Xray.Version != null)
                    {
                        Logging.Info("XrayCore version : " + Xray.Version);
                    }
                    else
                    {
                        throw new Exception(I18N.GetString("xray.exe version parse fail!"));
                    }
                }
                else
                {
                    //need do sth
                    var download = new DownloadProgress(2);
                    var result = download.ShowDialog();
                    if (result == DialogResult.Abort || result == DialogResult.Cancel)
                        return;
                    Logging.Info("XrayCore version : " + Xray.Version);
                }

                ControllerInstance = new XrayShellController();
                ViewControllerInstance = new MenuViewController(ControllerInstance);
                Hotkeys.HotKeys.Init(ControllerInstance, ViewControllerInstance);
                ControllerInstance.StartAsync();
                Application.Run();
            }
        }

        public static XrayShellController ControllerInstance;
        public static MenuViewController ViewControllerInstance;

        #region HandleExceptions

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string errMsg = e.ExceptionObject.ToString();
            Logging.Error(errMsg);
            MessageBox.Show(
                $"{I18N.GetString("Unexpected error, XrayShell will exit. Please report to")} https://github.com/TkYu/XrayShell/issues {Environment.NewLine}{errMsg}",
                "XrayShell non-UI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string errorMsg = $"Exception Detail: {Environment.NewLine}{e.Exception}";
            Logging.Error(errorMsg);
            MessageBox.Show(
                $"{I18N.GetString("Unexpected error, XrayShell will exit. Please report to")} https://github.com/TkYu/XrayShell/issues {Environment.NewLine}{errorMsg}",
                "XrayShell UI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }


        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Application.ApplicationExit -= Application_ApplicationExit;
            Application.ThreadException -= Application_ThreadException;
            try
            {
                Hotkeys.HotKeys.Destroy();
            }
            catch
            {
                //TODO
            }
        }

        #endregion
    }
}
