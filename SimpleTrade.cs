using System;
using System.Windows.Forms;
using ExileCore;
using ExileCore.PoEMemory;
using ExileCore.PoEMemory.Elements;
using ExileCore.PoEMemory.Components;
using ExileCore.Shared.Enums;
using SharpDX;
using System.Threading;

namespace SimpleTrade
{


    class SimpleTrade : BaseSettingsPlugin<SimpleTradeSettings>
    {

        private static bool IsRunning { get; set; } = false;

        private static float PrevInvitePos { get; set; } = 0;

        private static DateTime LastInvitePosChange { get; set; } = DateTime.Now;

        private readonly Random random = new Random();

        public override Job Tick()
        {
            if (IsRunning) return null;

            IsRunning = true;

            return new Job("SimpleTrade", Job);
        }

        private void Job()
        {
            try
            {
                if (GameController.IngameState.IngameUi.TradeWindow.IsVisible)
                {
                    if (GameController.IngameState.IngameUi.TradeWindow.NameSeller == Settings.AutoTradeToCharName.Value)
                    {
                        var _playerInventory = GameController.IngameState.ServerData.GetPlayerInventoryByType(InventoryTypeE.MainInventory);

                        if (_playerInventory != null)
                        {
                            Input.KeyUp(Keys.LControlKey);
                            Thread.Sleep(random.Next(75) + 65);
                            Input.KeyDown(Keys.LControlKey);

                            foreach (var _slotItem in _playerInventory.InventorySlotItems)
                            {
                                // Ignore swap item
                                if (_slotItem.PosX == 11 && _slotItem.PosY == 0)
                                {
                                    //break;
                                }

                                if (GameController.IngameState.IngameUi.TradeWindow.IsVisible)
                                {
                                    Mouse.SetCursorPosition(_slotItem.GetClientRect());
                                    Thread.Sleep(random.Next(75) + 65);
                                    if (GameController.IngameState.IngameUi.TradeWindow.IsVisible) Input.Click(MouseButtons.Left);
                                    Thread.Sleep(random.Next(75) + 65);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            Thread.Sleep(random.Next(75) + 65);
                            Input.KeyUp(Keys.LControlKey);
                        }
                    }
                }
                else if (GameController.IngameState.IngameUi.InvitesPanel.IsVisible && GameController.IngameState.IngameUi.InvitesPanel.ChildCount > 0)
                {
                    foreach (var e in GameController.IngameState.IngameUi.InvitesPanel.Children)
                    {
                        if (e.Children.Count == 3)
                        {
                            if (e.GetClientRect().Y != PrevInvitePos)
                            {
                                LastInvitePosChange = DateTime.Now;
                                PrevInvitePos = e.GetClientRect().Y;
                                break;
                            }

                            PrevInvitePos = e.GetClientRect().Y;
                        }
                    }

                    // Wait while invite panel scrolling up
                    if (LastInvitePosChange.AddMilliseconds(300) < DateTime.Now)
                    {
                        foreach (var e in GameController.IngameState.IngameUi.InvitesPanel.Children)
                        {
                            InviteElement invite = GetInviteElement(e);

                            if (invite.inviteType == InviteType.Party && invite.accountName == Settings.AcceptPartyFrom.Value)
                            {
                                Mouse.SetCursorPosition(invite.acceptButtonClientRect);
                                Thread.Sleep(random.Next(120) + 310);
                                Input.Click(MouseButtons.Left);
                                Thread.Sleep(random.Next(120) + 110);

                                Thread.Sleep(500);

                                IsRunning = false;
                                return;
                            }
                            else if (invite.inviteType == InviteType.Trade && invite.accountName == Settings.AcceptTradeFrom.Value)
                            {
                                var _playerInventory = GameController.IngameState.ServerData.GetPlayerInventoryByType(InventoryTypeE.MainInventory);

                                if (_playerInventory?.InventorySlotItems != null && _playerInventory.InventorySlotItems.Count <= 2)
                                {
                                    IsRunning = false;
                                    return;
                                }

                                Mouse.SetCursorPosition(invite.acceptButtonClientRect);
                                Thread.Sleep(random.Next(120) + 310);
                                Input.Click(MouseButtons.Left);
                                Thread.Sleep(random.Next(120) + 110);

                                Thread.Sleep(500);

                                IsRunning = false;
                                return;
                            }
                        }
                    }

                }
                else
                {
                    IsRunning = false;
                    return;
                }

                IsRunning = false;
                return;
            }
            catch
            {
                Input.KeyUp(Keys.LControlKey);
                IsRunning = false;
            }
        }

        private InviteElement GetInviteElement(Element element)
        {
            string accountName = null;
            InviteType inviteType = InviteType.Unknown;
            RectangleF acceptButtonRect = new RectangleF();

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

                if (inviteButtonsPanel.Children.Count == 2)
                {
                    acceptButtonRect = inviteButtonsPanel.Children[0].GetClientRect();
                }
            }

            return new InviteElement(accountName, inviteType, acceptButtonRect);
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
                
                /*if (GameController.IngameState.IngameUi.InvitesPanel.Children.Count > 0)
                {
                    if (GameController.IngameState.IngameUi.InvitesPanel.Children[0].GetClientRect().Y != prevInvitePos)
                    {
                        lastInvitePosChange = DateTime.Now;
                    }

                    if (lastInvitePosChange.AddMilliseconds(300) < DateTime.Now)
                    {
                        int i = 0;
                        foreach (var e in GameController.IngameState.IngameUi.InvitesPanel.Children)
                        {
                            InviteElement invite = GetInviteElement(e);

                            Graphics.DrawText($"inv acc name {invite.accountName}, type: {invite.inviteType}", new Vector2(100, 200 + i * 20));

                            Graphics.DrawBox(invite.acceptButtonClientRect, Color.FromRgba(0x2200FF00));

                            i++;
                        }
                    }

                    prevInvitePos = GameController.IngameState.IngameUi.InvitesPanel.Children[0].GetClientRect().Y;

                }*/
            }
        }
    }
}
