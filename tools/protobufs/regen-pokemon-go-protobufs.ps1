$ProjectRootDirectory = [IO.Path]::GetFullPath([IO.Path]::Combine($PSScriptRoot, '..', '..'))
$ProtobufCompilerPath = [IO.Path]::Combine($ProjectRootDirectory, 'packages', 'Google.Protobuf.Tools', 'tools', 'windows_x86', 'protoc.exe')
$ProtoRelativePath = [IO.Path]::Combine($ProjectRootDirectory, 'protos', 'PokemonGo', 'src')

$ProtoPaths = Get-ChildItem -Path $ProtoRelativePath  -Filter *.proto -Recurse -Name

ForEach ($Proto in $ProtoPaths)
{
	$RelativePath = [IO.Path]::Combine($ProtoRelativePath, $Proto)
	$CSharpRelativePath = [IO.Path]::Combine($ProjectRootDirectory, 'src', 'Plugins', 'PokemonGo', 'PokemonGo', 'Protobufs', $Proto)
	$Directory = [IO.Path]::GetDirectoryName($CSharpRelativePath)

	If ((Test-Path $Directory) -eq 0) {
		New-Item -Type Directory $Directory
	}
	
    & $ProtobufCompilerPath $RelativePath ('--csharp_out=' + $Directory) ('-I' + $ProtoRelativePath)
}
