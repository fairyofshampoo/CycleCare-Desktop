using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CycleCare.Utilities
{
    public class ValidationManager
    {
        public static bool IsPasswordValid(string password)
        {
            bool isValidPassword = false;

            if (!string.IsNullOrEmpty(password))
            {
                string passwordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,20}$";

                Regex regex = new Regex(passwordPattern);
                isValidPassword = regex.IsMatch(password);
            }

            return isValidPassword;
        }

        public static bool IsNameValid(string name)
        {
            bool isValidName = false;

            if (!string.IsNullOrEmpty(name))
            {
                isValidName = !string.IsNullOrEmpty(name) && Regex.IsMatch(name, "^[a-zA-Z\\s]+$");
            }

            return isValidName;
        }

        public static bool IsValidEmail(string email)
        {
            bool isValidEmail = false;

            if (!string.IsNullOrEmpty(email))
            {
                string emailPattern = "^[A-Za-z0-9+_.-]{1,64}@[A-Za-z0-9.-]{1,63}$";

                Regex regex = new Regex(emailPattern);
                isValidEmail = regex.IsMatch(email);
            }

            return isValidEmail;
        }

        public static bool IsTitleValid(string title)
        {
            bool isValid = false;
            if(!string.IsNullOrEmpty(title))
            {
                string titlePattern = "^(?!\\s)(.{1,24})$\r\n";

                Regex regex = new Regex(titlePattern);
                isValid = regex.IsMatch(title);
            }

            return isValid;
        }

        public static bool isDescriptionValid(string description) 
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(description))
            {
                string descriptionPattern = "^(?!\\s)(.{1,100})$\r\n";
                Regex regex = new Regex(descriptionPattern);
                isValid = regex.IsMatch(description);  
            }
            return isValid;
        }

        public static bool IsNotEmpty(string data)
        {
            bool isNotEmpty = false;
            if (!string.IsNullOrEmpty(data))
            {
                isNotEmpty = !string.IsNullOrEmpty(data);
            }
            return isNotEmpty;
        }

        public static bool IsValidPositiveInt(string data)
        {
            bool isValid = false;

            if (int.TryParse(data, out int number))
            {
                isValid = number > 0;
            }

            return isValid;
        }


        


    }
}
