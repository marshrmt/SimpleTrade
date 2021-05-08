using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ExileCore.Shared.Attributes;

namespace SimpleTrade
{
    class SimpleTradeSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(true);
        [Menu("Accept Party From Account Name")] public TextNode AcceptPartyFrom { get; set; } = new TextNode("");
        [Menu("Accept Trade From Account Name")] public TextNode AcceptTradeFrom { get; set; } = new TextNode("");
    }
}
