using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace filetest
{
    public partial class Form1 : Form
    {
        public string selFile = ""; //переменная в которой будет храниться путь к нашему файлу

        public Form1()
        {
            InitializeComponent();
            
        }

        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog(); //создаём файловый диалог
            
            string filename = "";


            if (openFileDialog.ShowDialog(this) != DialogResult.Cancel) //открываем файловый диалог и проверяем его результат
            {
                if (openFileDialog.FileName.EndsWith(".txt")) //проверка на расширение выбранного файла
                {
                    selFile = openFileDialog.FileName; //сюда записывается путь выбранного файла
                    filename = openFileDialog.SafeFileName; //сюда записывается название файла
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите файл с расширением \".txt\" ");
                }
            }

            try
            {
                StreamReader str = new StreamReader(selFile);
                string txt = str.ReadToEnd(); //переменная txt полностью зачитывает в себя выбранный файл
                MainText.Text = txt;
                FileName.Text = filename;
                str.Close(); //закрываем открытый ранее поток, чтобы мы в дальнейшем смогли изменять файл
            }
            catch (Exception ex)
            {

            }

        }

        private void созранитьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt = MainText.Text;
            try
            {
                string name = FileName.Text;
                if (File.Exists(selFile)) //проверка на существование файла
                {
                    StreamWriter str = new StreamWriter(selFile, false);
                    str.WriteAsync(txt);
                    str.Close();
                }
                else
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog(); //создаём диалог сохранения файла
                    saveFileDialog.FileName = name;
                    saveFileDialog.DefaultExt = "txt"; //задаём выходной тип файла
                    saveFileDialog.Filter = "txt files (*.txt) | .txt"; //задаём фильт по расширению файла
                    if (saveFileDialog.ShowDialog(this) != DialogResult.Cancel) //открываем файловый диалог и проверяем его результат
                    {
                        Stream file = saveFileDialog.OpenFile(); //открываем отдельный поток для сохранения файла
                        StreamWriter str = new StreamWriter(file); //создаём записывающий поток, в который присоединяем предыдущий отдельно созданный поток

                        str.Write(txt); //полностью записываем текст в файл

                        str.Close();
                        file.Close();
                    }
                }
            }
            catch(Exception ex)
            {

            }    
        }

        private void закрытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
