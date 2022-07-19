using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exchange_office
{
    public partial class EditForm : Form
    {
        private Transaction _transaction;

        public EditForm(Transaction t)
        {
            InitializeComponent();
            _transaction = t;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            tbTransactionType.Text = _transaction.TransactionType;
            dtpTransactionDate.Value = _transaction.TransactionDate;
            tbEuro.Text = _transaction.Euro.ToString();
            tbLeu.Text = _transaction.Leu.ToString();
            tbDollar.Text = _transaction.Dollar.ToString();
            tbFromCurrency.Text = _transaction.FromCurrency;
            tbToCurrency.Text = _transaction.ToCurrency;
            tbAmount.Text = _transaction.Amount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _transaction.TransactionType = tbTransactionType.Text.Trim();
            _transaction.TransactionDate = dtpTransactionDate.Value;
            _transaction.Euro = Int32.Parse(tbEuro.Text);
            _transaction.Leu = Int32.Parse(tbLeu.Text);
            _transaction.Dollar = Int32.Parse(tbDollar.Text);
            _transaction.FromCurrency = tbFromCurrency.Text.Trim();
            _transaction.ToCurrency = tbToCurrency.Text.Trim();
            _transaction.Amount = Int32.Parse(tbAmount.Text);
        }



    }
}
