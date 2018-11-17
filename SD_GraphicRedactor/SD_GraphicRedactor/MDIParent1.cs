using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SD_GraphicRedactor
{

    public partial class MDIParent1 : Form
    {
        ColorDialog colorDialog = new ColorDialog();
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
            ColorLabel.BackColor = Color.Black;
            Data.GetColor = Color.Black;
            SizeTextBox.Text = "1";
            Data.GetSize = 1;

        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            frmChild childForm = new frmChild();
            childForm.MdiParent = this;
            childForm.Text = "Окно " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "bmp (*.bmp)|*.bmp|jpeg (*.jpeg)|*.jpeg|png (*.png)|*.png";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                    GetPictureBox.LiveImage = new Bitmap(Path.GetFullPath(openFileDialog.FileName));
                    frmChild PaperForm = new frmChild(GetPictureBox.LiveImage)
                    {
                        MdiParent = this
                    };
                PaperForm.Show();

            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "bmp (*.bmp)|*.bmp|jpeg (*.jpeg)|*.jpeg|png (*.png)|*.png";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                (ActiveMdiChild as frmChild).Image.Save(FileName);
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        /// <summary>
        /// Выбор цвета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorLabel_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Data.GetColor = colorDialog.Color;                      //Возвращаем полученный цвет
                ColorLabel.BackColor = colorDialog.Color;               //Изменение цвета фона ColorLabel
            }
        }

        /// <summary>
        /// Изменение размера(толщины)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SizeTextBox_KeyUp(object sender, KeyEventArgs e)
        {
                int size;
                bool check;
                check = int.TryParse(SizeTextBox.Text, out size);       //Проверка на ввод
                if (SizeTextBox.Text == "")                             //Если ввод пустой, то не выводить сообщение
                {

                }
                else if (!check)                                        //Если введено не число
                {
                    MessageBox.Show("Введите целое число");
                }
                else
                Data.GetSize = size;
        }

        //Свернутые события вызова кнопок выбора инструмента
        #region Нажатие кнопок выбора инструмента




        /// <summary>
        /// Нажатие кнопки "Перо"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PenButton_Click(object sender, EventArgs e)
        {
            Actions.GetAction = "Pen";
        }
        /// <summary>
        /// Нажатие кнопки "Прямая"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LineButton_Click(object sender, EventArgs e)
        {
            Actions.GetAction = "Line";
        }
        /// <summary>
        /// Нажатие кнопки "Эллипс"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElipseButton_Click(object sender, EventArgs e)
        {
            Actions.GetAction = "Elipse";
        }
        /// <summary>
        /// Нажатие кнопки "Звезда"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StarButton_Click(object sender, EventArgs e)
        {
            Actions.GetAction = "Star";
        }
        /// <summary>
        /// Нажатие кнопки "Ластик"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EraserButton_Click(object sender, EventArgs e)
        {
            Actions.GetAction = "Eraser";
        }
        /// <summary>
        /// Нажатие кнопки "Увеличить масштаб"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScalePlus_Click(object sender, EventArgs e)
        {
            Actions.ScalePlus();
        }
        /// <summary>
        /// Нажатие кнопки "Уменьшить масштаб"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScaleMinus_Click(object sender, EventArgs e)
        {
            Actions.ScaleMinus();
        }


        #endregion

        /// <summary>
        /// Выдавливание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void эффект1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.Effect1();
        }

        /// <summary>
        /// Заострение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void эффект2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.Effect2();
        }

        /// <summary>
        /// Сглаживание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void эффект3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.Effect3();
        }

        /// <summary>
        /// Масштабирование+
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void эффект4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.ScalePlus();
        }

        /// <summary>
        /// Масштабирование-
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void эффект5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.ScaleMinus();
        }

        /// <summary>
        /// Возвращение к нормальному виду без масштабирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void эффект6НормальныйВидToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.ScaleNormal();
        }

        /// <summary>
        /// Рассеивание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void эффект7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actions.Effect7();
        }

        /// <summary>
        /// Поворот изображения вправо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void эффект8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetPictureBox.LiveImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            GetPictureBox.GetPB.Width = GetPictureBox.GetPB.Height * GetPictureBox.GetPB.Image.Width /
                                        GetPictureBox.GetPB.Image.Height;
            Refresh();
            Update();
        }

        /// <summary>
        /// Поворот изображения влево
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void эффект9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetPictureBox.LiveImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            GetPictureBox.GetPB.Width = GetPictureBox.GetPB.Height * GetPictureBox.GetPB.Image.Width /
                                        GetPictureBox.GetPB.Image.Height;
            Refresh();
            Update();
        }

        /// <summary>
        /// Отразить по горизонтали
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void эффект10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetPictureBox.LiveImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Refresh();
            Update();
        }
        /// <summary>
        /// Отразить по вертикали
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void эффект11ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetPictureBox.LiveImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            Refresh();
            Update();
        }
    }

    /// <summary>
    /// Класс для хранения значений
    /// </summary>
    class Data
    {
        /// <summary>
        /// Изменение, получение текущего цвета
        /// </summary>
        public static Color GetColor { get; set; }
        /// <summary>
        /// Изменение, получение текущей толщины
        /// </summary>
        public static int GetSize { get; set; }
    }
}
