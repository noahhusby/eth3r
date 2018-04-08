@echo off
dir

echo decrypting 
  copy dmg.exe IPSW
  cd IPSW
  dmg extract "048-2484-005.dmg" "decrootfs.dmg" -k 4bcdd29f167775f32fd7c6bfec2e1f2ffec9b8d7bf72832092a8be71501e347c459e9bc5
  del dmg.exe
  cd ..
  
echo Giving room to RootFS
  hfsplus "IPSW/decrootfs.dmg" grow 1920783616  
  
echo Extracting Cydia
  hfsplus "IPSW/decrootfs.dmg" untar "Cydia.tar" "/"  
  
echo Jailbreaking
  hfsplus "IPSW/decrootfs.dmg" untar "p0sixspwn.tar" "/"

echo Bypassing iCloud Lock
  hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/Setup"
  hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/PkgInfo"
  hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/warranty.plist"
  hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/Info.plist"
  hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/CountryAlias.plist"
  hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/_CodeSignature/CodeResources"
  hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/LanguagesByCountry.plist"

echo Making stuff better
  hfsplus "IPSW/decrootfs.dmg" rm "System/Library/CoreServices/SpringBoard.app/SlideToSetupStrings.strings"
  hfsplus "IPSW/decrootfs.dmg" add SlideToSetupStrings.strings "System/Library/CoreServices/SpringBoard.app/SlideToSetupStrings.strings"
  hfsplus "IPSW/decrootfs.dmg" rm "System/Library/CoreServices/SpringBoard.app/English.lproj/SpringBoard.strings" SpringBoard.strings
  hfsplus "IPSW/decrootfs.dmg" add SpringBoard.strings "System/Library/CoreServices/SpringBoard.app/English.lproj/SpringBoard.strings"
  
echo Adding Siri
  hfsplus "IPSW/decrootfs.dmg" add BypassAuth.deb "var/root/Media/Cydia/AutoInstall"
  hfsplus "IPSW/decrootfs.dmg" add Ac!dSiri.deb "var/root/Media/Cydia/AutoInstall"
  
echo Adding Activator
	hfsplus "IPSW/decrootfs.dmg" add libactivator_1.9.12_iphoneos-arm.deb "var/root/Media/Cydia/AutoInstall"
	
echo Installing SSH
	hfsplus "IPSW/decrootfs.dmg" untar "ssh_small.tar" "/"
  
  pause