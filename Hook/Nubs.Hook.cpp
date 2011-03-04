// DLL2.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "Nubs.Hook.h"

// Shared Data
#pragma data_seg("SHARDATA")
#pragma bss_seg("SHARDATA")

// Shared for WH_SHELL hook.
HHOOK hHookCBT = 0;
HHOOK hHookShell = 0;
HWND ShellWnd = NULL;
UINT ShellMsg = 0;

#pragma data_seg()

LRESULT CALLBACK ShellFunc (int nCode, WPARAM wParam, LPARAM lParam );

BOOL APIENTRY DllMain( HANDLE /*hModule*/, DWORD  ul_reason_for_call, LPVOID /*lpReserved*/) {
    
	switch (ul_reason_for_call) {
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			break;
	}

	return TRUE;
}


int __stdcall CALLBACK InstallShellHook(int hwnd, int uintMsg) {
	
	if (!hHookCBT) {
		hHookCBT = SetWindowsHookEx(WH_CBT, (HOOKPROC) ShellFunc, GetModuleHandle((LPCSTR)"NubHooks.dll"), 0);

		if (!hHookCBT) {
			return -1;
		}

		ShellWnd = (HWND)hwnd;
		ShellMsg = (UINT)uintMsg;
	}
	else {
		MessageBox(0, "Error: Hook Not Set", "NubHooks", 0);
	}

	return 0;
}

BOOL __stdcall CALLBACK UninstallShellHook(HWND hwnd) {
	UnhookWindowsHookEx(hHookCBT);
	hHookCBT = NULL;
	return(TRUE);
}

LRESULT CALLBACK ShellFunc (int nCode, WPARAM wParam, LPARAM lParam) {
	bool Handled = false;

	if (nCode >= 0) {

		switch (nCode) {
			case HCBT_ACTIVATE:
			case HCBT_DESTROYWND:  
			case HCBT_MOVESIZE: {
				PostMessage(ShellWnd, ShellMsg, nCode, wParam);
			}
			break;
		}
	};

	if (!Handled) {
		return CallNextHookEx(hHookCBT, nCode, wParam, lParam);
	}
	else {
		return NULL;
	}
}

