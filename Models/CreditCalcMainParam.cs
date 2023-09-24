using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace CredicCalc.Models
{

    public class CreditCalcMainParam
    {
        [Required(ErrorMessage = "���� '����� �����' �� ������ ���� ������")]
         [Range(5000, 30000000, ErrorMessage = "����� ����� ������ ���� � ��������� �� 5000 �� 30 000 000")]
        [RegularExpression(@"\d+", ErrorMessage = "������� ������ ����� �����")]

        public int LoanSum { get; set; }
        [Required(ErrorMessage = "���� '���� �����' �� ������ ���� ������")]
        [Range(1, 360, ErrorMessage = "���� ����� ������ ���� � ��������� �� 1 �� 360")]
        [RegularExpression(@"\d+", ErrorMessage = "������� ������ ����� �����")]
        public int LoanTermMonth { get; set; }

        [Required(ErrorMessage = "���� '������' �� ������ ���� ������")]
        // [Range(0.00f, 100f, ErrorMessage = "������ ������ ���� �� ������ 0")]
        [Remote(action: "CheckRate", controller: "CreditCalcMain", ErrorMessage = "������ ������ ���� �� ������ 0.01")]
        public double LoanRateYear { get; set; }

     

    }




}