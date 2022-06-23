// Auto-generated
#nullable enable
namespace Altinn.App.Models
{

public partial class Model
{
	public rapport? rapport { get; set; }
}

public partial class rapport
{
	public innsender? innsender { get; set; }
	public rapportering? rapportering { get; set; }
}

public partial class innsender
{
	public foretak? foretak { get; set; }
	public adresse? adresse { get; set; }
}

public partial class foretak
{
	public organisasjonsnummer? organisasjonsnummer { get; set; }
	public navn? navn { get; set; }
}

public partial class organisasjonsnummer
{
	public string? value { get; set; }
}



public partial class navn
{
	public string? value { get; set; }
}



public partial class adresse
{
	public adresselinje1? adresselinje1 { get; set; }
	public postnummer? postnummer { get; set; }
	public poststed? poststed { get; set; }
}

public partial class adresselinje1
{
	public string? value { get; set; }
}



public partial class postnummer
{
	public string? value { get; set; }
}



public partial class poststed
{
	public string? value { get; set; }
}



public partial class rapportering
{
	public kontaktperson1? kontaktperson1 { get; set; }
	public kontaktperson2? kontaktperson2 { get; set; }
	public beskrivelse? beskrivelse { get; set; }
}

public partial class kontaktperson1
{
	public navn1? navn1 { get; set; }
	public epost1? epost1 { get; set; }
	public telefonprefiks1? telefonprefiks1 { get; set; }
	public telefonnummer1? telefonnummer1 { get; set; }
}

public partial class navn1
{
	public string? value { get; set; }
}



public partial class epost1
{
	public string? value { get; set; }
}



public partial class telefonprefiks1
{
	public string? value { get; set; }
}



public partial class telefonnummer1
{
	public string? value { get; set; }
}



public partial class kontaktperson2
{
	public navn2? navn2 { get; set; }
	public epost2? epost2 { get; set; }
	public telefonprefiks2? telefonprefiks2 { get; set; }
	public telefonnummer2? telefonnummer2 { get; set; }
}

public partial class navn2
{
	public string? value { get; set; }
}



public partial class epost2
{
	public string? value { get; set; }
}



public partial class telefonprefiks2
{
	public string? value { get; set; }
}



public partial class telefonnummer2
{
	public string? value { get; set; }
}



public partial class beskrivelse
{
	public string? value { get; set; }
}


}
