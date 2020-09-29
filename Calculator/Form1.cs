using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace Calculator
{
    public partial class Form1 : Form
    {
        public double result;
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string getNumber = numberTextBox.Text;
            result = getResults(getNumber);
            resultLabel.Text = result.ToString();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (resultLabel.Text == "")
            {
                MessageBox.Show("Nothing to copy :(");
            }
            else
            {
                Clipboard.SetText(resultLabel.Text);
                MessageBox.Show("Result coped be happy use result with Ctrl+V");
            }
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            resultLabel.Text = "";
            numberTextBox.Text = "";
            numberTextBox.Focus();
        }
        private double getResults(string mathOperation)
        {

            var numberStack = new List<double>();
            var operationStackAndBrace = new List<string>();
            Dictionary<string, int> priorityOfMathematicalOperations = new Dictionary<string, int>()
            {
              {"+", 1}, {"-", 1}, {"*", 2}, {"/", 2}, {"^",3}, {"(", 0 }
            };
            string[] splitedMath = divideIntoParts(mathOperation);
            int numberOfElements = operationStackAndBrace.Count;
            int numberOfNumbers = numberStack.Count;
            bool changeSign;
            foreach (string operation in splitedMath)
            {
                numberOfElements = operationStackAndBrace.Count;
                numberOfNumbers = numberStack.Count;

                if (double.TryParse(operation, out double number))
                {
                    numberStack.Add(number);
                }
                else
                {
                    if (operation == "+" || operation == "-" || operation == "*" || operation == "/" ||
                       operation == "^" || operation == "(")
                    {
                        if (operation == "(" || numberOfElements == 0)
                        {
                            operationStackAndBrace.Add(operation);
                        }
                        else
                        {
                            if (priorityOfMathematicalOperations[operation] >=
                                priorityOfMathematicalOperations[operationStackAndBrace[numberOfElements - 1]])
                            {
                                operationStackAndBrace.Add(operation);
                            }
                            else if (numberOfNumbers > 1)
                            {
                                double num1 = numberStack[numberOfNumbers - 2], num2 = numberStack[numberOfNumbers - 1];
                                if (operationStackAndBrace[numberOfElements - 1] == "+")
                                {
                                    numberStack.RemoveRange(numberOfNumbers - 2, 2);
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    operationStackAndBrace.Add(operation);
                                    numberStack.Add(num1 + num2);
                                }
                                else if (operationStackAndBrace[numberOfElements - 1] == "-")
                                {
                                    numberStack.RemoveRange(numberOfNumbers - 2, 2);
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    operationStackAndBrace.Add(operation);
                                    numberStack.Add(num1 - num2);
                                }
                                else if (operationStackAndBrace[numberOfElements - 1] == "*")
                                {
                                    numberStack.RemoveRange(numberOfNumbers - 2, 2);
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    operationStackAndBrace.Add(operation);
                                    numberStack.Add(num1 * num2);
                                }
                                else if (operationStackAndBrace[numberOfElements - 1] == "/")
                                {
                                    if (num2 == 0)
                                    {
                                        MessageBox.Show("Cannot divide zero");
                                        return -1;
                                    }
                                    numberStack.RemoveRange(numberOfNumbers - 2, 2);
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    operationStackAndBrace.Add(operation);
                                    numberStack.Add(num1 / num2);
                                }
                                else if (operationStackAndBrace[numberOfElements - 1] == "^")
                                {
                                    numberStack.RemoveRange(numberOfNumbers - 2, 2);
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    operationStackAndBrace.Add(operation);
                                    numberStack.Add(Math.Pow(num1, num2));
                                }

                            }
                            else
                            {
                                MessageBox.Show("Enter Correct value");
                                return -1;
                            }
                        }

                    }
                    else if (operation == ")")
                    {
                        if (numberOfElements > 1 && operationStackAndBrace.Contains("(") && numberOfNumbers >1)
                        {
                            while (numberOfNumbers > 1 && numberOfElements > 1)
                            {
                                numberOfElements = operationStackAndBrace.Count;
                                numberOfNumbers = numberStack.Count;
                                double num1 = numberStack[numberOfNumbers - 2], num2 = numberStack[numberOfNumbers - 1];
                                if (operationStackAndBrace[numberOfElements - 1] == "+")
                                {
                                    numberStack.RemoveRange(numberOfNumbers - 2, 2);
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    numberStack.Add(num1 + num2);
                                    numberOfElements = operationStackAndBrace.Count;
                                    numberOfNumbers = numberStack.Count;
                                }
                                else if (operationStackAndBrace[numberOfElements - 1] == "-")
                                {
                                    numberStack.RemoveRange(numberOfNumbers - 2, 2);
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    numberStack.Add(num1 - num2);
                                    numberOfElements = operationStackAndBrace.Count;
                                    numberOfNumbers = numberStack.Count;
                                }
                                else if (operationStackAndBrace[numberOfElements - 1] == "*")
                                {
                                    numberStack.RemoveRange(numberOfNumbers - 2, 2);
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    numberStack.Add(num1 * num2);
                                    numberOfElements = operationStackAndBrace.Count;
                                    numberOfNumbers = numberStack.Count;
                                }
                                else if (operationStackAndBrace[numberOfElements - 1] == "/")
                                {
                                    if (num2 == 0)
                                    {
                                        MessageBox.Show("Cannot divide zero");
                                        return -1;
                                    }
                                    numberStack.RemoveRange(numberOfNumbers - 2, 2);
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    numberStack.Add(num1 / num2);
                                    numberOfElements = operationStackAndBrace.Count;
                                    numberOfNumbers = numberStack.Count;
                                }
                                else if (operationStackAndBrace[numberOfElements - 1] == "^")
                                {
                                    numberStack.RemoveRange(numberOfNumbers - 2, 2);
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    numberStack.Add(Math.Pow(num1, num2));
                                    numberOfElements = operationStackAndBrace.Count;
                                    numberOfNumbers = numberStack.Count;
                                }
                                if (operationStackAndBrace[numberOfElements - 1] == "(")
                                {
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    numberOfElements = operationStackAndBrace.Count;
                                    numberOfNumbers = numberStack.Count;
                                    if (numberOfElements>1 && numberOfNumbers>0) {
                                        if ((operationStackAndBrace[numberOfElements - 1] == "-") &&
                                            (operationStackAndBrace[numberOfElements - 2] == "("))
                                        {
                                            operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                            numberStack[numberOfNumbers - 1]*=-1 ;
                                            numberOfElements = operationStackAndBrace.Count;
                                        }
                                    }
                                    break;
                                }
                            }

                        }
                        else if (numberOfNumbers>0 && operationStackAndBrace.Contains("("))
                        {
                            if (operationStackAndBrace[numberOfElements - 1] == "(")
                            {
                                operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                numberOfElements = operationStackAndBrace.Count;
                                numberOfNumbers = numberStack.Count;
                                
                            }
                            if (numberOfElements > 1 && numberOfNumbers > 0)
                            {
                                if ((operationStackAndBrace[numberOfElements - 1] == "-") &&
                                    (operationStackAndBrace[numberOfElements - 2] == "("))
                                {
                                    operationStackAndBrace.RemoveAt(numberOfElements - 1);
                                    numberStack[numberOfNumbers - 1] *= -1;
                                    numberOfElements = operationStackAndBrace.Count;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter Correct value");
                            return -1;
                        }
                    }
                }
            }
            changeSign = false;
            numberOfElements = operationStackAndBrace.Count;
            numberOfNumbers = numberStack.Count;
            if((numberOfElements == numberOfNumbers) && (operationStackAndBrace[0] == "-"))
            {
                changeSign = true;
                operationStackAndBrace.RemoveAt(0);
                numberOfElements = operationStackAndBrace.Count;
                numberOfNumbers = numberStack.Count;

            }
            if ((numberOfElements + 1 == numberOfNumbers))
            {
                while (numberOfNumbers != 1)
                {
                    numberOfElements = operationStackAndBrace.Count;
                    numberOfNumbers = numberStack.Count;
                    double num1 = numberStack[numberOfNumbers - 2], num2 = numberStack[numberOfNumbers - 1];
                    if (operationStackAndBrace[numberOfElements - 1] == "+")
                    {
                        numberStack.RemoveRange(numberOfNumbers - 2, 2);
                        operationStackAndBrace.RemoveAt(numberOfElements - 1);
                        numberStack.Add(num1 + num2);
                        numberOfElements = operationStackAndBrace.Count;
                        numberOfNumbers = numberStack.Count;
                    }
                    else if (operationStackAndBrace[numberOfElements - 1] == "-")
                    {
                        numberStack.RemoveRange(numberOfNumbers - 2, 2);
                        operationStackAndBrace.RemoveAt(numberOfElements - 1);
                        numberStack.Add(num1 - num2);
                        numberOfElements = operationStackAndBrace.Count;
                        numberOfNumbers = numberStack.Count;
                    }
                    else if (operationStackAndBrace[numberOfElements - 1] == "*")
                    {
                        numberStack.RemoveRange(numberOfNumbers - 2, 2);
                        operationStackAndBrace.RemoveAt(numberOfElements - 1);
                        numberStack.Add(num1 * num2);
                        numberOfElements = operationStackAndBrace.Count;
                        numberOfNumbers = numberStack.Count;
                    }
                    else if (operationStackAndBrace[numberOfElements - 1] == "/")
                    {
                        if (num2 == 0)
                        {
                            MessageBox.Show("Cannot divide zero");
                            return -1;
                        }
                        numberStack.RemoveRange(numberOfNumbers - 2, 2);
                        operationStackAndBrace.RemoveAt(numberOfElements - 1);
                        numberStack.Add(num1 / num2);
                        numberOfElements = operationStackAndBrace.Count;
                        numberOfNumbers = numberStack.Count;
                    }
                    else if (operationStackAndBrace[numberOfElements - 1] == "^")
                    {
                        numberStack.RemoveRange(numberOfNumbers - 2, 2);
                        operationStackAndBrace.RemoveAt(numberOfElements - 1);
                        numberStack.Add(Math.Pow(num1, num2));
                        numberOfElements = operationStackAndBrace.Count;
                        numberOfNumbers = numberStack.Count;
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter Correct value");
                return -1;
            }

            if (changeSign)
            {
                return -numberStack[0];
            }
            else
            {
                return numberStack[0];
            }
        }

        private string[] divideIntoParts(string input)
        {
            string pattern = @"([*()\^\/]|(?<!E)[\+\-])";
            string[] substrings = Regex.Split(input, pattern);
            substrings = substrings.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            List<string> newSubstring = new List<string>();
            for(int i=0; i < substrings.Length; i++)
            {
                if (substrings.Length == 2 && substrings[i]=="-")
                {
                    if(decimal.TryParse(substrings[i+1], out decimal number))
                    {
                        newSubstring.Add(substrings[i]+substrings[i+1]);
                        i++;
                    }
                    else
                    {
                        newSubstring.Add(substrings[0]);
                        newSubstring.Add(substrings[1]);
                    }
                }
                else if (i==0 && (substrings[i] == "-") && (substrings[i+1] != "("))
                {
                    if (decimal.TryParse(substrings[i + 1], out decimal number))
                    {
                        newSubstring.Add(substrings[i] + substrings[i + 1]);
                        i++;
                    }
                    else
                    {
                        newSubstring.Add(substrings[i]);
                        newSubstring.Add(substrings[i+1]);
                        i++;
                    }
                }
                else if (substrings.Length > 2 && (i + 1 != substrings.Length) && i>0)
                {
                    if ((substrings[i] == "-") &&(substrings[i-1] =="(") )
                    {
                        if (decimal.TryParse(substrings[i+1], out decimal number))
                        {
                            newSubstring.Add(substrings[i] + substrings[i+1]);
                            i++;
                        }
                        else
                        {
                            newSubstring.Add(substrings[i]);
                        }
                    }
                    else
                    {
                        newSubstring.Add(substrings[i]);
                    }

                }
                else
                {
                    newSubstring.Add(substrings[i]);
                }
            }
            string[] newArray = newSubstring.ToArray();
            
            return newArray;
        }

        
    }
}
