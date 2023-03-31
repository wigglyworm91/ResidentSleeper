#include <windows.h>

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    static bool hovering = false;
    switch (msg)
    {
    case WM_CREATE:
        // Set window style to allow transparency
        SetWindowLong(hwnd, GWL_EXSTYLE, GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_LAYERED);

        // Set window to transparent
        SetLayeredWindowAttributes(hwnd, 0, 255, LWA_ALPHA);
        break;

    case WM_MOUSEMOVE:
        if (!hovering) {
            // change the transparency level of the window to make it nearly transparent
            SetLayeredWindowAttributes(hwnd, 0, 128, LWA_ALPHA);
            hovering = true;
        }
        break;

    case WM_MOUSELEAVE:
        if (hovering) {
            // restore the original transparency level of the window
            SetLayeredWindowAttributes(hwnd, 0, 255, LWA_ALPHA);
            hovering = false;
        }

    case WM_PAINT:
    {
        // Create black text with white border
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hwnd, &ps);
        SetTextColor(hdc, RGB(0, 0, 0));
        SetBkMode(hdc, TRANSPARENT);
        SetTextAlign(hdc, TA_CENTER | TA_TOP);
        HFONT hFont = CreateFont(20, 0, 0, 0, FW_NORMAL, FALSE, FALSE, FALSE, ANSI_CHARSET,
            OUT_TT_PRECIS, CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, DEFAULT_PITCH, "Arial");
        HFONT hOldFont = (HFONT)SelectObject(hdc, hFont);
        TextOut(hdc, 100, 50, "Hello, World!", 13);
        SelectObject(hdc, hOldFont);
        DeleteObject(hFont);
        EndPaint(hwnd, &ps);
        break;
    }

    case WM_DESTROY:
        PostQuitMessage(0);
        break;

    default:
        return DefWindowProc(hwnd, msg, wParam, lParam);
    }

    return 0;
}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
    WNDCLASSEX wc;
    HWND hwnd;
    MSG Msg;

    // Step 1: Registering the Window Class
    wc.cbSize = sizeof(WNDCLASSEX);
    wc.style = 0;
    wc.lpfnWndProc = WndProc;
    wc.cbClsExtra = 0;
    wc.cbWndExtra = 0;
    wc.hInstance = hInstance;
    wc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
    wc.hCursor = LoadCursor(NULL, IDC_ARROW);
    wc.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
    wc.lpszMenuName = NULL;
    wc.lpszClassName = "MyWindowClass";
    wc.hIconSm = LoadIcon(NULL, IDI_APPLICATION);

    if (!RegisterClassEx(&wc))
    {
        MessageBox(NULL, "Window Registration Failed!", "Error", MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    // Step 2: Creating the Window
    hwnd = CreateWindowEx(
        WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_TOOLWINDOW,   // extended window style
        "MyWindowClass",                                                        // window class name
        "Transparent Window",                                                   // window title
        WS_POPUP | WS_VISIBLE,                                                  // window style
        100, 100,                                                               // x, y position
        300, 300,                                                               // width, height
        NULL, NULL, hInstance, NULL);

    if (hwnd == NULL)
    {
        MessageBox(NULL, "Window Creation Failed!", "Error", MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    // Step 3: Display
// Make the window transparent and layered
    SetLayeredWindowAttributes(hwnd, 0, 255, LWA_ALPHA);

    // Step 4: The Message Loop
    while (GetMessage(&Msg, NULL, 0, 0) > 0)
    {
        TranslateMessage(&Msg);
        DispatchMessage(&Msg);
    }
    return Msg.wParam;
}