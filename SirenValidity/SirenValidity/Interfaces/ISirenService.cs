namespace SirenValidity.Interfaces
{

    /// <summary>
    /// The siren service interface.
    /// </summary>
    public interface ISirenService
    {
        /// <summary>
        /// Check Siren Validity.
        /// </summary>
        /// <param name="siren">The siren parameter.</param>
        /// <returns>Boolean</returns>
        bool CheckSirenValidity(string siren);

        /// <summary>
        /// Compute Full Siren.
        /// </summary>
        /// <param name="sirenWithoutControlNumber">The siren without control number parameter.</param>
        /// <returns>string</returns>
        string ComputeFullSiren(string sirenWithoutControlNumber);
    }
}
