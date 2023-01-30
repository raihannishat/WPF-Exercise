﻿using Calculator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    public class ScientificCalculatorViewModel : BasicCalculatorViewModel
    {
        public ScientificCalculatorViewModel(ICalculatorService calculatorService) : base(calculatorService)
        {
        }
    }
}
