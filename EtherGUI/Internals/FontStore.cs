using ImGuiNET;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace EtherGUI.Internals
{
    public class FontStore
    {
        private Dictionary<string, ImFontPtr> _fonts;

        private IntPtr? _defaultFontTextureId;

        private GraphicsDevice _graphicsDevice;
        private TextureManager _textureManager;

        public FontStore(GraphicsDevice graphicsDevice, TextureManager textureManager)
        {
            _graphicsDevice = graphicsDevice;
            _textureManager = textureManager;
            ImGuiIOPtr io = ImGui.GetIO();
            io.Fonts.Clear();
            io.Fonts.AddFontDefault();
            RebuildFonts();

            _fonts = new Dictionary<string, ImFontPtr>();
        }

        public unsafe bool Add(string filePath, string name, float sizePixels)
        {
            ImFontPtr font = ImGui.GetIO().Fonts.AddFontFromFileTTF(filePath, sizePixels);

            if (font.NativePtr == null) return false;

            _fonts.Add(name, font);
            RebuildFonts();

            return true;
        }

        public bool ChangeFontSize(string name, float sizePixels)
        {
            if (!_fonts.TryGetValue(name, out var font)) return false;

            font.FontSize = sizePixels;
            return true;
        }

        public bool IsDefaultFontLoaded() => _defaultFontTextureId.HasValue;
        public IntPtr GetDefaultFontID() => _defaultFontTextureId ?? throw new ArgumentNullException();

        public bool UseFont(string FontName)
        {
            if (!_fonts.TryGetValue(FontName, out ImFontPtr font)) return false;

            ImGui.PushFont(font);
            return true;
        }

        public bool PopFont()
        {
            ImGui.PopFont();
            return true;
        }

        private unsafe void RebuildFonts()
        {
            var io = ImGui.GetIO();

            io.Fonts.GetTexDataAsRGBA32(out byte* pixelData, out int width, out int height, out int bytesPerPixel);

            var pixels = new byte[width * height * bytesPerPixel];
            unsafe { Marshal.Copy(new IntPtr(pixelData), pixels, 0, pixels.Length); }

            var tex2d = new Texture2D(_graphicsDevice, width, height, false, SurfaceFormat.Color);
            tex2d.SetData(pixels);

            if (IsDefaultFontLoaded()) _textureManager.Unload(GetDefaultFontID());

            _defaultFontTextureId = _textureManager.Load(tex2d, _defaultFontTextureId);

            io.Fonts.SetTexID(GetDefaultFontID());
            io.Fonts.ClearTexData();
        }

    }
}
