using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Walidacja
{
    public class ViewModel:IDataErrorInfo
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Mail { get; set; }
        public string Telefon { get; set; }
        public virtual string Error
        {
            get
            {
                string result = this[string.Empty];
                if (result != null && result.Trim().Length == 0)
                {
                    result = null;
                }
                return result;
            }
        }


        public string this[string fieldName]
        {
            get
            {
                string result = null;
                if (fieldName == "Imie"||fieldName=="")
                {
                    if (string.IsNullOrEmpty(Imie))
                        result += "Imię nie może być puste!\n";
                }
                if (fieldName == "Nazwisko" || fieldName == "")
                {
                    if (string.IsNullOrEmpty(Nazwisko))
                        result += "Nazwisko nie może być puste!\n";
                }
                if (fieldName == "Mail" || fieldName == "")
                {
                    if (String.IsNullOrEmpty(Mail))
                    {
                        result += "Mail nie spełnia wymogów\n";
                    }
                    else
                    {
                        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                        Match match = regex.Match(Mail);
                        if (!match.Success)
                            result += "Mail musi mieć format  ***@***.**\n"; 
                    }
                }
                if (fieldName == "Telefon" || fieldName == "")
                {
                    if (String.IsNullOrEmpty(Telefon))
                    {
                        result += "Telefon musi składać się tylko z cyfr 0-9\n";
                    }
                    else
                    {
                        Regex regex = new Regex(@"^\(?[+( ]?([0-9]{3})\)?[) ]?([0-9]{3})[ ]?([0-9]{3})$");
                        Match match = regex.Match(Telefon);
                        if (!match.Success)
                            result += "Telefon musi posiadać tylko cyfry\n";
                    }
                }

                return result;
            }
        }
    }
}