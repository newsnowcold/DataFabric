$errors = @()
Get-ChildItem -Path .\src -Recurse -Filter *.component.ts | ForEach-Object {
  $p = $_.FullName
  $t = [IO.File]::ReadAllText($p)
  if ($t -like '*`r`n*') {
    $t2 = $t.Replace('`r`n', [Environment]::NewLine)
    [IO.File]::WriteAllText($p, $t2)
    Write-Host "Fixed: $p"
  }
}
Write-Host 'done'