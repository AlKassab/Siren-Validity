# Siren-Validity
Test Technique Serensia : Siren Validity

Impl�menter l'interface qui permet de valider et de calculer un SIREN pass� en string (ref:
https://fr.wikipedia.org/wiki/Syst�me_d%27identification_du_r�pertoire_des_entreprises)
interface IAmTheTest{
bool CheckSirenValidity(string siren);
// Returns the full siren from the sirenWithoutControlNumber
string ComputeFullSiren(string sirenWithoutControlNumber);
}
Note: Vous pouvez donner dans votre r�ponse les cas de tests unitaires que vous avez/auriez
utilis�

