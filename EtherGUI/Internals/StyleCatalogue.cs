using ImGuiNET;
using System;
using ImVec2 = System.Numerics.Vector2;
using ImVec4 = System.Numerics.Vector4;

namespace EtherGUI.Internals
{
    public static class StyleCatalogue
    {
        public static void ChangeStyle(Action stylizer)
        {
            stylizer();
        }

        public static void ChangeStyle(string theme)
        {
            switch (theme)
            {
                case "default":
                    NiceDarkStyle();
                    break;

                default:
                    NiceDarkStyle();
                    break;
            }
        }

        public static void NiceDarkStyle()
        {
            ImGuiStylePtr style = ImGui.GetStyle();
            var colors = ImGui.GetStyle().Colors;

            style.Alpha = 1.0f;

            style.WindowPadding = new ImVec2(12, 12);
            style.FramePadding = new ImVec2(8, 1);
            style.ItemSpacing = new ImVec2(8, 7);
            style.ItemInnerSpacing = new ImVec2(12, 7);
            style.TouchExtraPadding = new ImVec2(0, 0);
            style.IndentSpacing = 1;
            style.ScrollbarSize = 11;
            style.GrabMinSize = 12;

            style.WindowBorderSize = 1;
            style.ChildBorderSize = 1;
            style.PopupBorderSize = 1;
            style.FrameBorderSize = 0;
            style.TabBarBorderSize = 1;

            style.WindowRounding = 8;
            style.ChildRounding = 6;
            style.FrameRounding = 3;
            style.PopupRounding = 5;
            style.ScrollbarRounding = 10;
            style.GrabRounding = 12;
            style.TabRounding = 5;


            style.WindowTitleAlign = new ImVec2(0, 0.4f);
            style.SeparatorTextPadding = new ImVec2(17, 1);
            style.SeparatorTextBorderSize = 2;
            style.DockingSeparatorSize = 3;


            colors[(int)ImGuiCol.Text] = new ImVec4(0.88f, 0.84f, 0.90f, 1.00f);
            colors[(int)ImGuiCol.TextDisabled] = new ImVec4(0.44f, 0.38f, 0.49f, 1.00f);
            colors[(int)ImGuiCol.WindowBg] = new ImVec4(0.08f, 0.06f, 0.09f, 0.99f);
            colors[(int)ImGuiCol.ChildBg] = new ImVec4(0.09f, 0.06f, 0.10f, 0.00f);
            colors[(int)ImGuiCol.PopupBg] = new ImVec4(0.06f, 0.02f, 0.09f, 0.98f);
            colors[(int)ImGuiCol.Border] = new ImVec4(0.49f, 0.39f, 0.55f, 0.50f);
            colors[(int)ImGuiCol.BorderShadow] = new ImVec4(0.05f, 0.03f, 0.07f, 0.00f);
            colors[(int)ImGuiCol.FrameBg] = new ImVec4(0.24f, 0.15f, 0.32f, 0.54f);
            colors[(int)ImGuiCol.FrameBgHovered] = new ImVec4(0.34f, 0.26f, 0.49f, 0.40f);
            colors[(int)ImGuiCol.FrameBgActive] = new ImVec4(0.48f, 0.36f, 0.66f, 0.67f);
            colors[(int)ImGuiCol.TitleBg] = new ImVec4(0.23f, 0.14f, 0.30f, 1.00f);
            colors[(int)ImGuiCol.TitleBgActive] = new ImVec4(0.36f, 0.26f, 0.51f, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new ImVec4(0.00f, 0.00f, 0.00f, 0.51f);
            colors[(int)ImGuiCol.MenuBarBg] = new ImVec4(0.23f, 0.18f, 0.30f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarBg] = new ImVec4(0.19f, 0.12f, 0.25f, 0.53f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new ImVec4(0.45f, 0.38f, 0.53f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new ImVec4(0.41f, 0.41f, 0.41f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new ImVec4(0.51f, 0.51f, 0.51f, 1.00f);
            colors[(int)ImGuiCol.CheckMark] = new ImVec4(0.69f, 0.65f, 0.72f, 1.00f);
            colors[(int)ImGuiCol.SliderGrab] = new ImVec4(0.59f, 0.50f, 0.64f, 1.00f);
            colors[(int)ImGuiCol.SliderGrabActive] = new ImVec4(0.22f, 0.16f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.Button] = new ImVec4(0.22f, 0.16f, 0.26f, 1.00f);
            colors[(int)ImGuiCol.ButtonHovered] = new ImVec4(0.37f, 0.25f, 0.45f, 1.00f);
            colors[(int)ImGuiCol.ButtonActive] = new ImVec4(0.45f, 0.39f, 0.54f, 1.00f);
            colors[(int)ImGuiCol.Header] = new ImVec4(0.65f, 0.46f, 0.80f, 0.31f);
            colors[(int)ImGuiCol.HeaderHovered] = new ImVec4(0.54f, 0.45f, 0.64f, 0.80f);
            colors[(int)ImGuiCol.HeaderActive] = new ImVec4(0.78f, 0.67f, 0.87f, 1.00f);
            colors[(int)ImGuiCol.Separator] = new ImVec4(0.22f, 0.17f, 0.32f, 0.50f);
            colors[(int)ImGuiCol.SeparatorHovered] = new ImVec4(0.23f, 0.16f, 0.35f, 0.78f);
            colors[(int)ImGuiCol.SeparatorActive] = new ImVec4(0.43f, 0.34f, 0.57f, 1.00f);
            colors[(int)ImGuiCol.ResizeGrip] = new ImVec4(0.40f, 0.30f, 0.54f, 0.20f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new ImVec4(0.47f, 0.37f, 0.68f, 0.67f);
            colors[(int)ImGuiCol.ResizeGripActive] = new ImVec4(0.57f, 0.40f, 0.72f, 0.95f);
            colors[(int)ImGuiCol.Tab] = new ImVec4(0.23f, 0.17f, 0.31f, 0.86f);
            colors[(int)ImGuiCol.TabHovered] = new ImVec4(0.40f, 0.28f, 0.57f, 0.80f);
            colors[(int)ImGuiCol.TabActive] = new ImVec4(0.38f, 0.28f, 0.54f, 1.00f);
            colors[(int)ImGuiCol.TabUnfocused] = new ImVec4(0.10f, 0.08f, 0.17f, 0.97f);
            colors[(int)ImGuiCol.TabUnfocusedActive] = new ImVec4(0.21f, 0.12f, 0.31f, 1.00f);
            colors[(int)ImGuiCol.DockingPreview] = new ImVec4(0.36f, 0.26f, 0.62f, 0.70f);
            colors[(int)ImGuiCol.DockingEmptyBg] = new ImVec4(0.20f, 0.20f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.PlotLines] = new ImVec4(0.61f, 0.61f, 0.61f, 1.00f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new ImVec4(0.46f, 0.30f, 0.81f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogram] = new ImVec4(0.59f, 0.65f, 0.94f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new ImVec4(0.95f, 0.54f, 0.97f, 1.00f);
            colors[(int)ImGuiCol.TableHeaderBg] = new ImVec4(0.19f, 0.19f, 0.20f, 1.00f);
            colors[(int)ImGuiCol.TableBorderStrong] = new ImVec4(0.31f, 0.31f, 0.35f, 1.00f);
            colors[(int)ImGuiCol.TableBorderLight] = new ImVec4(0.23f, 0.23f, 0.25f, 1.00f);
            colors[(int)ImGuiCol.TableRowBg] = new ImVec4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.TableRowBgAlt] = new ImVec4(1.00f, 1.00f, 1.00f, 0.06f);
            colors[(int)ImGuiCol.TextSelectedBg] = new ImVec4(0.51f, 0.33f, 0.73f, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new ImVec4(1.00f, 0.62f, 0.96f, 0.90f);
            colors[(int)ImGuiCol.NavHighlight] = new ImVec4(0.49f, 0.34f, 0.75f, 1.00f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new ImVec4(1.00f, 1.00f, 1.00f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new ImVec4(0.80f, 0.80f, 0.80f, 0.20f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new ImVec4(0.80f, 0.80f, 0.80f, 0.35f);

        }
    }
}
