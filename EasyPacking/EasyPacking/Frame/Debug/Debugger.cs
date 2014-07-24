using System;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace OCTVision.Frame.Debug
{
	public enum LogLevel {
		LL_NONE = 0, 
		LL_ERROR = 1, 
		LL_WARN = 2, 
		LL_INFO = 3, 
		LL_ALL = 4
	};

	public static class Debugger
	{
		private static LogLevel m_log_level = LogLevel.LL_WARN;
		private static bool m_enable_stack = false;

		#region Properties
		public static LogLevel log_level {
			get { return m_log_level; }
			set { m_log_level = value; }
		}

		public static bool enable_stack {
			get { return m_enable_stack; }
			set { m_enable_stack = value; }
		}
		#endregion

		#region Methods
		private static void Log(LogLevel p_level, string p_content)
		{
			if ((int)m_log_level < (int)p_level) {
				return;
			}

			if (p_level == LogLevel.LL_ERROR || m_enable_stack) {
				StackFrame stack = new StackFrame (2, true);
				string file_path = stack.GetFileName ();
				string file_name = Path.GetFileName (file_path);
				string content = string.Format ("[{0}] <{1}:{2} {3}> {4}", DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss"), 
					file_name, stack.GetFileLineNumber(), stack.GetMethod().Name, p_content);
				Console.WriteLine (content);
			} else {
				string content = string.Format ("[{0}] {1}", DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss"), p_content);
				Console.WriteLine (content);
			}
		}

		public static void LogInfo(string p_content)
		{
			Log (LogLevel.LL_INFO, p_content);
		}

		public static void LogWarn(string p_content)
		{
			Log (LogLevel.LL_WARN, p_content);
		}

		public static void LogError(string p_content)
		{
			Log (LogLevel.LL_ERROR, p_content);
		}
		#endregion
	}
}
