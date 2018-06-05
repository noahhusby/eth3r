
# Eth3r [![Build Status](https://travis-ci.org/TKO-Cuber/eth3r.svg?branch=master)](https://travis-ci.org/TKO-Cuber/eth3r)

A custom firmware generator.
 
  **This is a beta program! We do not hold any responsibility for damage!!!**

# Download Links-

Windows: [**Eth3r**](https://github.com/TKO-Cuber/eth3r/releases/download/v0.0.2-beta_win/Eth3r-setup.exe)

Linux: [Eth3r-Linux](https://github.com/TKO-Cuber/eth3r/releases/download/v0.0.1-beta/Eth3r-Linux.zip)

*note: Windows is the primary version of Eth3r, and will be the most supported in the future*

# Instructions
	- For windows, download and run setup, then read the instructions page on Eth3r.
	
	-For Linux, follow the following:
	- Download this tool either from releases page or `git clone`ing it
	- Prepare a Sn0wbreeze IPSW for your device and desired iOS Version
		- **THIS IPSW HAS TO HAVE A ROOT PARTITION OF 2500 MB!!!**
	- Place this IPSW inside the Files folder of Eth3r
	- Run Eth3r
		- double click Eth3r.bat if on Windows
		- `sudo ./Eth3r-Linux` if on Linux
		- The Linux version is the primarily developed version... the one I work on the most.
	- Follow all instructions, and wait for the IPSW to be generated
	- After the Generation, pwn DFU using Sn0wbreeze
	- Shift+Click `Restore` in iTunes, and locate the Eth3r IPSW
	- Let the restore go through
	- Use `ipwndfu` and do `sudo ./ipwndfu -p` to enter pwned dfu mode
	- run `sudo ./ipwndfu -x` and the device should boot up normally!
  
  # To Do
  
  - Fix Ramdisk
  - Fix firmwares
  - Add support for more devices
  - Add support for verbose boot
  - More customization
  - Custom bootlogos
  - Add tweaks to be pre-installed
  
Oh yeah all credit to the respective owners for the tools (hfsplus, xpwntool, etc.) and to Saurik for Cydia.
All credit to planetbeing for xpwn... check it out here- https://github.com/planetbeing/xpwn/blob/master/LICENSE
  It is licensed under GPL-3.0
  
Also a huge credit to iH8Sn0w's Sn0wbreeze where this was heavily based off of. 


Oh yeah, the license. Here you go:


*  *  *  *  *

This software is licensed under the "Anyone But Stefan Esser“
(ABSE) license, described below. No other licenses may apply.


--------------------------------------------
The "Anyone But Stefan Esser" license
--------------------------------------------

Do anything you want with this program, with the exceptions listed
below under "EXCEPTIONS".

THIS SOFTWARE IS PROVIDED "AS IS" WITH NO WARRANTY OF ANY KIND.

In the unlikely event that you happen to make a zillion bucks off of
this, then good for you; consider buying a homeless person a meal.


EXCEPTIONS
----------

Stefan Esser (@i0n1c, that one angry german guy, etc.) may not make use of or
redistribute this program or any of its derivatives.
