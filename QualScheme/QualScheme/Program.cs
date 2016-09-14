using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

enum State { Solid, Liquid };

namespace QualScheme
{
    class Program
    {
        static int textID = 0, backID;
        static MouseState current, previous;
        static float x = 0.0f, y = 0.0f;
        [STAThread]
        public static void Main()
        {
            bool g1 = false, g2 = false, g3 = false, g4 = false;
            string input;

            Console.WriteLine("Use group 1? Y/N");
            input = Console.ReadLine();
            g1 = (input == "Y");
            Console.WriteLine("Use group 2 Y/N?");
            input = Console.ReadLine();
            g2 = (input == "Y");
            Console.WriteLine("Use group 3 Y/N?");
            input = Console.ReadLine();
            g3 = (input == "Y");
            Console.WriteLine("Use group 4 Y/N?");
            input = Console.ReadLine();
            g4 = (input == "Y");

            Solution s = new Solution();
            s.generateSolution(g1, g2, g3, g4);

            input = Console.ReadLine();

            while (input != "END")
            {
                if (input == "PRINT")
                    s.printSolution();

                else
                    s.react(input);

                input = Console.ReadLine();
            }
        } // Delete this when readding graphics





            // Ignoring graphical stuff for now
            /*
            using (var game = new GameWindow())
            {
                GL.Enable(EnableCap.Texture2D);

                game.Load += (sender, e) =>
                {
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;

                    //game.KeyDown += new EventHandler<KeyboardKeyEventArgs>(Key.Down);
                    backID = Program.loadTexture("labTable.png");
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
                        textID = Program.loadTexture("clearLiquidTestTube.png");
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

                    GL.BindTexture(TextureTarget.Texture2D, backID);

                    // Start background
                    GL.Begin(BeginMode.Quads);

                    GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-1.0f, -1.0f);//Top Left Corner
                    GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1.0f, 1.0f);//Bottom Left Corner
                    GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1.0f, 1.0f);//Bottom Right Corner
                    GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1.0f, -1.0f);//Top Right Corner

                    GL.End();

                    // If there's an item, show the item
                    if (textID != 0)
                    {
                        GL.BindTexture(TextureTarget.Texture2D, textID);

                        GL.Begin(BeginMode.Quads);

                        GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(0.0f + x, 2.0f + y);//Top Left Corner
                        GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(0.0f + x, 0.0f + y);//Bottom Left Corner
                        GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(0.5f + x, 0.0f + y);//Bottom Right Corner
                        GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(0.5f + x, 2.0f + y);//Top Right Corner


                        GL.End();
                    }

                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }

        public static int loadTexture(string textName)
        {
            Constants c = new Constants();
            String filename = c.getImgPath() + textName;
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            int id = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Bitmap bmp = new Bitmap(filename);
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            return id;
        }*/
    }
}
