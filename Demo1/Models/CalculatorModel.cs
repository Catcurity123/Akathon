namespace Calculator.Models
{
    public class CalculatorModel
    {
        public double Num1 { get; set; }
        public double Num2 { get; set; }
        public string Op { get; set; }
        public double? Result { get; set; }

        public void Calculate()
        {
            switch (Op)
            {
                case "+":
                    Result = Num1 + Num2;
                    break;
                case "-":
                    Result = Num1 - Num2;
                    break;
                case "*":
                    Result = Num1 * Num2;
                    break;
                case "/":
                    if (Num2 != 0)
                    {
                        Result = Num1 / Num2;
                    }
                    else
                    {
                        Result = null;
                    }
                    break;
            }
        }
    }
}