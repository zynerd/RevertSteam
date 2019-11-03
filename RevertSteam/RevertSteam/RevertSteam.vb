' Yes my code is garbage, I know.
' I threw this together as quick as I could with lots of troubleshooting, mostly due
' to the fact I haven't written actual code in about a year or so.
' -zynerd
Imports System.Environment
Imports System.Net
Imports System.IO
Public Class RevertSteam
    ' VARIABLES
    Dim WithEvents wc As WebClient = New WebClient()
    Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
    Dim steamDir As String
    Dim downloadFinished As Boolean = False
    Dim itemcount As Integer = 0

    ' PROGESSBAR STATES
    Dim state_kill As String = "Killing steam.exe process..."
    Dim state_downloading As String = "Downloading file "
    Dim state_deleting As String = "Deleting contents of package folder..."
    Dim state_done As String = "Done."

    ' DOWNLOAD FILE LIST
    Dim lst As New List(Of String) _
                    From {"https://steamcdn-a.akamaihd.net/client/tenfoot_misc_all.zip.1ca83d76835b4613170f5cead778b176b11f2b0c",
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
                        "https://u.teknik.io/wZ8Px.manifest"}

    Private Sub RevertButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RevertButton.Click
        ' Check if a path is selected
        Try
            If My.Computer.FileSystem.FileExists(steamDir & "\steam.exe") Then
                RevertButton.Enabled = False

                ' Kill the steam.exe process
                Try
                    TextBox1.Text = state_kill
                    Process.GetProcessesByName("steam")(0).Kill()
                Catch ex As Exception
                    '
                End Try

                ' Delete contents
                Try
                    deleteContents()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Else
                MessageBox.Show("Could not locate steam.exe in the selected directory! Please verify this is the correct installation folder for Steam before proceeding.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub downloadFiles(ByVal num As Integer)
        If itemcount < 29 Then
            TextBox1.Text = state_downloading & itemcount + 1 & " of 29"
            If itemcount < 27 Then
                wc.DownloadFileAsync(New Uri(lst(num)), steamDir & "\package\" + System.IO.Path.GetFileName(lst(itemcount)))
                wc.Dispose()
            End If
            If itemcount = 27 Then
                wc.DownloadFileAsync(New Uri(lst(num)), steamDir & "\package\steam_client_win32")
                wc.Dispose()
            End If
            If itemcount = 28 Then
                wc.DownloadFileAsync(New Uri(lst(num)), steamDir & "\package\steam_client_win32.manifest")
                wc.Dispose()
            End If
        End If
    End Sub

    Private Sub wc_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles wc.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub wc_downloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.DownloadDataCompleted
        If ProgressBar1.Value >= 100 Then
            ProgressBar1.Value = 0
        End If

        itemcount = itemcount + 1
        If itemcount < 29 Then
            downloadFiles(itemcount)
        Else
            setAsReadOnly()
        End If
    End Sub

    Private Sub FolderButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FolderButton.Click
        If (FolderBrowser.ShowDialog() = DialogResult.OK) Then
            steamDir = FolderBrowser.SelectedPath
            TextBox2.Text = steamDir
            Try
                If My.Computer.FileSystem.FileExists(steamDir & "\steam.exe") Then
                    '
                Else
                    MessageBox.Show("Could not locate steam.exe in the selected directory! Please verify this is the correct installation folder for Steam before proceeding.")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub RevertSteam_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AddHandler wc.DownloadFileCompleted, AddressOf wc_downloadCompleted

        wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)")
        Timer1.Start()
        Dim programFilesX86 As String = GetFolderPath(SpecialFolder.ProgramFilesX86)
        Dim dir As New IO.DirectoryInfo(programFilesX86 & "\Steam")
        If dir.Exists Then
            steamDir = dir.FullName
            TextBox2.Text = steamDir
        End If
    End Sub

    Sub deleteContents()
        Try
            TextBox1.Text = state_deleting
            For Each deleteFile In Directory.GetFiles(steamDir & "\package\", "*.*", SearchOption.TopDirectoryOnly)
                File.SetAttributes(deleteFile, FileAttributes.Normal)
                File.Delete(deleteFile)
            Next
            downloadFiles(itemcount)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub setAsReadOnly()
        Try
            File.SetAttributes(steamDir & "\package\steam_client_win32", FileAttributes.ReadOnly)
            File.SetAttributes(steamDir & "\package\steam_client_win32.manifest", FileAttributes.ReadOnly)

            ' Restart steam and enable patch button
            System.Diagnostics.Process.Start(steamDir & "\Steam.exe")

            TextBox1.Text = state_done
            MsgBox("Done! Steam will relaunch. If it starts downloading an update, let it do so; it should throw an error. Once you've clicked 'OK' on the Steam error, click on 'Patch'. Alternatively, Steam may launch with no errors into the reverted UI. In both cases, click on 'Patch' afterward to prevent Steam from auto-updating in the future. After patching, you can reopen Steam.", MsgBoxStyle.OkOnly, "IMPORTANT! READ ATTENTIVELY!")
            PatchButton.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub copyCfg()
        ' Create the steam.cfg file and write 2 lines
        Dim sTextFile As New System.Text.StringBuilder
        Dim sFileName As String = steamDir & "\steam.cfg"
        System.IO.File.WriteAllText(sFileName, "")
        sTextFile.AppendLine("BootStrapperInhibitAll=enable")
        sTextFile.AppendLine("BootStrapperForceSelfUpdate=disable")
        System.IO.File.AppendAllText(sFileName, sTextFile.ToString)
        TextBox1.Text = "Patched! You may now close the program."
    End Sub

    Private Sub PatchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatchButton.Click
        PatchButton.Enabled = False
        copyCfg()
    End Sub
End Class
