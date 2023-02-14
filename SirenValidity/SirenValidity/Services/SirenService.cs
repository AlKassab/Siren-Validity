using SirenValidity.Interfaces;

namespace SirenValidity.Services
{
    /// <summary>
    /// The siren service class.
    /// </summary>
    public class SirenService : ISirenService
    {

        /// <inheritdoc />
        public bool CheckSirenValidity(string siren)
        {
            if (string.IsNullOrEmpty(siren) || siren.Length != 9)
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < siren.Length - 1; i++)
            {
                int digit = int.Parse(siren[i].ToString());
                if (i % 2 != 0)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }
                sum += digit;
            }

            int checkDigit = (10 - (sum % 10)) % 10;
            int lastDigit = int.Parse(siren[8].ToString());

            return checkDigit == lastDigit;
        }

        /// <inheritdoc />
        public string ComputeFullSiren(string sirenWithoutControlNumber)
        {
            if (sirenWithoutControlNumber == null || sirenWithoutControlNumber.Length != 8)
            {
                throw new ArgumentException("Invalid SIREN without control number.");
            }

            int sum = 0;

            for (int i = 0; i < 8; i++)
            {
                int digit = sirenWithoutControlNumber[i] - '0';

                if (i % 2 == 1)
                {
                    digit *= 2;

                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
            }

            int controlNumber = (10 - sum % 10) % 10;

            return sirenWithoutControlNumber + controlNumber.ToString();
        }
    }
}
