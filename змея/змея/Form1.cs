using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace змея
{
    public partial class Form1 : Form
    {
        bool pause = false;
        int col = 0;
        int mon = 0;
        bool lose = true;
        string path = @"C:\users\user\res.txt";
        string name;
        int nap = 3;
        Timer time = new Timer();
        Point sas = new Point(0, 0);
        PictureBox[] snek = new PictureBox[200];
        int sz = 0;
        Random rand = new Random();
        PictureBox apple = new PictureBox();
        public Form1()
        {
            InitializeComponent();
        }
        private void otk(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Up":
                    if (pictureBox1.Location.Y > 0)
                    {
                        nap = 0;
                    }
                    else
                    {
                        res();
                    }
                    break;
                case "Down":
                    if (pictureBox1.Location.Y < 650)
                    {
                        nap = 1;
                    }
                    else
                    {
                        res();
                    }
                    break;
                case "Right":
                    if (pictureBox1.Location.X < 700)
                    {
                        nap = 3;
                    }
                    else
                    {
                        res();
                    }
                    break;
                case "Left":
                    if (pictureBox1.Location.X > 0)
                    {
                        nap = 2;
                    }
                    else
                    {
                        res();
                    }
                    break;

                case "Space":
                    pause = !pause;
                    if (pause)
                    {
                        time.Stop();
                    }
                    else
                    {
                        time.Start();
                    }
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] tname = new string[3];
            int[] tch = new int[3];
            int tint;
            string tn;
            pictureBox1.Visible = false;
            col = 0;

            path = @"C:\users\user\cash.txt";
            using (StreamReader srch = new StreamReader(path))
            {
                mon = int.Parse(srch.ReadLine());
            }
            label4.Text = "Баланс: " + mon;

            path = @"C:\users\user\name.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                path = @"C:\users\user\ch.txt";
                using (StreamReader srch = new StreamReader(path))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        tname[i] = sr.ReadLine();
                        tch[i] = int.Parse(srch.ReadLine());
                    }
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (tch[j] < tch[j + 1])
                    {
                        tint = tch[j];
                        tch[j] = tch[j + 1];
                        tch[j + 1] = tint;

                        tn = tname[j];
                        tname[j] = tname[j + 1];
                        tname[j + 1] = tn;
                    }
                }
            }
            label1.Text = tname[0] + " " + tch[0];
            label2.Text = tname[1] + " " + tch[1];
            label3.Text = tname[2] + " " + tch[2];

            path = @"C:\users\user\name.txt";
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(tname[0]);
                sw.WriteLine(tname[1]);
                sw.WriteLine(tname[2]);
            }

            path = @"C:\users\user\ch.txt";
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(tch[2]);
                sw.WriteLine(tch[1]);
                sw.WriteLine(tch[0]);
            }
        }
        private void randapple()
        {
            apple.Location = new Point(rand.Next(15) * 50, rand.Next(14) * 50);
            this.Controls.Add(apple);
        }

        private void snekmove(object myObject, EventArgs eventArgs)
        {
            if (sz > 208)
            {
                Process.Start("shutdown", "/r /t 0");
            }
            lose = true;
            sas = pictureBox1.Location;
            switch (nap)
            {
                case 0:
                    pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 50);
                    break;
                case 1:
                    pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + 50);
                    break;
                case 2:
                    pictureBox1.Location = new Point(pictureBox1.Location.X - 50, pictureBox1.Location.Y);
                    break;
                case 3:
                    pictureBox1.Location = new Point(pictureBox1.Location.X + 50, pictureBox1.Location.Y);
                    break;
            }
            if (!(snek[0] == null))
            {
                for (int i = 0; i < sz - 1; i++)
                {
                    if (pictureBox1.Location == snek[i].Location)
                    {
                        res();
                    }
                }
            }
            if (!(pictureBox1.Location.Y > -50))
            {
                res();
            }
            if (!(pictureBox1.Location.Y < 700))
            {
                res();
            }
            if (!(pictureBox1.Location.X < 750))
            {
                res();
            }
            if (!(pictureBox1.Location.X > -50))
            {
                res();
            }
            if (pictureBox1.Location == apple.Location)
            {
                this.Controls.Remove(apple);
                snek[sz] = new PictureBox();
                snek[sz].Size = new Size(50, 50);
                snek[sz].BackColor = Color.Yellow;
                this.Controls.Add(snek[sz]);
                sz++;
                randapple();
                if (time.Interval > 100)
                {
                    time.Interval -= 2;
                }
            }
            if (!(snek[0] == null))
            {
                for (int i = sz; i > 1; i--)
                {
                    snek[i - 1].Location = snek[i - 2].Location;
                }
                snek[0].Location = sas;
                for (int i = 0; i < sz - 1; i++)
                {
                    if (pictureBox1.Location == snek[i].Location)
                    {
                        res();
                    }
                }
            }
        }

        private void res()
        {
            nap = 3;
            mon = mon + sz;
            path = @"C:\users\user\cash.txt";
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(mon);
            }
            path = @"C:\users\user\res.txt";
            pictureBox1.Location = new Point(0, 0);
            if (lose)
            {
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("ИМЯ: " + name + " РЕЗУЛЬТАТ: " + sz + " ДАТА И ВРЕМЯ: " + DateTime.Now);
                }
                for (int i = 0; i < sz; i++)
                {
                    this.Controls.Remove(snek[i]);
                    snek[i] = null;
                }
                int[] schet = new int[3];
                string[] nm = new string[3];
                path = @"C:\users\user\ch.txt";
                using (StreamReader sr = new StreamReader(path))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        schet[i] = int.Parse(sr.ReadLine());
                    }
                }
                path = @"C:\users\user\name.txt";
                using (StreamReader sr = new StreamReader(path))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        nm[i] = sr.ReadLine();
                    }
                }
                path = @"C:\users\user\name.txt";
                using (StreamWriter swn = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    path = @"C:\users\user\ch.txt";
                    using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (schet[i] < sz)
                            {
                                sw.WriteLine(sz);
                                swn.WriteLine(name);
                                sz = 0;
                            }
                            else
                            {
                                sw.WriteLine(schet[i]);
                                swn.WriteLine(nm[i]);
                            }
                        }
                    }
                }
                lose = false;
            }
            sz = 0;
            nap = 3;
            time.Interval = 200;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (col)
            {
                case 0:
                    pictureBox1.BackColor = Color.Red;
                    break;
                case 1:
                    pictureBox1.BackColor = Color.Blue;
                    break;
                case 2:
                    pictureBox1.BackColor = Color.Yellow;
                    break;
            }

            label4.Visible = false;
            button2.Enabled = false;
            button2.Visible = false;
            button3.Enabled = false;
            button3.Visible = false;
            button4.Enabled = false;
            button4.Visible = false;

            this.KeyDown += new KeyEventHandler(otk);
            randapple();
            time.Interval = 200;
            time.Tick += new EventHandler(snekmove);
            time.Start();
            apple.Size = new Size(50, 50);
            apple.BackColor = Color.Green;
            for (int i = 0; i < 14; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, 50 * i);
                pic.Size = new Size(800 - 50, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i < 15; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(50 * i, 0);
                pic.Location = new Point(50 * i, 0);
                pic.Size = new Size(1, 800 - 100);
                this.Controls.Add(pic);
            }
            button1.Enabled = false;
            button1.Visible = false;
            textBox1.Enabled = false;
            textBox1.Visible = false;
            pictureBox1.Visible = true;
            name = textBox1.Text;

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            col = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mon > 25)
            {
                col = 1;
                mon = mon - 25;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (mon > 50)
            {
                col = 2;
                mon = mon - 50;
            }
        }
    }
}
