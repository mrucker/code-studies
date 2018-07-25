#To run this file open up powershell and dot source it in ".\<path to file>\CompareExeAndDll.ps1"
param([Switch]$completeComparison)
 
 #NOTE the last color will be the color chosen when there isn't a match
$matchColors = ("green", "blue", "red")

function All-Same {
	param($values)
	
	$allSame = $true
	
	for($i = 0; ($i+1) -lt $values.length; $i++) { 
		if($values[$i] -ne $values[$i+1]) { 
			$allSame = $false
			break; 
		} 
	} 
	
	$allSame
}

function Write-Header {
	Write-Host "LINE  EXE      EXE      DLL      DLL     "
	Write-Host "----  -------- -------- -------- --------"
}

function Get-Matches {
	param($displays)
	$matches = @()
	
	for($i = 0; ($i + 1) -lt $displays.Length; $i++) {
		$currentDisplay    = $displays[$i]
		
		if($matches -contains $currentDisplay) { continue }
		
		$followingDisplays = $displays[($i+1)..$displays.Length]
		$thereIsAMatch     = $followingDisplays -contains $currentDisplay
		
		if($thereIsAMatch ) {
			$matches += $currentDisplay
		}
	}
	
	, $matches
}

function Get-Display {
	param($byte)
	
	if($byte -eq $NULL) { "".PadRight(8, ' ') } else { $byte.ToString().PadRight(8, ' ') }
}

function Write-Display {
	param($display, $matches)
	
	$matchIndex = $matches.IndexOf($display)
	
	Write-Host "$display" -NoNewLine -ForegroundColor $matchColors[$matchIndex]
}

function Write-DisplayLine {
	param($displays, $lineNumber)
	
	$matches = Get-Matches $displays
	
	Write-Host "$($lineNumber.ToString().PadLeft(4, '0')): " -NoNewLine
	
	foreach($display in $displays) {
		Write-Display $display $matches
		Write-Host " " -NoNewLine
	}
	
	Write-Host
}

function Write-DisplayLines {
	param($values)
	
	$maxLength = $values | % {$_.Length} | measure -Maximum | Select -ExpandProperty Maximum

	for ($i = 0; $i -lt $maxLength; $i++) {

		$displays = $values | % { Get-Display $_[$i] }

		$allSame = All-Same $displays
		
		if($completeComparison -or -not $allSame) {
			Write-DisplayLine $displays $i
		}
	}
}

$exeBytes1 = Get-Content ".\project - Copy 0 (EXE)\bin\Debug\project.exe" -Encoding Byte
$exeBytes2 = Get-Content ".\project - Copy 1 (EXE)\bin\Debug\project.exe" -Encoding Byte
$dllBytes1 = Get-Content ".\project - Copy 2 (DLL)\bin\Debug\project.dll" -Encoding Byte
$dllBytes2 = Get-Content ".\project - Copy 3 (DLL)\bin\Debug\project.dll" -Encoding Byte

$exeBits1 = $exeBytes1 | % { [convert]::ToString([int32]$_, 2).PadLeft(8,'0') }
$exeBits2 = $exeBytes2 | % { [convert]::ToString([int32]$_, 2).PadLeft(8,'0') }
$dllBits1 = $dllBytes1 | % { [convert]::ToString([int32]$_, 2).PadLeft(8,'0') }
$dllBits2 = $dllBytes2 | % { [convert]::ToString([int32]$_, 2).PadLeft(8,'0') }

Write-Header

Write-DisplayLines $exeBits1,$exeBits2,$dllBits1,$dllBits2