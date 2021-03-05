﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XrayShell
{
    public static class Global
    {
        public static readonly string AppPath;
        public static readonly string Version;
        public static readonly string ConfigPath;
        public static readonly string ProcessPath;
        public static readonly string ProcessName;
        public static readonly int PathHash;

        public static readonly System.Drawing.Font Font;

        static Global()
        {
            Version = Application.ProductVersion;
            ProcessPath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
            PathHash = ProcessPath.GetHashCode();
            AppPath = Path.GetDirectoryName(ProcessPath);
            ProcessName = Path.GetFileNameWithoutExtension(ProcessPath);
            ConfigPath = Utils.GetTempPath($"XrayShell_{Application.StartupPath.GetHashCode()}.json");

            if (I18N.SC)
                Font = new System.Drawing.Font("Microsoft Yahei UI", 8.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            else if (I18N.TC)
                Font = new System.Drawing.Font("MingLiU", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            else if (I18N.JP)
                Font = new System.Drawing.Font("Meiryo UI", 8.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            else
                Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        }
    }
}
