[size=6][b]PlayerColors[/b][/size]

[quote]多人模式下使用相同角色时，通过颜色区分玩家。[/quote]

[size=5][b]功能特性[/b][/size]

[list]
[*][b]重复角色颜色区分[/b]：当多名玩家选择相同角色时，自动为每位玩家分配不同的颜色
[*][b]全方位颜色覆盖[/b]：画画、Ping 标记、目标线、鼠标、投票头像边框、队友头像边框全部染色
[*][b]支持最多 16 人[/b]：预设 16 种差异化颜色，自动循环使用
[*][b]鼠标增强[/b]：多人模式下始终增强鼠标饱和度，半透明状态更清晰（即使角色不同）
[*][b]Bug 修复[/b]：修复原版目标线 gradient 共享导致颜色逐渐变黑的问题（多人模式下始终生效）
[/list]

[size=5][b]安装[/b][/size]

1. 将 [b]PlayerColors.dll[/b] 和 [b]PlayerColors.pck[/b] 复制到游戏目录下的 [b]mods[/b] 文件夹：

[code]
Slay the Spire 2/
└── mods/
    ├── PlayerColors.dll
    └── PlayerColors.pck
[/code]

2. 启动游戏，MOD 会自动加载

[size=5][b]颜色方案[/b][/size]

[list]
[*][b]Slot 0[/b]：红 / 亮红 / 亮红
[*][b]Slot 1[/b]：蓝 / 亮蓝 / 亮蓝
[*][b]Slot 2[/b]：绿 / 黄绿 / 亮绿
[*][b]Slot 3[/b]：橙 / 亮橙 / 亮橙
[*][b]Slot 4[/b]：紫 / 亮紫 / 亮紫
[*][b]Slot 5[/b]：青 / 亮青 / 亮青
[*][b]Slot 6[/b]：黄 / 亮黄 / 亮黄
[*][b]Slot 7[/b]：粉红 / 亮粉红 / 亮粉红
[*]……（共 16 种颜色，自动循环使用）
[/list]

[size=5][b]技术实现[/b][/size]

[list]
[*]使用 Harmony 库进行运行时 Patch
[*]无需配置，开箱即用
[*]不影响原版单人模式和不同角色的多人模式体验
[/list]

[size=5][b]源代码[/b][/size]
[url=https://github.com/Masaicker/PlayerColorsMod-sts2]https://github.com/Masaicker/PlayerColorsMod-sts2[/url]

[size=5][b]致谢[/b][/size]

本项目基于 [url=https://github.com/Alchyr/ModTemplate-StS2]Alchyr/ModTemplate-StS2[/url] 模板创建。

[size=5][b]许可证[/b][/size]

本项目遵循原游戏的 MOD 许可政策。


[size=6][b]PlayerColors[/b][/size]

[quote]Distinguish players by color when sharing the same character in multiplayer.[/quote]

[size=5][b]Features[/b][/size]

[list]
[*][b]Duplicate Character Color Distinction[/b]: Automatically assigns unique colors to each player when multiple players select the same character
[*][b]Comprehensive Color Coverage[/b]: Colors drawing, ping markers, targeting lines, mouse cursors, vote icon borders, and teammate portrait borders
[*][b]Supports up to 16 Players[/b]: 16 preset distinct colors with automatic cycling
[*][b]Mouse Enhancement[/b]: Multiplayer mode always enhances mouse saturation for better visibility at 50% opacity (even with different characters)
[*][b]Bug Fix[/b]: Fixed vanilla issue where shared gradient caused targeting lines to darken over time (always active in multiplayer)
[/list]

[size=5][b]Installation[/b][/size]

1. Copy [b]PlayerColors.dll[/b] and [b]PlayerColors.pck[/b] to the [b]mods[/b] folder in your game directory:

[code]
Slay the Spire 2/
└── mods/
    ├── PlayerColors.dll
    └── PlayerColors.pck
[/code]

2. Launch the game — the mod will load automatically

[size=5][b]Color Scheme[/b][/size]

[list]
[*][b]Slot 0[/b]: Red / Bright Red / Bright Red
[*][b]Slot 1[/b]: Blue / Bright Blue / Bright Blue
[*][b]Slot 2[/b]: Green / Yellow-Green / Bright Green
[*][b]Slot 3[/b]: Orange / Bright Orange / Bright Orange
[*][b]Slot 4[/b]: Purple / Bright Purple / Bright Purple
[*][b]Slot 5[/b]: Cyan / Bright Cyan / Bright Cyan
[*][b]Slot 6[/b]: Yellow / Bright Yellow / Bright Yellow
[*][b]Slot 7[/b]: Pink / Bright Pink / Bright Pink
[*]… (16 colors total, automatically cycled)
[/list]

[size=5][b]Technical Details[/b][/size]

[list]
[*]Uses the Harmony library for runtime patching
[*]No configuration required, plug and play
[*]Does not affect vanilla single-player or multiplayer with different characters
[/list]

[size=5][b]Source Code[/b][/size]
[url=https://github.com/Masaicker/PlayerColorsMod-sts2]https://github.com/Masaicker/PlayerColorsMod-sts2[/url]

[size=5][b]Acknowledgments[/b][/size]

This project is based on the [url=https://github.com/Alchyr/ModTemplate-StS2]Alchyr/ModTemplate-StS2[/url] template.

[size=5][b]License[/b][/size]

This project follows the original game's modding policy.
