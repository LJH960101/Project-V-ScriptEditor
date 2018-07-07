using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.IO;

namespace ScriptEditor
{
    public partial class Form1 : Form
    {
        float autoDelayValue;
        public Form1()
        {
            FormClosing += new FormClosingEventHandler(Form1_Close);
            InitializeComponent();
            Application.EnableVisualStyles();
            richTextBox1.ShortcutsEnabled = true;
            richTextBox1.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
        }
        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 32)
            {
                AddLineNumbers();
            }
        }
        private void AddNewStringOnBeforeLine(string script)
        {
            int currentPosition = richTextBox1.GetFirstCharIndexOfCurrentLine();
            if (richTextBox1.Text.Length > 0 && currentPosition < richTextBox1.Text.Length && richTextBox1.Text[currentPosition] != '\n')
            {
                bool chk = false;
                for (int i = currentPosition; i > 0; --i)
                {
                    if (richTextBox1.Text[i] == '\n')
                    {
                        currentPosition = i;
                        SetrichTextBox1(currentPosition, "\n" + script);
                        chk = true;
                        return;
                    }
                }
                if (!chk)
                {
                    currentPosition = 0;
                    SetrichTextBox1(currentPosition, script + "\n");
                }
            }
            else if (currentPosition >= richTextBox1.Text.Length || richTextBox1.Text.Length == 0)
            {
                SetrichTextBox1(currentPosition, "\n" + script + "\n");
            }
            else if (richTextBox1.Text[currentPosition] == '\n')
            {
                SetrichTextBox1(currentPosition, script + "\n");
            }
        }
        private void AddNewStringOnCurrentLine(string script)
        {
            int currentPosition = richTextBox1.GetFirstCharIndexOfCurrentLine();
            if (richTextBox1.Text.Length > 0 && currentPosition < richTextBox1.Text.Length && richTextBox1.Text[currentPosition] != '\n')
            {
                bool chk = false;
                for (int i = currentPosition; i < richTextBox1.Text.Length; ++i)
                {
                    if (richTextBox1.Text[i] == '\n')
                    {
                        currentPosition = i;
                        SetrichTextBox1(currentPosition, "\n" + script + "\n");
                        chk = true;
                        return;
                    }
                }
                if (!chk)
                {
                    currentPosition = richTextBox1.Text.Length;
                    SetrichTextBox1(currentPosition, "\n" + script + "\n");
                }
            }
            else if (currentPosition >= richTextBox1.Text.Length || richTextBox1.Text.Length == 0) {
                SetrichTextBox1(currentPosition, "\n" + script + "\n");
            }
            else if (richTextBox1.Text[currentPosition] == '\n') {
                SetrichTextBox1(currentPosition, script + "\n");
                
            }
        }
        void SetrichTextBox1(int pos, string str)
        {
            richTextBox1.Text = richTextBox1.Text.Insert(pos, str);
            richTextBox1.Focus();

            int i = pos + str.Length-1;
            for (; i>=0; i--)
            {
                if (richTextBox1.Text.Length >= i || richTextBox1.Text[i] == '\n') break;
            }
            richTextBox1.Select(i+1, 0);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                autoDelayValue = float.Parse(textBox1.Text);
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[PC]");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[NPC]");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[L]");
        }
        private void button6_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[Ph 코드]");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[Pr]");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[Ad]");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[SS 안녕?$뭐해?$심심해!]\n[S 안녕?]\n[S 뭐해?]\n[S 심심해!]\n[SE]");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[SS 선택지$선택지$선택지]");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[S 선택지]");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[SE]");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[CN 이름]");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[CP 코드]");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AddNewStringOnBeforeLine("[W 초]");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int count = 0;
            int i = richTextBox1.GetFirstCharIndexOfCurrentLine();
            for (; i < richTextBox1.Text.Length; i++)
                if (richTextBox1.Text[i] == '\n') break;

            if (i >= richTextBox1.Text.Length) i = richTextBox1.Text.Length - 1;
            for (; i >= 0; --i)
            {
                ++count;
                if (richTextBox1.Text[i] == '[') count = 0;
                else if (richTextBox1.Text[i] == '\n' && count >= 2) break;
                else if (richTextBox1.Text[i] == '\n' && count < 2) count = 0;
            }
            AddNewStringOnBeforeLine("[W " + (count * autoDelayValue) + "]");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; ++i)
            {
                richTextBox1.Text = richTextBox1.Text.Replace("\r\n\r\n", "\r\n");
                richTextBox1.Text = richTextBox1.Text.Replace("\n\n", "\n");
            }
            Clipboard.SetData(DataFormats.Text, (Object)richTextBox1.Text.Trim('\r'));
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "All files (*.*)|*.*|txt files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());
                if (writer != null)
                {
                    writer.Write(richTextBox1.Text);
                    writer.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("backup.txt")) return;
            StreamReader sr = new StreamReader("backup.txt");
            richTextBox1.Text = sr.ReadToEnd();
            sr.Close();
        }
        private void Form1_Close(object sender, EventArgs e)
        {
            StreamWriter sr = new StreamWriter("backup.txt");
            sr.Write(richTextBox1.Text);
            sr.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[CS 이름]");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[GL 라인번호]");
        }

        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1
            int line = richTextBox1.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)richTextBox1.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)richTextBox1.Font.Size;
            }
            else
            {
                w = 50 + (int)richTextBox1.Font.Size;
            }

            return w;
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //AddLineNumbers();
        }
        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1
            int First_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int First_Line = richTextBox1.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1
            int Last_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int Last_Line = richTextBox1.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value
            richTextBox2.Text = "";
            richTextBox2.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                richTextBox2.Text += i + 1 + "\r\n";
            }
        }
        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            AddLineNumbers();
            richTextBox2.Invalidate();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[L 호감도]");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[TO 트리거번호]");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[TX 트리거번호]");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[WT 트리거번호]");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[COMMENT 0$댓글]");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[IF 캐릭터코드$레벨$우선순위$호감도]");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[COMMENT 캐릭터코드$댓글]");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[COMMENT @아이디$댓글]");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[LIKE 갯수]");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[NPC]\n[GAME_NPC]\n내 도전을 받아라!!\n[GAME_START]\n[GAME_HIGH]\n[PC]\n[GAME_PC]\n이겼지롱~~\n[GAME_SAME]\n[PC]\n[GAME_PC]\n똑같네 ㅋㅋㅋ\n[GAME_LOW]\n[PC]\n[GAME_PC]\nㅠㅜ 잘하네..\n[GAME_IGNORE]\n[PC]\n미안 지금 좀 바빠서 ㅠㅜ\n[GAME_END]\n[NPC]\n크아앙!!");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("$NAME");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            AddNewStringOnCurrentLine("[ENDING]");
        }
    }
}