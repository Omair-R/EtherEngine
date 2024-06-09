using ImGuiNET;
using EtherGUI.Internals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

using ImVec2 = System.Numerics.Vector2;

namespace EtherGUI
{
    public class EtherGui
    {
        public readonly TextureManager TextureManager;
        public readonly FontStore FontStore;
        internal readonly ImGuiInputManager _inputManager;

        internal GuiBatcher _batcher;
        private GraphicsDevice _graphicsDevice;
        private ImGuiIOPtr _io;

        ImVec2 _windowSize;

        public EtherGui(GraphicsDevice graphicsDevice, GameWindow window)
        {
            IntPtr context = ImGui.CreateContext();
            ImGui.SetCurrentContext(context);
            _graphicsDevice = graphicsDevice;
            TextureManager = new TextureManager();
            FontStore = new FontStore(graphicsDevice, TextureManager);
            _inputManager = new ImGuiInputManager(graphicsDevice, window);

            _batcher = new GuiBatcher(graphicsDevice, window, TextureManager);

            _io = ImGui.GetIO();
            _io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
            _io.ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;

            //FontStore.Add(Directory.GetCurrentDirectory() + @"\resources\Inter-Medium.ttf", "inter", 15);
            //FontStore.Add(Directory.GetCurrentDirectory() + @"\resources\Exo2-Medium.ttf", "exo2", 15);
            FontStore.Add(Directory.GetCurrentDirectory() + @"\resources\Archivo-Medium.ttf", "archivo", 15);

            ChangeStyle("default");


            _windowSize = new ImVec2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height) + ImGui.GetStyle().WindowPadding;
        }

        public void ChangeStyle(Action stylizer) => StyleCatalogue.ChangeStyle(stylizer);
        public void ChangeStyle(string theme) => StyleCatalogue.ChangeStyle(theme);

        internal void BuildDockSpace()
        {
            ImGui.SetNextWindowSize(_windowSize, ImGuiCond.FirstUseEver);
            ImGui.SetNextWindowSizeConstraints(_windowSize,
                                               _windowSize);
            ImGui.SetNextWindowPos(-ImGui.GetStyle().WindowPadding, ImGuiCond.FirstUseEver);

            ImGui.Begin("Dockspace demo - this title will be invisible",
                  ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoResize
                | ImGuiWindowFlags.NoBackground
                | ImGuiWindowFlags.NoTitleBar
                | ImGuiWindowFlags.NoCollapse
                | ImGuiWindowFlags.NoSavedSettings
                | ImGuiWindowFlags.NoBringToFrontOnFocus
                | ImGuiWindowFlags.NoDocking
                | ImGuiWindowFlags.NoScrollbar
                | ImGuiWindowFlags.NoDecoration);

            if (_io.ConfigFlags.HasFlag(ImGuiConfigFlags.DockingEnable))
            {
                uint dockspace_id = ImGui.GetID("DockSpace");
                ImGui.DockSpace(dockspace_id, _windowSize - ImGui.GetStyle().WindowPadding,
                    ImGuiDockNodeFlags.PassthruCentralNode
                    | ImGuiDockNodeFlags.AutoHideTabBar);
            }

            ImGui.End();
        }


    }
}
