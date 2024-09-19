using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
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

        int nRays = 200;
        float FoV = 65;

        bool playerForward = false;
        bool playerBack = false;
        bool playerLeft = false;
        bool playerRight = false;
        float playerRotationG = -90;
        float playerRotation = 0;
        float playerRotationSpeed = 2.0f;

        float c1x;
        float c1y;
        float mapx = 14;
        float mapy = 18;
        float cellsize = 37.5f;

        List<float[]> mapfp = new List<float[]>();


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
            playerRotation = playerRotationG * MathF.PI / 180;

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

        private void OnUpdate(object sender, EventArgs e)
        {
            playerRotation = playerRotationG * MathF.PI / 180;
            if (playerForward) MoveInDirection(1);
            if (playerBack) MoveInDirection(-1);
            if (playerLeft) playerRotationG -= playerRotationSpeed;
            if (playerRight) playerRotationG += playerRotationSpeed;

            if (playerRotationG < -180) playerRotationG = 180;
            if (playerRotationG > 180) playerRotationG = -180;
            cnv_TopDown.Invalidate();

            lbl_grad.Text = $"G: {playerRotationG}";
            lbl_rad.Text = $"R: {playerRotation}";
        }

        private bool RayCheckWall(float rx, float ry)
        {
            int mx = (int)((rx) / cellsize);
            int my = (int)((ry) / cellsize);

            Debug.WriteLine($"{mx} {my}");

            if (my >= 0 && my < map.GetLength(0) && mx >= 0 && mx < map.GetLength(1))
            {
                if (map[my, mx] == 1) return true;
            }

            return false;
        }

        public (float x, float y) CastRay(float castStartX, float castStartY, float angle)
        {

            if (Math.Abs(playerX - castStartX) > 200 || Math.Abs(playerY - castStartY) > 200)
            {

            }

            float rayDirectionX;
            float rayDirectionY;

            if (playerRotationG < -90.0f)
            {
                rayDirectionX = MathF.Cos(angle);
                rayDirectionY = MathF.Sin(angle);
            }
            else
            {
                rayDirectionX = MathF.Cos(angle);
                rayDirectionY = MathF.Sin(angle);
            }

            if (Math.Abs(rayDirectionY) == 1) rayDirectionX = 0;

            if (Math.Abs(rayDirectionX) == 1) rayDirectionY = 0;

            float pnX = rayDirectionX > 0.0f ? 1.0f : -1.0f;
            float pnY = rayDirectionY > 0.0f ? 1.0f : -1.0f;

            float distanceCellWallX = pnX > 0 ? cellsize - (castStartX % cellsize) : -(castStartX % cellsize);
            float distanceCellWallY = pnY > 0 ? cellsize - (castStartY % cellsize) : -(castStartY % cellsize);

            float castHitYShift = rayDirectionY == 0.0000f ? 0.0f : MathF.Abs(rayDirectionY) * MathF.Abs(distanceCellWallX) * pnY;
            float castHitXShift = rayDirectionX == 0.0000f ? 0.0f : MathF.Abs(rayDirectionX) * MathF.Abs(distanceCellWallY) * pnX;

            float castHitY = castStartY + distanceCellWallY;
            float castHitX = castStartX + distanceCellWallX;

            bool checkX = (MathF.Abs(castHitXShift) > MathF.Abs(castHitYShift));

            if (checkX)
            {
                if (Math.Abs(playerX - castStartX) > 1000 || Math.Abs(playerY - castStartY) > 1000 || castStartX < 0)
                {
                    return (0, 0);
                }
                else if (RayCheckWall(castHitX + pnX, castStartY))
                {
                    return (castHitX, castStartY + castHitYShift);
                }
                else
                {
                    return CastRay(castHitX + pnX, castStartY + castHitYShift, angle);
                }

            }
            else if (!checkX)
            {
                if (Math.Abs(playerX - castStartX) > 1000 || Math.Abs(playerY - castStartY) > 1000 || castStartY < 0)
                {
                    return (0, 0);
                }
                else if (RayCheckWall(castStartX, castHitY + pnY))
                {
                    return (castStartX + castHitXShift, castHitY);
                }
                else
                {
                    return CastRay(castStartX + castHitXShift, castHitY + pnY, angle);
                }


            }
            return (Round(MathF.Abs(distanceCellWallX) * rayDirectionX + castStartX), Round(MathF.Abs(distanceCellWallY) * rayDirectionY + castStartY));
        }






        public void MoveInDirection(int pn)
        {
            float x = 1 * MathF.Cos(playerRotation) * pn;
            float y = 1 * MathF.Sin(playerRotation) * pn;
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

        public float Round(float x)
        {
            return MathF.Round(x * 2) / 2;
        }


        public bool IsColiding(float xp, float yp)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == 1)
                    {
                        RectangleF testMe = new RectangleF(x * cellsize, y * cellsize, cellsize, cellsize);
                        if (testMe.Contains(xp, yp) || testMe.Contains(xp + playerSize, yp) || testMe.Contains(xp + playerSize, yp + playerSize) || testMe.Contains(xp, yp + playerSize))
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
            g.DrawLine(pen, playerX + 10, playerY + 10, 10 * MathF.Cos(playerRotation) + playerX + 10, 10 * MathF.Sin(playerRotation) + playerY + 10);

            //float rayX;
            //float rayY;
            //(rayX , rayY) = CastRay(playerX + 10, playerY + 10);

            pen = new Pen(Color.Red, 1);
            //if (rayX > 0.5f)
            //{
            //    g.DrawLine(pen, playerX + 10, playerY + 10, rayX, rayY);

            //}

            //Split Fov
            float split = FoV / nRays;
            float startpoint = playerRotationG - FoV / 2;
            mapfp.Clear();
            for (float i = startpoint; i < startpoint + FoV; i += 0.5f)
            {

                float rayX;
                float rayY;
                float rad = i * MathF.PI / 180;
                (rayX, rayY) = CastRay(playerX + 10, playerY + 10, rad);
                //g.DrawLine(pen, playerX + 10, playerY + 10, 100 * MathF.Cos(rad) + playerX + 10, 100 * MathF.Sin(rad) + playerY + 10);
                g.DrawLine(pen, playerX + 10, playerY + 10, rayX, rayY);
                //Debug.Write($"{startpoint + split * i} ");

                mapfp.Add(new float[]{rayX, rayY});
            }
            cnv_fp.Invalidate();




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

        private void cnv_fp_Click(object sender, EventArgs e)
        {

        }

        private void cnv_fp_Paint(object sender, PaintEventArgs e)
        {
            if (mapfp.Count > 0)
            {
                float oy = cnv_fp.Height / 2;
                Graphics g = e.Graphics;
                Pen pen = new Pen(Color.DarkSlateGray, 2);

                float lines = cnv_fp.Width / mapfp.Count;
                for (int i = 0; i < mapfp.Count; i++)
                {
                    float scale = (playerX * mapfp[i][0] + playerY * mapfp[i][1])/1000.0f;
                    g.DrawLine(pen, i * lines, oy - scale / 2, i * lines, oy + scale / 2);
                }
            }
        }
    }
}
