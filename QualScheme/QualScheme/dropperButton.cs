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
    class dropperButton
    {

        private static Solution thisSolution; // TODO: Come up with better name 
        static int textID, blank;
       
        static float x, y, length, height;
        bool show;
        public dropperButton()
        {
            show = true;
            x = 230; y = 175;
            length = 55f;
            height = 55f;
            textID = Textures.loadTexture("dropper.png");
            blank = Textures.loadTexture("blank.png");
        }

        public void draw()
        {
            if (show)
            {
                GL.BindTexture(TextureTarget.Texture2D, textID);


                GL.Color3(Color.White);

                GL.Begin(BeginMode.Quads);

                GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(x - length, y - height);//Top Left Corner
                GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(x - length, y + height);//Bottom Left Corner
                GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(x + length, y + height);//Bottom Right Corner
                GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(x + length, y - height);//Top Right Corner

                GL.End();
            }

        }
        
        public void deleteDropper()
        {
            show = false;
  
        }

        public bool isInCoordinates(float inx, float iny)
        {
            if ((x - length) < inx && inx < (x + length))
            {
                if ((y - height) < iny && iny < (y + height))
                {
                    return true;
                }
            }
            return false;
        }

        public void updateCoordinates(float inx, float iny)
        {
            x = inx;
            y = iny;
        }        


    }
}
