@echo 开始注册
copy mfc100.dll %windir%\system32\
regsvr32 %windir%\system32\mfc100.dll /s
@echo mfc100.dll注册成功
@pause