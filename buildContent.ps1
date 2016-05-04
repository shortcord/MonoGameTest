$env:Path = "C:\Program Files (x86)\MSBuild\MonoGame\v3.0\Tools"; #set path so mgcs and needed shit is there
cd "../../../RawContent" #chg to RawContent folder
mgcb /@:Content.mgcb #build shit