echo off

set added=0
set removed=0

for /f "tokens=1-3 delims= " %%A in ('git log --pretty^=tformat: --numstat --author^=%1') do call :Count %%A %%B %%C

echo added=%added%
echo removed=%removed%
goto :eof

:Count
  if NOT "%1" == "-" set /a added=%added% + %1
  if NOT "%2" == "-" set /a removed=%removed% + %2
goto :eof