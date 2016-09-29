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
    class ReagentPanel
    {
        float leftBound, rightBound, topBound, botBound;
        int pg1ID, pg2ID;
        bool show, activePage; // ActivePAge = true for page 1, false for page 2

        public ReagentPanel()
        {
            show = true;

            activePage = true;
            leftBound = 0f;
            rightBound = 340f;
            topBound = 276f;
            botBound = 992f;

            pg1ID = Textures.loadTexture("reagentPage1transparent.png");
            pg2ID = Textures.loadTexture("reagentPage2transparent.png");
        }

        public void draw()
        {
            if (show)
            {
                if (activePage)
                {
                    GL.BindTexture(TextureTarget.Texture2D, pg1ID);
                }

                else
                {
                    GL.BindTexture(TextureTarget.Texture2D, pg2ID);
                }

                GL.Color3(Color.White);
                GL.Begin(BeginMode.Quads);
                GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(leftBound, topBound);//Top Left Corner
                GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(leftBound, botBound);//Bottom Left Corner
                GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rightBound, botBound);//Bottom Right Corner
                GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rightBound, topBound);//Top Right Corner
                GL.End();
            }
        }

        public void switchPages()
        {
            activePage = !activePage;
        }
    }
}
