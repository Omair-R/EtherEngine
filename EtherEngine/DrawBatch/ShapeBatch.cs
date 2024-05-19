using EtherEngine.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.DrawBatch
{
    public class ShapeBatch : IDrawBatch
    {
        protected VertexPositionColor[] vertices;
        protected short[] indices;

        protected int verticesCount;
        protected int indicesCount;
        protected int maxVerticesCount;

        protected GraphicsDevice device;
        protected BasicEffect effect;

        protected bool isStarted;

        public float Alpha { get; set; }

        public ShapeBatch(GraphicsDevice graphicsDevice, int capacity = 1024, float alpha= 0.5f)
        {
            this.device = graphicsDevice;
            this.maxVerticesCount = capacity;
            this.Alpha = alpha;

            vertices = new VertexPositionColor[maxVerticesCount];
            indices = new short[maxVerticesCount * 3];

            verticesCount = 0;
            indicesCount = 0;
            isStarted = false;
        }

        public void Begin(RasterizerState rasterizerState = null, 
                Effect effect = null,
                Matrix? transformMatrix = null)
        {

            if (isStarted) throw new Exception("This Shape factory has already started.");

            BasicEffect basicEffect = null;

            if (effect != null)
            {
                basicEffect = effect as BasicEffect;

                if (basicEffect != null) throw new Exception("only accepts basic effects for now.");
            }
            
            SetEffect(basicEffect);

            if (rasterizerState == null) {
                rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
            }

            device.RasterizerState = rasterizerState;

            if (transformMatrix != null)
            {
                this.effect.View = (Matrix)transformMatrix;
            }


            isStarted = true;
        }

        public void End()
        {
            if (!isStarted) throw new Exception("This Shape factory has started yet, please start it first.");

            Flush();

            isStarted = false;
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void SetEffect(BasicEffect effect)
        {
            if (effect == null)
            {
                Viewport viewport = device.Viewport;

                this.effect = new BasicEffect(device)
                {
                    VertexColorEnabled = true,
                    TextureEnabled = false,
                    FogEnabled = false,
                    LightingEnabled = false,
                    Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1),
                    Alpha = this.Alpha //change blend type instead.
                };
            }
            else
            {
                this.effect = effect;
            }
        }

        public void ResetBuffers()
        {
            verticesCount = 0;
            indicesCount = 0;
        }

        private void CheckCapacity(int checkVertices)
        {
            if (checkVertices > maxVerticesCount) throw new Exception("The number of vertices is higher than the maximum capacity of the vertex buffer."); ;
        }

        protected void DrawRectangle(float x, float y, float width, float height, Color color)
        {
            CheckCapacity(4);

            var bottom_left = new VertexPositionColor(new Vector3(x, y, 0), color);
            var top_left = new VertexPositionColor(new Vector3(x, y + height, 0), color);
            var bottom_right = new VertexPositionColor(new Vector3(x + width, y, 0), color);
            var top_right = new VertexPositionColor(new Vector3(x + width, y + height, 0), color);

            indices[indicesCount++] = (short)(0 + verticesCount);
            indices[indicesCount++] = (short)(1 + verticesCount);
            indices[indicesCount++] = (short)(2 + verticesCount);
            indices[indicesCount++] = (short)(0 + verticesCount);
            indices[indicesCount++] = (short)(2 + verticesCount);
            indices[indicesCount++] = (short)(3 + verticesCount);

            vertices[verticesCount++] = top_left;
            vertices[verticesCount++] = bottom_left;
            vertices[verticesCount++] = bottom_right;
            vertices[verticesCount++] = top_right;
        }

        virtual public void DrawRectangle(StaticQuad quad, Color color)
        {
            DrawRectangle(quad.X - quad.Width / 2, quad.Y - quad.Height / 2, quad.Width, quad.Height, color);
        }

        virtual public void DrawRectangle(RotatableQuad quad, Color color)
        {
            Vector2[] quadVertices = quad.Vertices;
            CheckCapacity(quadVertices.Length);

            int triangleCount = quadVertices.Length - 2;

            for (int i = 0; i < triangleCount; i++)
            {
                indices[indicesCount++] = (short)(0 + verticesCount);
                indices[indicesCount++] = (short)(i + 1 + verticesCount);
                indices[indicesCount++] = (short)(i + 2 + verticesCount);
            }

            for (int i = 0; i < quadVertices.Length; i++)
            {
                vertices[verticesCount++] = new VertexPositionColor(new Vector3(quadVertices[i], 0), color);
            };
        }

        virtual public void DrawCircle(Circle circle, Color color, int resolution)
        {

            resolution = MathHelper.Clamp(resolution, 5, 256);

            CheckCapacity(resolution);

            int triangleCount = resolution - 2;

            for (int i = 0; i < triangleCount; i++)
            {
                indices[indicesCount++] = (short)(0 + verticesCount);
                indices[indicesCount++] = (short)(i + 1 + verticesCount);
                indices[indicesCount++] = (short)(i + 2 + verticesCount);
            }

            float drawingAngle = MathF.Tau / resolution;
            float radius = circle.Radius;

            for (int i = 0; i < resolution; i++)
            {
                vertices[verticesCount++] = new VertexPositionColor(new Vector3(radius * MathF.Cos(drawingAngle * i) + circle.Center.X,
                                                                                    radius * MathF.Sin(drawingAngle * i) + circle.Center.Y,
                                                                                    0),
                                                                                    color);
            }
        }

        virtual public void DrawPolygon(Polygon polygon, Color color)
        {
            CheckCapacity(polygon.Vertices.Length);

            int triangleCount = polygon.Vertices.Length - 2;

            for (int i = 0; i < triangleCount; i++)
            {
                indices[indicesCount++] = (short)(0 + verticesCount);
                indices[indicesCount++] = (short)(i + 1 + verticesCount);
                indices[indicesCount++] = (short)(i + 2 + verticesCount);
            }

            for (int i = 0; i < polygon.Vertices.Length; i++)
            {
                vertices[verticesCount++] = new VertexPositionColor(new Vector3(polygon.Vertices[i], 0), color);
            };
        }
    }
}
