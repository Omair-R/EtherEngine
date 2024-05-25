using Microsoft.Xna.Framework.Input;


namespace EtherEngine.Input
{
    public class KeyMapper
    {
        private Keys _innerKey;
        public Keys InnerKey { 
            get {
                return _innerKey;
            } 
        }

        public KeyMapper(Keys key) { 
            this._innerKey = key;
        }
        
        public void RemapKey(Keys key)
        {
            this._innerKey = key;
        }


    }
}
