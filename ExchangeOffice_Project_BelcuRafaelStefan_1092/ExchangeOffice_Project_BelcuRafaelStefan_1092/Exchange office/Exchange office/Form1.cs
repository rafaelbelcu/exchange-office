using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exchange_office
{
    public partial class Form1 : Form
    {
        List<Transaction> transactions;
        public Form1()
        {
            InitializeComponent();

            transactions = new List<Transaction>();
        }

        private void DisplayTransactions()
        {

            lvTransactions.Items.Clear();

            foreach (Transaction t in transactions)
            {
                var lvi = new ListViewItem(t.TransactionType);
                lvi.SubItems.Add(t.TransactionDate.ToShortDateString());
                lvi.SubItems.Add(t.Euro.ToString());
                lvi.SubItems.Add(t.Leu.ToString());
                lvi.SubItems.Add(t.Dollar.ToString());
                lvi.SubItems.Add(t.FromCurrency);
                lvi.SubItems.Add(t.ToCurrency);
                lvi.SubItems.Add(t.Amount.ToString());

                lvi.Tag = t;
                lvTransactions.Items.Add(lvi);
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSerializeBinary_Click(object sender, EventArgs e)
        {
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    using (FileStream stream = File.Create("serialized.bin"))
                        formatter.Serialize(stream, transactions);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnDeserializeBinary_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead("serialized.bin"))
            {
                transactions = (List<Transaction>)formatter.Deserialize(stream);
                DisplayTransactions();
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save as CSV file";
            dialog.Filter = "CSV File | *.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(dialog.FileName))
                {
                    writer.WriteLine("TransactionType,TransactionAmount ,TransactionDate, Euro, Leu, Dollar, FromCurrency, ToCurrency, Amount");
                    foreach (Transaction t in transactions)
                        writer.WriteLine($"{t.TransactionType},{t.TransactionDate}," +
                            $"{t.Euro},{t.Leu},{t.Dollar},{t.FromCurrency},{t.ToCurrency},{t.Amount}");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvTransactions.SelectedItems.Count == 0)
            {
                MessageBox.Show("Choose a transaction!");
                return;
            }

            if (MessageBox.Show(
                "Are you sure?",
                "Delete Transaction",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ListViewItem lvi = lvTransactions.SelectedItems[0];
                Transaction t = (Transaction)lvi.Tag;
                transactions.Remove(t);
                DisplayTransactions();
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvTransactions.SelectedItems.Count == 0)
            {
                MessageBox.Show("Choose one transaction!");
                return;
            }

            ListViewItem lvi = lvTransactions.SelectedItems[0];
            Transaction t = (Transaction)lvi.Tag;

            EditForm editForm = new EditForm(t);
            if (editForm.ShowDialog() == DialogResult.OK)
                DisplayTransactions();
        }

        private void btnAddTransaction_Click(object sender, EventArgs e)
        {
            string transactionType = tbTransactionType.Text.Trim();
            DateTime transactionDate = dtpTransactionDate.Value;
            int euro = int.Parse(tbEuro.Text);
            int leu = int.Parse(tbLeu.Text);
            int dollar = int.Parse(tbDollar.Text);
            string fromCurrency = tbFromCurrency.Text.Trim();
            string toCurrency = tbToCurrency.Text;
            int amount = int.Parse(tbAmount.Text);


            var t = new Transaction(transactionType, transactionDate, euro, leu, dollar, fromCurrency, toCurrency, amount);
            transactions.Add(t);

            DisplayTransactions();


        }

        int currentTransactionIndex = 0;

        public int CtrlKey { get; private set; }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("Microsoft Sans Serif", 15);

            StringFormat format = new StringFormat(StringFormatFlags.LineLimit);
            format.Trimming = StringTrimming.EllipsisCharacter;

            var pageSettings = e.PageSettings;
            var intPrintAreaHeight = pageSettings.PaperSize.Height - pageSettings.Margins.Top - pageSettings.Margins.Bottom;
            var intPrintAreaWidth = pageSettings.PaperSize.Width - pageSettings.Margins.Left - pageSettings.Margins.Right;

            var marginLeft = pageSettings.Margins.Left;
            var marginTop = pageSettings.Margins.Top;

            int rowHeight = 40;
            var columnWidth = intPrintAreaWidth / 3;

            var currentY = marginTop;
            while (currentTransactionIndex < transactions.Count)
            {
                var currentX = marginLeft;


                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);

                e.Graphics.DrawString(
                    transactions[currentTransactionIndex].TransactionType,
                    font,
                    Brushes.DarkGray,
                    new RectangleF(currentX, currentY, columnWidth, rowHeight),
                    format
                    );


                currentX += columnWidth;
                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);


                e.Graphics.DrawString(
                    transactions[currentTransactionIndex].TransactionDate.ToShortDateString(),
                    font,
                    Brushes.DarkGray,
                    new RectangleF(currentX, currentY, columnWidth, rowHeight),
                    format
                    );


                currentX += columnWidth;
                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);

                e.Graphics.DrawString(
                    transactions[currentTransactionIndex].Euro.ToString(),
                    font,
                    Brushes.DarkGray,
                    new RectangleF(currentX, currentY, columnWidth, rowHeight),
                    format
                    );

                currentX += columnWidth;
                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);

                e.Graphics.DrawString(
                    transactions[currentTransactionIndex].Leu.ToString(),
                    font,
                    Brushes.DarkGray,
                    new RectangleF(currentX, currentY, columnWidth, rowHeight),
                    format
                    );

                currentX += columnWidth;
                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);

                e.Graphics.DrawString(
                    transactions[currentTransactionIndex].Dollar.ToString(),
                    font,
                    Brushes.DarkGray,
                    new RectangleF(currentX, currentY, columnWidth, rowHeight),
                    format
                    );

                currentX += columnWidth;
                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);

                e.Graphics.DrawString(
                    transactions[currentTransactionIndex].FromCurrency,
                    font,
                    Brushes.DarkGray,
                    new RectangleF(currentX, currentY, columnWidth, rowHeight),
                    format
                    );

                currentX += columnWidth;
                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);

                e.Graphics.DrawString(
                    transactions[currentTransactionIndex].ToCurrency,
                    font,
                    Brushes.DarkGray,
                    new RectangleF(currentX, currentY, columnWidth, rowHeight),
                    format
                    );

                currentX += columnWidth;
                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);

                e.Graphics.DrawString(
                    transactions[currentTransactionIndex].Amount.ToString(),
                    font,
                    Brushes.DarkGray,
                    new RectangleF(currentX, currentY, columnWidth, rowHeight),
                    format
                    );


                currentTransactionIndex++;
                currentY += rowHeight;

                if (currentY + rowHeight > intPrintAreaHeight)
                {
                    e.HasMorePages = true;
                    break;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }



        private void lvTransactions_DragEnter(object sender, DragEventArgs e)
        {
            tbTransactionType.Text = "" + e.AllowedEffect +
"\r\n:\r\n";

            foreach (string availableFormat in e.Data.GetFormats(true))
            {
                tbTransactionType.Text += "\t" + availableFormat + "\r\n";
            }

            if (e.Data.GetDataPresent(DataFormats.Text, true))
            {

                if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move && (e.KeyState & CtrlKey) != CtrlKey)
                {

                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        private void lvTransactions_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text, true))
            {
                listBox1.Items.Add(e.Data.GetData(DataFormats.Text));
            }
        }

        private void tbTransactionType_MouseDown(object sender, MouseEventArgs e)
        {
            tbTransactionType.DoDragDrop(tbTransactionType.Text, DragDropEffects.Copy);
        }



    }



}
