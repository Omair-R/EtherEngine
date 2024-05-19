using Microsoft.Xna.Framework.Input;


namespace EtherEngine.Input
{
    public class KeyMapper
    {
        private Keys innerKey;
        public Keys InnerKey { 
            get {
                return innerKey;
            } 
        }

        public KeyMapper(Keys key) { 
            this.innerKey = key;
        }
        
        public void RemapKey(Keys key)
        {
            this.innerKey = key;
        }


    }
}
