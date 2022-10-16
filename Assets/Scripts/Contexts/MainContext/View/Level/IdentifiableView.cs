using strange.extensions.mediation.impl;

namespace Contexts.MainContext
{
    public class IdentifiableView : View
    {
        public ushort ID { get; private set; }

        public void SetID(ushort id)
        {
            ID = id;
        }
    }
}
