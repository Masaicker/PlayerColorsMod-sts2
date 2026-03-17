# PlayerColors

多人模式下使用相同角色时，通过颜色区分玩家。

## 功能特性

- **重复角色颜色区分**：当多名玩家选择相同角色时，自动为每位玩家分配不同的颜色
- **全方位颜色覆盖**：画画、Ping 标记、目标线、鼠标、投票头像边框、队友头像边框全部染色
- **目标线 & 鼠标**：多人模式下始终染色区分（即使角色不同）
- **支持最多 16 人**：预设 16 种差异化颜色，自动循环使用
- **鼠标专用配色**：高饱和度颜色，50% 半透明下仍清晰可辨
- **Bug 修复**：修复原版目标线 gradient 共享导致颜色越来越黑的问题（多人模式下始终生效）

## 安装

1. 将 `PlayerColors.dll` 和 `PlayerColors.pck` 复制到游戏目录下的 `mods` 文件夹：

   ```
   Slay the Spire 2/
   └── mods/
       ├── PlayerColors.dll
       └── PlayerColors.pck
   ```

2. 启动游戏，MOD 会自动加载

## 颜色方案

| 槽位 | 画画 | 光标 | 目标线 |
|:----:|------|------|--------|
| 0 | 红 | 亮红 | 亮红 |
| 1 | 蓝 | 亮蓝 | 亮蓝 |
| 2 | 绿 | 黄绿 | 亮绿 |
| 3 | 橙 | 亮橙 | 亮橙 |
| 4 | 紫 | 亮紫 | 亮紫 |
| 5 | 青 | 亮青 | 亮青 |
| 6 | 黄 | 亮黄 | 亮黄 |
| 7 | 粉红 | 亮粉红 | 亮粉红 |
| ... | (共16色) | | |

## 技术实现

- 使用 Harmony 库进行运行时 Patch
- 无需配置，开箱即用
- 不影响原版单人和不同角色多人模式的体验

## 致谢

本项目基于 [Alchyr/ModTemplate-StS2](https://github.com/Alchyr/ModTemplate-StS2) 模板创建。

## 许可证

本项目遵循原游戏的 MOD 许可政策。

## 支持一下
如果你觉得这个项目对你有帮助，欢迎在 Ko-fi 给我买杯咖啡 ☕
<p>
  <a href="https://ko-fi.com/masaicker">
    <img src="https://cdn.prod.website-files.com/5c14e387dab576fe667689cf/670f5a0171bfb928b21a7e00_support_me_on_kofi_beige.png" alt="Buy me a coffee" width="200">
  </a>
</p>

---

# PlayerColors

Distinguish players by color when sharing the same character in multiplayer.

## Features

- **Duplicate Character Color Distinction**: Automatically assigns unique colors to each player when multiple players select the same character
- **Comprehensive Color Coverage**: Colors drawing, ping markers, targeting lines, mouse cursors, vote icon borders, and teammate portrait borders
- **Targeting Lines & Mouse**: Always colored in multiplayer mode (even with different characters)
- **Supports up to 16 Players**: 16 preset distinct colors with automatic cycling
- **Dedicated Mouse Colors**: High-saturation colors remain distinguishable at 50% opacity
- **Bug Fix**: Fixed vanilla targeting line gradient darkening issue (always active in multiplayer)

## Installation

1. Copy `PlayerColors.dll` and `PlayerColors.pck` to the `mods` folder in your game directory:

   ```
   Slay the Spire 2/
   └── mods/
       ├── PlayerColors.dll
       └── PlayerColors.pck
   ```

2. Launch the game, the mod will load automatically

## Color Scheme

| Slot | Drawing | Cursor | Target Line |
|:----:|:--------:|:------:|:-----------:|
| 0 | Red | Bright Red | Bright Red |
| 1 | Blue | Bright Blue | Bright Blue |
| 2 | Green | Yellow-Green | Bright Green |
| 3 | Orange | Bright Orange | Bright Orange |
| 4 | Purple | Bright Purple | Bright Purple |
| 5 | Cyan | Bright Cyan | Bright Cyan |
| 6 | Yellow | Bright Yellow | Bright Yellow |
| 7 | Pink | Bright Pink | Bright Pink |
| ... | (16 colors total) | | |

## Technical Details

- Uses Harmony library for runtime patching
- No configuration required, plug and play
- Does not affect vanilla single-player or multiplayer with different characters

## Acknowledgments

This project is based on the [Alchyr/ModTemplate-StS2](https://github.com/Alchyr/ModTemplate-StS2) template.

## License

This project follows the original game's modding policy.

## Support
If you found this project useful, you’re welcome to buy me a coffee on Ko-fi ☕

<p>
  <a href="https://ko-fi.com/masaicker">
    <img src="https://cdn.prod.website-files.com/5c14e387dab576fe667689cf/670f5a0171bfb928b21a7e00_support_me_on_kofi_beige.png" alt="Buy me a coffee" width="200">
  </a>
</p>
