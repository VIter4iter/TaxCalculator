using System.Collections.Generic;
using System.Linq;
using TaxCalculator.Models;

namespace TaxCalculator.Services
{
    public class TaxService
    {
        private readonly ApplicationContext _applicationContext;

        public TaxService(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public double CalculateTax(int income)
        {
            double summaryTax = 0;
            List<Threshold> thresholds = _applicationContext.Thresholds.ToList();
            for (int i = 0; i < thresholds.Count; ++i)
            {
                if (i == 0)
                {
                    if (income - thresholds[i].Value > 0)
                    {
                        summaryTax += CalculateTaxForThreshold(thresholds[i].Value,thresholds[i].Percent);
                    }
                    else
                    {
                        summaryTax += CalculateTaxForThreshold(income, thresholds[i].Percent);
                        break;
                    }
                }
                else if (i == thresholds.Count - 1)
                {
                    summaryTax += CalculateTaxForThreshold(income-thresholds[i-1].Value,thresholds[i].Percent);
                }
                else
                {
                    if (income - thresholds[i].Value > 0)
                    {
                        summaryTax += CalculateTaxForThreshold(thresholds[i].Value-thresholds[i-1].Value,thresholds[i].Percent);
                    }
                    else
                    {
                        summaryTax += CalculateTaxForThreshold(income-thresholds[i-1].Value, thresholds[i].Percent);
                        break;
                    }
                }
                
            }

            return summaryTax;
        }

        private double CalculateTaxForThreshold( int valueInThreshold, double percent)
        {
            return valueInThreshold * percent / 100;
        }
    }
}