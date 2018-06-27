@echo off
    for /f "skip=1 tokens=2" %%a in ('ideviceinfo') do (
        echo %%a
	)