using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SD_GraphicRedactor
{
    /// <summary>
    /// Класс содержащий тип действия и эффекты
    /// </summary>
    public class Actions : frmChild
    {

        /// <summary>
        /// Получаем название действия
        /// </summary>
        public static string GetAction { get; set; }

        static private bool ok = true;
        
        /// <summary>
        /// Масштабирование+
        /// </summary>
        static public void ScalePlus()
        {
            try
            {
                if (ok)
                {
                    GetPictureBox.SaveImage = GetPictureBox.LiveImage;
                    ok = false;
                }

                GetPictureBox.LiveImage = new Bitmap(GetPictureBox.LiveImage, GetPictureBox.LiveImage.Width * 2,
                    GetPictureBox.LiveImage.Height * 2);
                GetPictureBox.GetPB.Image = GetPictureBox.LiveImage;
            }
            catch (Exception)
            {
                ScaleNormal();
                MessageBox.Show("Ошибка масштабирования");
            }

        }

        /// <summary>
        /// Масштабирование-
        /// </summary>
        static public void ScaleMinus()
        {
            try
            {
                if (ok)
                {
                    GetPictureBox.SaveImage = GetPictureBox.LiveImage;
                    ok = false;
                }

                GetPictureBox.LiveImage = new Bitmap(GetPictureBox.LiveImage, GetPictureBox.LiveImage.Width / 2,
                    GetPictureBox.LiveImage.Height / 2);
                GetPictureBox.GetPB.Image = GetPictureBox.LiveImage;
            }
            catch (Exception)
            {
                ScaleNormal();
                MessageBox.Show("Ошибка масштабирования");                
            }

        }

        /// <summary>
        /// Нормальный вид
        /// </summary>
        static public void ScaleNormal()
        {
            
            GetPictureBox.LiveImage = new Bitmap(GetPictureBox.LiveImage, GetPictureBox.SaveImage.Width, GetPictureBox.SaveImage.Height);
            GetPictureBox.GetPB.Image = GetPictureBox.LiveImage;
        }

        /// <summary>
        /// Обновление
        /// </summary>
        static public void EffectsRefresh()
        {
            GetPictureBox.GetPB.Invalidate();
            GetPictureBox.GetPB.Refresh();
            GetPictureBox.g = Graphics.FromImage(GetPictureBox.LiveImage); //создание объекта Graphics для Image

        }

        /// <summary>
        /// Выдавливание
        /// </summary>
        static public void Effect1()
        {
            var tempBmp = new Bitmap(GetPictureBox.GetPB.Image);
            int dispX = 1, dispY = 1;
            for (var i = 0; i < tempBmp.Height - 2; ++i)
            for (var j = 0; j < tempBmp.Width - 2; ++j)
            {
                Color pixel1 = new Color(), pixel2 = new Color();
                pixel1 = tempBmp.GetPixel(j, i);
                pixel2 = tempBmp.GetPixel(j + dispX, i + dispY);
                var red = Math.Min(Math.Abs(pixel1.R - pixel2.R) + 128, 255);
                var green = Math.Min(Math.Abs(pixel1.G - pixel2.G) + 128, 255);
                var blue = Math.Min(Math.Abs(pixel1.B - pixel2.B) + 128, 255);
                GetPictureBox.LiveImage.SetPixel(j, i, Color.FromArgb(red, green, blue));

                //if (i % 10 == 0)
                //{
                //    GetPictureBox.GetPB.Invalidate();
                //    GetPictureBox.GetPB.Refresh();
                //}
            }

            EffectsRefresh();
        }

        /// <summary>
        /// Заострение
        /// </summary>
        static public void Effect2()
        {
            var tempBmp = new Bitmap(GetPictureBox.GetPB.Image);
            int DX = 1, DY = 1;
            for (var i = DX; i < tempBmp.Height - DY - 1; ++i)
            for (var j = DY; j < tempBmp.Width - DY - 1; ++j)
            {
                var red = (int) (tempBmp.GetPixel(j, i).R + 0.5 * tempBmp.GetPixel(j, i).R
                                 - tempBmp.GetPixel(j - DX, i - DY).R);
                var green = (int) (tempBmp.GetPixel(j, i).G + 0.7 * tempBmp.GetPixel(j, i).G
                                   - tempBmp.GetPixel(j - DX, i - DY).G);
                var blue = (int) (tempBmp.GetPixel(j, i).B + 0.5 * tempBmp.GetPixel(j, i).B
                                  - tempBmp.GetPixel(j - DX, i - DY).B);
                red = Math.Min(Math.Max(red, 0), 255);
                green = Math.Min(Math.Max(green, 0), 255);
                blue = Math.Min(Math.Max(blue, 0), 255);
                GetPictureBox.LiveImage.SetPixel(j, i, Color.FromArgb(red, green, blue));
            }

            EffectsRefresh();
        }

        /// <summary>
        /// Сглаживание
        /// </summary>
        static public void Effect3()
        {
            var tempBmp = new Bitmap(GetPictureBox.GetPB.Image);
            const int DX = 1;
            const int DY = 1;
            for (var i = DX; i < tempBmp.Height - DX - 1; ++i)
            {
                for (var j = DY; j < tempBmp.Width - DY - 1; ++j)
                {
                    var red = (tempBmp.GetPixel(j - 1, i - 1).R +
                               tempBmp.GetPixel(j - 1, i).R +
                               tempBmp.GetPixel(j - 1, i + 1).R +
                               tempBmp.GetPixel(j, i - 1).R +
                               tempBmp.GetPixel(j, i).R +
                               tempBmp.GetPixel(j, i + 1).R +
                               tempBmp.GetPixel(j + 1, i - 1).R +
                               tempBmp.GetPixel(j + 1, i).R +
                               tempBmp.GetPixel(j + 1, i + 1).R) / 9;

                    var green = (tempBmp.GetPixel(j - 1, i - 1).G +
                                 tempBmp.GetPixel(j - 1, i).G +
                                 tempBmp.GetPixel(j - 1, i + 1).G +
                                 tempBmp.GetPixel(j, i - 1).G +
                                 tempBmp.GetPixel(j, i).G +
                                 tempBmp.GetPixel(j, i + 1).G +
                                 tempBmp.GetPixel(j + 1, i - 1).G +
                                 tempBmp.GetPixel(j + 1, i).G +
                                 tempBmp.GetPixel(j + 1, i + 1).G) / 9;

                    var blue = (tempBmp.GetPixel(j - 1, i - 1).B +
                                tempBmp.GetPixel(j - 1, i).B +
                                tempBmp.GetPixel(j - 1, i + 1).B +
                                tempBmp.GetPixel(j, i - 1).B +
                                tempBmp.GetPixel(j, i).B +
                                tempBmp.GetPixel(j, i + 1).B +
                                tempBmp.GetPixel(j + 1, i - 1).B +
                                tempBmp.GetPixel(j + 1, i).B +
                                tempBmp.GetPixel(j + 1, i + 1).B) / 9;
                    red = Math.Min(Math.Max(red, 0), 255);
                    green = Math.Min(Math.Max(green, 0), 255);
                    blue = Math.Min(Math.Max(blue, 0), 255);
                    GetPictureBox.LiveImage.SetPixel(j, i, Color.FromArgb(red, green, blue));
                }

                EffectsRefresh();
            }


        }

        /// <summary>
        /// Рассеивание
        /// </summary>
        static public void Effect7()
        {
            var rnd = new Random();
            var tempbmp = new Bitmap(GetPictureBox.GetPB.Image);
            for (var i = 3; i < tempbmp.Height - 3; ++i)
            {
                for (var j = 3; j < tempbmp.Width - 3; ++j)
                {
                    var DX = rnd.Next(4) - 2;
                    var DY = rnd.Next(4) - 2;
                    var red = tempbmp.GetPixel(j + DX, i + DY).R;
                    var green = tempbmp.GetPixel(j + DX, i + DY).G;
                    var blue = tempbmp.GetPixel(j + DX, i + DY).B;
                    GetPictureBox.LiveImage.SetPixel(j, i, Color.FromArgb(red, green, blue));
                }

                EffectsRefresh();
            }
        }
    }
}
