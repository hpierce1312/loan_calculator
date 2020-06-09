//BUS 442
//Major Project 1
//Hudson Pierce
//10/24/2019
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Major_Project_1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            for (double i = 0; i <= 10; i += 0.5)
            {
                annualAPRComboBox.Items.Add(i);
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            DialogResult response = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (response == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void clear()
        {
            listBox.Items.Clear();
        }

        private void LoanAmtTextBox_TextChanged(object sender, EventArgs e)
        {
            clear();
        }

        private void AnnualAPRComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear();
        }

        private void RebateTextBox_TextChanged(object sender, EventArgs e)
        {
            clear();
        }

        private void SixRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            clear();
        }

        private void TwelveRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            clear();
        }

        private void EighteenRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            clear();
        }

        private void TwentyFourRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            clear();
        }

        private void LoanAmtTextBox_Click(object sender, EventArgs e)
        {
            loanAmtTextBox.SelectAll();
        }

        private void AnnualAPRComboBox_Click(object sender, EventArgs e)
        {
            annualAPRComboBox.SelectAll();
        }

        private void RebateTextBox_Click(object sender, EventArgs e)
        {
            rebateTextBox.SelectAll();
        }

        private void LoanAmtTextBox_Enter(object sender, EventArgs e)
        {
            loanAmtTextBox.SelectAll();
        }

        private void AnnualAPRComboBox_Enter(object sender, EventArgs e)
        {
            annualAPRComboBox.SelectAll();
        }

        private void RebateTextBox_Enter(object sender, EventArgs e)
        {
            rebateTextBox.SelectAll();
        }

        private void LoanAmtTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
        }

        private void AnnualAPRComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
        }

        private void RebateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }
        }

        double rate = 0;
        double loanBalance = 0;
        double rebate = 0;
        double principal = 0;
        double pmt = 0;
        double interest = 0;
        double rateDec;
        double totalInterest = 0;
        double totalPrincipal = 0;
        double totalOfInterest = 0;
        double totalOfPrincipal = 0;
        int month;
        private void CalcLoanBtn_Click(object sender, EventArgs e)
        {
            try
            {
                listBox.Items.Clear();
                double.TryParse(loanAmtTextBox.Text, out loanBalance);
                rate = Convert.ToDouble(annualAPRComboBox.Text);

                if (rate == 0)
                {
                    MessageBox.Show("Please enter a non-zero value", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listBox.Items.Clear();
                }
                else if (loanBalance == 0)
                {
                    MessageBox.Show("Please enter a non-zero value", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listBox.Items.Clear();
                }

                if (sixRadioBtn.Checked)
                    month = 6;

                else if (twelveRadioBtn.Checked)
                    month = 12;

                else if (eighteenRadioBtn.Checked)
                    month = 18;

                else
                    month = 24;


                if (rebateCheckBox.Checked)
                {
                    double.TryParse(rebateTextBox.Text, out rebate);
                }

                loanBalance = loanBalance - rebate;
                rateDec = rate / 100;

                principal = rateDec / month * (loanBalance + 0 * Math.Pow(1 + rateDec / month, month)) / ((Math.Pow(1 + rateDec / month, month) - 1) * (1 + rateDec / month * 0));
                interest = loanBalance * rateDec / month;
                pmt = principal + interest;

                string formatString = "{0,5}{1,12}{2,12}{3,12}{4,12}";

                double totalInterest = 0;
                double totalPrincipal = 0;
                double totalPmt = 0;
                double totalLoanBalance = 0;

                for (int monthsCounter = 1; monthsCounter <= month; monthsCounter++)
                {
                    interest = loanBalance * rateDec / month;
                    principal = pmt - interest;
                    loanBalance = loanBalance - principal;

                    listBox.Items.Add(string.Format(formatString, monthsCounter, interest.ToString("C2"), principal.ToString("N2"), pmt.ToString("N2"), loanBalance.ToString("N2")));

                    totalInterest += interest;
                    totalPrincipal += principal;
                    totalPmt += pmt;
                    totalLoanBalance += loanBalance;

                }
                listBox.Items.Add("");
                listBox.Items.Add(string.Format("{0,5}{1,12}{2,12}{3,12}", "Total", totalInterest.ToString("C2"), totalPrincipal.ToString("N2"), totalPmt.ToString("N2")));
                totalOfInterest = totalInterest;
                totalOfPrincipal = totalPrincipal;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }
        }

        private void RebateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rebateCheckBox.Checked)
                rebateTextBox.ReadOnly = false;
            else
                rebateTextBox.ReadOnly = true;
                rebateTextBox.Clear();
        }

        private void AcceptLoanBtn_Click(object sender, EventArgs e)
        {
            totalInterest += totalOfInterest;
            totalPrincipal += totalOfPrincipal;

            listBox.Items.Clear();
            loanAmtTextBox.Clear();
            annualAPRComboBox.SelectedIndex = 0;
            rebateTextBox.Clear();
            rebateCheckBox.Checked = false;
            sixRadioBtn.Checked = true;
            rebateTextBox.ReadOnly = true;
            loanAmtTextBox.Focus();
        }

        private void DisplayLoansBtn_Click(object sender, EventArgs e)
        {
            totalLoansLabel.Text = totalPrincipal.ToString("C2");
            totalInterestLabel.Text = totalInterest.ToString("C2");
        }

        private void AnnualAPRComboBox_TextChanged(object sender, EventArgs e)
        {
            clear();
        }
    }
}


