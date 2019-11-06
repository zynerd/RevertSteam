using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.AccessControl;

namespace RevertSteam
{
    public partial class RevertSteamWindow : Form
    {

        // VARS
        public string steamDir;
        public int count = 0;
        readonly IList<string> downloadList = new List<string>() {
        "https://steamcdn-a.akamaihd.net/client/tenfoot_misc_all.zip.1ca83d76835b4613170f5cead778b176b11f2b0c",
        "https://steamcdn-a.akamaihd.net/client/tenfoot_dicts_all.zip.33245b7d523f68418283e93b0572508fa127ee8f",
        "https://steamcdn-a.akamaihd.net/client/tenfoot_fonts_all.zip.12f7f1fa582860ea27358593f7082b3db603e444",
        "https://steamcdn-a.akamaihd.net/client/tenfoot_ambientsounds_all.zip.89b80bcfdd11b2b99257ddbbdc374e2df54e2738",
        "https://steamcdn-a.akamaihd.net/client/tenfoot_sounds_all.zip.vz.ffef2b2fc386819a842ea79484b966a937c2ca7e_1209792",
        "https://steamcdn-a.akamaihd.net/client/tenfoot_images_all.zip.vz.87fcc1807f5af70f47475338456472e256dc61c5_31324448",
        "https://steamcdn-a.akamaihd.net/client/tenfoot_all.zip.vz.983739851e2da7a362b1de2760a2e30a205e75df_2619140",
        "https://steamcdn-a.akamaihd.net/client/friendsui_all.zip.vz.dd7a37b3cfae6b356320e3cc2f8b22b9143d9022_15642680",
        "https://steamcdn-a.akamaihd.net/client/resources_misc_all.zip.vz.4a1fa88d21b005b67a41a9a0fc6044ae1fa46791_2225211",
        "https://steamcdn-a.akamaihd.net/client/resources_music_all.zip.vz.7a62e15083d4a65668f0d1fa58ad8c1b99fb5ace_3708050",
        "https://steamcdn-a.akamaihd.net/client/resources_hidpi_all.zip.vz.66e6d0c4758df08e7a52aeca5d75f7cf2d243268_56612",
        "https://steamcdn-a.akamaihd.net/client/resources_all.zip.vz.96169894093c2f53e32ea35b55d745a31d49c47a_55267206",
        "https://steamcdn-a.akamaihd.net/client/strings_en_all.zip.vz.d60ce477f1b7666c30dc674365dfad8386c7d5ba_89114",
        "https://steamcdn-a.akamaihd.net/client/strings_all.zip.vz.636c36650554f4f01c1d250ac06f8d0b57c95762_2256485",
        "https://steamcdn-a.akamaihd.net/client/public_all.zip.vz.679b04946dfa3ee4ca66189babe5c1b80d40b011_865650",
        "https://steamcdn-a.akamaihd.net/client/bins_codecs_win32.zip.vz.6fa7c50348e00d80d38cf90c79c67583243e0df3_2548326",
        "https://steamcdn-a.akamaihd.net/client/bins_misc_win32.zip.vz.f61dd11c5e3a4770500e5f70efeb8a41a812807e_11920451",
        "https://steamcdn-a.akamaihd.net/client/bins_cef_win32_win8-64.zip.vz.6e3143d686de8e4ddd0e9bee24591d9b191ae35b_49860617",
        "https://steamcdn-a.akamaihd.net/client/bins_cef_win32_win10-64.zip.vz.eeedfd9e5070e56a46dcfe01477ee9b0620848e1_51668605",
        "https://steamcdn-a.akamaihd.net/client/bins_cef_win32_win7.zip.vz.6e3143d686de8e4ddd0e9bee24591d9b191ae35b_49860617",
        "https://steamcdn-a.akamaihd.net/client/bins_cef_win32_win7-64.zip.vz.eeedfd9e5070e56a46dcfe01477ee9b0620848e1_51668605",
        "https://steamcdn-a.akamaihd.net/client/bins_webhelpers_win32_win8-64.zip.vz.e2c7965224459f3833879185cb6ba78919287ba5_1595810",
        "https://steamcdn-a.akamaihd.net/client/bins_webhelpers_win32_win10-64.zip.vz.3202639c83b19f05daa2771694ff4f4d46b00e33_2078683",
        "https://steamcdn-a.akamaihd.net/client/bins_webhelpers_win32_win7.zip.vz.e2c7965224459f3833879185cb6ba78919287ba5_1595810",
        "https://steamcdn-a.akamaihd.net/client/bins_webhelpers_win32_win7-64.zip.vz.3202639c83b19f05daa2771694ff4f4d46b00e33_2078683",
        "https://steamcdn-a.akamaihd.net/client/bins_win32.zip.vz.c3869c99394b4fc870232e0c049efcf07aabc82b_17497480",
        "https://steamcdn-a.akamaihd.net/client/steam_win32.zip.vz.3e176dee57abf078370ee9223d046655f715e124_1224012",
        "https://u.teknik.io/q2lqO",
        "https://u.teknik.io/wZ8Px.manifest"
        };

        public string status_downloading = "Downloading file ";
        public string status_done = "Done.";
        public string status_patched = "";

        public string sNotFound = "Could not locate steam.exe in the selected directory! Please verify this is the correct installation folder for Steam before proceeding.";
        public string sDone = "Done! Steam will relaunch and attempt to download an update; disregard this. Once it has opened into your library, click 'Patch' to prevent it from automatically updating in the future.";

        public RevertSteamWindow()
        {
            InitializeComponent();
        }

        private void RevertSteamWindow_Load(object sender, EventArgs e)
        {
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            DirectoryInfo dir = new DirectoryInfo(programFilesX86 + "/Steam/");
            if (dir.Exists)
            {
                steamDir = dir.FullName;
                browseTextBox.Text = steamDir;

            }
        }

        public void KillProcess()
        {
            foreach (var process in Process.GetProcessesByName("steam"))
            {
                process.Kill();
            }

            DeleteContents();
        }

        public void DeleteContents()
        {
            System.IO.DirectoryInfo dDir = new DirectoryInfo(steamDir + "/package/");

            foreach (FileInfo file in dDir.EnumerateFiles())
            {
                File.SetAttributes(file.FullName, FileAttributes.Normal);
                file.Delete();
            }

            DownloadFiles(count);
        }

        public void DownloadFiles(int num)
        {

            statusTextBox.Text = status_downloading + (count + 1) + " of 29";

            using (var wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " +
                                  "Windows NT 5.2; .NET CLR 1.0.3705;)");

                if (num < 27)
                {
                    wc.DownloadFileAsync(new Uri(downloadList[num]), steamDir + "/package/" + Path.GetFileName(downloadList[count]));
                    wc.Dispose();
                }

                if (num == 27)
                {
                    wc.DownloadFileAsync(new Uri(downloadList[num]), steamDir + "/package/steam_client_win32");
                    wc.Dispose();
                }

                if (num == 28)
                {
                    wc.DownloadFileAsync(new Uri(downloadList[num]), steamDir + "/package/steam_client_win32.manifest");
                    wc.Dispose();
                }

                wc.DownloadFileCompleted += DownloadCompleted;
                wc.DownloadProgressChanged += DownloadProgressChanged;
            }
        }

        public void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadProgressBar.Value = e.ProgressPercentage;
        }

        public void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            count += 1;
            if (count < 29)
            {
                DownloadFiles(count);
            }
            else
            {
                SetAsReadOnly();
            }

        }

        public void SetAsReadOnly()
        {
            File.SetAttributes(steamDir + "/package/steam_client_win32", FileAttributes.ReadOnly);
            File.SetAttributes(steamDir + "/package/steam_client_win32.manifest", FileAttributes.ReadOnly);

            System.Diagnostics.Process.Start(steamDir + "/Steam.exe");

            statusTextBox.Text = status_done;
            patchButton.Enabled = true;
            MessageBox.Show(sDone);
        }

        public void Patch()
        {
            StringBuilder sTextFile = new StringBuilder();
            string sFileName = steamDir + "/steam.cfg";

            File.WriteAllText(sFileName, "");
            sTextFile.AppendLine("BootStrapperInhibitAll=enable");
            sTextFile.AppendLine("BootStrapperForceSelfUpdate=disable");
            File.AppendAllText(sFileName, sTextFile.ToString());
            statusTextBox.Text = "Patched! You may now close the program.";
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (steamDirBrowser.ShowDialog() == DialogResult.OK)
            {
                steamDir = steamDirBrowser.SelectedPath;
                browseTextBox.Text = steamDir;

                if (File.Exists(steamDir + @"/Steam.exe"))
                {

                }
                else
                {
                    MessageBox.Show(sNotFound);
                }
            }
        }

        private void revertButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(steamDir + @"\steam.exe"))
            {
                revertButton.Enabled = false;
                KillProcess();
            }
            else
            {
                MessageBox.Show(sNotFound);
            }
        }

        private void patchButton_Click(object sender, EventArgs e)
        {
            patchButton.Enabled = false;
            Patch();
        }
    }
}
