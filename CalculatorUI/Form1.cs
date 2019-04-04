using System;
using System.Windows.Forms;

namespace CalculatorUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private double? num1 = null;
        private bool overriteDisplay;
        private double? num2 = null;
        private Calculator.CalculatorFunctions calcs = new Calculator.CalculatorFunctions();
        private string currentOperator;

        private void btnOne_Click(object sender, EventArgs e)
        {
            AddToDisplay(Convert.ToInt32(btnOne.Text));
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            AddToDisplay(Convert.ToInt32(btnTwo.Text));
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            AddToDisplay(Convert.ToInt32(btnThree.Text));
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            AddToDisplay(Convert.ToInt32(btnFour.Text));
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            AddToDisplay(Convert.ToInt32(btnFive.Text));
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            AddToDisplay(Convert.ToInt32(btnSix.Text));
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            AddToDisplay(Convert.ToInt32(btnSeven.Text));
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            AddToDisplay(Convert.ToInt32(btnEight.Text));
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            AddToDisplay(Convert.ToInt32(btnNine.Text));
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            AddToDisplay(Convert.ToInt32(btnZero.Text));
        }

        private void btnDecimal_Click(object sender, EventArgs e)
        {
            AddToDisplay(btnDecimal.Text);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            BackspaceDigit();
        }

        private void BackspaceDigit()
        {
            if (txtDisplay.Text.Length > 0)
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            }
        }

        private void AddToDisplay(dynamic digit)
        {
            if (overriteDisplay)
            {
                txtDisplay.Text = "0";
            }

            txtDisplay.Text = txtDisplay.Text != "0" ?
                string.Concat(txtDisplay.Text, digit.ToString()) :
                digit.ToString();
            overriteDisplay = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetCurrentOperatorStatus("Add");
        }
        private void btnSubtract_Click(object sender, EventArgs e)
        {
            SetCurrentOperatorStatus("Subtract");
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            SetCurrentOperatorStatus("Multiply");
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            SetCurrentOperatorStatus("Division");
        }

        private void btnCarat_Click(object sender, EventArgs e)
        {
            SetCurrentOperatorStatus("Carat", true);
        }

        private void btnPwr_Click(object sender, EventArgs e)
        {
            SetCurrentOperatorStatus("Power");
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            SetCurrentOperatorStatus("SquareRoot", true);
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {
            PreparePercentageCalculation();
        }

        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            SetCurrentOperatorStatus("PlusMinus", true);
        }

        private void PreparePercentageCalculation()
        {
            if (num1 != null && !string.IsNullOrEmpty(currentOperator))
            {
                var temp = Convert.ToDouble(txtDisplay.Text);
                if (currentOperator == "Add" || currentOperator == "Subtract")
                {
                    temp = calcs.CalculatePercentage(temp) * Convert.ToDouble(num1);
                    num2 = temp;
                }
                else
                {   
                    if (currentOperator != "Division")
                    {
                        num2 = temp;
                        temp = calcs.CalculatePercentage(temp);
                        currentOperator = "Percent";
                    }
                    else
                    {
                        temp = calcs.CalculatePercentage(temp);
                        num2 = temp;
                    }
                }
                txtDisplay.Text = temp.ToString();
            }
        }
        private void SetCurrentOperatorStatus(string operatorValue, bool immediateCalc = false)
        {
            if (!immediateCalc)
            {
                if (string.IsNullOrEmpty(currentOperator))
                {
                    AssignNumberValues();
                    currentOperator = operatorValue;
                }
                else
                {
                    num2 = Convert.ToDouble(txtDisplay.Text);
                    CalculateValues(Convert.ToDouble(num1), Convert.ToDouble(num2), currentOperator);
                    ResetDisplay(false);
                    AssignNumberValues();
                    currentOperator = operatorValue;
                }
            }
            else
            {
                if (AssignNumberValues())
                {
                    var temp = num2;
                    CalculateValues(Convert.ToDouble(temp), Convert.ToDouble(num2), operatorValue);
                }
                else
                {
                    CalculateValues(Convert.ToDouble(num1), Convert.ToDouble(num2), operatorValue);
                }
                AssignNumberValues();
            }
        }
        private bool AssignNumberValues()
        {
            var temp = Convert.ToDouble(txtDisplay.Text);
            var isNum2Set = false;
            if (num1 == null || string.IsNullOrEmpty(currentOperator))
            {
                num1 = temp;
                isNum2Set = false;
            }
            else
            {
                num2 = temp;
                isNum2Set = true;
            }
            overriteDisplay = true;
            return isNum2Set;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetDisplay(true);
        }
        private void btnClearErr_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
        }
        private void ResetDisplay(bool setDisplayToZero)
        {
            if (setDisplayToZero)
            {
                txtDisplay.Text = "0";
            }

            num1 = null;
            num2 = null;
            currentOperator = string.Empty;
            overriteDisplay = false;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            num2 = num2 == null ?
            Convert.ToDouble(txtDisplay.Text) : num2;
            CalculateValues(Convert.ToDouble(num1), Convert.ToDouble(num2), currentOperator);
            ResetDisplay(false);
        }

        private void CalculateValues(double number1, double number2, string opName)
        {
            switch (opName)
            {
                case "Add":
                    txtDisplay.Text = calcs.PerformCalculation(calcs.Add, number1, number2).ToString();
                    break;
                case "Subtract":
                    txtDisplay.Text = calcs.PerformCalculation(calcs.Subtract, number1, number2).ToString();
                    break;
                case "Multiply":
                    txtDisplay.Text = calcs.PerformCalculation(calcs.Multiplication, number1, number2).ToString();
                    break;
                case "Division":
                    txtDisplay.Text = calcs.PerformCalculation(calcs.Division, number1, number2).ToString();
                    break;
                case "Carat":
                    txtDisplay.Text = calcs.CaratValue(number1).ToString();
                    break;
                case "PlusMinus":
                    txtDisplay.Text = calcs.PlusMinus(number1).ToString();
                    break;
                case "Power":
                    txtDisplay.Text = calcs.PerformCalculation(calcs.PowerOf, number1, number2).ToString();
                    break;
                case "Percent":
                    txtDisplay.Text = calcs.PerformCalculation(calcs.Percentage, number1, number2).ToString();
                    break;
                case "SquareRoot":
                    txtDisplay.Text = calcs.SquareRoot(number1).ToString();
                    break;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData) 
            {
                case Keys.D0:
                case Keys.NumPad0:
                    btnZero_Click(null, new EventArgs());
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    btnOne_Click(null, new EventArgs());
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    btnTwo_Click(null, new EventArgs());
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    btnThree_Click(null, new EventArgs());
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    btnFour_Click(null, new EventArgs());
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    btnFive_Click(null, new EventArgs());
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    btnSix_Click(null, new EventArgs());
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    btnSeven_Click(null, new EventArgs());
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    btnEight_Click(null, new EventArgs());
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    btnNine_Click(null, new EventArgs());
                    break;
                case Keys.Add:
                    btnAdd_Click(null, new EventArgs());
                    break;
                case Keys.Subtract:
                    btnSubtract_Click(null, new EventArgs());
                    break;
                case Keys.Multiply:
                    btnMultiply_Click(null, new EventArgs());
                    break;
                case Keys.Divide:
                    btnDivide_Click(null, new EventArgs());
                    break;
                case Keys.Back:
                    btnBack_Click(null, new EventArgs());
                    break;
                case Keys.Delete:
                    btnClear_Click(null, new EventArgs());
                    break;
                case Keys.Enter:
                    btnEqual_Click(null, new EventArgs());
                    break;
                case Keys.Decimal:
                    btnDecimal_Click(null, new EventArgs());
                    break;
            }
            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
