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
        }
    }
}
