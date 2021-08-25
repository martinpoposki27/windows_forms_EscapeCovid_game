# proektVP_193004_193026

Линк до repo на GitHub: https://github.com/martinpoposki27/proektVP_193004_193026.git

EscapeCovid

Windows Forms Project by Sara Stalevska and Martin Poposki

Опис на апликацијата:

Апликацијата која ја развивме е класична 2Д репрезентација со едноставни функционалности и ограниченин контроли. 
Целта на играчот е да ги избегне вирусите кои ја намалуваат неговиот имунитет, притоа уништувајќи ги со вакцините како муниција.

Упатство и опции:

Играчот се движи низ екранот со помош на стрелките за навигација, а муницијата ја испушта со притискање на Space барот.
На почеток се дадени одреден број вакцини кои му служат на играчот како муниција против ковид вирусите кои се генерираат на екранот на произволни позиции.
Во моментот кога ќе се потрошат доделените вакцини, чија што информација на достапност стои на екранот, во играта се појавува нова залиха која играчот може да ја подигне од таа позиција за да продолжи да ги напаѓа вирусите. 
Доколку играчот ќе биде судрен со одреден број на вируси, неговиот имунитет се потрошил, процес кој се прикажува на екранот преку ProgressBar.
Дополнително, на екранот се бројат вакцинирани личности, односно бројот на погодени вируси, кои играат улога на резултат во играта.

MenuStrip нуди опција за почеток на нова игра (New), опција да се паузира тековната (Pause), како и копче Help кое проследува во нов прозорец со инструкции и краток опис на играта.
Тајмерот на играта се паузира при секојм клик на на било која од опциите на MenuStrip, притоа играта се продолжува преку копчето Pause кое се трансформира во Start.

Податочни структури:

Бидејќи вакцините односно муницијата која се користи е посебен објект, искористивме посебна класа Bullet. Целта ни беше да имаме листа од објекти вирус кои полесно би ги отстранувале од екранот кога тие ќе бидат победени или допрени со играчот.


    public class Bullet
    {
        public string direction { get; set; }
        public int bulletLeft { get; set; }
        public int bulletTop { get; set; }
        private int speed = 20;
        private PictureBox bullet = new PictureBox();
        private Timer bulletTimer = new Timer();
        
        public void MakeBullet(Form form)
        {
            bullet.BackColor = Color.DarkGreen;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;
            bullet.BringToFront();

            form.Controls.Add(bullet);

            bulletTimer.Interval = speed;
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();

        }

        private void BulletTimerEvent(object sender, EventArgs e)
        {
            if(direction == "left")
            {
                bullet.Left -= speed;
            }
            if(direction == "right")
            {
                bullet.Left += speed;
            }
            if(direction == "up")
            {
                bullet.Top -= speed;
            }
            if(direction == "down")
            {
                bullet.Top += speed;
            }

            if(bullet.Left < 10 || bullet.Left > 860 || bullet.Top < 10 || bullet.Top > 600)
            {
                bulletTimer.Stop();
                bulletTimer.Dispose();
                bullet.Dispose();
                bulletTimer = null;
                bullet = null;
            }
        }
    }
    
Изгледот на апликацијата е приложен на следните слики:
       ![2021-08-25 (3)](https://user-images.githubusercontent.com/65679767/130709774-116e58eb-632e-4b3c-bf71-0bb0ecd17c56.png)
       ![2021-08-25 (2)](https://user-images.githubusercontent.com/65679767/130709790-6fb28089-ba00-481e-82c1-2a29e032f190.png)



 
