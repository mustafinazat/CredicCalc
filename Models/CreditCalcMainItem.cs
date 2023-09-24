using System.Xml.Linq;

namespace CredicCalc.Models
{
    public class CreditCalcItem
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double PaymentSum { get; set; }
        public double MainDebt { get; set; }
            
        public double Percent { get; set; }
        public double RemainingDebt { get; set; }


        public string DateStr 
        {
            get { return this.Date.ToString("dd.MM.yyyy"); }
        }

        public string PaymentSumStr
        {
            get { return String.Format("{0:0.00}", this.PaymentSum); }
        }

        public string MainDebtStr
        {
            get { return String.Format("{0:0.00}", this.MainDebt); }
        }

        public string PercentStr
        {
            get { return String.Format("{0:0.00}", this.Percent); }
        }

        public string RemainingDebtStr
        {
            get { return String.Format("{0:0.00}", this.RemainingDebt); }
        }

    }
}
