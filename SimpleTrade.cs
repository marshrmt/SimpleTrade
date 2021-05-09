using System;
using System.Windows.Forms;
using ExileCore;
using ExileCore.PoEMemory;
using ExileCore.Shared.Enums;
using SharpDX;
using System.Threading;
using System.Linq;

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
                    if (GameController.IngameState.IngameUi.TradeWindow?.NameSeller == Settings.AcceptTradeFrom.Value)
                    {
                        var _playerInventory = GameController.IngameState.ServerData.GetPlayerInventoryByType(InventoryTypeE.MainInventory);

                        if (_playerInventory != null)
                        {
                            Mouse.BlockInput(true);

                            Input.KeyUp(Keys.LControlKey);
                            Thread.Sleep(random.Next(75) + 65);
                            Input.KeyDown(Keys.LControlKey);

                            var sortedInventory = _playerInventory?.InventorySlotItems?.OrderByDescending(I => I.SizeX * I.SizeY).ThenBy(I => I.PosX).ThenBy(I => I.PosY);

                            foreach (var _slotItem in sortedInventory)
                            {
                                // Ignore swap item
                                if (_slotItem.PosX == 11 && _slotItem.PosY == 0)
                                {
                                    continue;
                                }

                                if (GameController.IngameState.IngameUi.TradeWindow.IsVisible)
                                {
                                    Mouse.SetCursorPosition(_slotItem.GetClientRect());
                                    Thread.Sleep(random.Next(55) + 75);
                                    if (GameController.IngameState.IngameUi.TradeWindow.IsVisible) Input.Click(MouseButtons.Left);
                                    Thread.Sleep(random.Next(45) + 45);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            Thread.Sleep(random.Next(75) + 65);
                            Input.KeyUp(Keys.LControlKey);

                            Thread.Sleep(250);

                            if (GameController.IngameState.IngameUi.TradeWindow.IsVisible)
                            {
                                Mouse.SetCursorPosition(GameController.IngameState.IngameUi.TradeWindow.AcceptButton.GetClientRect());
                                Thread.Sleep(random.Next(120) + 310);
                                if (GameController.IngameState.IngameUi.TradeWindow?.NameSeller == Settings.AcceptTradeFrom.Value) Input.Click(MouseButtons.Left);
                                Thread.Sleep(random.Next(120) + 110);
                            }

                            Mouse.BlockInput(false);

                            while (GameController.IngameState.IngameUi.TradeWindow.IsVisible)
                            {
                                Thread.Sleep(75);
                            }

                            Thread.Sleep(500);
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

                            if (invite.inviteType == InviteType.Party && invite.characterName == Settings.AcceptPartyFrom.Value)
                            {
                                Mouse.BlockInput(true);

                                Mouse.SetCursorPosition(invite.acceptButtonClientRect);
                                Thread.Sleep(random.Next(120) + 310);
                                Input.Click(MouseButtons.Left);
                                Thread.Sleep(random.Next(120) + 110);

                                Mouse.BlockInput(false);

                                Thread.Sleep(500);

                                IsRunning = false;
                                return;
                            }
                            else if (invite.inviteType == InviteType.Trade && invite.characterName == Settings.AcceptTradeFrom.Value)
                            {
                                var _playerInventory = GameController.IngameState.ServerData.GetPlayerInventoryByType(InventoryTypeE.MainInventory);

                                if (_playerInventory?.InventorySlotItems != null && _playerInventory.InventorySlotItems.Count <= 2)
                                {
                                    IsRunning = false;
                                    return;
                                }

                                Mouse.BlockInput(true);

                                Mouse.SetCursorPosition(invite.acceptButtonClientRect);
                                Thread.Sleep(random.Next(120) + 310);
                                Input.Click(MouseButtons.Left);
                                Thread.Sleep(random.Next(120) + 110);

                                Mouse.BlockInput(false);

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
                Mouse.BlockInput(false);
                Input.KeyUp(Keys.LControlKey);
                IsRunning = false;
            }
        }

        private InviteElement GetInviteElement(Element element)
        {
            string accountName = null;
            string characterName = null;
            InviteType inviteType = InviteType.Unknown;
            RectangleF acceptButtonRect = new RectangleF();

            if (element.Children.Count == 3)
            {
                Element inviteTitlePanel = element.Children[0];
                Element characterInfoPanel = element.Children[1];
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

                if (characterInfoPanel.Children.Count == 1)
                {
                    characterInfoPanel = characterInfoPanel.Children[0];

                    if (characterInfoPanel.Children.Count == 1)
                    {
                        characterInfoPanel = characterInfoPanel.Children[0];

                        if (characterInfoPanel.Children.Count == 5)
                        {
                            characterName = characterInfoPanel.Children[0].Text;
                        }
                    }
                    
                }

                if (inviteButtonsPanel.Children.Count == 2)
                {
                    acceptButtonRect = inviteButtonsPanel.Children[0].GetClientRect();
                }
            }

            return new InviteElement(accountName, characterName, inviteType, acceptButtonRect);
        }

        public override void Render()
        {
            
        }
    }
}
