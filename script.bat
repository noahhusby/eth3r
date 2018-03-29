@echo off
title Gen-r8
color 0a
:: Just 'cuz you aren't a true hacker until everything is in lime green 
		
		
echo.		
echo.	
echo.	
echo.
echo.
echo.
echo.


echo 		 	         _______ _________          _______  _______ 
echo		  		        (  ____ \\__   __/^|\     /^|(  ____ \(  ____ )
echo				        ^| (    \/   ) (   ^| )   ( ^|^| (    \/^| (    )^|
echo	  			        ^| (__       ^| ^|   ^| (___) ^|^| (__    ^| (____)^|
echo		    	    	        ^|  __)      ^| ^|   ^|  ___  ^|^|  __)   ^|     __)
echo				        ^| (         ^| ^|   ^| (   ) ^|^| (      ^| (\ (   
echo				        ^| (____/\   ^| ^|   ^| )   ( ^|^| (____/\^| ) \ \__
echo			    	    	(_______/   )_(   ^|/     \^|(_______/^|/   \__/







echo.
echo.
echo                                                   Pre-Alpha 
echo                                                 Version 0.0.1
echo.
echo.
echo.
echo.
echo                                              Eth3r by TKO_Cuber
echo                                               GUI by NX_Master
echo.
echo.
echo.
echo.
echo.
echo.
echo.
pause

::                                So I'm gonna attempt to document this code so curious people can ravage through this... good luck to you.
		
		:: Main Program Time
  cd Files
  CLS
    ECHO We need to get some input from you...
	ECHO.
	ECHO What are the firmware keys for your device and target iOS version?
	ECHO You can find them here https://goo.gl/3vof6m
	ECHO When you have them, then
	pause
    echo.
	set /p RootFS_Key="Enter Root Filesystem Key: "
	set /p Ramdisk_IV="Enter Restore Ramdisk IV: "
	set /p Ramdisk_Key="Enter Restore Ramdisk Key: "
	echo.
	echo Cool. Now just a bit more info and we can get going.
	echo.
	set /p Model="Enter Device Model (iPhone2,1 or iPad4,2 etc): "
	set /p Version="Enter desired iOS version (6.1.3, 5.1.1, etc.): "
	set /p Build="Enter the build number for desired iOS version (10B329, 12C235, etc.): "
			:: This code asks the user for info, and sets their inputted info as a variable.
	

:DA_REAL_STUFF
CLS
  echo Downloading the IPSW (May take a bit depending on your connection)
    ::%SYSTEMROOT%\System32\WindowsPowerShell\v1.0\powershell.exe -Command "(new-object System.Net.WebClient).DownloadFile('https://api.ipsw.me/v4/ipsw/download/%Model%/%Build%', '%Model%_%Version%_%Build%_Restore.ipsw')"

  
echo Extracting IPSW
  7za.exe  x -oIPSW "%Model%_%Version%_%Build%_Restore.ipsw"  
		::extracts the IPSw to a folder named IPSW
  
  :Get
echo Getting the info we need from the files...
  cd IPSW
    echo Getting RootFS name
	  dir /b /o-s>{temp}
      set /P rootfs=<{temp}
	  del {temp}
	  
	  echo %rootfs%
	  pause

echo Decrypting RootFS
  cd IPSW
  rename "%rootfs%" "rootfs.dmg"
  cd ..
  dmg extract "IPSW/rootfs.dmg" "IPSW/decrootfs.dmg" -k %RootFS_Key%  

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
  
::echo Adding Siri
  ::hfsplus "IPSW/decrootfs.dmg" add BypassAuth.deb "var/root/Media/Cydia/AutoInstall"
  ::hfsplus "IPSW/decrootfs.dmg" add Ac!dSiri.deb "var/root/Media/Cydia/AutoInstall"
    
:next
echo Rebuilding RootFS
  dmg build "IPSW/decrootfs.dmg" "IPSW/%Rootfs_Name%"  
  xpwntool %Ramdisk_Name%" -t "IPSW/orig_%Ramdisk_Name%" -iv %Ramdisk_IV% -k %Ramdisk_Key%  

  ::Maid Mode: ACTIVATE!
echo Deleting unrequired files
  ::Maid Mode: Deactivate!

  ::Pack dem files up and we're good to go!
echo Packing up the IPSW
  COPY 7z.exe "IPSW/7z.exe"  
  cd IPSW
  7z u -tzip -mx0 Gen_R8.ipsw -x!7z.exe   
  del 7z.exe 
  cd ..
  
echo.
echo.
echo.
echo.
echo             Your IPSW has been generated.
echo.
echo           You will need iTunes 11.0.5 to restore
echo.
echo.
echo                   Gen-r8 by TKO_Cuber
echo.
pause

  ::Cue the final song... close the curtains... we did it bois!
  ::The audience stands and applauds. 
  ::Claps roar through the room.
  ::The show is finally over.
  
  :: ~ End ~
  
  

