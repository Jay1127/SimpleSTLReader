using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SimpleSTLReader.Model;
using SimpleSTLReader.Reader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    class Viewport : GameWindow
    {
        private Vector3[] _vertices;
        private int _vertexAttribute;
        private int _positionBuffer;
        float _timeValue;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Title = "SimpleSTLReader";

            StlModel stlModel = new StlReader().Read($@"{Environment.CurrentDirectory}\Utah_teapot.stl");
            _vertices = stlModel.Vertices.Select(vertex => new Vector3((float)vertex.X, (float)vertex.Y, (float)vertex.Z))
                                         .ToArray();

            GL.GenVertexArrays(1, out _vertexAttribute);
            GL.BindVertexArray(_vertexAttribute);

            // position attribute
            GL.GenBuffers(1, out _positionBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _positionBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, Vector3.SizeInBytes * _vertices.Length, _vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // projection
            GL.MatrixMode(MatrixMode.Projection);
            GL.Ortho(-10, 10, -10, 10, -10, 10);

            GL.Viewport(0, 0, Width, Height);
        }
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.PolygonSmooth);
            GL.Enable(EnableCap.LineSmooth);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);            
           
            GL.MatrixMode(MatrixMode.Modelview);
            _timeValue += (float)e.Time;
            var model = Matrix4.CreateFromAxisAngle(new Vector3(0.5f, 0.5f, 0.5f), (float)_timeValue * (float)(50.0f * Math.PI / 180));
            GL.LoadMatrix(ref model);

            GL.EnableVertexAttribArray(_vertexAttribute);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length);
            GL.EnableVertexAttribArray(0);

            SwapBuffers();
        }
    }
}