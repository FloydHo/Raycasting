using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using Timer = System.Windows.Forms.Timer;

namespace Raycasting
{
    public partial class form_Raycasting : Form
    {
        int[,] map = {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,1,1,1,0,1,0,0,0,0,0,1},
            {1,0,0,1,0,0,0,1,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,1,0,0,0,0,0,1},
            {1,0,0,0,0,0,1,1,0,0,0,0,0,1},
            {1,0,0,1,1,0,0,0,0,0,0,0,0,1},
            {1,0,0,1,1,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,1,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,1,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        RectangleF[] walls = new RectangleF[200];

        float rotationSpeed = 0f;
        float playerX = 420;
        float playerY = 75;
        float playerSpeedStandart = 3.0f;
        float playerSpeed = 2.0f;
        float playerSize = 20;



        bool playerForward = false;
        bool playerBack = false;
        bool playerLeft = false;
        bool playerRight = false;
        float playerRotation = 5.5f;
        float playerRotationSpeed = 0.08f;

        float c1x;
        float c1y;
        float mapx = 14;
        float mapy = 18;
        float cellsize = 37.5f;


        public form_Raycasting()
        {
            InitializeComponent();
            Init();
            c1x = cnv_TopDown.Width;
            c1y = cnv_TopDown.Height;

            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += OnUpdate;
            timer.Start();
        }

        private void Init()
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == 1)
                    {
                        walls.Append(new RectangleF(x * cellsize, y * cellsize, cellsize, cellsize));
                    }
                }
            }

        }

        private bool RayCheckWall(float rx, float ry)
        {
            int mx = (int)((rx)/ cellsize);
            int my = (int)((ry) / cellsize);

            Debug.WriteLine($"{mx} {my}");

            if (my >= 0 && my < map.GetLength(0) && mx >= 0 && mx < map.GetLength(1))
            {
                if (map[my, mx] == 1) return true;
            }

            return false;
        }






        public (float x, float y) CastRay(float castStartX, float castStartY)
        {
            if (castStartX - playerX > 200 || castStartY - playerY > 200) return (0, 0);

            float rayDirectionX = MathF.Cos(playerRotation);
            float rayDirectionY = MathF.Sin(playerRotation);


            if (rayDirectionX < 0.0001f && rayDirectionX > 0)
            {
                rayDirectionX = 0f;

            }

            if (rayDirectionY < 0.0001f && rayDirectionY > 0)
            {
                rayDirectionY = 0f;
            }

            //Ob Positiv oder Negativ
            float rayX = rayDirectionX > 0 ? 1 : -1;
            float rayY = rayDirectionY > 0 ? 1 : -1;

            float distanceCellWallX;
            float distanceCellWallY;

            distanceCellWallX = rayX > 0 ? cellsize - (castStartX % cellsize) : (castStartX % cellsize);
            distanceCellWallY = rayY > 0 ? cellsize - (castStartY % cellsize) : (castStartY % cellsize);

            float castHitX = castStartX + distanceCellWallX * rayDirectionX;
            float castHitY = castStartY + distanceCellWallY * rayDirectionY;



            if (Math.Round(castHitX,1) % cellsize == 0)
            {
                if (RayCheckWall(castHitX + 1*rayX, castHitY))
                {
                    return (castHitX, castHitY);
                }
            }
            else if (Math.Round(castHitY,1) % cellsize == 0) 
            {
                if (RayCheckWall(castHitX, castHitY + 1 * rayY))
                {
                    return (castHitX, castHitY);
                }
            }

            return CastRay(castHitX, castHitY);
        }







        private void OnUpdate(object sender, EventArgs e) 
        {
            if (playerForward) MoveInDirection();
            if (playerBack) playerY += playerSpeed;
            if (playerLeft) playerRotation -= playerRotationSpeed;
            if (playerRight) playerRotation += playerRotationSpeed;

            if (playerRotation <= 0) playerRotation = 2*MathF.PI;
            if (playerRotation > 2 * MathF.PI) playerRotation = 0;
            cnv_TopDown.Invalidate();
        }

        public void MoveInDirection()
        {
            float x = 1 * MathF.Cos(playerRotation);
            float y = 1 * MathF.Sin(playerRotation);
            if (!IsColiding(playerX + x * playerSpeed, playerY + y * playerSpeed))
            {
                playerX += x * playerSpeed;
                playerY += y * playerSpeed;
            }
            else 
            {
                playerX -= x * playerSpeed * 1.5f;
                playerY -= y * playerSpeed * 1.5f;
            }


        }

        public bool IsColiding(float xp, float yp)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == 1)
                    {
                        RectangleF testMe = new RectangleF(x*cellsize,y*cellsize ,cellsize,cellsize);
                        if (testMe.Contains(xp, yp) || testMe.Contains(xp+playerSize, yp) || testMe.Contains(xp + playerSize, yp + playerSize) || testMe.Contains(xp, yp + playerSize))
                        {
                            return true;
                        }

                    }
                }
            }
            return false;
        }


        private void cnv_TopDown_Click(object sender, EventArgs e)
        {

        }

        private void cnv_TopDown_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.DarkSlateGray, 1);
            //DrawGutter
            for (int x = 0; x < map.GetLength(1); x++)
            {
                g.DrawLine(pen, x * cellsize, 0, x * cellsize, c1y);
            }
            for (int y = 0; y < map.GetLength(0); y++)
            {
                g.DrawLine(pen, 0, y * cellsize, c1x, y * cellsize);
            }
            Brush brush = new SolidBrush(Color.DarkGray);
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == 1)
                    {
                        g.FillRectangle(brush, x * cellsize, y * cellsize, cellsize, cellsize);
                    }
                }
            }
            brush = new SolidBrush(Color.Blue);
            g.FillEllipse(brush, playerX, playerY, 20, 20);




            pen = new Pen(Color.Yellow, 3);
            g.DrawLine(pen, playerX+10, playerY+10, 20 * MathF.Cos(playerRotation) + playerX + 10, 20 * MathF.Sin(playerRotation)+playerY + 10);

            //if (RayCheckWall(100 * MathF.Cos(playerRotation) + playerX + 10, 100 * MathF.Sin(playerRotation) + playerY + 10))
            //{
            //    brush = new SolidBrush(Color.Yellow);
            //    g.FillEllipse(brush, 100 * MathF.Cos(playerRotation) + playerX + 10, 100 * MathF.Sin(playerRotation) + playerY + 10, 10, 10);

            //}
            float rayX;
            float rayY;
            (rayX , rayY) = CastRay(playerX + 10, playerY + 10);

            if (rayX > 1)
            {
                g.DrawLine(pen, playerX + 10, playerY + 10, rayX, rayY);
                g.FillEllipse(brush, rayX, rayY, 10,10);
            }
        }

        private void form_Raycasting_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    playerForward = true;             
                    break;
                case Keys.A:
                    playerLeft = true;
                    break;
                case Keys.S:
                    playerBack = true;
                    break;
                case Keys.D:
                    playerRight = true;
                    break;
                default:
                    break;
            }    
        }

        private void form_Raycasting_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    playerForward = false;
                    break;
                case Keys.A:
                    playerLeft = false;
                    break;
                case Keys.S:
                    playerBack = false;
                    break;
                case Keys.D:
                    playerRight = false;
                    break;
                default:
                    break;
            }
        }

    }
}
