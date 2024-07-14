using EtherEngine.Core.Shapes;
using EtherUtils.Validate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EtherEngine.Core.DrawBatch
{
    public class ShapeBatch : IDrawBatch
    {
        protected VertexPositionColor[] _vertices;
        protected short[] _indices;

        protected int _verticesCount;
        protected int _indicesCount;
        protected int _maxVerticesCount;

        protected GraphicsDevice _device;
        protected BasicEffect _effect;

        protected FlagValidator _startedValidator;

        public float Alpha { get; set; }

        public ShapeBatch(GraphicsDevice graphicsDevice, int capacity = 1024, float alpha = 0.5f)
        {
            _device = graphicsDevice;
            _maxVerticesCount = capacity;
            Alpha = alpha;

            _vertices = new VertexPositionColor[_maxVerticesCount];
            _indices = new short[_maxVerticesCount * 3];

            _verticesCount = 0;
            _indicesCount = 0;
            _startedValidator = new FlagValidator(new Exception("This Shape factory has already started."));
        }

        public void Begin(RasterizerState rasterizerState = null,
                Effect effect = null,
                Matrix? transformMatrix = null)
        {

            _startedValidator.CheckOpposite();

            BasicEffect basicEffect = null;

            if (effect != null)
            {
                basicEffect = effect as BasicEffect;

                if (basicEffect != null) throw new NotSupportedException("only accepts basic effects for now.");
            }

            SetEffect(basicEffect);

            if (rasterizerState == null)
            {
                rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
            }

            _device.RasterizerState = rasterizerState;

            if (transformMatrix != null)
            {
                _effect.View = (Matrix)transformMatrix;
            }


            _startedValidator.Up();
        }

        public void End()
        {
            _startedValidator.Check();

            Flush();

            _startedValidator.Down();
        }

        public void Flush()
        {
            _device.BlendState = BlendState.AlphaBlend;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _device.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    _vertices,
                    0,
                    _verticesCount,
                    _indices,
                    0,
                    _indicesCount / 3);
            }

            _verticesCount = 0;
            _indicesCount = 0;
        }

        public void SetEffect(BasicEffect effect)
        {
            if (effect == null)
            {
                Viewport viewport = _device.Viewport;

                _effect = new BasicEffect(_device)
                {
                    VertexColorEnabled = true,
                    TextureEnabled = false,
                    FogEnabled = false,
                    LightingEnabled = false,
                    Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1),
                    Alpha = Alpha //change blend type instead.
                };
            }
            else
            {
                _effect = effect;
            }
        }

        public void ResetBuffers()
        {
            _verticesCount = 0;
            _indicesCount = 0;
        }

        private void CheckCapacity(int checkVertices)
        {
            if (checkVertices > _maxVerticesCount) throw new OverflowException("The number of vertices is higher than the maximum capacity of the vertex buffer.");
            if (checkVertices + _verticesCount > _maxVerticesCount) Flush();
        }

        public void DrawShape(in IShape shape, in Color color) //TODO: Optimize this
        {
            if (shape is Circle circle)
            {
                DrawCircle(circle, color, 30); // TODO:Fix the resolution;
            }
            else if (shape is RotatableQuad rotatableQuad)
            {
                DrawRectangle(rotatableQuad, color);
            }
            else if (shape is StaticQuad quad)
            {
                DrawRectangle(quad, color);
            }
            else if (shape is Polygon poly)
            {
                DrawPolygon(poly, color);
            }
            else throw new NotSupportedException();

        }

        protected void DrawRectangle(float x, float y, float width, float height, Color color)
        {
            CheckCapacity(4);

            var bottom_left = new VertexPositionColor(new Vector3(x, y, 0), color);
            var top_left = new VertexPositionColor(new Vector3(x, y + height, 0), color);
            var bottom_right = new VertexPositionColor(new Vector3(x + width, y, 0), color);
            var top_right = new VertexPositionColor(new Vector3(x + width, y + height, 0), color);

            _indices[_indicesCount++] = (short)(0 + _verticesCount);
            _indices[_indicesCount++] = (short)(1 + _verticesCount);
            _indices[_indicesCount++] = (short)(2 + _verticesCount);
            _indices[_indicesCount++] = (short)(0 + _verticesCount);
            _indices[_indicesCount++] = (short)(2 + _verticesCount);
            _indices[_indicesCount++] = (short)(3 + _verticesCount);

            _vertices[_verticesCount++] = top_left;
            _vertices[_verticesCount++] = bottom_left;
            _vertices[_verticesCount++] = bottom_right;
            _vertices[_verticesCount++] = top_right;
        }

        virtual public void DrawRectangle(in StaticQuad quad, in Color color)
        {
            DrawRectangle(quad.X - quad.Width / 2, quad.Y - quad.Height / 2, quad.Width, quad.Height, color);
        }

        virtual public void DrawRectangle(in RotatableQuad quad,in Color color)
        {
            Vector2[] quadVertices = quad.Vertices;
            CheckCapacity(quadVertices.Length);

            int triangleCount = quadVertices.Length - 2;

            for (int i = 0; i < triangleCount; i++)
            {
                _indices[_indicesCount++] = (short)(0 + _verticesCount);
                _indices[_indicesCount++] = (short)(i + 1 + _verticesCount);
                _indices[_indicesCount++] = (short)(i + 2 + _verticesCount);
            }

            for (int i = 0; i < quadVertices.Length; i++)
            {
                _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(quadVertices[i], 0), color);
            };
        }

        virtual public void DrawCircle(in Circle circle, in Color color, int resolution)
        {

            resolution = MathHelper.Clamp(resolution, 5, 256);

            CheckCapacity(resolution);

            int triangleCount = resolution - 2;

            for (int i = 0; i < triangleCount; i++)
            {
                _indices[_indicesCount++] = (short)(0 + _verticesCount);
                _indices[_indicesCount++] = (short)(i + 1 + _verticesCount);
                _indices[_indicesCount++] = (short)(i + 2 + _verticesCount);
            }

            float drawingAngle = MathF.Tau / resolution;
            float radius = circle.Radius;

            for (int i = 0; i < resolution; i++)
            {
                _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(radius * MathF.Cos(drawingAngle * i) + circle.Center.X,
                                                                                    radius * MathF.Sin(drawingAngle * i) + circle.Center.Y,
                                                                                    0),
                                                                                    color);
            }
        }

        virtual public void DrawPolygon(in Polygon polygon, in Color color)
        {
            CheckCapacity(polygon.Vertices.Length);

            int triangleCount = polygon.Vertices.Length - 2;

            for (int i = 0; i < triangleCount; i++)
            {
                _indices[_indicesCount++] = (short)(0 + _verticesCount);
                _indices[_indicesCount++] = (short)(i + 1 + _verticesCount);
                _indices[_indicesCount++] = (short)(i + 2 + _verticesCount);
            }

            for (int i = 0; i < polygon.Vertices.Length; i++)
            {
                _vertices[_verticesCount++] = new VertexPositionColor(new Vector3(polygon.Vertices[i], 0), color);
            };
        }
    }
}
