using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace Consolation
{
    /// <summary>
    /// A console to display Unity's debug logs in-game.
    /// </summary>
    class ConsoleLog : MonoBehaviour
    {

        struct Log
        {
            public string message;
            public string stackTrace;
            public LogType type;

        }

        #region Inspector Settings

        /// <summary>
        /// The hotkey to show and hide the console window.
        /// </summary>
        public KeyCode toggleKey = KeyCode.BackQuote;

        /// <summary>
        /// Whether to open the window by shaking the device (mobile-only).
        /// </summary>
        public bool shakeToOpen = true;

        /// <summary>
        /// The (squared) acceleration above which the window should open.
        /// </summary>
        public float shakeAcceleration = 3f;

        /// <summary>
        /// Whether to only keep a certain number of logs.
        ///
        /// Setting this can be helpful if memory usage is a concern.
        /// </summary>
        public static bool restrictLogCount = false;

        /// <summary>
        /// Number of logs to keep before removing old ones.
        /// </summary>
        public static int maxLogs = 6000;

        #endregion

        static readonly List<Log> logs = new List<Log>();
        Vector2 scrollPosition;
        bool visible;
        bool collapse;

        // Visual elements:

        static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>
        {
            { LogType.Assert, Color.white },
            { LogType.Error, Color.red },
            { LogType.Exception, Color.red },
            { LogType.Log, Color.white },
            { LogType.Warning, Color.yellow },
        };

        const string windowTitle = "Console";
        const int margin = 20;
        static readonly GUIContent clearLabel = new GUIContent("Clear", "Clear the contents of the console.");
        static readonly GUIContent collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");



        readonly Rect titleBarRect = new Rect(0, 0, 10000, 20 * (Screen.height / 720.0f));
        Rect windowRect = new Rect(margin, margin, Screen.width - (margin * 2), Screen.height - (margin * 2));
        
        private void Awake()
        {
                DontDestroyOnLoad(gameObject);
        }
        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        Vector3 TouchPos = Vector3.zero;
        void Update()
        {

            if (Input.GetKeyDown(toggleKey))
            {
                visible = !visible;
            }


            if (shakeToOpen && Input.acceleration.sqrMagnitude > shakeAcceleration)
            {
                visible = !visible;
            }

            if (windowRect.Contains(Input.mousePosition))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    TouchPos = Input.mousePosition;
                }
                if (Input.GetMouseButton(0))
                {
                    Vector3 Pos = Input.mousePosition - TouchPos;
                    scrollPosition = new Vector2(0f, scrollPosition.y + Pos.y);
                    TouchPos = Input.mousePosition;
                }
            }
        }


        void OnGUI()
        {
                if (GUI.Button(new Rect((Screen.width * 0.5f) - GetRatioW(25), GetRatioH(10), GetRatioW(150), GetRatioH(50)), "Log"))
                {
                    visible = !visible;
                    if (visible)
                    {
                        ScrollToBottom();
                    }
                }

                if (!visible)
                {
                    return;
                }

                windowRect = GUILayout.Window(123456, windowRect, DrawConsoleWindow, windowTitle);
        }

        int GetRatioW(int iValue)
        {
            //if ( Screen.currentResolution.width)
            
            if (Screen.orientation == ScreenOrientation.Portrait)
            {
                return (int)(iValue * (Screen.width / 1280.0f)); //return (int)(iValue * (Screen.height / Screen.currentResolution.width));
            }
            return (int)(iValue * (Screen.width / 1280.0f)); //return (int)(iValue * (Screen.width / Screen.currentResolution.width));
        }

        int GetRatioH(int iValue)
        {
            //return (int)(iValue * (Screen.height / 720.0f));
            if (Screen.orientation == ScreenOrientation.Portrait)
            {
                return (int)(iValue * (Screen.height / 720.0f)); //return (int)(iValue * (Screen.width / Screen.currentResolution.width));
            }
            return (int)(iValue * (Screen.height / 720.0f)); //return (int)(iValue * (Screen.height / Screen.currentResolution.height));
        }

        /// <summary>
        /// Displays a window that lists the recorded logs.
        /// </summary>
        /// <param name="windowID">Window ID.</param>
        void DrawConsoleWindow(int windowID)
        {
            DrawLogsList();
            DrawToolbar();

            // Allow the window to be dragged by its title bar.
            GUI.DragWindow(titleBarRect);
        }

        /// <summary>
        /// Displays a scrollable list of logs.
        /// </summary>
        void DrawLogsList()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            // Used to determine height of accumulated log labels.
            GUILayout.BeginVertical();

            // Iterate through the recorded logs.
            for (var i = 0; i < logs.Count; i++)
            {
                var log = logs[i];

                // Combine identical messages if collapse option is chosen.
                if (collapse && i > 0)
                {
                    var previousMessage = logs[i - 1].message;

                    if (log.message == previousMessage)
                    {
                        continue;
                    }
                }


                GUI.contentColor = logTypeColors[log.type];

                StringBuilder Errlog = new StringBuilder();
                Errlog.Append("<size=30>");
                Errlog.Append(log.message);
                Errlog.Append("\n");
                Errlog.Append(log.stackTrace);
                Errlog.Append("</size>");

                GUILayout.Label(Errlog.ToString());

                //GUILayout.Label(log.stackTrace);
                if (log.type == LogType.Error)
                {
                    GUILayout.Label(Errlog.ToString());
                }
                if (log.type == LogType.Exception)
                {
                    GUILayout.Label(Errlog.ToString());
                }

            }

            GUILayout.EndVertical();
            var innerScrollRect = GUILayoutUtility.GetLastRect();
            GUILayout.EndScrollView();
            var outerScrollRect = GUILayoutUtility.GetLastRect();

            // If we're scrolled to bottom now, guarantee that it continues to be in next cycle.
            if (Event.current.type == EventType.Repaint && IsScrolledToBottom(innerScrollRect, outerScrollRect))
            {
                ScrollToBottom();
            }

            // Ensure GUI colour is reset before drawing other components.
            GUI.contentColor = Color.white;
        }

        /// <summary>
        /// Displays options for filtering and changing the logs list.
        /// </summary>
        void DrawToolbar()
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button(clearLabel, GUILayout.Height(40 * (Screen.height / 720.0f))))
            {
                logs.Clear();
            }

            if (GUILayout.Button("Close", GUILayout.Height(40 * (Screen.height / 720.0f))))
            {
                visible = !visible;
            }

            collapse = GUILayout.Toggle(collapse, collapseLabel, GUILayout.ExpandWidth(false));

            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Records a log from the log callback.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="stackTrace">Trace of where the message came from.</param>
        /// <param name="type">Type of message (error, exception, warning, assert).</param>
        public static void HandleLog(string message, string stackTrace, LogType type)
        {
            logs.Add(new Log
            {
                message = message,
                stackTrace = stackTrace,
                type = type,
            });

            TrimExcessLogs();

            //Debug.Log("LogType  " + type);

            /*string sendlog = string.Format( "NickName : {0} Uid : {1}, Log : {2}\n{3}", 
                UserInfoDatas.Instance._userInfoData._nickName,
                UserInfoDatas.Instance._userInfoData._uid,
                message, stackTrace);
                */
            if (type == LogType.Error || type == LogType.Exception)
            {
                //NetworkManager.Instance.SendErrorLog( sendlog );
            }

        }

        /// <summary>
        /// Determines whether the scroll view is scrolled to the bottom.
        /// </summary>
        /// <param name="innerScrollRect">Rect surrounding scroll view content.</param>
        /// <param name="outerScrollRect">Scroll view container.</param>
        /// <returns>Whether scroll view is scrolled to bottom.</returns>
        bool IsScrolledToBottom(Rect innerScrollRect, Rect outerScrollRect)
        {
            var innerScrollHeight = innerScrollRect.height;

            // Take into account extra padding added to the scroll container.
            var outerScrollHeight = outerScrollRect.height - GUI.skin.box.padding.vertical;

            // If contents of scroll view haven't exceeded outer container, treat it as scrolled to bottom.
            if (outerScrollHeight > innerScrollHeight)
            {
                return true;
            }

            var scrolledToBottom = Mathf.Approximately(innerScrollHeight, scrollPosition.y + outerScrollHeight);
            return scrolledToBottom;
        }

        /// <summary>
        /// Moves the scroll view down so that the last log is visible.
        /// </summary>
        void ScrollToBottom()
        {
            scrollPosition = new Vector2(0, Int32.MaxValue);
        }

        /// <summary>
        /// Removes old logs that exceed the maximum number allowed.
        /// </summary>
        public static void TrimExcessLogs()
        {
            if (!restrictLogCount)
            {
                return;
            }

            var amountToRemove = Mathf.Max(logs.Count - maxLogs, 0);

            if (amountToRemove == 0)
            {
                return;
            }

            logs.RemoveRange(0, amountToRemove);
        }
    }
}
