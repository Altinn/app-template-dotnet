function GetFylkeKommuner {
  Param(
    [Parameter(Mandatory=$true)]
    [String]
    $fnr
  )

  $uri = "https://ws.geonorge.no/kommuneinfo/v1/fylker/$fnr"
  $responseObject = Invoke-RestMethod -Uri $uri

  $fylker = [PSCustomObject]@{
    fylkesnavn = $responseObject.fylkesnavn
    fylkesnummer = $responseObject.fylkesnummer
    kommuner = $responseObject.kommuner | sort-object kommunenavn
  }

  $json = $fylker | ConvertTo-Json

  return $json
}

function GetKommuner {
  $uri = "https://ws.geonorge.no/kommuneinfo/v1/kommuner"
  $responseObject = Invoke-RestMethod -Uri $uri
  
  $kommuner = New-Object System.Collections.Generic.List[System.Object]
  foreach ($obj in $responseObject) {
    $kommuner.Add([PSCustomObject]@{
      label = $obj.kommunenavn
      value = $obj.kommunenummer
    })
  }
  $json = $kommuner | Sort-Object label | ConvertTo-Json
  
  return $json
}

function Update-Kommuner {
  GetKommuner | Out-File -FilePath "kommuner.json" -EnCoding utf8
}

function Update-FylkerKommuner {
  $fylkesnummer = @("03", "11", "15", "18", "30", "34", "38", "42", "46", "50", "54")

  foreach ($fnr in $fylkesnummer) {
    GetFylkeKommuner($fnr) | out-File -FilePath "fylke-$fnr-kommuner.json"
    Start-Sleep -Milliseconds 100
  }
}

Update-FylkerKommuner