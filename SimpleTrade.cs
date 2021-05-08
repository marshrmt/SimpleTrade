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



                        if (inviteTitlePanel.Children.Count == 2)
                        {
                            Element accountElement = inviteTitlePanel.Children[0];
                            Element inviteTextElement = inviteTitlePanel.Children[1];

                            if (accountElement.Children.Count == 2)
                            {
                                Element accountNameElement = accountElement.Children[1];

                                Graphics.DrawText($"Account name sent invite: {accountNameElement.Text}", new Vector2(100, 200));
                                Graphics.DrawText($"Invite Text: {inviteTextElement.Text}", new Vector2(100, 220));
                            }
                        }

                        if (invitePlayerPanel.Children.Count == 1)
                        {
                            Element invitePlayerWrapperPanel = invitePlayerPanel.Children[0];
                            if (invitePlayerWrapperPanel.Children.Count == 1)
                            {
                                invitePlayerWrapperPanel = invitePlayerWrapperPanel.Children[0];

                                if (invitePlayerWrapperPanel.Children.Count == 5)
                                {
                                    Element invitePlayerNamePanel = invitePlayerWrapperPanel.Children[0];
                                    Graphics.DrawText($"invitePlayerPanel child count: {invitePlayerNamePanel.Children.Count}", new Vector2(100, 240));
                                }
                                
                            }
                                
                        }
                        

                        //Graphics.DrawText($"text: {inviteTitlePanel.Children[0].Text}, long text: {inviteTitlePanel.Children[0].LongText}, child count: {inviteTitlePanel.Children[0].Children.Count}", new Vector2(100, 200));
                        //Graphics.DrawText($"text: {inviteTitlePanel.Children[1].Text}, long text: {inviteTitlePanel.Children[1].LongText}, child count: {inviteTitlePanel.Children[1].Children.Count}", new Vector2(100, 220));
                    }
                }
            }
        }
    }
}
