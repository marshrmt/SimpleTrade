using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ExileCore.Shared.Attributes;

namespace SimpleTrade
{
    class SimpleTradeSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(true);
    }
}
