﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

enum State { Solid, Liquid };

namespace QualScheme
{
    class Program
    {
        static int textID = 0;
        static MouseState current, previous;
        static float x = 0.0f, y = 0.0f;
        [STAThread]
        public static void Main()
        {
            using (var game = new GameWindow())
            {
                GL.Enable(EnableCap.Texture2D);

                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;

                    //game.KeyDown += new EventHandler<KeyboardKeyEventArgs>(Key.Down);
                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                game.UpdateFrame += (sender, e) =>
                {
                    // add game logic, input handling
                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }

                    if (game.Keyboard[Key.Down])
                    {
                        textID = Program.loadTexture();
                    }

                    current = Mouse.GetState();

                    if (current[MouseButton.Left])
                    {
                        x += ((current.X - previous.X) / 100.0f);
                        y -= ((current.Y - previous.Y) / 100.0f);
                    }
                    previous = current;
                };

                game.RenderFrame += (sender, e) =>
                {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    //GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

                    if (textID != 0)
                    {
                        GL.Begin(BeginMode.Quads);

                        

                        GL.BindTexture(TextureTarget.Texture2D, textID);

                        GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(0.0f + x, 0.0f + y);//Top Left Corner
                        GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(0.0f + x, 1.0f + y);//Bottom Left Corner
                        GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1.0f + x, 1.0f + y);//Bottom Right Corner
                        GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1.0f + x, 0.0f + y);//Top Right Corner

                       
                        GL.End();
                    }

                    else
                    {
                        GL.Begin(PrimitiveType.Triangles);

                        GL.Color3(Color.MidnightBlue);
                        GL.Vertex2(-1.0f, 1.0f);
                        GL.Color3(Color.SpringGreen);
                        GL.Vertex2(0.0f, -1.0f);
                        GL.Color3(Color.Ivory);
                        GL.Vertex2(1.0f, 1.0f);

                        GL.End();
                    }

                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }

        public static int loadTexture()
        {
            String filename = "..//..//test.png";
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            // We will not upload mipmaps, so disable mipmapping (otherwise the texture will not appear).
            // We can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
            // mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Bitmap bmp = new Bitmap(filename);
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            return id;
        }
    }
}