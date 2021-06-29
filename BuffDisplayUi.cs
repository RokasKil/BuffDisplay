using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace BuffDisplay
{
    class BuffDisplayUi : UIState
    {
		private UIElement area;
		
		public override void OnInitialize()
		{
			area = new UIElement();
			area.Width.Set(0f, 1f); // idk if you actually need to do this
			area.Height.Set(0f, 1f);
			Append(area);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			
			base.DrawSelf(spriteBatch);
			Rectangle screenRectangle = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
			
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC npc = Main.npc[i];

				if (!npc.active || (!npc.boss && BuffDisplayConfig.Instance.BossesOnly))
					continue; //Maybe should break here
				if (screenRectangle.Intersects(npc.getRect()) || screenRectangle.Intersects(getBuffRect(npc)))
				{
					int buffCount = 0;
					while (buffCount < npc.buffType.Length && npc.buffType[buffCount] != 0)
					{
						buffCount++;
					}
					Vector2 onScreenPosition = npc.position - Main.screenPosition;
					onScreenPosition.X += npc.width / 2 - BuffDisplayConfig.Instance.IconSize * buffCount / 2.0f;
					onScreenPosition.Y -= BuffDisplayConfig.Instance.HeightOffset + BuffDisplayConfig.Instance.IconSize;
					for (int j = 0; j < buffCount; j++)
					{
						
						float lighting = Main.LocalPlayer.detectCreature ? 1f : Math.Max(Math.Min(Lighting.Brightness((int)(Main.screenPosition.X + onScreenPosition.X + BuffDisplayConfig.Instance.IconSize / 2) / 16, (int)(Main.screenPosition.Y + onScreenPosition.Y + BuffDisplayConfig.Instance.IconSize / 2) / 16), 1.0f), 0.0f);
						BuffDisplay.Instance?.Logger.Info($"Drawing with {lighting}");

						spriteBatch.Draw(Main.buffTexture[npc.buffType[j]], new Rectangle((int)onScreenPosition.X, (int)onScreenPosition.Y, BuffDisplayConfig.Instance.IconSize, BuffDisplayConfig.Instance.IconSize), new Color(lighting, lighting, lighting, BuffDisplayConfig.Instance.Transparency));
						onScreenPosition.X += BuffDisplayConfig.Instance.IconSize;
					}
				}
			}

		}

		private Rectangle getBuffRect(NPC npc)
        {
			return new Rectangle((int)(npc.position.X + npc.width / 2 - BuffDisplayConfig.Instance.IconSize * 5 / 2.0f), (int)(npc.position.Y - (BuffDisplayConfig.Instance.HeightOffset + BuffDisplayConfig.Instance.IconSize)), 25 * 5, 25);
        }

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}
	}
}
