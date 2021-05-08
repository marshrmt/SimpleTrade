using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ExileCore.Shared.Attributes;

namespace SimpleTrade
{
    class SimpleTradeSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(true);
        [Menu("Accept Party From")] public TextNode AcceptPartyFrom { get; set; } = new TextNode("");
        [Menu("Accept Trade From")] public TextNode AcceptTradeFrom { get; set; } = new TextNode("");
        [Menu("Auto trade to Char Name")] public TextNode AutoTradeToCharName { get; set; } = new TextNode("");
    }
}
