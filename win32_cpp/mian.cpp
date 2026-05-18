#include <windows.h>
#include <shlwapi.h>
#include <shlobj.h>
#pragma comment(lib, "shlwapi.lib")
#pragma comment(lib, "shell32.lib")
#pragma comment(linker,"\"/manifestdependency:type='win32' \
name='Microsoft.Windows.Common-Controls' version='6.0.0.0' \
processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
#define CHANGE_LOCAION_BUTTON 1
#define REMOVE_AD 2

const TCHAR* Original_Office_Path {
    R"(C:\Program Files\Microsoft Office)"
};

BOOL IsRunningAsAdmin() {
    BOOL isAdmin = FALSE;
    PSID adminGroup = NULL;

    SID_IDENTIFIER_AUTHORITY ntAuthority = SECURITY_NT_AUTHORITY;
    if (AllocateAndInitializeSid(&ntAuthority, 2, SECURITY_BUILTIN_DOMAIN_RID,
        DOMAIN_ALIAS_RID_ADMINS, 0, 0, 0, 0, 0, 0, &adminGroup)) {
        if (!CheckTokenMembership(NULL, adminGroup, &isAdmin)) {
            isAdmin = FALSE;
        }
        FreeSid(adminGroup);
    }

    return isAdmin;
}


TCHAR* GetPath(HWND hwnd)
{
    TCHAR* selectedPath{};  // 默认空字符串表示失败/取消

    HRESULT hr = CoInitialize(NULL);
    if (FAILED(hr))
    {
        MessageBox(hwnd, "COM 初始化失败", "错误", MB_ICONERROR);
        return selectedPath;
    }

    //默认路径
    TCHAR szDefaultPath[MAX_PATH]{"C:\\"};
    TCHAR szDisplayName[MAX_PATH]{};

    BROWSEINFO bi{};
    bi.hwndOwner = hwnd;
    bi.pidlRoot = NULL;
    bi.pszDisplayName = szDisplayName;
    bi.lpszTitle = "请选择Office安装更改后的文件夹";
    bi.ulFlags = BIF_RETURNONLYFSDIRS | BIF_NEWDIALOGSTYLE;
    bi.lpfn = NULL;
    bi.lParam = (LPARAM)szDefaultPath;

    //显示对话框
	LPITEMIDLIST pidl = SHBrowseForFolder(&bi);
    
    if (pidl != NULL)
    {
        TCHAR szPath[MAX_PATH];
        if (SHGetPathFromIDList(pidl, szPath))
        {
            selectedPath = szPath;
        }
        else
        {
            MessageBox(hwnd, "无法获取路径", "错误", MB_ICONERROR);
        }
        CoTaskMemFree(pidl);
    }
    else
    {
        MessageBox(hwnd, "取消选择", "提示", MB_ICONWARNING);
    }

    CoUninitialize();
    return selectedPath;
}

void chang_office_location(HWND hwnd)
{
    if (PathFileExists(Original_Office_Path))
    {
        MessageBox(hwnd, "发现默认安装位置已存在文件夹！", "错误", MB_ICONERROR);
    }
    else
    {
        TCHAR* path = GetPath(hwnd);
        if (path == NULL)
        {
            return;
        }
        TCHAR office_path[MAX_PATH];
        PathCombine(office_path, path, "Microsoft Office");
        CreateDirectory(office_path, NULL);
        BOOLEAN temp = CreateSymbolicLink(Original_Office_Path, office_path, SYMBOLIC_LINK_FLAG_DIRECTORY);
        if (temp)
        {
            MessageBox(hwnd, "完成安装位置设置", "提示", MB_ICONASTERISK);
        }
        else
        {
            MessageBox(hwnd, "创建符号链接失败！请确保以管理员身份运行此程序。", "错误", MB_ICONERROR);
        }
    }
}


LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch (uMsg)
    {
        case WM_DESTROY:
        {
            PostQuitMessage(0);
            return 0;
        }
        case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hwnd, &ps);
            FillRect(hdc, &ps.rcPaint, (HBRUSH)(COLOR_WINDOW + 1));
            EndPaint(hwnd, &ps);
        }
        case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            switch (wmId)
            {
                case CHANGE_LOCAION_BUTTON:
                {
                    chang_office_location(hwnd);
                    break;
                }
                case REMOVE_AD:
                {
                    HKEY hKey;
                    if (RegCreateKeyEx(
                        HKEY_LOCAL_MACHINE,
                        R"(SOFTWARE\Microsoft\MSOfficePLUS)",
                        0, NULL, REG_OPTION_NON_VOLATILE,
                        KEY_SET_VALUE, NULL, &hKey, NULL) == ERROR_SUCCESS)
                    {
                        RegSetValueEx(hKey, "InstallSuccess", 0, REG_SZ,
                            (const BYTE*)L"", sizeof(wchar_t));
                        RegCloseKey(hKey);
                        MessageBox(hwnd, "完成设置", "提示", MB_ICONASTERISK);
                    }
                    else
                    {
                        MessageBox(hwnd, "操作注册表失败！请确保以管理员身份运行此程序。", "错误", MB_ICONERROR);
                    }
                    break;
                }
            }
        }
        default:
			return DefWindowProc(hwnd, uMsg, wParam, lParam);
    }
}

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PWSTR pCmdLine, int nCmdShow)
{
    int argc{0};
    LPWSTR* argv = CommandLineToArgvW(GetCommandLineW(), &argc);

    const LPCSTR CLASS_NAME{"shubaobao"};

    WNDCLASS wc{};

    wc.lpfnWndProc = WindowProc;
    wc.hInstance = hInstance;
    wc.lpszClassName = CLASS_NAME;
    RegisterClass(&wc);

    HWND hwnd = CreateWindowEx(
        0,                              // Optional window styles.
        CLASS_NAME,                     // Window class
        "Office工具",    // Window text
        WS_OVERLAPPEDWINDOW & ~WS_MAXIMIZEBOX & ~WS_THICKFRAME,            // Window style
        // Size and position
        CW_USEDEFAULT, CW_USEDEFAULT, 500, 340,
        NULL,       // Parent window    
        NULL,       // Menu
        hInstance,  // Instance handle
        NULL        // Additional application data
    );

    if (hwnd == NULL)
    {
        MessageBox(hwnd, "无法启动窗口", "错误", MB_ICONERROR);
        return 0;
    }

    if (argv != NULL)
    {
        for (int i = 0; i < argc; i++)
        {
            if (wcscmp(argv[i], L"-dev") == 0)
            {
                AllocConsole();
                break;
            }
        }
        LocalFree(argv);
    }

    HWND hwndButton_ONE = CreateWindow(
        "BUTTON",
        "更改Office安装位置",
        WS_TABSTOP | WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON,
        160,         // x position 
        80,         // y position 
        180,        // Button width
        50,        // Button height
        hwnd,
        (HMENU)CHANGE_LOCAION_BUTTON,
        (HINSTANCE)GetWindowLongPtr(hwnd, GWLP_HINSTANCE),
        NULL);

    HWND hwndButton_TWO = CreateWindow(
        "BUTTON",
        "阻止OfficePLUS自动安装",
        WS_TABSTOP | WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON,
        160,         // x position 
        160,         // y position 
        180,        // Button width
        50,        // Button height
        hwnd,
        (HMENU)REMOVE_AD,
        (HINSTANCE)GetWindowLongPtr(hwnd, GWLP_HINSTANCE),
        NULL);

    HANDLE hMutex = CreateMutex(NULL, FALSE, "shubaobao");
    if (GetLastError() == ERROR_ALREADY_EXISTS)
    {
        MessageBox(NULL, "程序已经在运行中！", "提示", MB_ICONINFORMATION);
        CloseHandle(hMutex);
        return 0;
    }

    ShowWindow(hwnd, nCmdShow);
    SetWindowDisplayAffinity(hwnd, WDA_EXCLUDEFROMCAPTURE);

    if (!IsRunningAsAdmin())
    {
        MessageBox(hwnd, "没有管理员权限", "提示", MB_ICONWARNING);
    }

    MSG msg{ };
    while (GetMessage(&msg, NULL, 0, 0) > 0)
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }
}
