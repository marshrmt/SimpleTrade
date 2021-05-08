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
            Graphics.DrawText($"Invites Panel is visible: {GameController.IngameState.IngameUi.InvitesPanel.IsVisible}", new Vector2(100, 120));
            Graphics.DrawText($"Trade Window is visible: {GameController.IngameState.IngameUi.TradeWindow.IsVisible}", new Vector2(100, 140));

            if (GameController.IngameState.IngameUi.TradeWindow.IsVisible)
            {
                Graphics.DrawText($"Trading with: {GameController.IngameState.IngameUi.TradeWindow.NameSeller}", new Vector2(100, 160));
            }

            Graphics.DrawText($"UI Children count: {GameController.IngameState.IngameUi.Children.Count}", new Vector2(100, 180));


            int i = 0;
            int row = 0;
            string result = "";
            string sign;
            foreach (var c in GameController.IngameState.IngameUi.Children)
            {
                if (c.IsVisible)
                {
                    sign = "+";
                }
                else
                {
                    sign = "-";
                }

                result += $"{i}[{sign}] ";

                if (i % 20 == 0)
                {
                    Graphics.DrawText(result, new Vector2(100, 200 + row * 20));
                    result = "";
                    row++;
                }
                i++;
            }
        }
    }
}
