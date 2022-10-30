using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models.Validators
{
    /// <summary>
    /// Classe de validação customizada para campos de senha
    /// </summary>
    public class PasswordValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            
            var password = value.ToString();

            return password.Length >= 8
                && password.Length <= 20
                && password.Any(char.IsUpper) 
                && password.Any(char.IsLower)
                && password.Any(char.IsDigit)
                && (
                       password.Contains("@")
                    || password.Contains("!")
                    || password.Contains("#")
                    || password.Contains("$")
                    ||password.Contains("&")
                    );

        }

    }
}
