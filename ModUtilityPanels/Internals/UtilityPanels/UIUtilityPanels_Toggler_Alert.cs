﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Services.AnimatedColor;


namespace ModUtilityPanels.Internals.UtilityPanels {
	/// @private
	partial class UIUtilityPanels : UIState {
		private IDictionary<string, bool> AlertTabs = new Dictionary<string, bool>();



		////////////////

		public void AddTabAlert( string tabName, bool isPriority ) {
			this.AlertTabs[ tabName ] = isPriority;
		}

		public void ClearTabAlert( string tabName ) {
			this.AlertTabs.Remove( tabName );
		}


		////////////////

		public bool IsTogglerUpdateAlertShown( out string tabName ) {
			tabName = this.AlertTabs
					.Where( kv => kv.Value )
					.Select( kv => kv.Key )
					.FirstOrDefault()
				?? this.AlertTabs
					.Select( kv => kv.Key )
					.FirstOrDefault();

			return this.AlertTabs.Count > 0;
		}


		////////////////

		private void DrawTogglerAlert( SpriteBatch sb, float opacity ) {
			if( this.AlertTabs.Any( kv => kv.Value ) ) {
				this.DrawTogglerPriorityAlert( sb, opacity );
			}

			this.DrawTogglerAlertIcon( sb );
		}


		////

		private void DrawTogglerAlertIcon( SpriteBatch sb ) {
			Color color = AnimatedColors.Alert?.CurrentColor
				?? Color.White;

			float scale = 1.15f;

			Vector2 pos = UIUtilityPanels.TogglerPosition;
			pos.X += 11f;
			pos.Y += 14f;

			Vector2 dim = Main.fontMouseText.MeasureString( "!" );

			Utils.DrawBorderStringFourWay(
				sb: sb,
				font: Main.fontMouseText,
				text: "!",
				x: pos.X,
				y: pos.Y,
				textColor: color,
				borderColor: Color.Black,
				origin: dim * 0.5f,
				scale: scale
			);
		}


		 private int PriorityAlertAnim = 0;

		private void DrawTogglerPriorityAlert( SpriteBatch sb, float opacity ) {
			Texture2D tex;

			if( this.PriorityAlertAnim == 0 ) {
				this.PriorityAlertAnim = 1;
				tex = UIUtilityPanels.AlertBorder1;
			} else if( this.PriorityAlertAnim == 1 ) {
				this.PriorityAlertAnim = 2;
				tex = UIUtilityPanels.AlertBorder2;
			} else if( this.PriorityAlertAnim == 2 ) {
				this.PriorityAlertAnim = 3;
				tex = UIUtilityPanels.AlertBorder3;
			} else {
				this.PriorityAlertAnim = 0;
				tex = UIUtilityPanels.AlertBorder2;
			}

			sb.Draw(
				texture: tex,
				position: UIUtilityPanels.TogglerPosition + new Vector2(-2f, -2f),
				color: Color.White * opacity
			);
		}
	}
}
