using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace CredicCalc.Models
{

    public class CreditCalcMainParam
    {
        [Required(ErrorMessage = "Поле 'Сумма займа' не должно быть пустым")]
         [Range(5000, 30000000, ErrorMessage = "Сумма займа должна быть в диапазоне от 5000 до 30 000 000")]
        [RegularExpression(@"\d+", ErrorMessage = "Вводите только целые числа")]

        public int LoanSum { get; set; }
        [Required(ErrorMessage = "Поле 'Срок займа' не должно быть пустым")]
        [Range(1, 360, ErrorMessage = "Срок займа должен быть в диапазоне от 1 до 360")]
        [RegularExpression(@"\d+", ErrorMessage = "Вводите только целые числа")]
        public int LoanTermMonth { get; set; }

        [Required(ErrorMessage = "Поле 'Ставка' не должно быть пустым")]
        // [Range(0.00f, 100f, ErrorMessage = "Ставка должна быть не меньше 0")]
        [Remote(action: "CheckRate", controller: "CreditCalcMain", ErrorMessage = "Ставка должна быть не меньше 0.01")]
        public double LoanRateYear { get; set; }

     

    }




}