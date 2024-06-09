using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace EtherGUI.Internals
{
    public class TextureManager
    {
        private long _lastId;
        private Dictionary<IntPtr, Texture2D> _loadedTextures;

        public TextureManager()
        {
            _loadedTextures = new Dictionary<IntPtr, Texture2D>();
        }

        public IntPtr Load(Texture2D texture, IntPtr? reusablePtr = null)
        {
            IntPtr id;

            if (reusablePtr.HasValue) id = reusablePtr.Value;
            else id = new IntPtr(_lastId++);

            _loadedTextures.Add(id, texture);

            return id;
        }

        public void Unload(IntPtr textureId)
        {
            _loadedTextures.Remove(textureId);
        }

        public Texture2D Get(IntPtr id) => _loadedTextures[id];
        public bool Has(IntPtr id) => _loadedTextures.ContainsKey(id);

    }
}
