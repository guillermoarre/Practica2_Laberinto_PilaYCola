using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica2_Laberinto_PilaYCola
{
    

    public partial class Form1 : Form
    {
        static int nfilas = 11, ncol = 11;
        int[,] Laberinto = new int[nfilas, ncol];

        int[,] PathCola = new int[nfilas, ncol];
        int[,] PathPila = new int[nfilas, ncol];

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbpila.Items.Clear();
            lbcola.Items.Clear();
            Random k;
            k = new Random();
            int aleat, valor = 0;
            Graphics g = pb.CreateGraphics();
            Pen lapiz = new Pen(Color.DarkBlue);
            Size s = new System.Drawing.Size(50, 50);
            Image pasto = Image.FromFile(@"Verde2.bmp");
            Image pared = Image.FromFile(@"Marron2.bmp");
            Image entrada = Image.FromFile(@"Azul2.bmp");
            Image textura;
            Point p = new Point(0, 0);
            Rectangle[,] r;
            r = new Rectangle[nfilas, ncol];
            for (int i = 0; i < nfilas; i++)
            {
                for (int j = 0; j < ncol; j++)
                {
                    if ((i == 0 && j == 0) || (i == nfilas - 1 && j == ncol - 1))
                    {
                        if (i == 0)
                        {
                            textura = entrada;
                        }
                        else
                        {
                            textura = pasto;
                        }
                        valor = 0;

                    }
                    else
                    {
                        if (i % 2 == 0 && j % 2 != 0)
                        {
                            textura = pasto;
                            valor = 0;
                        }
                        else
                        {
                            aleat = k.Next(2);
                            if (aleat == 0)
                            {
                                textura = pasto;
                            }

                            else
                                textura = pared;
                            valor = aleat;
                        }
                    }
                    Laberinto[i, j] = valor;
                    PathPila[i, j] = 1;
                    PathCola[i, j] = 1;
                    r[i, j].Location = p;
                    r[i, j].Size = s;
                    g.DrawImage(textura, r[i, j]);
                    p.X += 50;
                }

                p.X = 0;
                p.Y += 50;
            }
        

    }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Queue Cola = new Queue();
            Queue ColaRecorrido = new Queue();
            string ColaReserve = "";
            int filaC, columnaCola;


            Cola.Clear();
            Cola.Enqueue("0,0");
            bool StopCola = true;

            while (StopCola != false)
            {
                if (Cola.Count != 0)
                {
                    string[] temp2c = Cola.Peek().ToString().Split(',');
                    filaC = int.Parse(temp2c[0]);
                    columnaCola = int.Parse(temp2c[1]);
                }
                else
                {
                    StopCola = true;
                    break;
                }

                PathCola[filaC, columnaCola] = 0;

                if (filaC == 10 && columnaCola == 10)
                {
                    StopCola = false;
                    break;
                }

                ColaRecorrido = new Queue(Cola);

                while (ColaRecorrido.Count != 0)
                {
                    ColaReserve += ColaRecorrido.Dequeue().ToString() + " ; ";
                }

                lbcola.Items.Add(ColaReserve);
                ColaReserve = "";
                Cola.Dequeue();

                if (columnaCola < 10 && PathCola[filaC, columnaCola + 1] == 1 && Laberinto[filaC, columnaCola + 1] == 0)
                {
                    Cola.Enqueue(filaC + "," + (columnaCola + 1));
                    PathCola[filaC, columnaCola + 1] = 0;
                }

                if (columnaCola != 0 && PathCola[filaC, columnaCola - 1] == 1 && Laberinto[filaC, columnaCola - 1] == 0)
                {
                    Cola.Enqueue(filaC + "," + (columnaCola - 1));
                    PathCola[filaC, columnaCola - 1] = 0;
                }

                if (filaC < 10 && PathCola[filaC + 1, columnaCola] == 1 && Laberinto[filaC + 1, columnaCola] == 0)
                {
                    Cola.Enqueue((filaC + 1) + "," + columnaCola);
                    PathCola[filaC + 1, columnaCola] = 0;
                }

                if (filaC != 0 && PathCola[filaC - 1, columnaCola] == 1 && Laberinto[filaC - 1, columnaCola] == 0)
                {
                    Cola.Enqueue((filaC - 1) + "," + columnaCola);
                    PathCola[filaC - 1, columnaCola] = 0;
                }
            }


            Stack Pila = new Stack();
            string PilaReserve = "";
            int filaPila, columnaPila;

            Pila.Clear();
            Pila.Push("0,0");
            bool StopPila = true;

            while (StopPila != false)
            {
                if (Pila.Count != 0)
                {
                    string[] temp = Pila.Peek().ToString().Split(',');
                    filaPila = int.Parse(temp[0]);
                    columnaPila = int.Parse(temp[1]);
                }
                else
                {
                    StopPila = true;
                    break;
                }

                PathPila[filaPila, columnaPila] = 0;

                if (filaPila == 10 && columnaPila == 10)
                {
                    StopPila = false;
                    break;
                }

                Stack PilaRecorrido = (Stack)Pila.Clone();

                while (PilaRecorrido.Count != 0)
                {
                    PilaReserve += PilaRecorrido.Pop().ToString() + " ; ";
                }

                lbpila.Items.Add(PilaReserve);
                PilaReserve = "";
                Pila.Pop();

                if (columnaPila < 10 && PathPila[filaPila, columnaPila + 1] == 1 && Laberinto[filaPila, columnaPila + 1] == 0)
                {
                    Pila.Push(filaPila + "," + (columnaPila + 1));
                    PathPila[filaPila, columnaPila + 1] = 0;
                }

                if (columnaPila != 0 && PathPila[filaPila, columnaPila - 1] == 1 && Laberinto[filaPila, columnaPila - 1] == 0)
                {
                    Pila.Push(filaPila + "," + (columnaPila - 1));
                    PathPila[filaPila, columnaPila - 1] = 0;
                }

                if (filaPila < 10 && PathPila[filaPila + 1, columnaPila] == 1 && Laberinto[filaPila + 1, columnaPila] == 0)
                {
                    Pila.Push((filaPila + 1) + "," + columnaPila);
                    PathPila[filaPila + 1, columnaPila] = 0;
                }

                if (filaPila != 0 && PathPila[filaPila - 1, columnaPila] == 1 && Laberinto[filaPila - 1, columnaPila] == 0)
                {
                    Pila.Push((filaPila - 1) + "," + columnaPila);
                    PathPila[filaPila - 1, columnaPila] = 0;
                }
            }




            if ((StopCola && StopPila) == false)
            {
                MessageBox.Show("Solución encontrada");
            }
            else
            {
                MessageBox.Show("No hay solución");
            }


            if (Pila.Count == 1)
            {
                lbpila.Items.Add(Pila.Pop().ToString());
            }

            if (Cola.Count == 1)
            {
                lbcola.Items.Add(Cola.Dequeue().ToString());
            }

        }
    }
}
