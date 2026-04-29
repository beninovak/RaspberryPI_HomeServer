using System;
using System.IO;

namespace HomeServer.Client.Util;

public static class Constants {
    public static readonly string appStorage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "home_server");
    public static readonly string localSaveFolder = Path.Combine(appStorage, "saves");
    public static readonly string localChangesFolder = Path.Combine(appStorage, "changes");
}