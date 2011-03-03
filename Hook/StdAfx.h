// stdafx.h : include file for standard system include files,
//  or project specific include files that are used frequently, but
//      are changed infrequently
//

#ifndef STDAFX_H
#define STDAFX_H

// Insert your headers here
#define WIN32_LEAN_AND_MEAN		// Exclude rarely-used stuff from Windows headers

#include <windows.h>
#include <mmsystem.h>
#include <Psapi.h>

#ifdef _DEBUG
#include <assert.h>
#define ATLASSERT(expr) assert(expr)
#endif

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif //STDAFX_H
