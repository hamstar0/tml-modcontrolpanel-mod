﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using ModLibsCore.Services.Hooks.LoadHooks;
using ModLibsUI.Classes.UI.Theme;
using ModLibsUI.Classes.UI.Elements;
using ModUtilityPanels.Classes.UI;
using ModUtilityPanels.Services.UI.UtilityPanels;


namespace ModUtilityPanels.Internals.UtilityPanels.WelcomeTab {
	/// @private
	partial class UIWelcomeUtilityPanelsTab : UIUtilityPanelsTab {
		private static IList<string> SupportMessages = new List<string> {
			"Buy me coffee for coding! :)",
			"Did you know I make other mods?",
			"Want more?",
			"Please support Mod Libs!"
		};



		////////////////

		private UIWebUrl SupportUrl = null;

		////

		private bool RequestClose = false;
		
		private int RandomSupportTextIdx = -1;



		////////////////

		/// @private
		public UIWelcomeUtilityPanelsTab( UITheme theme ) : base( theme ) {
			this.Theme = theme;
		}

		////////////////

		/// @private
		public void AddCloseButton( UITextPanelButton button ) {
			this.Append( button );
		}

		////////////////

		/// @private
		public override void OnInitializeMe() {
			this.RandomSupportTextIdx = Main.rand.Next( UIWelcomeUtilityPanelsTab.SupportMessages.Count );
			
			this.InitializeComponents();

			LoadHooks.AddWorldUnloadEachHook( () => {
				this.RandomSupportTextIdx = Main.rand.Next( UIWelcomeUtilityPanelsTab.SupportMessages.Count );
				this.SupportUrl?.SetText( UIWelcomeUtilityPanelsTab.SupportMessages[this.RandomSupportTextIdx] );
			} );
		}


		////////////////

		/// @private
		public override void Update( GameTime gameTime ) {
			base.Update( gameTime );

			if( this.RequestClose ) {
				this.RequestClose = false;

				UtilityPanelsTabs.CloseDialog();

				return;
			}
		}


		////////////////

		/// @private
		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );
			
			if( this.SupportUrl.IsMouseHovering ) {
				if( !this.SupportUrl.WillDrawOwnHoverUrl ) {
					this.SupportUrl.DrawHoverEffects( sb );
				}
			}
		}
	}
}
