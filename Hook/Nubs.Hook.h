#ifndef DLL2_H
#define DLL2_H

///////////////////////////////////////////////////////////////////////////////
// This function is exported from the DLL2.dll
int __stdcall CALLBACK InstallShellHook(HWND hwnd, UINT uintMsg);
BOOL __stdcall CALLBACK UninstallShellHook(HWND hwnd);

#endif //DLL2_H
