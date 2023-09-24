using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CredicCalc.Models
{
   
        public class CreditCalcAdditionalParam
        {
            [Required(ErrorMessage = "Поле 'Сумма займа' не должно быть пустым")]
            [Range(5000, 30000000, ErrorMessage = "Сумма займа должна быть в диапазоне от 5000 до 30 000 000")]
            [RegularExpression(@"\d+", ErrorMessage = "Вводите только целые числа")]

            public int LoanSum { get; set; }



            [Required(ErrorMessage = "Поле 'Срок займа (в днях)' не должно быть пустым")]
            [Range(1, 200000, ErrorMessage = "Срок займа должен быть в диапазоне от 1 до 200000")]
            [RegularExpression(@"\d+", ErrorMessage = "Вводите только целые числа")]
            [Remote(action: "CheckTerm", controller: "CreditCalcAdditional", AdditionalFields = "StepDay")]
                public int LoanTermDay { get; set; }

            [Required(ErrorMessage = "Поле 'Ставка (в день)' не должно быть пустым")]
            [Remote(action: "CheckRate", controller: "CreditCalcAdditional", ErrorMessage = "Ставка должна быть не меньше 0.01")]
            public double LoanRateDay { get; set; }


        [Required(ErrorMessage = "Поле 'Шаг платежа (в днях)' не должно быть пустым")]
        [Range(1, 300, ErrorMessage = "Срок займа должен быть в диапазоне от 1 до 300")]
        [RegularExpression(@"\d+", ErrorMessage = "Вводите только целые числа")]
      
        public int StepDay { get; set; }


    }
}