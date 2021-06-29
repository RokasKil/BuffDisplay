using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace BuffDisplay
{
    class BuffDisplayConfig : ModConfig
    {
        public static BuffDisplayConfig Instance;
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(false)]
        [Label("Bosses only")]
        [Tooltip("Show buffs/debuffs on bosses only")]
        public bool BossesOnly { get; set; }

        [Range(0f, 1f)]
        [Increment(.01f)]
        [DefaultValue(0.6f)]
        [Slider]
        [Label("Transparency")]
        [Tooltip("Transparency of the buff/debuff icon")]
        public float Transparency { get; set; }

        [DefaultValue(25)]
        [Label("Icon size")]
        [Tooltip("Icon width and height in pixel")]
        public int IconSize { get; set; }
        [DefaultValue(5)]
        [Label("Height offset")]
        [Tooltip("Icon height offset above the npc in pixels")]
        public int HeightOffset { get; set; }

    }
}
