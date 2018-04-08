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

::                                So I'm gonna attempt to document this code so curious people can rummage through this... good luck to you.
		
		:: Main Program Time
  cd Files		
  CLS
	echo Please put your IPSW for the desired iOS version in the Files folder
	echo.
	echo.
	pause
	echo.
    ECHO We need to get some input from you...
	ECHO.
	ECHO What are the firmware keys for your device and target iOS version?
	ECHO You can find them here https://goo.gl/3vof6m
	ECHO When you have them, then
	pause
    echo.
	set /p RootFS_Key="Enter Root Filesystem Key: "
	echo.
	echo Cool. Now just a bit more info and we can get going.
	echo.
	set /p Model="Enter Device Model (iPhone2,1 or iPad4,2 etc): "
	set /p Version="Enter desired iOS version (6.1.3, 5.1.1, etc.): "
	set /p Build="Enter the build number for desired iOS version (10B329, 12C235, etc.): "
			:: This code asks the user for info, and sets their inputted info as a variable.
	

	
REM if not exist User_Info (
	REM mkdir User_Info
REM )
	REM cd User_Info

REM if exist pref1.txt (
			REM if exist pref2.txt (
				REM if exist pref3.txt (
					REM for /F "tokens=1" %%A IN ('pref1.txt') do set Model_In_Pref=%%A
					REM set /p delete="All your preferance files are taken up... delete your first saved configuration containing the info for your %Model_In_Pref%? (yes or no)"
					REM if "%delete%"=="yes" (
						REM del pref1.txt
					REM )
				REM )
			REM )
		REM )
		REM if not exist pref1.txt (
			REM if not exist pref2.txt (
				REM if not exist pref3.txt (
					REM echo "/%Model%">> pref1.txt
					REM echo "/%Build%">> pref1.txt
					REM echo "/%RootFS%">> pref1.txt
					REM echo "/%RootFS_Key%">> pref1.txt
					REM )
				REM )
			REM )
			REM if exist pref1.txt (
				REM if not exist pref2.txt (
					REM if not exist pref3.txt (
						REM for /F "tokens=1" %%A IN ('pref1.txt') do set Original_Model=%%A
						REM if "%Model%" NEQ "%Original_Model%" (
							REM echo "%Model%">> pref2.txt
							REM echo %Build%>> pref2.txt
							REM echo "%RootFS%">> pref2.txt
							REM echo %RootFS_Key%>> pref2.txt
						REM )
					REM )
				REM )
			REM )
			REM if exist pref1.txt (
				REM if exist pref2.txt (
					REM if not exist pref3.txt (
						REM for /F "tokens=1" %%A IN ('pref1.txt') do set Original_Model=%%A
						REM if "%Model%" NEQ "%Original_Model%" (
							REM echo "%Model%">> pref3.txt
							REM echo %Build%>> pref3.txt
							REM echo "%rootfs%">> pref3.txt
							REM echo %RootFS_Key%>> pref3.txt
						REM )
					REM )
				REM )
			REM )
			
			::         Well, this leaves me with an interesting situation. I would like to echo all 
			::         of the user's device info (firmware keys, model, rootfs name) to one of three
			::         files dedicated to saving, then later use the 'FOR' command to retrieve the
			::         info later. The thing is, the iphone model has a comma in it (iPhone2,1 etc.)
			::         so batch seems to see the comma and end the echoing... which is quite strange.
			::         I have absolutely no idea how to echo a variable that contains a comma like
			::         an iPhone model properly. If you see this and you know how to solve this issue,
			::         do me a big favor and submit a pull request. You'll get credit as well. :)
			

	
	
	
:DA_REAL_STUFF
CLS
    cd ..
    ::%SYSTEMROOT%\System32\WindowsPowerShell\v1.0\powershell.exe -Command "(new-object System.Net.WebClient).DownloadFile('https://github.com/iH8sn0w/sn0wbreeze/releases/2.9.14/1077/sn0wbreeze-v2.9.14.zip', 'Sn0wbreeze.exe')"
	

	
echo Extracting IPSW
  cd Files
  7za.exe  x -oIPSW "%Model%_%Version%_%Build%_Restore.ipsw"  >NUL
		::extracts the IPSW to a folder named IPSW
  
  :Get
echo Getting the info we need from the files...
  cd IPSW
    echo Getting RootFS name
	  dir /b /o-s>{temp}
      set /P rootfs=<{temp}
	  del {temp}
	  
	  echo The rootfs name is %rootfs%

echo Decrypting Root Filesystem
  rename "%rootfs%" "rootfs.dmg"
  cd ..
  dmg extract "IPSW/rootfs.dmg" "IPSW/decrootfs.dmg" -k %RootFS_Key%  >NUL

echo Giving room to RootFS
  hfsplus "IPSW/decrootfs.dmg" grow 1920783616  
  
echo Extracting Cydia
  hfsplus "IPSW/decrootfs.dmg" untar "Cydia.tar" "/"  >NUL
  
echo Jailbreaking
  hfsplus "IPSW/decrootfs.dmg" untar "p0sixspwn.tar" "/" >NUL

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
  
REM echo Making directories
	REM hfsplus "IPSW/decrootfs.dmg" mkdir "var/root/Media/Cydia"
	REM hfsplus "IPSW/decrootfs.dmg" mkdir "var/root/Media/Cydia/AutoInstall"
	
REM echo Adding Siri
  REM hfsplus "IPSW/decrootfs.dmg" add BypassAuth.deb "var/root/Media/Cydia/AutoInstall"
  REM hfsplus "IPSW/decrootfs.dmg" add Ac!dSiri.deb "var/root/Media/Cydia/AutoInstall"
  
REM echo Adding Activator
	REM hfsplus "IPSW/decrootfs.dmg" add "libactivator_1.9.12_iphoneos-arm.deb" "var/root/Media/Cydia/AutoInstall"
	
echo Installing SSH
	hfsplus "IPSW/decrootfs.dmg" untar "ssh_small.tar" "/" >NUL
	
:next
echo Rebuilding RootFS
  copy dmg.exe IPSW >NUL
  cd IPSW
  dmg build "decrootfs.dmg" "%rootfs%"  >NUL



	
	
:end
echo.
echo.
echo.
echo.
echo              Your RootFS has been generated
echo.
echo            To use it, replace to RootFS dmg of a 
echo        Sn0wbreeze IPSW with the rootfs we generated.
echo       The Sn0wbreeze IPSW should have a root partition
echo                size of 2500 or more MegaBytes.
echo.
echo                    Eth3r by TKO_Cuber
echo.
pause

  ::Cue the final song... close the curtains...
  
  

