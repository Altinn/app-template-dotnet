public class model
{
	public rapport? rapport { get; set; }
}

public class rapport
{
	public innsender? innsender { get; set; }
	public rapportering? rapportering { get; set; }
}

public class innsender
{
	public foretak? foretak { get; set; }
	public adresse? adresse { get; set; }
}

public class foretak
{
	public organisasjonsnummer? organisasjonsnummer { get; set; }
	public navn? navn { get; set; }
}

public class organisasjonsnummer
{
}



public class navn
{
}



public class adresse
{
}

public class rapportering {}
