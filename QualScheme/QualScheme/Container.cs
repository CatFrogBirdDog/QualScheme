using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace QualScheme
{
    class Container
    {
        private static Solution thisSolution; // TODO: Come up with better name 

        public void draw(float x, float y, int textID)
        {
            GL.BindTexture(TextureTarget.Texture2D, textID);

            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(100f, 100f);//Top Left Corner
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(100f, 200f);//Bottom Left Corner
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(200f, 200f);//Bottom Right Corner
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(200f, 100f);//Top Right Corner

            GL.End();
        }
    }
}
