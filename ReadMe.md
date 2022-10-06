DirCopy
============

DirCopy is a console app to copy directory recursively.

Copy `D:\SrcDir` to `D:\DestDir` recursively and ignore `D:\DestDir\IgnoreThisDir\`:

```powershell
.\DirCopy -Src "D:\SrcDir" -Dest "D:\DestDir" -Ignore "D:\DestDir\IgnoreThisDir\"
```

All parameter must be absolute path.

Exit code will be non-zero if any error happened.