using System;
using ExileCore;
using ExileCore.PoEMemory;
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
            Color highlight = Color.FromRgba(0x22FFFFFF);

            Graphics.DrawText("Simple Trade is working", new SharpDX.Vector2(100, 100));
            Graphics.DrawText($"Invites Panel is visible: {GameController.IngameState.IngameUi.InvitesPanel.IsVisible}", new Vector2(100, 120));
            Graphics.DrawText($"Trade Window is visible: {GameController.IngameState.IngameUi.TradeWindow.IsVisible}", new Vector2(100, 140));

            if (GameController.IngameState.IngameUi.TradeWindow.IsVisible)
            {
                Graphics.DrawText($"Trading with: {GameController.IngameState.IngameUi.TradeWindow.NameSeller}", new Vector2(100, 160));
            }

            if (GameController.IngameState.IngameUi.InvitesPanel.IsVisible)
            {
                Graphics.DrawText($"Invites Panel children count: {GameController.IngameState.IngameUi.InvitesPanel.Children.Count}", new Vector2(100, 180));

                if (GameController.IngameState.IngameUi.InvitesPanel.Children.Count == 1)
                {
                    Element inviteElement = GameController.IngameState.IngameUi.InvitesPanel.Children[0];

                    if (inviteElement.Children.Count == 3)
                    {
                        Element inviteTitlePanel = inviteElement.Children[0];
                        Element invitePlayerPanel = inviteElement.Children[1];
                        Element inviteButtonsPanel = inviteElement.Children[2];

                        //Graphics.DrawBox(inviteTitlePanel.GetClientRect(), Color.FromRgba(0x220000FF));
                        //Graphics.DrawBox(inviteElement.Children[1].GetClientRect(), Color.FromRgba(0x2200FF00));
                        //Graphics.DrawBox(inviteElement.Children[2].GetClientRect(), Color.FromRgba(0x22FF0000));

                        Graphics.DrawText($"text: {inviteTitlePanel.Text}, long text: {inviteTitlePanel.LongText}, child count: {inviteTitlePanel.Children.Count}", new Vector2(100, 200));
                    }
                }
            }
        }
    }
}
