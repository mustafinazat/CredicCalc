using CredicCalc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace CredicCalc.Controllers
{
    public class CreditCalcAdditionalController : Controller
    {
        private readonly ILogger<CreditCalcAdditionalController> _logger;

        public CreditCalcAdditionalController(ILogger<CreditCalcAdditionalController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CreditCalcAdditionalParam param = new CreditCalcAdditionalParam();
            param.LoanSum = 60000;
            param.LoanTermDay = 10;
            param.LoanRateDay = 10;
            param.StepDay = 1;
            return View(param);
        
        }

        public IActionResult Results(CreditCalcAdditionalParam? creditCalcAdditionalParam)
        {
            if (Request.Method == "GET")
            {
                return RedirectToAction("Index");
            }
            List<CreditCalcItem> items = new List<CreditCalcItem>();
            double sum = creditCalcAdditionalParam.LoanSum;
            double rate = creditCalcAdditionalParam.LoanRateDay;
            double stepRate = rate / 100 * creditCalcAdditionalParam.StepDay;
            double numOfSteps = creditCalcAdditionalParam.LoanTermDay / creditCalcAdditionalParam.StepDay;


            double stepPayment = sum * (stepRate + (stepRate / ((Math.Pow(1 + stepRate, numOfSteps)) - 1)));
            DateTime now = DateTime.Now;
            double overpayment = 0;
            int counter = 1;
            for (int j = 0; j < creditCalcAdditionalParam.LoanTermDay; j+=creditCalcAdditionalParam.StepDay)
            {
                if (sum <= 0.0001) break;
                CreditCalcItem item = new CreditCalcItem();
                item.Id = counter;
                item.Date = now.AddDays(creditCalcAdditionalParam.StepDay *(counter));
                item.PaymentSum = stepPayment;
                item.Percent = sum * stepRate;
                item.MainDebt = stepPayment - item.Percent;
                item.RemainingDebt = sum - item.MainDebt;
                overpayment = overpayment + sum * stepRate;
                sum = sum - (stepPayment - (sum * stepRate));
                counter++;
                items.Add(item);
            }
            items.Last().RemainingDebt = 0;
            ViewBag.Param = creditCalcAdditionalParam;
            ViewBag.LastDate = items.Last().Date.ToString("dd.MM.yyyy");
            ViewBag.Payment = String.Format("{0:0.00}", stepPayment);
            ViewBag.Overpayment = String.Format("{0:0.00}", overpayment);
            ViewBag.FullSum = String.Format("{0:0.00}", overpayment + creditCalcAdditionalParam.LoanSum);
            return View(items);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckRate(double LoanRateDay)
        {
            return Json(LoanRateDay >= 0.01);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckTerm(int LoanTermDay, int StepDay )
        {

            if (LoanTermDay < StepDay)
            {
                return Json("Срок займа должен быть не меньше шага платежа");
            }
             if (LoanTermDay % StepDay != 0) 
            {
                return Json("Срок займа должен быть кратен шагу платежа");
            }

            return Json(true);
        }

     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}