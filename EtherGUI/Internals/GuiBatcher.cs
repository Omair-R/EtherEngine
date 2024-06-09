using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EtherGUI.Internals
{
    public class GuiBatcher
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly GameWindow _window;
        private readonly TextureManager _textureManager;

        private BasicEffect _basicEffect;
        private RasterizerState _rasterizerState;

        private byte[] _vertices;
        private byte[] _indices;

        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;

        private VertexDeclaration _vertexDeclaration;

        public GuiBatcher(GraphicsDevice graphicsDevice, GameWindow window, TextureManager textureManager)
        {
            _graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            _window = window ?? throw new ArgumentNullException(nameof(window));
            _textureManager = textureManager;

            VertexElement[] elements = new VertexElement[] {

                new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),

                new VertexElement(8, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),

                new VertexElement(16, VertexElementFormat.Color, VertexElementUsage.Color, 0)

            };

            _vertexDeclaration = new VertexDeclaration(elements);

            _rasterizerState = new RasterizerState()
            {
                CullMode = CullMode.None,
                DepthBias = 0,
                FillMode = FillMode.Solid,
                MultiSampleAntiAlias = false,
                ScissorTestEnable = true,
                SlopeScaleDepthBias = 0
            };


        }

        protected virtual Effect ApplyEffect(Texture2D texture)
        {
            _basicEffect = _basicEffect ?? new BasicEffect(_graphicsDevice);

            var io = ImGui.GetIO();

            _basicEffect.World = Matrix.Identity;
            _basicEffect.View = Matrix.Identity;
            _basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0f, io.DisplaySize.X, io.DisplaySize.Y, 0f, -1f, 1f);
            _basicEffect.TextureEnabled = true;
            _basicEffect.Texture = texture;
            _basicEffect.VertexColorEnabled = true;

            return _basicEffect;
        }

        public void RenderDrawData(ImDrawDataPtr drawData)
        {
            var lastViewport = _graphicsDevice.Viewport;
            var lastScissorBox = _graphicsDevice.ScissorRectangle;

            _graphicsDevice.BlendFactor = Color.White;
            _graphicsDevice.BlendState = BlendState.NonPremultiplied;
            _graphicsDevice.RasterizerState = _rasterizerState;
            _graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;

            drawData.ScaleClipRects(ImGui.GetIO().DisplayFramebufferScale);

            _graphicsDevice.Viewport = new Viewport(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight);

            UpdateBuffers(drawData);

            RenderCommandLists(drawData);

            _graphicsDevice.Viewport = lastViewport;
            _graphicsDevice.ScissorRectangle = lastScissorBox;
        }

        private unsafe void UpdateBuffers(ImDrawDataPtr drawData)
        {
            if (drawData.TotalVtxCount == 0)
            {
                return;
            }

            // Expand buffers if we need more room
            if (_vertexBuffer is null || drawData.TotalVtxCount > _vertexBuffer.VertexCount)
            {
                _vertexBuffer?.Dispose();

                _vertexBuffer = new VertexBuffer(_graphicsDevice,
                                                 _vertexDeclaration,
                                                 (int)(drawData.TotalVtxCount * 1.5f),
                                                 BufferUsage.None);

                _vertices = new byte[_vertexBuffer.VertexCount * _vertexDeclaration.VertexStride];
            }

            if (_indexBuffer is null || drawData.TotalIdxCount > _indexBuffer.IndexCount)
            {
                _indexBuffer?.Dispose();

                _indexBuffer = new IndexBuffer(_graphicsDevice,
                                               IndexElementSize.SixteenBits,
                                               (int)(drawData.TotalIdxCount * 1.5f),
                                               BufferUsage.None);

                _indices = new byte[_indexBuffer.IndexCount * sizeof(ushort)];
            }

            int vtxOffset = 0;
            int idxOffset = 0;

            for (int n = 0; n < drawData.CmdListsCount; n++)
            {
                ImDrawListPtr cmdList = drawData.CmdLists[n];

                fixed (void* vtxDstPtr = &_vertices[vtxOffset * _vertexDeclaration.VertexStride])
                fixed (void* idxDstPtr = &_indices[idxOffset * sizeof(ushort)])

                {
                    Buffer.MemoryCopy((void*)cmdList.VtxBuffer.Data, vtxDstPtr, _vertices.Length, cmdList.VtxBuffer.Size * _vertexDeclaration.VertexStride);
                    Buffer.MemoryCopy((void*)cmdList.IdxBuffer.Data, idxDstPtr, _indices.Length, cmdList.IdxBuffer.Size * sizeof(ushort));
                }

                vtxOffset += cmdList.VtxBuffer.Size;
                idxOffset += cmdList.IdxBuffer.Size;
            }

            _vertexBuffer.SetData(_vertices, 0, drawData.TotalVtxCount * _vertexDeclaration.VertexStride);
            _indexBuffer.SetData(_indices, 0, drawData.TotalIdxCount * sizeof(ushort));
        }

        private unsafe void RenderCommandLists(ImDrawDataPtr drawData)
        {
            _graphicsDevice.SetVertexBuffer(_vertexBuffer);
            _graphicsDevice.Indices = _indexBuffer;

            int vtxOffset = 0;
            int idxOffset = 0;

            for (int n = 0; n < drawData.CmdListsCount; n++)
            {
                ImDrawListPtr cmdList = drawData.CmdLists[n];

                for (int cmdi = 0; cmdi < cmdList.CmdBuffer.Size; cmdi++)
                {
                    ImDrawCmdPtr drawCmd = cmdList.CmdBuffer[cmdi];

                    if (drawCmd.ElemCount == 0)
                    {
                        continue;
                    }

                    if (!_textureManager.Has(drawCmd.TextureId))
                    {
                        throw new InvalidOperationException($"Could not find a texture with id '{drawCmd.TextureId}', please check your bindings");
                    }

                    _graphicsDevice.ScissorRectangle = new Rectangle(
                        (int)drawCmd.ClipRect.X,
                        (int)drawCmd.ClipRect.Y,
                        (int)(drawCmd.ClipRect.Z - drawCmd.ClipRect.X),
                        (int)(drawCmd.ClipRect.W - drawCmd.ClipRect.Y)
                    );

                    var effect = ApplyEffect(_textureManager.Get(drawCmd.TextureId));

                    foreach (var pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        _graphicsDevice.DrawIndexedPrimitives(
                            PrimitiveType.TriangleList,
                            (int)drawCmd.VtxOffset + vtxOffset,
                            (int)drawCmd.IdxOffset + idxOffset,
                            (int)drawCmd.ElemCount / 3
                        );

                    }
                }

                vtxOffset += cmdList.VtxBuffer.Size;
                idxOffset += cmdList.IdxBuffer.Size;
            }
        }

    }
}
