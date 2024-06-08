using Microsoft.Xna.Framework.Input;


namespace EtherEngine.Managers.Input
{
    public class KeyMapper
    {
        private Keys _innerKey;
        public Keys InnerKey
        {
            get
            {
                return _innerKey;
            }
        }

        public KeyMapper(Keys key)
        {
            _innerKey = key;
        }

        public void RemapKey(Keys key)
        {
            _innerKey = key;
        }


    }
}
