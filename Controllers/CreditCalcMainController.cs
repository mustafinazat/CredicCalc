using CredicCalc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web.Helpers;



namespace CredicCalc.Controllers
{
    public class CreditCalcMainController : Controller
    {
        private readonly ILogger<CreditCalcMainController> _logger;

        public CreditCalcMainController(ILogger<CreditCalcMainController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CreditCalcMainParam param = new CreditCalcMainParam();
            param.LoanTermMonth = 6;
            param.LoanSum = 100000;
            param.LoanRateYear = 10;
            return View(param);
        }

       // [HttpPost]
        public IActionResult Results(CreditCalcMainParam creditCalcMainParam)
        {

            
            if (Request.Method == "GET") 
            {
                return RedirectToAction("Index");
            }

            List<CreditCalcItem> items = new List<CreditCalcItem>();
            double sum = creditCalcMainParam.LoanSum;
            double rate = creditCalcMainParam.LoanRateYear;
            double monthRate = rate / 100 / 12;

            double monthPayment = sum * (monthRate + (monthRate / ((Math.Pow(1+ monthRate, creditCalcMainParam.LoanTermMonth))-1)));
            DateTime now = DateTime.Now;
            double overpayment = 0;
            for (int j = 0; j < creditCalcMainParam.LoanTermMonth; j++) 
            {
                CreditCalcItem item = new CreditCalcItem();
                item.Id = j + 1;
                item.Date = now.AddMonths(j+1);
                item.PaymentSum =monthPayment;
                item.Percent =  sum * monthRate;
                item.MainDebt = monthPayment - item.Percent;
                item.RemainingDebt = sum - item.MainDebt;
                overpayment = overpayment + sum * monthRate;
                sum = sum - (monthPayment - (sum * monthRate));
             
                items.Add(item);
            }
            items.Last().RemainingDebt = 0;
            ViewBag.Param = creditCalcMainParam;
            ViewBag.LastDate = items.Last().Date.ToString("dd.MM.yyyy");
            ViewBag.Payment = String.Format("{0:0.00}", monthPayment);
            ViewBag.Overpayment = String.Format("{0:0.00}", overpayment);
            ViewBag.FullSum = String.Format("{0:0.00}", overpayment + creditCalcMainParam.LoanSum);

            return View( items);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckRate(double LoanRateYear)
        {
          return Json(LoanRateYear >=0.01);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}