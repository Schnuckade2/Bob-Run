[Setup]
; Unique AppId (Identifies the app for updates/uninstalls)
AppId={{8A2B3C4D-5E6F-7G8H-9I0J-1K2L3M4N5O6P}
AppName=Bob Run
AppVersion=Beta 1.0
; Installs to AppData\Roaming\Bob Run
DefaultDirName={userappdata}\Bob Run
DefaultGroupName=Bob Run
UninstallDisplayIcon={app}\BobRun.exe
Compression=lzma
SolidCompression=yes
WizardStyle=modern
; The icon for the installer .exe file (must be next to this .iss file)
SetupIconFile=bobicon.ico

[Files]
; 1. Copy all game files from your Unity build folder
Source: "C:\Users\lenna\Documents\2 C#\Unity\Bob Run\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; 2. Copy your shortcut icon file to the app folder
Source: "bobicon.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
; Desktop Shortcut using bobicon.ico
Name: "{userdesktop}\Bob Run"; Filename: "{app}\BobRun.exe"; IconFilename: "{app}\bobicon.ico"
; Start Menu Shortcut using bobicon.ico
Name: "{group}\Bob Run"; Filename: "{app}\BobRun.exe"; IconFilename: "{app}\bobicon.ico"

[Run]
; Option to launch the game after the installer finishes
Filename: "{app}\BobRun.exe"; Description: "{cm:LaunchProgram,Bob Run}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
; Ensures the icon file is removed when the game is uninstalled
Type: files; Name: "{app}\bobicon.ico"