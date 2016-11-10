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
        // Id of bakcground texture
        static int labID;
        // The main solution being tested
        static Solution mainSolution;
        static Solution modifiedSolution;
        // Present and past positions of mouse      
        static MouseDevice current, previous; // Maybe not need previous, we'll see
        // Main menu
        static MainMenu titleScreen;
        // Bools to control the flow of the game
        static bool inMain, inGame;
        static ReagentPanel panel;

        static Container c;
        static dropperButton drop;
        [STAThread]
        public static void Main()
        {
            using (var game = new GameWindow())
            {
                GL.Enable(EnableCap.Texture2D);

                game.Load += (sender, e) =>
                {
                    // Go fullscreen for now
                    game.WindowState = WindowState.Fullscreen;
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;

                    // Initialize several variables
                    labID = Textures.loadTexture("labTable.png");
                    titleScreen = new MainMenu();
                    inMain = true;
                    inGame = false;

                    panel = new ReagentPanel();
                    c = new Container();
                    drop = new dropperButton();
                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                // Currently handles passive mouse movement
                game.MouseMove += (sender, e) =>
                {
                    current = game.Mouse;
                    if (current[MouseButton.Left])
                    {
                        if (c.isInCoordinates(current.X, current.Y))
                        {
                            c.updateCoordinates(current.X, current.Y);
                        }
                        else if (drop.isInCoordinates(current.X, current.Y))
                        {
                            drop.updateCoordinates(current.X, current.Y);
                        }
                    }
                    previous = current;
                };


                game.KeyDown += (sender, e) =>
                {
                    // add game logic, input handling
                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }

                    if (game.Keyboard[Key.Down])
                    {
                        // Example of keyboardness
                    }
                };

                game.MouseDown += (sender, e) =>
                {
                    MouseDevice clicker = game.Mouse;

                    if (clicker[MouseButton.Left])
                    {
                        Console.WriteLine(clicker.X + ", " + clicker.Y);

                        if (inMain)
                        {
                            bool check = titleScreen.checkClick(clicker.X, clicker.Y);

                            if (!check)
                            {
                                inMain = false;
                                inGame = true;

                                mainSolution = titleScreen.genSolution();
                                mainSolution.printSolution();
                                modifiedSolution = mainSolution;

                            }
                        }

                        else
                        {
                            panel.getClick(clicker.X, clicker.Y);  
                        }
                    }


                    if (clicker[MouseButton.Right])
                    {
                        if (!inMain)
                        {
                            drop.deleteDropper();
                        }
                    }
                };

                game.RenderFrame += (sender, e) =>
                {
                    // Clear screen
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    GL.MatrixMode(MatrixMode.Projection);

                    // Setupt 2D drawing in a 1920 x 1080 monitor
                    GL.LoadIdentity();
                    GL.Ortho(0, 1920, 1080, 0, -1, 1);

                    if (inMain)
                    {
                        titleScreen.draw();
                    }

                    else
                    {
                        // Always make sure the reset color to white for unaltered images
                       // GL.Color3(Color.White);
                        GL.BindTexture(TextureTarget.Texture2D, labID);

                        // Start background
                        GL.Begin(BeginMode.Quads);

                        GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(0f, 0f);//Top Left Corner
                        GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(0f, 1080f);//Bottom Left Corner
                        GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1920f, 1080f);//Bottom Right Corner
                        GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1920f, 0f);//Top Right Corner

                        GL.End();

                        c.draw();
                        panel.draw();
                        drop.draw();


                        MouseDevice clicker = game.Mouse;

                            //currently bound to specific location for demo; update to match test tube bounds
                        
                        if (clicker.X > 980 && clicker.Y > 370) //add bounds for other sides
                            c.show = true;

                        if(c.show && game.Keyboard[Key.Down])
                           
                            {
                                
                                // drop.drop(clicker.X, clicker.Y);
                                if (modifiedSolution.react("hydrochloricAcid"))
                                    //want to use the modified solution to actually change things in the soln
                                {
                                    c.colorChange();
                                }
                            }   
                        

                    }

                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}
