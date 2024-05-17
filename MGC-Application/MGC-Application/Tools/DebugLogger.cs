﻿namespace MGC_Application;

public static class DebugLogger
{
    /// <summary>Log allows for application to log special changes or errors within application.</summary>
    /// <param name="_message">The message of which to log.</param>
    public static void Log(string _message)
    {
        string logFileName = $"Logs.txt";
        string logPath = $"Logs/{logFileName}";

        using (StreamWriter writer = new StreamWriter(logPath, true))
            writer.WriteLine($"{DateTime.Now} : {_message}");
    }

    /// <summary>Break makes a empty line of space in log files for ease of reading.</summary>
    public static void Break()
    {
        string logFileName = $"Logs.txt";
        string logPath = $"Logs/{logFileName}";

        using (StreamWriter write = new StreamWriter(logPath, true))
            write.WriteLine("\n");
    }
}
