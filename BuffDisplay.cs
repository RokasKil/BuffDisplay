using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace BuffDisplay
{
	public class BuffDisplay : Mod
	{
		private BuffDisplayUi buffDisplayUi;
		private UserInterface userInterface;
		private static BuffDisplay instance;

		public static BuffDisplay Instance { get => instance;}

        public override void Load()
        {
			if (!Main.dedServ)
			{
				buffDisplayUi = new BuffDisplayUi();
				buffDisplayUi.Activate();
				userInterface = new UserInterface();
				userInterface.SetState(buffDisplayUi);
			}
			instance = this;
		}
		public override void UpdateUI(GameTime gameTime)
		{
			userInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int healthBarsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Entity Health Bars"));
			
			if (healthBarsIndex != -1)
			{
				layers.Insert(healthBarsIndex, new LegacyGameInterfaceLayer(
					"Buff Display",
					delegate
					{
						userInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.Game)
				);
			}
		}

        public override void Unload()
        {
			instance = null;
			base.Unload();

        }

    }
}