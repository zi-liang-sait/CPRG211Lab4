namespace CPRG211Lab4
{
    public partial class MainPage : ContentPage
    {
        private readonly Calculation calculation;
        private bool justCalculated = false; //For tracking behavior of operation buttons. Is set to true upon pressing Equals.

        public MainPage()
        {
            InitializeComponent();
            calculation = new Calculation();
        }

        private void Number_Button_Clicked(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            //Clear entry for new calculation
            if (justCalculated)
            {
                mainEntry.Text = "0";
                justCalculated = false;
            }

            //If the only number in entry is 0, remove the 0 for the incoming input.'
            //But if the input is a decimal point, the 0 should remain.
            if (mainEntry.Text == "0" && clickedButton.Text != ".")
            {
                mainEntry.Text = "";
            } else if (mainEntry.Text == "-0" && clickedButton.Text != ".")
            {
                mainEntry.Text = "-";
            }

            //Disallow decimal point entry if number already has a decimal point
            if (!(mainEntry.Text.Contains('.') && clickedButton.Text == "."))
            {
                mainEntry.Text += clickedButton.Text;
            }
        }

        private void Clear_Button_Clicked(object sender, EventArgs e)
        {
            calculation.Number1 = null;
            calculation.Number2 = null;
            calculation.Operation = null;
            justCalculated = false;
            mainEntry.Text = "0";
        }

        private void Negative_Button_Clicked(object sender, EventArgs e)
        {
            //Clear entry for new calculation
            //If previous result is not numeric (invalid or divide by zero), set previous result to zero.
            if (justCalculated)
            {
                justCalculated = false;
                if (!double.TryParse(mainEntry.Text, out _)) // Check if string is numeric: https://stackoverflow.com/questions/894263/identify-if-a-string-is-a-number
                {
                    mainEntry.Text = "0";
                }
            }

            //Toggle existence of leading -
            if (mainEntry.Text.StartsWith('-'))
            {
                mainEntry.Text = mainEntry.Text[1..];
            } 
            else
            {
                mainEntry.Text = "-" + mainEntry.Text;
            }
        }

        private void Percent_Button_Clicked(object sender, EventArgs e)
        {            
            //Clear entry for new calculation
            //If previous result is not numeric (invalid or divide by zero), set previous result to zero.
            if (justCalculated)
            {
                justCalculated = false;
                if (!double.TryParse(mainEntry.Text, out _))
                {
                    mainEntry.Text = "0";
                }
            }

            //Simply divide existing input by 100.
            mainEntry.Text = (double.Parse(mainEntry.Text) / 100).ToString();
        }

        private void Operation_Button_Clicked(object sender, EventArgs e)
        {
            //Clear entry for new calculation
            //If previous result is not numeric (invalid or divide by zero), set previous result to zero.
            if (justCalculated)
            {
                justCalculated = false;
                if (!double.TryParse(mainEntry.Text, out _))
                {
                    mainEntry.Text = "0";
                }
            }

            //If the first number is not stored, store the first number and allow entering second number.
            if (calculation.Number1 == null)
            {
                calculation.Number1 = double.Parse(mainEntry.Text);
                calculation.Operation = ((Button)sender).Text;
                mainEntry.Text = "0";
            } else
            //If the first number is already stored, store the second number, calculate, display the result,
            //and set the result as the first number to allow chaining calculations.
            {
                calculation.Number2 = double.Parse(mainEntry.Text);
                mainEntry.Text = calculation.GetResult();
                calculation.Number1 = double.Parse(mainEntry.Text);
                calculation.Number2 = null;
                calculation.Operation = ((Button)sender).Text;
                mainEntry.Text = "0";
            }
        }

        private void Equals_Button_Clicked(object sender, EventArgs e)
        {
            if (calculation.Number1 != null && calculation.Operation != null)
            {
                calculation.Number2 = double.Parse(mainEntry.Text);
                mainEntry.Text = calculation.GetResult();
                calculation.Number1 = null;
                calculation.Number2 = null;
                calculation.Operation = null;
                justCalculated = true; //to ensure proper behavior of other buttons
            }
        }
    }

}
