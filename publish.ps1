# PlayerColors 一键发布：编译DLL + 构建PCK → 复制到游戏mods目录
$ErrorActionPreference = "Stop"

$ProjectDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$SteamRoot = (Get-ItemProperty "HKCU:\Software\Valve\Steam" -Name SteamPath).SteamPath
$ModsDir = "$SteamRoot/steamapps/common/Slay the Spire 2/mods"
$PckPath = [System.IO.Path]::GetFullPath("$ProjectDir/PlayerColors.pck")
$ManifestPath = [System.IO.Path]::GetFullPath("$ProjectDir/mod_manifest.json")
$ImagePath = [System.IO.Path]::GetFullPath("$ProjectDir/PlayerColors/mod_image.png")
$ImageImportPath = [System.IO.Path]::GetFullPath("$ProjectDir/PlayerColors/mod_image.png.import")
$ImageCtexPath = [System.IO.Path]::GetFullPath("$ProjectDir/.godot/imported/mod_image.png-03fc2d642c2561f50bb1b0175ad889ba.ctex")

# ========== 辅助函数：写入一个文件条目到 PCK ==========
function Write-PckFileEntry($bw, [string]$resPath, [byte[]]$data, [long]$offsetFromBase) {
    $pathBytes = [System.Text.Encoding]::UTF8.GetBytes($resPath)
    $pathLen = $pathBytes.Length + 1
    $paddedLen = [Math]::Ceiling($pathLen / 4) * 4
    $bw.Write([int]$paddedLen)
    $bw.Write($pathBytes)
    for ($i = $pathBytes.Length; $i -lt $paddedLen; $i++) { $bw.Write([byte]0) }
    $bw.Write([long]$offsetFromBase)
    $bw.Write([long]$data.Length)
    $md5 = [System.Security.Cryptography.MD5]::Create().ComputeHash($data)
    $bw.Write($md5)
    $bw.Write([int]0)
}

try {
    Write-Host "=== PlayerColors Publish ===" -ForegroundColor Cyan

    # 询问是否包含封面图片
    $includeImage = $false
    if (Test-Path $ImageImportPath) {
        Write-Host ""
        Write-Host "  Include mod cover image (mod_image.png)?" -ForegroundColor White
        Write-Host "  [Y] Yes  [N] No (default)" -ForegroundColor DarkGray
        $key = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown").Character
        if ($key -eq 'y' -or $key -eq 'Y') { $includeImage = $true }
        Write-Host ""
    }

    # 1. 编译发布DLL
    Write-Host "[1/3] Publishing DLL..." -ForegroundColor Yellow
    dotnet publish "$ProjectDir/PlayerColors.csproj" -c Release --no-restore -o "$ProjectDir/bin/publish"
    if ($LASTEXITCODE -ne 0) { throw "DLL publish failed!" }

    # 2. 构建PCK（Godot PCK V2 格式）
    Write-Host "[2/3] Building PCK..." -ForegroundColor Yellow

    # 准备文件列表: [resPath, fileData]
    $files = @()
    $files += ,@("res://mod_manifest.json", [System.IO.File]::ReadAllBytes($ManifestPath))
    if ($includeImage) {
        $files += ,@("res://PlayerColors/mod_image.png.import", [System.IO.File]::ReadAllBytes($ImageImportPath))
        $files += ,@("res://.godot/imported/mod_image.png-03fc2d642c2561f50bb1b0175ad889ba.ctex", [System.IO.File]::ReadAllBytes($ImageCtexPath))
        Write-Host "  + mod_image.png (.import + .ctex)" -ForegroundColor Gray
    }

    $ms = New-Object System.IO.MemoryStream
    $bw = New-Object System.IO.BinaryWriter($ms)

    # Header
    $bw.Write([byte[]]@(0x47, 0x44, 0x50, 0x43))  # Magic: GDPC
    $bw.Write([int]2)                                # Pack version
    $bw.Write([int]4); $bw.Write([int]5); $bw.Write([int]1)  # Engine 4.5.1
    $bw.Write([int]0)                                # Flags
    $fileBaseOffsetPos = $ms.Position
    $bw.Write([long]0)                               # File base offset (fill later)
    for ($i = 0; $i -lt 16; $i++) { $bw.Write([int]0) }  # Reserved

    # Directory
    $bw.Write([int]$files.Count)

    # 先计算每个文件数据相对于 base 的偏移
    $dataOffset = [long]0
    $offsets = @()
    foreach ($f in $files) {
        $offsets += $dataOffset
        $dataLen = $f[1].Length
        $dataPadding = (4 - ($dataLen % 4)) % 4
        $dataOffset += $dataLen + $dataPadding
    }

    # 写入目录条目
    for ($i = 0; $i -lt $files.Count; $i++) {
        Write-PckFileEntry $bw $files[$i][0] $files[$i][1] $offsets[$i]
    }

    # 写入文件数据
    $fileBasePos = $ms.Position
    foreach ($f in $files) {
        $bw.Write($f[1])
        $dataPadding = (4 - ($f[1].Length % 4)) % 4
        for ($j = 0; $j -lt $dataPadding; $j++) { $bw.Write([byte]0) }
    }

    # 回填 file base offset
    $ms.Position = $fileBaseOffsetPos
    $bw.Write([long]$fileBasePos)

    $bw.Flush()
    [System.IO.File]::WriteAllBytes($PckPath, $ms.ToArray())
    $bw.Close(); $ms.Close()

    $pckSize = (Get-Item $PckPath).Length
    Write-Host "  PCK built: $pckSize bytes" -ForegroundColor Gray

    # 3. 复制到游戏mods目录
    Write-Host "[3/3] Copying to mods folder..." -ForegroundColor Yellow
    if (!(Test-Path $ModsDir)) { New-Item -ItemType Directory -Path $ModsDir | Out-Null }
    Copy-Item "$ProjectDir/bin/publish/PlayerColors.dll" "$ModsDir/PlayerColors.dll" -Force
    Copy-Item $PckPath "$ModsDir/PlayerColors.pck" -Force

    Write-Host ""
    Write-Host "=== Publish OK ===" -ForegroundColor Green
    Write-Host "  DLL -> $ModsDir/PlayerColors.dll"
    Write-Host "  PCK -> $ModsDir/PlayerColors.pck ($pckSize bytes)"

    # 清理本地临时PCK
    Remove-Item $PckPath -Force -ErrorAction SilentlyContinue
}
catch {
    Write-Host ""
    Write-Host "=== FAILED ===" -ForegroundColor Red
    Write-Host "  $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""
Write-Host "Press any key to exit..." -ForegroundColor DarkGray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
