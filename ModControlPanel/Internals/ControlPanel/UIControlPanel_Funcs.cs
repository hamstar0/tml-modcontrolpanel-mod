﻿using System;
using Terraria;
using Terraria.UI;
using ModLibsCore.Services.Timers;


namespace ModControlPanel.Internals.ControlPanel {
	/// @private
	partial class UIControlPanel : UIState {
		public bool CanOpen() {
			return !this.IsOpen && !Main.inFancyUI;
		}


		public void Open() {
			this.IsOpen = true;

			Main.playerInventory = false;
			Main.editChest = false;
			Main.npcChatText = "";

			Main.inFancyUI = true;
			Main.InGameUI.SetState( (UIState)this );

			this.Backend = Main.InGameUI;

			//this.Recalculate();
			this.RecalculateMe();

			Timers.RunNow( this.RecalculateMe );
		}


		public void Close() {
			this.IsOpen = false;

			Main.inFancyUI = false;
			Main.InGameUI.SetState( (UIState)null );

			this.Backend = null;
		}
	}
}
