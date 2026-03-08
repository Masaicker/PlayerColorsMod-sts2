using System.Collections.Generic;
using System.Linq;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Screens.Map;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Runs;

namespace PlayerColors;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    private const string ModId = "PlayerColors";

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } =
        new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    // MOD 槽位预设色（4人）
    private static readonly Color[] SlotDrawColors =
    {
        new Color("E53935"), // 红
        new Color("1E88E5"), // 蓝
        new Color("43A047"), // 绿
        new Color("FB8C00"), // 橙
    };

    // 光标专用色（高饱和度，半透明下仍可区分）
    private static readonly Color[] SlotCursorColors =
    {
        new Color("FF1744"), // 亮红
        new Color("42A5F5"), // 亮蓝（高亮度，半透明下仍醒目）
        new Color("76FF03"), // 黄绿（和蓝色拉开距离）
        new Color("FF9100"), // 亮橙
    };

    // 目标线颜色（比画画色亮一些，和原版角色同思路）
    private static readonly Color[] SlotLineColors =
    {
        new Color("FF6659FF"), // 亮红
        new Color("6AB7FFFF"), // 亮蓝
        new Color("76D275FF"), // 亮绿
        new Color("FFBD45FF"), // 亮橙
    };

    // 目标线轮廓色（比画画色暗一些）
    private static readonly Color[] SlotLineOutlineColors =
    {
        new Color("AB000DFF"), // 暗红
        new Color("005CB2FF"), // 暗蓝
        new Color("087F23FF"), // 暗绿
        new Color("C68A00FF"), // 暗橙
    };

    public static void Initialize()
    {
        Harmony harmony = new(ModId);
        harmony.PatchAll();
        Logger.Info("PlayerColors MOD loaded");
    }

    /// <summary>
    /// 判断当前局是否存在重复角色
    /// </summary>
    private static bool HasDuplicateCharacters()
    {
        var run = NRun.Instance;
        if (run == null) return false;
        var players = run._state.Players;
        if (players.Count <= 1) return false;
        return players.Select(p => p.Character.Id).Distinct().Count() < players.Count;
    }

    /// <summary>
    /// 获取玩家的槽位索引
    /// </summary>
    private static int GetSlotIndex(Player player)
    {
        var run = NRun.Instance;
        if (run == null) return 0;
        return run._state.GetPlayerSlotIndex(player);
    }

    /// <summary>
    /// 根据 NetId 找到 Player 对象
    /// </summary>
    private static Player GetPlayer(ulong netId)
    {
        return NRun.Instance?._state.GetPlayer(netId);
    }

    /// <summary>
    /// 获取玩家的画画/Ping颜色（有重复用MOD色，无重复用原版色）
    /// </summary>
    private static Color GetDrawColor(Player player)
    {
        if (!HasDuplicateCharacters()) return player.Character.MapDrawingColor;
        int slot = GetSlotIndex(player);
        return SlotDrawColors[slot % SlotDrawColors.Length];
    }

    /// <summary>
    /// 获取玩家的目标线颜色
    /// </summary>
    private static Color GetLineColor(Player player)
    {
        if (!HasDuplicateCharacters()) return player.Character.RemoteTargetingLineColor;
        int slot = GetSlotIndex(player);
        return SlotLineColors[slot % SlotLineColors.Length];
    }

    /// <summary>
    /// 获取玩家的目标线轮廓颜色
    /// </summary>
    private static Color GetLineOutlineColor(Player player)
    {
        if (!HasDuplicateCharacters()) return player.Character.RemoteTargetingLineOutline;
        int slot = GetSlotIndex(player);
        return SlotLineOutlineColors[slot % SlotLineOutlineColors.Length];
    }

    /// <summary>
    /// 获取玩家的光标染色（多人模式下始终染色）
    /// </summary>
    private static Color GetCursorColor(Player player)
    {
        if (HasDuplicateCharacters())
        {
            int slot = GetSlotIndex(player);
            return SlotCursorColors[slot % SlotCursorColors.Length];
        }
        // 无重复时用角色原色的高饱和度版本，半透明下更易辨识
        Color c = player.Character.MapDrawingColor;
        float h, s, v;
        c.ToHsv(out h, out s, out v);
        return Color.FromHsv(h, Mathf.Min(s * 1.3f, 1f), Mathf.Min(v * 1.2f, 1f));
    }

    // ========== Harmony Patches ==========

    /// <summary>
    /// Patch 1: 地图画画颜色
    /// </summary>
    [HarmonyPatch(typeof(NMapDrawings), "CreateLineForPlayer")]
    static class Patch_MapDrawing
    {
        static void Postfix(Player player, Line2D __result)
        {
            __result.DefaultColor = GetDrawColor(player);
        }
    }

    /// <summary>
    /// Patch 2: 地图 Ping 颜色
    /// PingMapCoord 内部创建 NMapPingVfx 后立即设置 Modulate，再 AddChild 到 NMapPoint 的第0位
    /// 我们在 Postfix 中找到该 NMapPoint 的第一个 NMapPingVfx 子节点并覆盖颜色
    /// </summary>
    [HarmonyPatch(typeof(NMapScreen), nameof(NMapScreen.PingMapCoord))]
    static class Patch_MapPing
    {
        static void Postfix(NMapScreen __instance, MapCoord coord, Player player)
        {
            // 通过 _mapPointDictionary 拿到对应的 NMapPoint
            if (!__instance._mapPointDictionary.TryGetValue(coord, out var mapPoint)) return;

            // Ping VFX 被 MoveChild 到了 index 0
            var pingVfx = mapPoint.GetChildOrNull<NMapPingVfx>(0);
            if (pingVfx == null) return;

            pingVfx.Modulate = GetDrawColor(player);
        }
    }

    /// <summary>
    /// Patch 3: 目标选择线颜色
    /// 原版 bug: gradient 是共享资源，多次乘法导致颜色越来越黑
    /// 修复: 用 Duplicate() 创建独立副本，从原始白色渐变开始乘
    /// 同时修复原版 _lineBack.SetGradient(gradient) 写错变量名的 bug
    /// </summary>
    [HarmonyPatch(typeof(NRemoteTargetingIndicator), nameof(NRemoteTargetingIndicator.Initialize))]
    static class Patch_TargetingLine
    {
        // 缓存原始渐变（白色，只有 alpha 不同），首次调用时保存
        private static Gradient _originalLineGradient;
        private static Gradient _originalLineBackGradient;

        static bool Prefix(NRemoteTargetingIndicator __instance, Player player)
        {
            Color lineColor = GetLineColor(player);
            Color outlineColor = GetLineOutlineColor(player);

            __instance._player = player;

            __instance._line.DefaultColor = lineColor;
            __instance._lineBack.DefaultColor = outlineColor;

            Gradient gradient = __instance._line.GetGradient();
            if (gradient != null)
            {
                // 首次调用时保存原始渐变（白色底）
                if (_originalLineGradient == null)
                {
                    _originalLineGradient = (Gradient)gradient.Duplicate();
                }

                // 从原始白色渐变开始乘，而非在上一个玩家的结果上继续乘
                Gradient newGradient = (Gradient)_originalLineGradient.Duplicate();
                for (int i = 0; i < newGradient.GetPointCount(); i++)
                {
                    newGradient.SetColor(i, newGradient.GetColor(i) * lineColor);
                }
                __instance._line.SetGradient(newGradient);
            }

            Gradient gradient2 = __instance._lineBack.GetGradient();
            if (gradient2 != null)
            {
                if (_originalLineBackGradient == null)
                {
                    _originalLineBackGradient = (Gradient)gradient2.Duplicate();
                }

                Gradient newGradient2 = (Gradient)_originalLineBackGradient.Duplicate();
                for (int j = 0; j < newGradient2.GetPointCount(); j++)
                {
                    newGradient2.SetColor(j, newGradient2.GetColor(j) * outlineColor);
                }
                __instance._lineBack.SetGradient(newGradient2);
            }

            return false; // 跳过原方法
        }
    }

    /// <summary>
    /// Patch 4: 远程光标染色（多人模式下始终生效）
    /// AddCursor 在 lobby 阶段调用时 NRun.Instance 还不存在，
    /// 所以改为 patch UpdateImage，它在光标每次状态更新时调用（进入游戏后持续触发）
    /// </summary>
    [HarmonyPatch(typeof(NRemoteMouseCursor), nameof(NRemoteMouseCursor.UpdateImage))]
    static class Patch_RemoteCursor
    {
        static void Postfix(NRemoteMouseCursor __instance)
        {
            var player = GetPlayer(__instance.PlayerId);
            if (player == null) return;

            __instance.Modulate = GetCursorColor(player);
        }
    }

    /// <summary>
    /// Patch 5: 投票头像边框染色（仅在有重复角色时添加描边）
    /// </summary>
    [HarmonyPatch(typeof(NMultiplayerVoteContainer), nameof(NMultiplayerVoteContainer.RefreshPlayerVotes))]
    static class Patch_VoteIcon
    {
        static void Postfix(NMultiplayerVoteContainer __instance)
        {
            if (!HasDuplicateCharacters()) return;

            foreach (var voteIcon in __instance._votes)
            {
                var player = voteIcon.player;
                int slot = GetSlotIndex(player);
                Color color = SlotDrawColors[slot % SlotDrawColors.Length];

                // 给头像轮廓染色
                var outline = voteIcon.node.GetNode<TextureRect>("Outline");
                if (outline != null)
                {
                    outline.Modulate = color;
                }
            }
        }
    }
}
