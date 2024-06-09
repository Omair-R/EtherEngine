using ImGuiNET;
using Microsoft.Xna.Framework;
using System;

namespace EtherGUI
{
    //Adapted from the example in the ImGUI.NET repo.
    public class GuiBatch
    {
        
        private EtherGui _etherGui;

        public GuiBatch(EtherGui etherGui)
        {
            _etherGui = etherGui ?? throw new ArgumentNullException(nameof(etherGui));
        }

        public void Begin(GameTime gameTime, bool isBuildDockSpace = true)
        {
            ImGui.GetIO().DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _etherGui._inputManager.UpdateInputState();
            ImGui.NewFrame();

            if(isBuildDockSpace)
                _etherGui.BuildDockSpace();
        }

        public void End()
        {
            ImGui.Render();
            unsafe { _etherGui._batcher.RenderDrawData(ImGui.GetDrawData()); }
        }

    }
}
