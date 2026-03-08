# PlayerColors

多人模式下使用相同角色时，通过颜色区分玩家。

## 功能特性

- **重复角色颜色区分**：当多名玩家选择相同角色时，自动为每位玩家分配不同的颜色
- **全方位颜色覆盖**：画画、Ping 标记、目标线、远程光标、投票头像边框全部染色
- **支持最多 16 人**：预设 16 种差异化颜色，自动循环使用
- **光标专用配色**：高饱和度光标颜色，50% 半透明下仍清晰可辨
- **Bug 修复**：修复原版目标线 gradient 共享导致颜色越来越黑的问题

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

---

# PlayerColors

Distinguish players by color when sharing the same character in multiplayer.

## Features

- **Duplicate Character Color Distinction**: Automatically assigns unique colors to each player when multiple players select the same character
- **Comprehensive Color Coverage**: Colors drawing, ping markers, targeting lines, remote cursors, and vote icon borders
- **Supports up to 16 Players**: 16 preset distinct colors with automatic cycling
- **Dedicated Cursor Colors**: High-saturation cursor colors remain distinguishable at 50% opacity
- **Bug Fix**: Fixed vanilla issue where shared gradient caused targeting lines to darken over time

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
