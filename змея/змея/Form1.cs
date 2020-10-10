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

namespace змея
{
    public partial class Form1 : Form
    {
        string path = @"C:\users\user\res.txt";
        string name;
        int nap = 3;
        int ch = 0;
        Timer time = new Timer();
        Point sas = new Point(0,0);
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
                    else{
                        res();
                    }
                    break;
                case "Down":
                    if (pictureBox1.Location.Y < 650)
                    {
                        nap = 1;
                    }
                    else{
                        res();
                    }
                    break;
                case "Right":
                    if (pictureBox1.Location.X < 700)
                    {
                        nap = 3;
                    }
                    else{
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
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }

        private void randapple()
        {
            apple.Location= new Point(rand.Next(15)*50,rand.Next(14)*50);
            this.Controls.Add(apple);
        }

        private void snekmove(object myObject, EventArgs eventArgs)
        {
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
            }
            if (!(snek[0] == null))
            {
                for (int i = sz; i > 1; i--)
                {
                    snek[i-1].Location = snek[i-2].Location;
                }
                snek[0].Location = sas;
                for (int i=0;i<sz-1;i++)
                {
                    if (pictureBox1.Location==snek[i].Location)
                    {
                        res();
                    }
                }
            }
        }

        private void res()
        {
            pictureBox1.Location = new Point(0, 0);
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("ИМЯ: "+name+" РЕЗУЛЬТАТ: "+sz+" ДАТА И ВРЕМЯ: "+ DateTime.Now);
            }
            for (int i = 0; i < sz; i++)
            {
                this.Controls.Remove(snek[i]);
                snek[i] = null;
            }
            sz = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
        }
    }
}
