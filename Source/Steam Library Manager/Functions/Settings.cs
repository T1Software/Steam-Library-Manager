﻿using System.Linq;
namespace Steam_Library_Manager.Functions
{
    class Settings
    {
        public static void UpdateMainForm()
        {
            try
            {
                // Update Steam Path label
                Definitions.Accessors.MainForm.linkLabel_SteamPath.Text = Properties.Settings.Default.SteamInstallationPath;

                // Update game size calculation method selectbox
                Definitions.Accessors.MainForm.SLM_sizeCalculationMethod.SelectedIndex = (Properties.Settings.Default.GameSizeCalculationMethod != "ACF") ? 1 : 0;

                // Update archive size calculation method selectbox
                Definitions.Accessors.MainForm.SLM_archiveSizeCalcMethod.SelectedIndex = (!Properties.Settings.Default.ArchiveSizeCalculationMethod.StartsWith("Uncompressed")) ? 1 : 0;

                // Update sort games method by saved one
                Definitions.Accessors.MainForm.SLM_SortGamesBy.SelectedItem = Properties.Settings.Default.SortGamesBy;

                // Update log errors to file checkbox
                Definitions.Accessors.MainForm.checkbox_LogErrorsToFile.Checked = Properties.Settings.Default.LogErrorsToFile;

                // Check for Updates at Startup checkbox visual update
                Definitions.Accessors.MainForm.checkbox_CheckForUpdatesAtStartup.Checked = Properties.Settings.Default.CheckForUpdatesAtStartup;

                // Current Version text
                Definitions.Accessors.MainForm.label_CurrentVersion.Text = Definitions.Updater.CurrentVersion.ToString();

                // Find game directories and update them on form
                Functions.SteamLibrary.UpdateLibraries();
            }
            catch { }
        }

        public static void UpdateBackupDirs()
        {
            try
            {
				// Define a new string collection to update backup library settings
                System.Collections.Specialized.StringCollection BackupDirs = new System.Collections.Specialized.StringCollection();

                // foreach defined library in library list
                foreach (Definitions.List.LibraryList Library in Definitions.List.Library.Where(x => x.Backup))
                {
                    // then add this library path to new defined string collection
                    BackupDirs.Add(Library.Directory.Remove(Library.Directory.Length - 1, 1));
                }

				// change our current backup directories setting with new defined string collection
                Properties.Settings.Default.BackupDirectories = BackupDirs;

				// Save the settings to file
                Save();
            }
            catch { }
        }

        public static void Save()
        {
            try
            {
                // Save settings to file
                Properties.Settings.Default.Save();
            }
            catch { }
        }

    }
}
