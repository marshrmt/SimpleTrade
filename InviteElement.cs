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

        public InviteElement(string accName, string charName, InviteType type, RectangleF buttonRect)
        {
            this.accountName = accName;
            this.characterName = charName;
            this.inviteType = type;
            this.acceptButtonClientRect = buttonRect;
        }
}
}
