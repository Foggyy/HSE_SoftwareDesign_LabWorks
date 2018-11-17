using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SD_GraphicRedactor
{    
    public partial class frmChild : Form
    {
        static public Color currentColor;                     //текущий цвет
        static public int LineWidth;                          //ширина линии
        static public Point startPoint;                       //начальная точка

        public Bitmap Image;                                   //Основное изображение
        private Bitmap HelpImage;                              //Вспомогательное изображение, нужно для отображения анимации
        private Graphics g;                                   //Объект GDI
        static public bool Clicked;                           //переменная для проверки зажатия ЛКМ

        public frmChild()
        {
            InitializeComponent();
            Image = new Bitmap(1920, 1080);      //задаем границы изображения
            pictureBox1.Image = Image;
            GetPictureBox.GetPB = pictureBox1;
            GetPictureBox.LiveImage = Image;
            GetPictureBox.SaveImage = Image;
        }

        public frmChild(Bitmap picture)
        {
            InitializeComponent();
            Image = picture;
            pictureBox1.Image = Image;
            pictureBox1.Width = picture.Width;
            pictureBox1.Height = picture.Height;
            Bitmap HelpBmp = new Bitmap(Image, pictureBox1.Width, pictureBox1.Height);
            Image = HelpBmp;
            pictureBox1.Image = Image;
        }

        /// <summary>
        /// Запуск окна(формы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void frmChild_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            pictureBox1.Invalidate();
            pictureBox1.Refresh();
            g = Graphics.FromImage(Image);                                 //создание объекта Graphics для Image
            GetPictureBox.g = Graphics.FromImage(Image);
            g.SmoothingMode = SmoothingMode.AntiAlias;                     //сглаживание

        }

        /// <summary>
        /// Движение мыши по PictureBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {          
            currentColor = Data.GetColor;
            LineWidth = Data.GetSize;
            g = Graphics.FromImage(Image);
            GetPictureBox.g = Graphics.FromImage(Image);
            if (e.Button == MouseButtons.Left)
            {

                switch (Actions.GetAction)
                {
                    case "Pen":
                        if (Clicked)
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;                                          //сглаживание краев
                            g.DrawLine(new Pen(currentColor,LineWidth), startPoint.X, startPoint.Y, e.X, e.Y);                           
                            startPoint.X = e.X;                                                                 //обновляем координаты
                            startPoint.Y = e.Y;
                            pictureBox1.Refresh();                                                              //перерисовка
                        }
                        break;

                    case "Line":
                        if (Clicked)
                        {
                            HelpImage = (Bitmap) Image.Clone();                     //клонируем основное изображение
                            g.DrawLine(new Pen(currentColor, LineWidth), startPoint.X, startPoint.Y, e.X, e.Y); //рисуем "моргающую" линию, показывающую соединение двух точек 
                            pictureBox1.Invalidate();                              //обновление изображения каждый раз при рисовании (похоже на событие Paint)
                            Image = HelpImage;
                            GetPictureBox.LiveImage = HelpImage;
                            Refresh();
                            pictureBox1.Image = HelpImage;
                            pictureBox1.Invalidate();
                            GetPictureBox.GetPB = pictureBox1;
                        }                      
                        break;

                    case "Elipse":
                        if (Clicked)
                        {
                            HelpImage = (Bitmap)Image.Clone();                     //клонируем основное изображение
                            g.DrawEllipse(new Pen(currentColor,LineWidth), startPoint.X, startPoint.Y, e.X - startPoint.X, e.Y - startPoint.Y); //рисуем "моргающий" круг
                            pictureBox1.Invalidate();
                            Image = HelpImage;
                            GetPictureBox.LiveImage = HelpImage;
                            Refresh();
                            pictureBox1.Image = HelpImage;
                            pictureBox1.Invalidate();
                            GetPictureBox.GetPB = pictureBox1;
                        }
                        break;

                    case "Eraser":
                        g.DrawLine(new Pen(pictureBox1.BackColor, LineWidth), startPoint.X, startPoint.Y, e.X, e.Y);
                        startPoint.X = e.X;                                                                 //обновляем координаты
                        startPoint.Y = e.Y;
                        pictureBox1.Refresh();
                        break;

                    case "Star":
                        HelpImage = (Bitmap) Image.Clone();
                        PointF[] points = new PointF[2 * 5 + 1];
                        double alpha = 0;                                       //поворот
                        double a = alpha, da = Math.PI / 5, l;
                        double r = Math.Abs(startPoint.Y - e.Y), R = Math.Abs(startPoint.X - e.X);  //радиусы, модуль нужен чтобы рисовать только 1 звезду
                        

                        for (int k = 0; k < 2 * 5 + 1; k++)
                        {
                            l = k % 2 == 0 ? r : R;
                            points[k] = new PointF((float)(startPoint.X + l * Math.Cos(a)), (float)(startPoint.Y + l * Math.Sin(a)));
                            a += da;
                        }
                        g.DrawLines(new Pen(currentColor,LineWidth),points);
                        pictureBox1.Invalidate();
                        Image = HelpImage;
                        GetPictureBox.LiveImage = HelpImage;
                        Refresh();
                        pictureBox1.Image = HelpImage;
                        pictureBox1.Invalidate();
                        GetPictureBox.GetPB = pictureBox1;
                        break;
                }
            }

        }

        /// <summary>
        /// Нажатие на ЛКМ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Clicked = true;
            startPoint.X = e.X;
            startPoint.Y = e.Y;
        }

        /// <summary>
        /// Отжатие ЛКМ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (Actions.GetAction)
            {

                case "Line":
                    g.DrawLine(new Pen(currentColor,LineWidth), startPoint.X, startPoint.Y, e.X, e.Y);
                    pictureBox1.Refresh();
                    break;

                case "Elipse":
                    g.DrawEllipse(new Pen(currentColor, LineWidth), startPoint.X, startPoint.Y, e.X - startPoint.X, e.Y - startPoint.Y);
                    pictureBox1.Refresh();
                    break;
                case "Star":
                    PointF[] points = new PointF[2 * 5 + 1];
                    double alpha = 0;                                       //поворот
                    double a = alpha, da = Math.PI / 5, l;
                    double r = Math.Abs(startPoint.Y - e.Y), R = Math.Abs(startPoint.X - e.X);  //радиусы, модуль нужен чтобы рисовать только 1 звезду


                    for (int k = 0; k < 2 * 5 + 1; k++)
                    {
                        l = k % 2 == 0 ? r : R;
                        points[k] = new PointF((float)(startPoint.X + l * Math.Cos(a)), (float)(startPoint.Y + l * Math.Sin(a)));
                        a += da;
                    }
                    g.DrawLines(new Pen(currentColor, LineWidth), points);
                    pictureBox1.Invalidate();
                    break;
            }


        }

    }

    public class GetPictureBox
    {
        public static PictureBox GetPB { get; set; }
        public static Bitmap LiveImage { get; set; }
        public static Bitmap SaveImage { get; set; }
        public static Graphics g { get; set; }
    }
}
