﻿using IWshRuntimeLibrary;
using System.Diagnostics;
using System.IO.Compression;

namespace MGC_Application;

public static class FileTools
{
    /// <summary>Run executes the exe of the game.</summary>
    /// <param name="_game">The program of to run the exe of.</param>
    /// <param name="_pathfile">The pathfile of the game.</param>
    public static bool Run(string _game, string _pathfile)
    {
        try
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = $"{_pathfile}/{_game}/{_game}.exe";
            process.Start();

            return true;
        }
        catch(FileLoadException ex)
        {
            DebugLogger.Log($"Error running {_game}.exe: {ex.Message}");
            return false;
        }
    }

    /// <summary>Install extracts and installs the game files in its own directory.</summary>
    /// <param name="_game">The game of which will be installed.</param>
    /// <param name="_pathfile">The pathfile of which the game files are located at.</param>
    public static bool Install(string _game, string _pathfile)
    {
        try
        {
            string startFile = $"{_pathfile}/{_game}.zip";
            string endDir = $"{_pathfile}/{_game}";

            ZipFile.ExtractToDirectory(startFile, endDir);
            Thread.Sleep(1000);
            System.IO.File.Delete(startFile);
            
            return true;
        }
        catch(DirectoryNotFoundException ex)
        {
            DebugLogger.Log($"Error installing {_game} files: {ex.Message}");
            return false;
        }
    }

    /// <summary>Uninstall removes the game files from the games folder.</summary>
    /// <param name="_game">The game of which to remove.</param>
    /// <param name="_pathfile">The filepath of the chosen game.</param>
    public static bool Uninstall(string _game, string _pathfile)
    {
        try
        {
            string dir = $"{_pathfile}/{_game}";

            System.IO.File.Delete($"{_pathfile}/{_game}.zip");
            Directory.Delete(dir, true);

            return true;
        }
        catch(DirectoryNotFoundException ex)
        {
            DebugLogger.Log($"Error uninstalling {_game} files: {ex.Message}");
            return false;
        }
    }

    /// <summary>Update compares file from server to local machine and updates files when necessary.</summary>
    /// <param name="_game">The game of which to update.</param>
    /// <param name="_pathfile">The filepath of said game.</param>
    public static bool Update(string _game, string _pathfile)
    {
        string dir = $"{_pathfile}/{_game}";

        try
        {
            return true;
        }
        catch (FileLoadException ex)
        {
            DebugLogger.Log($"Error updating locals files: {ex.Message}");
            return false;
        }
    }

    /// <summary>Verifys if the game is installed.</summary>
    /// <param name="_game">The game of which to check installation of.</param>
    /// <param name="_pathfile">The pathfile of the installed game.</param>
    public static bool VerifyGameLocation(string _game, string _pathfile)
    {
        string dir = $"{_pathfile}/{_game}";

        if (Directory.Exists(dir))
            return true;
        else
            return false;
    }

    /// <summary>Creates a directory in the system files.</summary>
    /// <param name="_folderName">The name for the directory.</param>
    /// <param name="_toReCreate">If the directory needs to be re-made on load.</param>
    public static void CreateDirectory(string _folderName, bool _toReCreate = false)
    {
        if (!Directory.Exists(_folderName))
        {
            Directory.CreateDirectory(_folderName);
        }

        else if (Directory.Exists(_folderName) && _toReCreate)
        {
            Directory.Delete(_folderName, true);
            Directory.CreateDirectory(_folderName);
        }
    }

    /// <summary>Creates a Desktop shortcut for the game.exe file.</summary>
    /// <param name="_game">The game of which to create a shortcut path for.</param>
    /// <param name="_pathFile">The pathfile of which linking the exe shortcut to.</param>
    public static void CreateDesktopShortcut(string _game, string _pathFile)
    {
        string desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string shortCutDir = Path.Combine(desktopDir, $"{_game}.lnk");
        string pathFile = @$"{_pathFile}/{_game}/{_game}.exe";
        
        WshShell shell = new WshShell();
        IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortCutDir);

        shortcut.Description = $"{_game} shortcut.";
        shortcut.TargetPath = pathFile;
        DebugLogger.Log(shortcut.TargetPath);

        shortcut.Save();
    }
}
