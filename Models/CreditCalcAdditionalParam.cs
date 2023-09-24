using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CredicCalc.Models
{
   
        public class CreditCalcAdditionalParam
        {
            [Required(ErrorMessage = "���� '����� �����' �� ������ ���� ������")]
            [Range(5000, 30000000, ErrorMessage = "����� ����� ������ ���� � ��������� �� 5000 �� 30 000 000")]
            [RegularExpression(@"\d+", ErrorMessage = "������� ������ ����� �����")]

            public int LoanSum { get; set; }



            [Required(ErrorMessage = "���� '���� ����� (� ����)' �� ������ ���� ������")]
            [Range(1, 200000, ErrorMessage = "���� ����� ������ ���� � ��������� �� 1 �� 200000")]
            [RegularExpression(@"\d+", ErrorMessage = "������� ������ ����� �����")]
            [Remote(action: "CheckTerm", controller: "CreditCalcAdditional", AdditionalFields = "StepDay")]
                public int LoanTermDay { get; set; }

            [Required(ErrorMessage = "���� '������ (� ����)' �� ������ ���� ������")]
            [Remote(action: "CheckRate", controller: "CreditCalcAdditional", ErrorMessage = "������ ������ ���� �� ������ 0.01")]
            public double LoanRateDay { get; set; }


        [Required(ErrorMessage = "���� '��� ������� (� ����)' �� ������ ���� ������")]
        [Range(1, 300, ErrorMessage = "���� ����� ������ ���� � ��������� �� 1 �� 300")]
        [RegularExpression(@"\d+", ErrorMessage = "������� ������ ����� �����")]
      
        public int StepDay { get; set; }


    }
}