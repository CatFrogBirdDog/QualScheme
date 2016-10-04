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
        float
            panelLeft, panelRight, panelTop, panelBot,
            // The bounds of the text inside of the panel
            topTextBound, botTextBound,
            // The bounds (top and bottom) of reagents in the first panel
            pg1ButtonBound1, pg1ButtonBound2, pg1ButtonBound3, pg1ButtonBound4, pg1ButtonBound5,
            pg1ButtonBound6, pg1ButtonBound7, pg1ButtonBound8, pg1ButtonBound9, pg1ButtonBound10,
            // The bound (top and bottom) of the reagents in the second panel
            pg2ButtonBound1, pg2ButtonBound2, pg2ButtonBound3, pg2ButtonBound4,
            pg2ButtonBound5, pg2ButtonBound6, pg2ButtonBound7, pg2ButtonBound8,
            // The bounds of the currently selected button
            buttonTop, buttonBot,
            // The button to hide the reaction table
            hideLeft, hideRight, hideTop, hidebot,
            // The button to show the reaction table
            showTop, showBot, showLeft, showRight,
            // Arrows
            arrowTop, arrowBot, pg1ArrowLeft, pg1ArrowRight, pg2ArrowLeft, pg2ArrowRight;
        

        int pg1ID, pg2ID, panel1ID, panel2ID;
        bool show, activePage, buttonPage; // ActivePAge && buttonPage = true for page 1, false for page 2
        string activeReagent;

        public ReagentPanel()
        {
            show = true;

            // Initialize a lot of stuff
            activePage = true;
            buttonPage = true;

            panelLeft = 0;
            panelRight = 340;
            panelTop = 0;
            panelBot = 1080;

            topTextBound = 276f;
            botTextBound = 992f;

            pg1ButtonBound1 = 300;
            pg1ButtonBound2 = 374;
            pg1ButtonBound3 = 452;
            pg1ButtonBound4 = 528;
            pg1ButtonBound5 = 605;
            pg1ButtonBound6 = 677;
            pg1ButtonBound7 = 765;
            pg1ButtonBound8 = 840;
            pg1ButtonBound9 = 916;
            pg1ButtonBound10 = 997;

            pg2ButtonBound1 = 293;
            pg2ButtonBound2 = 370;
            pg2ButtonBound3 = 454;
            pg2ButtonBound4 = 534;
            pg2ButtonBound5 = 612;
            pg2ButtonBound6 = 694;
            pg2ButtonBound7 = 779;
            pg2ButtonBound8 = 857;

            buttonTop = 0;
            buttonBot = 0;

            hideLeft = 140;
            hideRight = 200;
            hideTop = 1017;
            hidebot = 1070;

            arrowTop = 1010;
            arrowBot = 1064;
            pg1ArrowLeft = 250;
            pg1ArrowRight = 1064;
            pg2ArrowLeft = 20;
            pg2ArrowRight = 84;

            showTop = 990;
            showBot = 1070;
            showLeft = 20;
            showRight = 1070;

            activeReagent = "";

            pg1ID = Textures.loadTexture("reagentPage1transparent.png");
            pg2ID = Textures.loadTexture("reagentPage2transparent.png");
            panel1ID = Textures.loadTexture("reagentPanelArrowRight.png");
            panel2ID = Textures.loadTexture("reagentPanelArrowLeft.png");
        }

        public void draw()
        {
            if (show)
            {
                GL.Color3(Color.White);

                if (activePage)
                {
                    GL.BindTexture(TextureTarget.Texture2D, panel1ID);
                }

                else
                {
                    GL.BindTexture(TextureTarget.Texture2D, panel2ID);
                }

                GL.Begin(BeginMode.Quads);
                GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(panelLeft, panelTop);//Top Left Corner
                GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(panelLeft, panelBot);//Bottom Left Corner
                GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(panelRight, panelBot);//Bottom Right Corner
                GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(panelRight, panelTop);//Top Right Corner
                GL.End();

                if (activePage)
                {
                    GL.BindTexture(TextureTarget.Texture2D, pg1ID);
                }

                else
                {
                    GL.BindTexture(TextureTarget.Texture2D, pg2ID);
                }
                                          
                GL.Begin(BeginMode.Quads);
                GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(panelLeft, topTextBound);//Top Left Corner
                GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(panelLeft, botTextBound);//Bottom Left Corner
                GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(panelRight, botTextBound);//Bottom Right Corner
                GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(panelRight, topTextBound);//Top Right Corner
                GL.End();

                // Make sure button is actually selected and we are on the right page
                if (activePage == buttonPage && buttonTop != buttonBot)
                {
                    // Have to Unbind texture
                    GL.BindTexture(TextureTarget.Texture2D, 0);

                    GL.LineWidth(5);
                    GL.Begin(BeginMode.LineLoop);
                    {
                        GL.Vertex2(panelLeft, buttonTop);
                        GL.Vertex2(panelLeft, buttonBot);
                        GL.Vertex2(panelRight, buttonBot);
                        GL.Vertex2(panelRight, buttonTop);
                    }
                    GL.End();
                }
            }

            else
            {
                // Show stub?
            }
        }

        public void switchPages()
        {
            activePage = !activePage;
        }

        public void getClick(int x, int y)
        {
            if (panelLeft <= x && x <= panelRight)
            {
                if (show)
                { 
                    
                    if (topTextBound <= y && y <= botTextBound)
                    {
                        if (activePage) getPage1Click(x, y);
                        else getPage2Click(x, y);
                    }

                    else if (activePage && arrowTop <= y && y <= arrowBot)
                    {
                        if (pg1ArrowLeft <= x && x <= pg1ArrowRight)
                        {
                            switchPages();
                        }
                    }

                    else if (!activePage && arrowTop <= y && y <= arrowBot)
                    {
                        if (pg2ArrowLeft <= x && x <= pg2ArrowRight)
                        {
                            switchPages();
                        }
                    }

                    if (hideLeft <= x && x <= hideRight)
                    {                       
                        if (hideTop <= y && y <= hidebot)
                        {
                            show = !show;
                        }
                    }
                }

                else
                {
                    if (showLeft <= x && x <= showRight)
                    {
                        if (showTop <= y && y <= showBot)
                        {
                            show = !show;
                        }
                    }
                }              
            }
            
        }

        void getPage1Click(int x, int y)
        {
            if (pg1ButtonBound1 <= y && y <= pg1ButtonBound2)
            {
                buttonPage = true;
                buttonTop = pg1ButtonBound1;
                buttonBot = pg1ButtonBound2;
                activeReagent = "ammonia";
            }

            else if (pg1ButtonBound2 <= y && y <= pg1ButtonBound3)
            {
                buttonPage = true;
                buttonTop = pg1ButtonBound2;
                buttonBot = pg1ButtonBound3;
                activeReagent = "ammoniumMolybdate";
            }

            else if (pg1ButtonBound3 <= y && y <= pg1ButtonBound4)
            {
                buttonPage = true;
                buttonTop = pg1ButtonBound3;
                buttonBot = pg1ButtonBound4;
                activeReagent = "ammoniumOxalate";
            }

            else if (pg1ButtonBound4 <= y && y <= pg1ButtonBound5)
            {
                buttonPage = true;
                buttonTop = pg1ButtonBound4;
                buttonBot = pg1ButtonBound5;
                activeReagent = "ammoniumSulfate";
            }

            else if (pg1ButtonBound5 <= y && y <= pg1ButtonBound6)
            {
                buttonPage = true;
                buttonTop = pg1ButtonBound5;
                buttonBot = pg1ButtonBound6;
                activeReagent = "bariumChloride";
            }

            else if (pg1ButtonBound6 <= y && y <= pg1ButtonBound7)
            {
                buttonPage = true;
                buttonTop = pg1ButtonBound6;
                buttonBot = pg1ButtonBound7;
                activeReagent = "dimethylglyoxime";
            }

            else if (pg1ButtonBound7 <= y && y <= pg1ButtonBound8)
            {
                buttonPage = true;
                buttonTop = pg1ButtonBound7;
                buttonBot = pg1ButtonBound8;
                activeReagent = "hydrochloricAcid";
            }

            else if (pg1ButtonBound8 <= y && y <= pg1ButtonBound9)
            {
                buttonPage = true;
                buttonTop = pg1ButtonBound8;
                buttonBot = pg1ButtonBound9;
                activeReagent = "isoamylAlcohol";
            }

            else if (pg1ButtonBound9 <= y && y <= pg1ButtonBound10)
            {
                buttonPage = true;
                buttonTop = pg1ButtonBound9;
                buttonBot = pg1ButtonBound10;
                activeReagent = "nitricAcid";
            }            
        }

        void getPage2Click(int x, int y)
        {
            if (pg2ButtonBound1 <= y && y <= pg2ButtonBound2)
            {
                buttonPage = false;
                buttonTop = pg2ButtonBound1;
                buttonBot = pg2ButtonBound2;
                activeReagent = "potassiumChromate";
            }

            else if (pg2ButtonBound2 <= y && y <= pg2ButtonBound3)
            {
                buttonPage = false;
                buttonTop = pg2ButtonBound2;
                buttonBot = pg2ButtonBound3;
                activeReagent = "silverNitrate";
            }

            else if (pg2ButtonBound3 <= y && y <= pg2ButtonBound4)
            {
                buttonPage = false;
                buttonTop = pg2ButtonBound3;
                buttonBot = pg2ButtonBound4;
                activeReagent = "sodiumCoboltrinitrite";
            }

            else if (pg2ButtonBound4 <= y && y <= pg2ButtonBound5)
            {
                buttonPage = false;
                buttonTop = pg2ButtonBound4;
                buttonBot = pg2ButtonBound5;
                activeReagent = "sodiumHydroxide";
            }

            else if (pg2ButtonBound5 <= y && y <= pg2ButtonBound6)
            {
                buttonPage = false;
                buttonTop = pg2ButtonBound5;
                buttonBot = pg2ButtonBound6;
                activeReagent = "sodiumStannite";
            }

            else if (pg2ButtonBound6 <= y && y <= pg2ButtonBound7)
            {
                buttonPage = false;
                buttonTop = pg2ButtonBound6;
                buttonBot = pg2ButtonBound7;
                activeReagent = "sulfuricAcid";
            }

            else if (pg2ButtonBound7 <= y && y <= pg2ButtonBound8)
            {
                buttonPage = false;
                buttonTop = pg2ButtonBound7;
                buttonBot = pg2ButtonBound8;
                activeReagent = "thioacetamide";
            }            
        }
    }
}
