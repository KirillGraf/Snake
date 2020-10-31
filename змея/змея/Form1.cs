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
        bool ded = false;
        bool pause = false;
        int col = 0;
        int mon = 0;
        bool lose = true;
        string path = @"C:\users\user\res.txt";
        string name;
        int nap = 3;
        Timer time = new Timer();
        Point sas = new Point(0, 0);
        PictureBox[] snek = new PictureBox[209];
        PictureBox[] x = new PictureBox[15];
        PictureBox[] y = new PictureBox[16];
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
                case "W":
                    if (pictureBox1.Location.Y > 0)
                    {
                        nap = 0;
                    }
                    else
                    {
                        label6.Visible = true;
                        label6.Visible = true;
                        label11.Enabled = true;
                        label11.Visible = true;
                        ded = true;
                        time.Stop();
                    }
                    break;
                case "S":
                    if (pictureBox1.Location.Y < 650)
                    {
                        nap = 1;
                    }
                    else
                    {
                        label6.Visible = true;
                        label6.Visible = true;
                        label11.Enabled = true;
                        label11.Visible = true;
                        ded = true;
                        time.Stop();
                    }
                    break;
                case "D":
                    if (pictureBox1.Location.X < 700)
                    {
                        nap = 3;
                    }
                    else
                    {
                        label6.Visible = true;
                        label6.Visible = true;
                        label11.Enabled = true;
                        label11.Visible = true;
                        ded = true;
                        time.Stop();
                    }
                    break;
                case "A":
                    if (pictureBox1.Location.X > 0)
                    {
                        nap = 2;
                    }
                    else
                    {
                        label6.Visible = true;
                        label6.Visible = true;
                        label11.Enabled = true;
                        label11.Visible = true;
                        ded = true;
                        time.Stop();
                    }
                    break;

                case "Up":
                    if (pictureBox1.Location.Y > 0)
                    {
                        nap = 0;
                    }
                    else
                    {
                        label6.Visible = true;
                        label6.Visible = true;
                        label11.Enabled = true;
                        label11.Visible = true;
                        ded = true;
                        time.Stop();
                    }
                    break;
                case "Down":
                    if (pictureBox1.Location.Y < 650)
                    {
                        nap = 1;
                    }
                    else
                    {
                        label6.Visible = true;
                        label6.Visible = true;
                        label11.Enabled = true;
                        label11.Visible = true;
                        ded = true;
                        time.Stop();
                        time.Stop();
                    }
                    break;
                case "Right":
                    if (pictureBox1.Location.X < 700)
                    {
                        nap = 3;
                    }
                    else
                    {
                        label6.Visible = true;
                        label6.Visible = true;
                        label11.Enabled = true;
                        label11.Visible = true;
                        ded = true;
                        time.Stop();
                    }
                    break;
                case "Left":
                    if (pictureBox1.Location.X > 0)
                    {
                        nap = 2;
                    }
                    else
                    {
                        label6.Visible = true;
                        label6.Visible = true;
                        label11.Enabled = true;
                        label11.Visible = true;
                        ded = true;
                        time.Stop();
                    }
                    break;

                case "Escape":
                    if (!ded)
                    {
                        pause = !pause;
                        if (pause)
                        {
                            label5.Visible = true;
                            label11.Visible = true;
                            label11.Enabled = true;

                            time.Stop();
                        }
                        else
                        {
                            label5.Visible = false;
                            label11.Visible = false;
                            label11.Enabled = false;

                            time.Start();
                        }
                    }
                    break;


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            time.Tick += new EventHandler(snekmove);

            label5.Visible = false;
            label6.Visible = false;
            label6.Visible = false;
            label10.Visible = false;

            label11.Visible = false;
            label11.Enabled = false;

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

            for (int i = 0; i < 15; i++)
            {
                x[i] = new PictureBox();
                x[i].BackColor = Color.Black;
                x[i].Location = new Point(0, 50 * i);
                x[i].Size = new Size(800 - 50, 1);
                x[i].Visible = false;
                this.Controls.Add(x[i]);
            }
            for (int i = 0; i < 16; i++)
            {
                y[i] = new PictureBox();
                y[i].BackColor = Color.Black;
                y[i].Location = new Point(50 * i, 0);
                y[i].Location = new Point(50 * i, 0);
                y[i].Size = new Size(1, 800 - 100);
                y[i].Visible = false;
                this.Controls.Add(y[i]);
            }
        }
        private void randapple()
        {
            apple.Location = new Point(rand.Next(15) * 50, rand.Next(14) * 50);
            this.Controls.Add(apple);
        }

        private void snekmove(object myObject, EventArgs eventArgs)
        {
            label10.Text ="Счет:"+ sz;
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
                        label6.Visible = true;
                        label6.Visible = true;
                        label11.Enabled = true;
                        label11.Visible = true;
                        time.Stop();
                    }
                }
            }
            if (!(pictureBox1.Location.Y > -50))
            {
                label6.Visible = true;
                label6.Visible = true;
                label11.Enabled = true;
                label11.Visible = true;
                ded = true;
                time.Stop();
            }
            if (!(pictureBox1.Location.Y < 700))
            {
                label6.Visible = true;
                label6.Visible = true;
                label11.Enabled = true;
                label11.Visible = true;
                ded = true;
                time.Stop();
            }
            if (!(pictureBox1.Location.X < 750))
            {
                label6.Visible = true;
                label6.Visible = true;
                label11.Enabled = true;
                label11.Visible = true;
                ded = true;
                time.Stop();
            }
            if (!(pictureBox1.Location.X > -50))
            {
                label6.Visible = true;
                label6.Visible = true;
                label11.Enabled = true;
                label11.Visible = true;
                ded = true;
                time.Stop();
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
                    time.Interval -= 3;
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
                        label6.Visible = true;
                        label6.Visible = true;
                        label11.Enabled = true;
                        label11.Visible = true;
                        ded = true;
                        time.Stop();
                    }
                }
            }
        }

        private void res()
        {
            label6.Visible = false;
            label6.Visible = false;
            time.Start();

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

            for (int i = 0; i < 16; i++)
            {
                y[i].Visible = true;
            }
            for (int i = 0; i < 15; i++)
            {
                x[i].Visible = true;
            }

            label4.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;

            button2.Enabled = false;
            button2.Visible = false;
            button3.Enabled = false;
            button3.Visible = false;
            button4.Enabled = false;
            button4.Visible = false;

            label10.Visible = true;
            this.KeyDown += new KeyEventHandler(otk);
            randapple();
            time.Interval = 200;
            time.Start();
            apple.Size = new Size(50, 50);
            apple.BackColor = Color.Green;
            apple.Visible = true;


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
            label4.Text = "Баланс: " + mon;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mon > 25)
            {
                col = 1;
                mon = mon - 25;
            }
            label4.Text = "Баланс: " + mon;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (mon > 50)
            {
                col = 2;
                mon = mon - 50;
            }
            label4.Text = "Баланс: " + mon;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            label11.Enabled = false;
            label11.Visible = false;
            ded = false;
            res();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            label6.Visible = false;
            time.Stop();

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

            label4.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;

            button2.Enabled = true;
            button2.Visible = true;
            button3.Enabled = true;
            button3.Visible = true;
            button4.Enabled = true;
            button4.Visible = true;
            label11.Enabled = false;
            label11.Visible = false;
            label6.Enabled = false;
            label6.Visible = false;
            label5.Enabled = false;
            label5.Visible = false;

            button1.Enabled = true;
            button1.Visible = true;
            textBox1.Enabled = true;
            textBox1.Visible = true;
            pictureBox1.Visible = false;

            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;

            label4.Text = "Баланс: " + mon;

            apple.Visible = false;

            for (int i = 0; i < 16; i++)
            {
                y[i].Visible = false;
            }
            for (int i = 0; i < 15; i++)
            {
                x[i].Visible = false;
            }

        }

    }
}
