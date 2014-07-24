using System;
using System.Collections;

namespace OCTVision.Frame.Common
{
	#region enum define

	public enum ServiceResult {
		ER_OK = 0, 
		ER_UNEXPECTED_ERROR = 100,
		ER_JSON_PARSE_FAILED = 101, 
	};

	#endregion

	#region delegate define

	public delegate void Callback_Simple(ServiceResult result);
	public delegate void Callback_ArrayList(ServiceResult result, ArrayList data);

	#endregion

	#region const define

	public class StaticConst
	{

	}

	#endregion
}

