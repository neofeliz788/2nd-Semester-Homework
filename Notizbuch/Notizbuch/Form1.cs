using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Notizbuch
{
    public partial class Notizbuch : Form
    {
        private bool isSaved = false;
        private bool isSavedAs = false;
        private string filePath = "";

        private DialogResult MessageBoxYesNo(string text1, string text2)
        {
            DialogResult result = MessageBox.Show(text1, text2,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                return result;
            }
            return result;
        }

        public Notizbuch()
        {
            InitializeComponent();
        }

        private void NotePadLoad(object sender, EventArgs e)
        {
        }

        private void CreateNewFile(object sender, EventArgs e)
        {
            Application.Restart();
        }


        private void SaveFile(object sender, EventArgs e)
        {
            if (isSaved == false)
            {
                if (isSavedAs == false)
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "Text file | *.txt";
                    save.Title = "Save text file";
                    save.ShowDialog();
                    filePath = save.FileName;
                    if (!filePath.Equals(""))
                    {
                        File.WriteAllText(filePath, richTextBox1.Rtf);
                        isSaved = true;
                        isSavedAs = true;
                    }
                }
                else
                {
                    File.WriteAllText(filePath, richTextBox1.Rtf);
                    isSaved = true;
                }
            }
        }
        private void ChangeTextBox(object sender, EventArgs e)
        {
            isSaved = false;
        }
        private void NotePad_ExitClick(object sender, FormClosingEventArgs e)
        {
            if (isSaved == false)
            {
                if (MessageBoxYesNo("Сохранить текущий файл?", "Сохранение") == DialogResult.Yes)
                {
                    SaveFile(sender, e);
                }
            }
        }
        private void MenuExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void MenuOpenClick(object sender, EventArgs e)
        {
            if (isSaved == false)
            {
                if (MessageBoxYesNo("Сохранить текущий файл перед открытием нового?", "Сохранение") == DialogResult.Yes)
                {
                    SaveFile(sender, e);
                }
            }
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "| *.txt";
            open.ShowDialog();
            if (!open.FileName.Equals(""))
            {
                richTextBox1.Rtf = File.ReadAllText(open.FileName);
                isSaved = true;
                isSavedAs = true;
            }
        }
        private void AboutProgrammClick(object sender, EventArgs e)
        {
            MessageBox.Show("Блокнот\nАвтор - Динар Исламов, Никакие права не защищены",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }
        private void PrintClick(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintPageHandler;
            printDialog.AllowSomePages = true;
            printDialog.ShowHelp = true;
            printDialog.Document = printDocument;

            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Rtf, richTextBox1.Font, Brushes.Black, 10, 10);
        }
        private void FontColorClick(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            DialogResult result = cd.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (!richTextBox1.SelectedText.Equals(""))
                {
                    richTextBox1.SelectionColor = cd.Color;
                    isSaved = false;
                }
                else
                {
                    richTextBox1.SelectionColor = cd.Color;
                    isSaved = false;
                }
            }
        }
        private void FontSettingClick(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            DialogResult result = fontDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (!richTextBox1.SelectedText.Equals(""))
                {
                    richTextBox1.SelectionFont = fontDialog.Font;
                    isSaved = false;
                }
                else
                {
                    richTextBox1.SelectionFont = fontDialog.Font;
                    isSaved = false;
                }
            }
        }
    }
}