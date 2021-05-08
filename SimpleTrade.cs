using System;
using ExileCore;
using ExileCore.PoEMemory.Elements;
using ExileCore.PoEMemory.Components;
using ExileCore.Shared.Enums;
using SharpDX;

namespace SimpleTrade
{
    class SimpleTrade : BaseSettingsPlugin<SimpleTradeSettings>
    {

        public override void Render()
        {
            Graphics.DrawText("Simple Trade is working", new SharpDX.Vector2(100, 100));
            Graphics.DrawText($"Invites Panel is visible: {GameController.IngameState.IngameUi.InvitesPanel.IsVisible}", new SharpDX.Vector2(100, 120));
            Graphics.DrawText($"Trade Window is visible: {GameController.IngameState.IngameUi.TradeWindow.IsVisible}", new SharpDX.Vector2(100, 140));

            if (GameController.IngameState.IngameUi.TradeWindow.IsVisible)
            {
                Graphics.DrawText($"Trading with: {GameController.IngameState.IngameUi.TradeWindow.NameSeller}", new SharpDX.Vector2(100, 160));
            }

            if (GameController.IngameState.IngameUi.InvitesPanel.IsVisible)
            {
                Graphics.DrawText($"invites text: {GameController.IngameState.IngameUi.InvitesPanel.Text}", new SharpDX.Vector2(100, 180));
                Graphics.DrawText($"invites long text: {GameController.IngameState.IngameUi.InvitesPanel.LongText}", new SharpDX.Vector2(100, 200));
            }
        }
    }
}
