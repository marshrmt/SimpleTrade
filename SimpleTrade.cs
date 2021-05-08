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

        private InviteElement GetInviteElement(Element element)
        {
            string accountName = null;
            InviteType inviteType = InviteType.Unknown;
            Element acceptButton = null;

            if (element.Children.Count == 3)
            {
                Element inviteTitlePanel = element.Children[0];
                Element inviteButtonsPanel = element.Children[2];

                if (inviteTitlePanel.Children.Count == 2)
                {
                    Element accountElement = inviteTitlePanel.Children[0];
                    Element inviteTextElement = inviteTitlePanel.Children[1];

                    if (accountElement.Children.Count == 2)
                    {
                        Element accountNameElement = accountElement.Children[1];

                        accountName = accountNameElement.Text;
                    }

                    if (inviteTextElement?.Text != null)
                    {
                        if (inviteTextElement.Text.Contains("party"))
                        {
                            inviteType = InviteType.Party;
                        }
                        else if (inviteTextElement.Text.Contains("trade"))
                        {
                            inviteType = InviteType.Trade;
                        }
                    }
                }
            }

            return new InviteElement(accountName, inviteType, acceptButton);
        }

        public override void Render()
        {
            Graphics.DrawText("Simple Trade is working", new Vector2(100, 100));
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

                        Graphics.DrawText($"inviteButtonsPanel children count: {inviteButtonsPanel.Children.Count}", new Vector2(100, 200));

                        if (inviteButtonsPanel.Children.Count == 2)
                        {
                            Graphics.DrawBox(inviteButtonsPanel.Children[0].GetClientRect(), Color.FromRgba(0x2200FF00));
                        }

                        //Graphics.DrawBox(inviteTitlePanel.GetClientRect(), Color.FromRgba(0x220000FF));
                        //Graphics.DrawBox(inviteElement.Children[1].GetClientRect(), Color.FromRgba(0x2200FF00));
                        //Graphics.DrawBox(inviteElement.Children[2].GetClientRect(), Color.FromRgba(0x22FF0000));
                    }
                }
            }
        }
    }
}
