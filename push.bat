set branch_name=git branch --show-current
::echo %branch_name%
FOR /F "tokens=*" %%g IN ('%branch_name%') do (SET VAR=%%g)
::
echo %VAR%
pause
git config --global http.sslVerify false
git push origin %VAR%
pause