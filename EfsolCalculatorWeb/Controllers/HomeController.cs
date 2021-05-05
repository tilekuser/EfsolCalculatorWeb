using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EfsolCalculatorWeb.Models;

namespace EfsolCalculatorWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(CalculatorData input)
        {
            double result = 0;
            List<double> valueForSolutions = new List<double> { };
            switch (input.Action)
            {
                case '+':
                    result = input.FirstValue + input.SecondValue;
                    break;

                case '-':
                    result = input.FirstValue - input.SecondValue;
                    break;

                case 'x':
                    var secondValueChar = input.SecondValue.ToString().ToCharArray();
                    for (int i = secondValueChar.Length - 1; i >= 0; i--)
                    {
                        var inputSecondParse = double.Parse(secondValueChar[i].ToString());
                        var firstMulti = input.FirstValue * inputSecondParse;
                        result = input.FirstValue * input.SecondValue;
                        valueForSolutions.Add(firstMulti);
                    }
                    input.valueForSolutions = valueForSolutions;

                    result = input.FirstValue * input.SecondValue;
                    break;

                case '÷':
                    double solution = 0;
                    double result2 = 0;
                    double ss = input.FirstValue;
                    if(input.FirstValue == (double)input.FirstValue && input.SecondValue == (double)input.SecondValue) 
                    {
                        ViewBag.Message = $" Для удобства вычислений преобразуем делимое и делитель в целые числа. Для этого умножим делимое {input.FirstValue} и делитель {input.SecondValue} на 100. В результате наша задача сведется к делению следующих чисел:";
                        for (int i = 0; i < 100; i++)
                        {
                            result = i;
                            if (input.SecondValue * i > ss)
                            {
                                result = i - 1;
                                double findSolution = input.SecondValue * result;
                                solution = ss - findSolution;
                                if (result2 == 0)
                                {
                                    result2 = solution;
                                }
                                valueForSolutions.Add(findSolution * 100);
                                if (solution <= result2)
                                {
                                    ss = solution * 100;
                                    valueForSolutions.Add(solution * 1000);
                                    i = 0;
                                    continue;
                                }
                                if (solution > result2)
                                {
                                    valueForSolutions.Add(solution*100);
                                    break;
                                }
                                result2 = solution;
                            }
                        }
                    }
                    input.valueForSolutions = valueForSolutions;
                    result = input.FirstValue / input.SecondValue;
                    break;
                default:
                    break;
            }
            input.Result = result;
            return View(input);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
