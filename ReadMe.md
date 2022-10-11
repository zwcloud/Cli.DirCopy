DirCopy
============

DirCopy is a console app to copy directory recursively.

Copy `D:\SrcDir` to `D:\DestDir` recursively

```powershell
.\DirCopy -Src "D:\SrcDir" -Dest "D:\DestDir"
```

Copy `D:\SrcDir` to `D:\DestDir` recursively, ignore `D:\DestDir\IgnoreA\` and `D:\DestDir\Dir\IgnoreB\`:

```powershell
.\DirCopy -Src "D:\SrcDir" -Dest "D:\DestDir" -Ignore "D:\DestDir\IgnoreA\" "D:\DestDir\Dir\IgnoreB\"
```

All parameters must be absolute path.

Exit code will be non-zero if any error happened.
