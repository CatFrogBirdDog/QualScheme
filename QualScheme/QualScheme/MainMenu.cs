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
    class MainMenu
    {
        int screenID, checkID;
        bool group1, group2, group3, anions;
        static float btnLeft, btnRight, g1Top, g1Bot,
            g2Top, g2Bot, g3Top, g3Bot, aTop, aBot,
            confLeft, confRight, confTop, confBot;

        public MainMenu()
        {
            group1 = false;
            group2 = false;
            group3 = false;
            anions = false;
            screenID = Textures.loadTexture("titleScreen.png");
            checkID = Textures.loadTexture("checkmark.png");

            btnLeft = 1099f;
            btnRight = 1140f;
            g1Top = 375f;
            g1Bot = 408f;
            g2Top = 468f;
            g2Bot = 500f;
            g3Top = 565f;
            g3Bot = 598f;
            aTop = 662f;
            aBot = 696f;
            confTop = 811;
            confBot = 953;
            confLeft = 657;
            confRight = 1203;
        }

        public void draw()
        {
            GL.BindTexture(TextureTarget.Texture2D, screenID);

            // Start background
            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(0f, 0f);//Top Left Corner
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(0f, 1080f);//Bottom Left Corner
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1920f, 1080f);//Bottom Right Corner
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1920f, 0f);//Top Right Corner

            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, checkID);

            GL.Begin(BeginMode.Quads);
                                              
            // Draw Checkmarks
            if (group1)
            {
                GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(btnLeft, g1Top);//Top Left Corner
                GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(btnLeft, g1Bot);//Bottom Left Corner
                GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(btnRight, g1Bot);//Bottom Right Corner
                GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(btnRight, g1Top);//Top Right Corner
            }

            if (group2)
            {
                GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(btnLeft, g2Top);//Top Left Corner
                GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(btnLeft, g2Bot);//Bottom Left Corner
                GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(btnRight, g2Bot);//Bottom Right Corner
                GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(btnRight, g2Top);//Top Right Corner
            }

            if (group3)
            {
                GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(btnLeft, g3Top);//Top Left Corner
                GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(btnLeft, g3Bot);//Bottom Left Corner
                GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(btnRight, g3Bot);//Bottom Right Corner
                GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(btnRight, g3Top);//Top Right Corner
            }

            if (anions)
            {
                GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(btnLeft, aTop);//Top Left Corner
                GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(btnLeft, aBot);//Bottom Left Corner
                GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(btnRight, aBot);//Bottom Right Corner
                GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(btnRight, aTop);//Top Right Corner
            }

            GL.End();
        }

        public Solution genSolution()
        {
            Solution returnVal = new Solution();

            returnVal.generateSolution(group1, group2, group3, anions);

            return returnVal;
        }

        public bool checkClick(int x, int y)
        {            
            // if clicking generate solution, return false (go into game loop)
            if (confLeft < x && x < confRight)
            {
                if (confTop < y && y < confBot)
                {
                    return false;
                }
            }
            
            // If clicking just a check mark or a pointless click, return true (keep going)

            // If we are in the right column for a group button
            if (btnLeft < x && x < btnRight)
            {
                if (g1Top < y && y < g1Bot)
                {
                    group1 = !group1;
                }

                else if (g2Top < y && y < g2Bot)
                {
                    group2 = !group2;
                }

                else if (g3Top < y && y < g3Bot)
                {
                    group3 = !group3;
                }

                else if (aTop < y && y < aBot)
                {
                    anions = !anions;
                }
            }
            return true;
        }
    }
}
