using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace translator
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> dictionaryLookup;
        string from,to;
        ListViewItemComparerDesc[] descSorters = new ListViewItemComparerDesc[2];
        ListViewItemComparerAsc[] ascSorters = new ListViewItemComparerAsc[2];
        int sorted;
        string rmbWord;
        FontStyle fontStyleAbove;
        

        public Form1()
        {
            //init
            dictionaryLookup = new Dictionary<string, string>(new DictionaryComparer());
            from = to = null;
            rmbWord = null;
            fontStyleAbove = FontStyle.Regular;
            for (int i = 0; i < 2; i++)
            {
                descSorters[i] = new ListViewItemComparerDesc(i);
                ascSorters[i] = new ListViewItemComparerAsc(i);
            }
            sorted = -1;

          

            InitializeComponent();

            //add fonts in toolbar
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                toolStripComboBox1.Items.Add(font.Name);
            }
            toolStripComboBox1.Text = "Calibri";

            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer1.ForeColor = Color.Gray;
            this.MinimumSize = new Size(500, 400);
            // Set to details view.
            TranslationListView.View = View.Details;
            //TranslationList.Sorting = SortOrder.Ascending;
            
        }

        
        private class ListViewItemComparer : System.Collections.IComparer
        {
            public int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            virtual public int Compare(object x, object y)
            {
                int returnVal = -1;
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                ((ListViewItem)y).SubItems[col].Text);
                return returnVal;
            }
        }

        private class ListViewItemComparerAsc : ListViewItemComparer
        {
            public ListViewItemComparerAsc() : base()
            {

            }
            public ListViewItemComparerAsc(int column) : base(column)
            {

            }
        }

        private class ListViewItemComparerDesc : ListViewItemComparer
        {
            public ListViewItemComparerDesc() : base()
            {

            }
            public ListViewItemComparerDesc(int column) : base(column)
            {

            }

            public override int Compare(object x, object y)
            {
                return -base.Compare(x, y);
            }
        }



        private class DictionaryComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return string.Compare(x, y, true)==0?true:false;
            }

            public int GetHashCode(string obj)
            {
                return base.GetHashCode();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog()== DialogResult.OK)
            {
                List<string> lines = new List<string>
                {
                    from+" "+to
                };

                foreach(var key in dictionaryLookup)
                {
                    lines.Add(key.Key + " " + key.Value);
                }

                File.WriteAllLines(saveFileDialog.FileName, lines.ToArray());
            }
            
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //open .txt file
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ReadAndAddWords(fileDialog.FileName);
            }
        }



        private void TranslationListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (sorted == e.Column)
            {
                TranslationListView.ListViewItemSorter = descSorters[e.Column];
                sorted = -1;
            }
            else
            {
                TranslationListView.ListViewItemSorter = ascSorters[e.Column];
                sorted = e.Column;
            }
        }

        private void TranslationListView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                foreach(ListViewItem selectedItem in TranslationListView.SelectedItems)
                {
                    TranslationListView.Items.Remove(selectedItem);
                    dictionaryLookup.Remove(selectedItem.Text);
                }
            }
        }

        private void TranslateButton_Click(object sender, EventArgs e)
        {
            richTextBoxBelow.Clear();
            string[] words = System.Text.RegularExpressions.Regex.Split(richTextBoxAbove.Text, @"([\s])");
            for (int i = 0; i < words.Length; i++)
            {
                if (dictionaryLookup.TryGetValue(words[i], out string value))
                {
                    richTextBoxBelow.AppendText(value);
                }
                else
                {
                    richTextBoxBelow.SelectionColor = Color.Red;
                    richTextBoxBelow.AppendText(words[i]);
                    richTextBoxBelow.SelectionColor = richTextBoxAbove.ForeColor;
                }
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            TranslationListView.Columns[0].Width = (e.SplitX-15) / 2;
            TranslationListView.Columns[1].Width = (e.SplitX - 15) / 2;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddWordWindow();
        }


        private void richTextBoxBelow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (richTextBoxBelow.Text != string.Empty)
                {
                    Control ControlSender = (Control)sender;
                    int index = richTextBoxBelow.GetCharIndexFromPosition(new Point(e.X,e.Y));
                    int beg = index;
                    int end = index;

                    //ignore white char
                    if (char.IsWhiteSpace(richTextBoxBelow.Text[index]))
                    {
                        rmbWord = null;
                        return;
                    }

                    //get whole word
                    while (beg > 0)
                    {
                        if (!char.IsWhiteSpace(richTextBoxBelow.Text[beg - 1]))
                            beg--;
                        else
                            break;
                    }
                    while (end < richTextBoxBelow.Text.Length - 1)
                    {
                        if (!char.IsWhiteSpace(richTextBoxBelow.Text[end + 1]))
                            end++;
                        else
                            break;
                    }

                    rmbWord = richTextBoxBelow.Text.Substring(beg, end - beg + 1);
                    if (dictionaryLookup.ContainsKey(rmbWord) || dictionaryLookup.ContainsValue(rmbWord))
                    {
                        rmbWord = null;
                        return;
                    }
                    contextMenuStrip1.Items[0].Text = "Add " + rmbWord;
                }
            }
        }

        private void richTextBoxBelow_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                //if Text == string.Empty , then white char was closest to cursor
                if (rmbWord!=null)
                {
                    Control ControlSender = (Control)sender;
                    contextMenuStrip1.Show(ControlSender.PointToScreen(e.Location));
                }
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(rmbWord!=null)
                AddWordWindow(rmbWord);
        }

        private void TranslationListView_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var file in files)
            {
                if (System.IO.Path.GetExtension(file).Equals(".txt", StringComparison.InvariantCultureIgnoreCase))
                {
                    ReadAndAddWords(file);
                }
            }

        }

        private void ReadAndAddWords(string file)
        {
            //clear existing dictionaries
            TranslationListView.Items.Clear();
            dictionaryLookup.Clear();
            string[] dictionaryText = File.ReadAllLines(file);
            sorted = -1;


            //load translated languages names
            string[] words = dictionaryText[0].Split();
            leftcolumn.Text = from = words[0];
            rightcolumn.Text = to = words[1];


            //process following lines of dictionaryText
            List<ListViewItem> items = new List<ListViewItem>();
            for (int i = 1; i < dictionaryText.Length; i++)
            {
                words = dictionaryText[i].Split();
                if (words.Length == 2)
                {
                    if (words[0].All(Char.IsLetter) && words[1].All(Char.IsLetter) && !dictionaryLookup.ContainsKey(words[0]))
                    {
                        //add items to ListView and dictionary
                        var item = new ListViewItem(words[0]);
                        item.SubItems.Add(words[1]);
                        items.Add(item);
                        dictionaryLookup.Add(words[0], words[1]);
                    }
                }
            }

            //fill translation list
            TranslationListView.Items.AddRange(items.ToArray());
        }

        private void TranslationListView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBoxAbove.BackColor = colorDialog1.Color;
            }
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripButton1.CheckState == CheckState.Checked)
            {
                toolStripButton1.CheckState = CheckState.Unchecked;
                fontStyleAbove = ~FontStyle.Bold & fontStyleAbove;
                int selectionStart = richTextBoxAbove.SelectionStart;
                richTextBoxAbove.SelectAll();
                richTextBoxAbove.SelectionFont = new Font(richTextBoxAbove.Font, fontStyleAbove);
                richTextBoxAbove.DeselectAll();
                richTextBoxAbove.SelectionStart = selectionStart;
                richTextBoxAbove.SelectionLength = 0;
            }
            else
            {
                toolStripButton1.CheckState = CheckState.Checked;
                fontStyleAbove |= FontStyle.Bold;
                int selectionStart = richTextBoxAbove.SelectionStart;
                richTextBoxAbove.SelectAll();
                richTextBoxAbove.SelectionFont = new Font(richTextBoxAbove.Font, fontStyleAbove);
                richTextBoxAbove.DeselectAll();
                richTextBoxAbove.SelectionStart = selectionStart;
                richTextBoxAbove.SelectionLength = 0;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(toolStripButton2.CheckState == CheckState.Checked)
            {
                toolStripButton2.CheckState = CheckState.Unchecked;
                fontStyleAbove = ~FontStyle.Italic & fontStyleAbove;
                int selectionStart = richTextBoxAbove.SelectionStart;
                richTextBoxAbove.SelectAll();
                richTextBoxAbove.SelectionFont = new Font(richTextBoxAbove.Font, fontStyleAbove);
                richTextBoxAbove.DeselectAll();
                richTextBoxAbove.SelectionStart = selectionStart;
                richTextBoxAbove.SelectionLength = 0;
            }
            else
            {
                toolStripButton2.CheckState = CheckState.Checked;
                fontStyleAbove |= FontStyle.Italic;
                int selectionStart = richTextBoxAbove.SelectionStart;
                richTextBoxAbove.SelectAll();
                richTextBoxAbove.SelectionFont = new Font(richTextBoxAbove.Font, fontStyleAbove);
                richTextBoxAbove.DeselectAll();
                richTextBoxAbove.SelectionStart = selectionStart;
                richTextBoxAbove.SelectionLength = 0;
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (toolStripButton3.CheckState == CheckState.Checked)
            {
                toolStripButton3.CheckState = CheckState.Unchecked;
                fontStyleAbove = ~FontStyle.Underline & fontStyleAbove;
                int selectionStart = richTextBoxAbove.SelectionStart;
                richTextBoxAbove.SelectAll();
                richTextBoxAbove.SelectionFont = new Font(richTextBoxAbove.Font, fontStyleAbove);
                richTextBoxAbove.DeselectAll();
                richTextBoxAbove.SelectionStart = selectionStart;
                richTextBoxAbove.SelectionLength = 0;
            }
            else
            {
                toolStripButton3.CheckState = CheckState.Checked;
                fontStyleAbove |= FontStyle.Underline;
                int selectionStart = richTextBoxAbove.SelectionStart;
                richTextBoxAbove.SelectAll();
                richTextBoxAbove.SelectionFont = new Font(richTextBoxAbove.Font, fontStyleAbove);
                richTextBoxAbove.DeselectAll();
                richTextBoxAbove.SelectionStart = selectionStart;
                richTextBoxAbove.SelectionLength = 0;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBoxAbove.ForeColor = colorDialog1.Color;
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            var font= new Font(toolStripComboBox1.Text, 12, fontStyleAbove);
            richTextBoxAbove.Font = font;
          
        }

        private void AddWordWindow(string value=null)
        {
            AddWord addWord = new AddWord(); ;
            if (from != null)
            {
                addWord.Label1Add = from;
                addWord.Label2Add = to;
            }

            if (value != null)
            {
                addWord.TextBox1 = value;
                addWord.DisableTextBox1();
            }

            switch (addWord.ShowDialog())
            {
                case DialogResult.OK:
                    if (addWord.TextBox1 != "" && addWord.TextBox2 != "")
                    {
                        if (!dictionaryLookup.ContainsKey(addWord.TextBox1))
                        {
                            dictionaryLookup.Add(addWord.TextBox1, addWord.TextBox2);
                            TranslationListView.Items.Add(new ListViewItem(new string[] { addWord.TextBox1, addWord.TextBox2 }));

                        }
                    }
                    break;
                case DialogResult.Cancel:
                    break;
                default:
                    break;
            }
            addWord.Close();
        }
    }
}





//StringBuilder textBelow = new StringBuilder(richTextBoxAbove.Text);
//int beg = 0;
//char[] word = new char[100];
//for (int i = 0; i < textBelow.Length; i++)
//{
//    if (char.IsWhiteSpace(textBelow[beg]))
//    {
//        beg++;
//        richTextBoxBelow.AppendText(textBelow[beg].ToString());
//    }

//    if(char.IsWhiteSpace(textBelow[i]))
//    {
//        if (beg < i)
//        {
//            if (word.Length < i - beg)
//                word = new char[i - beg];
//            textBelow.CopyTo(beg, word, 0, i - beg);
//            if(dictionaryLookup.TryGetValue(word.ToString(),out string value))
//            {
//                //translate
//                textBelow.Replace(word.ToString(), value, beg, i - beg);

//                beg += value.Length;
//                i = beg;
//            }
//            else
//            {
//                //paint red


//                beg = i;
//            }
//        }
//    }
//}