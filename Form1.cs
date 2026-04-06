using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Drawing;
namespace CALCULATOR
{
    public partial class Form1 : Form
    {
        private double firstNumber = 0;
        private double secondNumber = 0;
        private string operation = "";
        private bool isNewInput = true;
        private Label displayLabel;

        public Form1()
        {  
            InitializeComponent();
            this.Text = "Calculator";
            this.Size = new Size(320, 480);
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            BuildUI();
        }

        private void BuildUI()
        {
            displayLabel = new Label();
            displayLabel.Text = "0";
            displayLabel.Font = new Font("Segoe UI", 28, FontStyle.Regular);
            displayLabel.ForeColor = Color.White;
            displayLabel.BackColor = Color.FromArgb(20, 20, 20);
            displayLabel.TextAlign = ContentAlignment.MiddleRight;
            displayLabel.Size = new Size(288, 70);
            displayLabel.Location = new Point(10, 10);
            displayLabel.Padding = new Padding(0, 0, 10, 0);
            this.Controls.Add(displayLabel);

            string[] labels = { "C","±","%","÷","7","8","9","×","4","5","6","-","1","2","3","+","0",".","=" };
            int[]    cols   = {  0,  1,  2,  3,  0,  1,  2,  3,  0,  1,  2,  3,  0,  1,  2,  3,  0,  2,  3 };
            int[]    rows   = {  0,  0,  0,  0,  1,  1,  1,  1,  2,  2,  2,  2,  3,  3,  3,  3,  4,  4,  4 };
            Color[] colors = {
    Color.FromArgb(220,70,70),  // C
    Color.FromArgb(70,70,70),
    Color.FromArgb(70,70,70),
    Color.FromArgb(0,120,215),  // ÷

    Color.FromArgb(50,50,50),
    Color.FromArgb(50,50,50),
    Color.FromArgb(50,50,50),
    Color.FromArgb(0,120,215),  // ×

    Color.FromArgb(50,50,50),
    Color.FromArgb(50,50,50),
    Color.FromArgb(50,50,50),
    Color.FromArgb(0,120,215),  // -

    Color.FromArgb(50,50,50),
    Color.FromArgb(50,50,50),
    Color.FromArgb(50,50,50),
    Color.FromArgb(0,120,215),  // +

    Color.FromArgb(50,50,50),
    Color.FromArgb(50,50,50),
    Color.FromArgb(0,120,215)   // =
};
            int btnW = 66, btnH = 66, margin = 8, startX = 10, startY = 92;

            for (int i = 0; i < labels.Length; i++)
            {
                int x = startX + cols[i] * (btnW + margin);
                int y = startY + rows[i] * (btnH + margin);
                int w = (labels[i] == "0") ? btnW * 2 + margin : btnW;

                Button btn = new Button();
                btn.Text = labels[i];
                btn.Size = new Size(w, btnH);
                btn.Location = new Point(x, y);
                btn.BackColor = colors[i];
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 16, FontStyle.Regular);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Cursor = Cursors.Hand;
                btn.Click += new EventHandler(Button_Click);
                this.Controls.Add(btn);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string val = btn.Text;

            if (val == "C")  { Clear(); return; }
            if (val == "±")  { ToggleSign(); return; }
            if (val == "%")  { Percent(); return; }
            if (val == "=")  { Calculate(); return; }
            if (val == "+" || val == "-" || val == "×" || val == "÷") { SetOperation(val); return; }
            if (val == ".")  { AddDecimal(); return; }

            if (isNewInput)
            {
                displayLabel.Text = val;
                isNewInput = false;
            }
            else
            {
                displayLabel.Text = (displayLabel.Text == "0") ? val : displayLabel.Text + val;
            }
        }

        private void SetOperation(string op)
        {
            firstNumber = double.Parse(displayLabel.Text);
            operation = op;
            isNewInput = true;
        }

        private void Calculate()
        {
            if (operation == "") return;
            secondNumber = double.Parse(displayLabel.Text);
            double result = 0;

            if (operation == "+") result = firstNumber + secondNumber;
            else if (operation == "-") result = firstNumber - secondNumber;
            else if (operation == "×") result = firstNumber * secondNumber;
            else if (operation == "÷") result = (secondNumber != 0) ? firstNumber / secondNumber : double.NaN;

            displayLabel.Text = double.IsNaN(result) ? "Error" : FormatResult(result);
            operation = "";
            isNewInput = true;
        }

        private void Clear()
        {
            displayLabel.Text = "0";
            firstNumber = 0;
            secondNumber = 0;
            operation = "";
            isNewInput = true;
        }

        private void ToggleSign()
        {
            double val;
            if (double.TryParse(displayLabel.Text, out val))
                displayLabel.Text = FormatResult(val * -1);
        }

        private void Percent()
        {
            double val;
            if (double.TryParse(displayLabel.Text, out val))
                displayLabel.Text = FormatResult(val / 100);
        }

        private void AddDecimal()
        {
            if (isNewInput) { displayLabel.Text = "0."; isNewInput = false; return; }
            if (!displayLabel.Text.Contains('.'))
                displayLabel.Text += ".";
        }

        private string FormatResult(double value)
        {
            if (value == Math.Floor(value) && !double.IsInfinity(value))
                return ((long)value).ToString();
            return value.ToString("G10");
        }
    }
}