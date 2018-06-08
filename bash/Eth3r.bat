@echo off
title Gen-r8
color 0a
:: Just 'cuz you aren't a true hacker until everything is in lime green


cls

if not exist Files/done_ipsw ]]; then
	cls
	echo "Welcome to Eth3r!"
	echo " "
	echo " "
	echo "Please create a sn0wbreeze IPSW for your desired iOS version"
	echo "**and place it inside YOUR C:/ DRIVE**"
	echo "We will wait for you while you do so."
	echo " "
	echo "You will need Windows for Sn0wbreeze... sorry about that ¯\_(ツ)_/¯"
	echo " "
	echo "**PLEASE NOTE THAT THE SN0WBREEZE IPSW SHOULD HAVE A ROOT PARTITION SIZE OF 2500MB TO WORK!***"
	echo " "
	pause
	echo " "
	echo "." > Files/done_ipsw
fi


::Cue the magic, cue the logo, cue the.....  bash?

cls
echo " "
echo " "
echo 		 	         _______ _________          _______  _______
echo		  		        (  ____ \\__   __/^|\     /^|(  ____ \(  ____ )
echo				        ^| (    \/   ) (   ^| )   ( ^|^| (    \/^| (    )^|
echo	  			        ^| (__       ^| ^|   ^| (___) ^|^| (__    ^| (____)^|
echo		    	    	        ^|  __)      ^| ^|   ^|  ___  ^|^|  __)   ^|     __)
echo				        ^| (         ^| ^|   ^| (   ) ^|^| (      ^| (\ (
echo				        ^| (____/\   ^| ^|   ^| )   ( ^|^| (____/\^| ) \ \__
echo			    	    	(_______/   )_(   ^|/     \^|(_______/^|/   \__/
echo " "
echo " "
echo " "
echo '                                  by TKO-Cuber '
echo " "
echo " "
echo " "
echo '                                 Windows Beta '
echo '                                 Version 0.0.1 '
echo " "
echo " "
pause
cls
echo " "
echo " "
echo 		 	         _______ _________          _______  _______
echo		  		        (  ____ \\__   __/^|\     /^|(  ____ \(  ____ )
echo				        ^| (    \/   ) (   ^| )   ( ^|^| (    \/^| (    )^|
echo	  			        ^| (__       ^| ^|   ^| (___) ^|^| (__    ^| (____)^|
echo		    	    	        ^|  __)      ^| ^|   ^|  ___  ^|^|  __)   ^|     __)
echo				        ^| (         ^| ^|   ^| (   ) ^|^| (      ^| (\ (
echo				        ^| (____/\   ^| ^|   ^| )   ( ^|^| (____/\^| ) \ \__
echo			    	    	(_______/   )_(   ^|/     \^|(_______/^|/   \__/
echo " "
echo " "
echo " "
echo "                    What would you like to do today? "
echo " "
echo "		[1] Generate a custom IPSW "
echo "		[2] Pwn DFU "
echo "		[3] Pwn BootROM "
echo "		[4] Boot after restore"
echo "		[5] Verbose Boot "
echo " "
echo " "
set /p mode="/>/>/> "



timeout /t 3 >nul
echo " "
echo "We need to get some input from you..."
echo " "
echo "What are the firmware keys for your device and target iOS version?"
echo "You can find them here: https://goo.gl/3vof6m"
echo "When you have them, then"
read -rsp $'Press any key to continue.....\n' -n1 key
echo " "
echo "Enter Root Filesystem Key: "
read RootFS_Key
echo " "
echo "Cool. Now just a bit more info and we can get going."
echo " "
echo "Enter Device Model (iPhone2,1 or iPad4,2 etc): "
read Model
echo "Enter desired iOS version (6.1.3, 5.1.1, etc.): "
read Version
echo "Enter the build number for desired iOS version (10B329, 12C235, etc.): "
read Build
echo "What is the Root Filesystem .dmg name? (example: 048-2484-005.dmg): "
read rootfs
echo " "
echo " "
clear
echo "We will work with "$Model"_"$Version"_"$Build"_Restore.ipsw"
echo " "
sleep 3
cd Files

	echo "Extracting IPSW"
	wine 7za.exe  x -oIPSW "$Model"_"$Version"_"$Build"_Restore.ipsw

	echo "Decrypting Root Filesystem"
	  cd IPSW
	  mv $rootfs "rootfs.dmg"
	  cd ..
	  wine dmg extract "IPSW/rootfs.dmg" "IPSW/decrootfs.dmg" -k $RootFS_Key  #> /dev/null 2>&1

	echo "Giving room to RootFS"
	  wine hfsplus "IPSW/decrootfs.dmg" grow 1920783616

	if [[ "$Version" == "6.1.3" ]]; then
		echo "Extracting Cydia"
		  wine hfsplus "IPSW/decrootfs.dmg" untar "Cydia.tar" "/" > /dev/null 2>&1
		echo "Jailbreaking"
		  wine hfsplus "IPSW/decrootfs.dmg" untar "p0sixspwn.tar" "/" > /dev/null 2>&1
		echo "Branding"
		  wine hfsplus "IPSW/decrootfs.dmg" rm "System/Library/CoreServices/SpringBoard.app/SlideToSetupStrings.strings"
		  wine hfsplus "IPSW/decrootfs.dmg" add SlideToSetupStrings.strings "System/Library/CoreServices/SpringBoard.app/SlideToSetupStrings.strings"
		  wine hfsplus "IPSW/decrootfs.dmg" rm "System/Library/CoreServices/SpringBoard.app/English.lproj/SpringBoard.strings"
		  wine hfsplus "IPSW/decrootfs.dmg" add SpringBoard.strings "System/Library/CoreServices/SpringBoard.app/English.lproj/SpringBoard.strings"
		echo "Installing SSH"
			wine hfsplus "IPSW/decrootfs.dmg" untar "ssh_small.tar" "/" > /dev/null 2>&1
		echo "Bypassing iCloud Lock"
			  wine hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/Setup"
			  wine hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/PkgInfo"
			  wine hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/warranty.plist"
			  wine hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/Info.plist"
			  wine hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/CountryAlias.plist"
			  wine hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/_CodeSignature/CodeResources"
			  wine hfsplus "IPSW/decrootfs.dmg" rm "Applications/Setup.app/LanguagesByCountry.plist"
	fi

	if [[ "$Version" == "6.1.6" ]]; then
		echo "Jailbreaking"
			wine hfsplus "IPSW/decrootfs.dmg" untar "p0sixspwn.tar" "/" > /dev/null 2>&1
			echo "Installing SSH"
				hfsplus "IPSW/decrootfs.dmg" untar "ssh_small.tar" "/" > /dev/null 2>&1
		echo "Extracting Cydia"
			wine hfsplus "IPSW/decrootfs.dmg" untar "Cydia.tar" "/" > /dev/null 2>&1
	fi

	echo "Rebuilding Root Filesystem"
		cp dmg.exe IPSW
		cd IPSW
		wine dmg.exe build "decrootfs.dmg" $rootfs
		rm dmg.exe

	echo "Repacking RootFS into Sn0wbreeze IPSW"
	  cd ..
		echo "Extracting Sn0wbreeze IPSW"
		wine 7za.exe  x -oSN0 sn0wbreeze*.ipsw
		echo "Moving files"
		cd SN0
		rm $rootfs
		cd ..
		cd IPSW
		mv $rootfs ../SN0
		cd ..
		cp 7za.exe SN0
		cd SN0
		echo "zipping files"
		wine 7za.exe u -tzip -mx0 Eth3r_"$Model"_"$Version"_"$Build"_Restore.ipsw -x!7za.exe
		echo "moving final product"
		mv Eth3r_"$Model"_"$Version"_"$Build"_Restore.ipsw ../..
		cd ..
		echo " "
		echo " "
		echo " "
		echo "          Your IPSW has been generated and named 												 "
		echo "     'Eth3r_"$Model"_"$Version"_"$Build"_Restore.ipsw'                       "
		echo "         and is located in the main Eth3r folder.												 "
		echo " "
		echo "      To restore to this IPSW, enter pwned DFU mode"
		echo "     then restore in iTunes. Once that works, come back"
		echo "     to Eth3r, and use the 'boot after restore' option."
		echo " "
		echo " "
    read -rsp $'Press any key to exit\n' -n1 key
		exit

		: '
		if [[ "$mode" == "1" ]]; then
			echo Restoring to the new Eth3r IPSW
			echo " "
			echo " "
			./idevicerestore/src/idevicerestore -c Eth3r_"$Model"_"$Version"_"$Build"_Restore.ipsw
			echo " "
			if [[ "$Model" != "iPhone2,1" ]]; then
				echo "Your IPSW has been generated and restored to via idevicerestore."
				echo "This finishes the process you have selected."
				echo "Thank you for using Eth3r and have a good day/night."
				echo "         Eth3r by TKO-Cuber"
				sleep 5
				echo " "
				echo " "
				read -rsp $'Press any key to exit\n' -n1 key
				exit
			fi
			if [[ "$Model" == "iPhone2,1" ]]; then
					#Anyone order an extra large if statement? Well someone must have.
				if [[ "$Version" == "6.1.6" ]] || [[ "$Version" == "6.1.3" ]] || [[ "$Version" == "6.1" ]] || [[ "$Version" == "5.1.1" ]] || [[ "$Version" == "5.0.1" ]]; then
					echo " "
					echo "Because you are using an $Model on $Version, we will verbose boot your device now."
					echo " "
					cd xpwndfu/ipwndfu
					echo "Pwning DFU"
					echo " "
					sudo ./ipwndfu -p
					echo " "
					echo "Flashing NOR"
					echo " "
					sudo ./ipwndfu -f nor-backups/$iOS.dump
					echo " "
					echo "Pwning DFU again"
					echo " "
					sudo ./ipwndfu -p
					echo " "
					echo "Installing Alloc8 exploit to NOR"
					echo " "
					sudo ./ipwndfu -x
					echo " "
					echo 						       			"Verbose boot done."
					echo 			      	"This completes the process you selected."
					echo    "Thank you very much for using Eth3r! Hope to see you again soon!"
					echo " "
					echo "         							Eth3r by TKO-Cuber"
					cd ../../..
					echo " "
					echo " "
					read -rsp $'Press any key to exit\n' -n1 key
					exit
				fi
			fi
		fi
'

#idevicerestore isn't working right now... says cannot connect to DFU mode.
#maybe if i can fix idevicerestore then I can add support for immediate restoring.

