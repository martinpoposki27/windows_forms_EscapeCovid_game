using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscapeCovid
{
    public partial class EscapeCovid : Form
    {

        bool goLeft, goRight, goUp, goDown, gameOver;
        string facing = "up";
        int playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int virusSpeed = 3;
        int score;
        Random randNum = new Random();
        List<PictureBox> virusesList = new List<PictureBox>();

        public EscapeCovid()
        {
            InitializeComponent();
            RestartGame();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if(playerHealth > 1)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
                player.Image = Properties.Resources.dead;
                GameTimer.Stop();

            }


            txtAmmo.Text = "Вакцини: " + ammo;
            txtKills.Text = "Вакцинирани: " + score;

            if(goLeft == true && player.Left > 0)
            {
                player.Left -= speed;
            }
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += speed;
            }
            if (goUp == true && player.Top > 40)
            {
                player.Top -= speed;
            }
            if (goDown == true && player.Top + player.Height < this.ClientSize.Height)
            {
                player.Top += speed;
            }
            foreach(Control X in this.Controls)
            {
                if(X is PictureBox && (string)X.Tag == "ammo")
                {
                    if (player.Bounds.IntersectsWith(X.Bounds))
                    {
                        this.Controls.Remove(X);
                        ((PictureBox)X).Dispose();
                        ammo += 5;

                    }
                }

                if(X is PictureBox && (string)X.Tag == "virus")
                {

                    if (player.Bounds.IntersectsWith(X.Bounds))
                    {
                        playerHealth -= 1;
                    }

                    if(X.Left > player.Left)
                    {
                        X.Left -= virusSpeed;
                        ((PictureBox)X).Image = Properties.Resources.virus;
                    }
                    if (X.Left < player.Left)
                    {
                        X.Left += virusSpeed;
                        ((PictureBox)X).Image = Properties.Resources.virus;
                    }
                    if (X.Top > player.Top)
                    {
                        X.Top -= virusSpeed;
                        ((PictureBox)X).Image = Properties.Resources.virus;
                    }
                    if (X.Top < player.Top)
                    {
                        X.Top += virusSpeed;
                        ((PictureBox)X).Image = Properties.Resources.virus;
                    }
                }

                foreach(Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "bullet" && X is PictureBox && (string)X.Tag == "virus")
                    {
                        if (X.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;
                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(X);
                            ((PictureBox)X).Dispose();
                            virusesList.Remove(  ((PictureBox)X)  );
                            MakeVirus();

                        }

                    } 


                }

            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(gameOver == true)
            {
                return;
            }

            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.left1;

            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.right1;

            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.up1;

            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                player.Image = Properties.Resources.down1;

            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if(e.KeyCode == Keys.Space && ammo > 0 && gameOver ==false)
            {
                ammo--;
                ShootBullet(facing);

                if(ammo < 1)
                {
                    DropAmmo();
                }
            }
            if(e.KeyCode == Keys.Enter && gameOver == true)
            {
                RestartGame();

            }
        }

        private void ShootBullet(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletLeft = player.Left + (player.Width / 2) + 18;
            shootBullet.bulletTop = player.Top + (player.Height / 2) + 18;
            shootBullet.MakeBullet(this);

        }

        private void MakeVirus()
        {
            PictureBox virus = new PictureBox();
            virus.Tag = "virus";
            virus.Image = Properties.Resources.virus1;
            virus.Left = randNum.Next(0, 900);
            virus.Top = randNum.Next(0, 800);
            virus.SizeMode = PictureBoxSizeMode.AutoSize;
            virusesList.Add(virus);
            this.Controls.Add(virus);
            player.BringToFront();
        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = randNum.Next(10, this.ClientSize.Width - ammo.Width);
            ammo.Top = randNum.Next(50, this.ClientSize.Height - ammo.Height);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);
            ammo.BringToFront();
            player.BringToFront();

        }

        private void RestartGame()
        {
            player.Image = Properties.Resources.up1;
            foreach(PictureBox i in virusesList)
            {
                this.Controls.Remove(i);

            }
            virusesList.Clear();
            for(int i=0; i<3; i++)
            {
                MakeVirus();
            }
            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            gameOver = false;

            playerHealth = 100;
            score = 0;
            ammo = 10;

            GameTimer.Start();

        }
    }
}
