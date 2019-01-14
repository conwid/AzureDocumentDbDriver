param ([string] $folder)

$sourceFolder=[System.IO.Path]::Combine($folder.Replace('"',''))
$targetFolder=[System.IO.Path]::Combine($sourceFolder,"ReleasePackage")
$xmlFile=[System.IO.Path]::Combine($sourceFolder,"header.xml");
$pngFiles=[System.IO.Path]::Combine($sourceFolder,"*.png");
$dllFiles=[System.IO.Path]::Combine($sourceFolder,"*.dll")
$zipFile=[System.IO.Path]::Combine($sourceFolder,"AzureCosmosDbDriver.lpx")


If (Test-Path $targetFolder){
	Remove-Item $targetFolder -Recurse -Force
}

If (Test-Path $zipFile){
	Remove-Item $zipFile -Force
}

New-Item -ItemType directory -Path $targetFolder
Copy-Item -Path $xmlFile -Destination $targetFolder
Copy-Item -Path $pngFiles -Destination $targetFolder
Copy-Item -Path $dllFiles -Destination $targetFolder

Add-Type -AssemblyName System.IO.Compression.FileSystem
$compressionLevel    = [System.IO.Compression.CompressionLevel]::"Fastest"  
[System.IO.Compression.ZipFile]::CreateFromDirectory($targetFolder, $zipFile, $compressionLevel, $False)