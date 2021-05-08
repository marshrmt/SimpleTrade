using ExileCore.PoEMemory;
using SharpDX;

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
        public string accountName { get; private set; } = null;
        public string characterName { get; private set; } = null;
        public InviteType inviteType { get; private set; } = InviteType.Unknown;
        public RectangleF acceptButtonClientRect { get; private set; } = new RectangleF();

        public InviteElement(string name, InviteType type, RectangleF buttonRect)
        {
            this.accountName = name;
            this.inviteType = type;
            this.acceptButtonClientRect = buttonRect;
        }
}
}
