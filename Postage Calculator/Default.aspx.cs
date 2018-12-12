using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Postage_Calculator
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void widthTextBox_TextChanged(object sender, EventArgs e)
        {
            resultLabel.Text = DisplayResult();
        }

        protected void heightTextBox_TextChanged(object sender, EventArgs e)
        {
            resultLabel.Text = DisplayResult();
        }

        protected void lengthTextBox_TextChanged(object sender, EventArgs e)
        {
            resultLabel.Text = DisplayResult();
        }

        protected void groundRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            resultLabel.Text = DisplayResult();
        }

        protected void airRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            resultLabel.Text = DisplayResult();
        }

        protected void nextDayRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            resultLabel.Text = DisplayResult();
        }

        // Start of helper methods

        private double GetVolume(double height, double width, double length = 1)
        {
            return height * width * length;
        }

        private double GetMultiplier()
        {
            if (groundRadioButton.Checked)
                return .15;
            if (airRadioButton.Checked)
                return .25;
            else
                return .45;
        }

        private bool IsMinInfoProvided()
        {
            if (!string.IsNullOrWhiteSpace(widthTextBox.Text) && !string.IsNullOrWhiteSpace(heightTextBox.Text) &&
                (groundRadioButton.Checked || airRadioButton.Checked || nextDayRadioButton.Checked)) return true;

            return false;
        }

        private bool VerifyTextBoxContents(string textBoxText)
        {
            if (string.IsNullOrWhiteSpace(textBoxText))
                return true;
            if (double.TryParse(textBoxText, out var dimension))
                return true;
            return false;
        }

        public double CalculateShipping()
        {
            var multiplier = GetMultiplier();

            if (string.IsNullOrWhiteSpace(lengthTextBox.Text))
                return multiplier * GetVolume(double.Parse(heightTextBox.Text), double.Parse(widthTextBox.Text));
            return multiplier * GetVolume(double.Parse(heightTextBox.Text), double.Parse(widthTextBox.Text), double.Parse(lengthTextBox.Text));
        }

        private string DisplayResult()
        {
            if (IsMinInfoProvided())
            {
                // First verify that the entered values are valid doubles
                if (!VerifyTextBoxContents(widthTextBox.Text.Trim()))
                    return "Please enter a valid value for width!";
                if (!VerifyTextBoxContents(heightTextBox.Text.Trim()))
                    return "Please enter a valid value for height!";
                if (!VerifyTextBoxContents(lengthTextBox.Text.Trim()))
                    return "please enter a valid value for length!";

                return $"Mailing this package will cost {CalculateShipping():C}";
            }

            return "Please enter all required information!";
        }
    }
}