using ExileCore.PoEMemory;

namespace SimpleTrade
{
    enum InviteType
    {
        Party,
        Trade,
        Unknown
    }

    class InviteElement
    {
        private string accountName { get; } = null;
        private InviteType inviteType { get; } = InviteType.Unknown;
        private Element acceptButton { get; } = null;

        public InviteElement(string name, InviteType type, Element button)
        {
            this.accountName = name;
            this.inviteType = type;
            this.acceptButton = button;
        }
}
}
