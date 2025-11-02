using System.Text.RegularExpressions;

namespace Porter.Domain.Validators
{
    public class NameValidator
    {
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length <=5)
            {
                return false;
            }

            // Esta regex aceita letras (maiúsculas/minúsculas), espaços, acentos, hífens e apóstrofos
            // A verificação `^` e `$` garante que toda a string deve corresponder ao padrão
            string pattern = @"^[A-Za-zÀ-ÖØ-öø-ÿ\s'-]+$";

            

            return Regex.IsMatch(name, pattern);
        }
    }
}
