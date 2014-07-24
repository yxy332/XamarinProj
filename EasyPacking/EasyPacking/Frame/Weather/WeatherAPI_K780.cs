using System;
using System.Net;
using System.Json;
using System.IO;
using System.Collections;
using OCTVision.Frame.Common;

#if __IOS__
	using MonoTouch.UIKit;
#endif

namespace OCTVision.Frame.Weather
{
	public class WeatherAPI_K780
	{
		#region const Fields

		public const string BASE_URL = "http://api.k780.com:88/";

		#endregion

		#region private Fields

		private int _Weaid = 408;
		private string _Appkey = "11110";
		private string _Sign = "b7c1ac64c767765d27e18f20f28b8979";

		#endregion

		#region Delegates

		private Callback_ArrayList _ServiceCallback = null;

		#endregion

		#region Properties

		public int WeatherID {
			set{
				_Weaid = value;
			}
		}
		public string AppKey {
			set {
				_Appkey = value;
			}
		}
		public string Sign {
			set{
				_Sign = value;
			}
		}

		#endregion

		#region Constructors

		public WeatherAPI_K780 ()
		{
		}

		#endregion

		#region public Methods

		public void AsyncTryRefresh (Callback_ArrayList callback)
		{
			if (callback == null) {
				throw new NullReferenceException ();
			}

			#if __IOS__
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			#endif

			_ServiceCallback = callback;
			string url = BASE_URL + "?app=weather.future&weaid=" + _Weaid + "&appkey=" + _Appkey + "&sign=" + _Sign + "&format=json";
			HttpWebRequest request = WebRequest.CreateHttp (url);
			request.ContentType = "application/json";
			request.Method = "GET";
			request.BeginGetResponse (OnResult, request);
		}
	
		#endregion

		#region non-public Methods

		protected void OnResult(IAsyncResult result)
		{
			var request = result.AsyncState as HttpWebRequest;

			#if __IOS__
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			#endif

			using (HttpWebResponse response = request.EndGetResponse (result) as HttpWebResponse) {
				if (response.StatusCode != HttpStatusCode.OK) {
					Console.WriteLine ("response.StatusCode != HttpStatusCode.OK");
					_ServiceCallback (ServiceResult.ER_OK, null);
					
					return;
				}

				using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
					var content = reader.ReadToEnd ();
					Console.WriteLine (content);
					var JsonValue = JsonObject.Parse (content);

					if (JsonValue != null) {
						var resultArray = JsonValue ["result"];
						ArrayList data = new ArrayList ();

						for(int i = 0; i < resultArray.Count; ++i) {
							WeatherData tempData;
							tempData._weaid = resultArray [i] ["weaid"];
							tempData._citynm = resultArray [i] ["citynm"];
							tempData._days = resultArray [i] ["days"];
							tempData._simcode = resultArray [i] ["simcode"];
							tempData._temperature = resultArray [i] ["temperature"];
							tempData._weather = resultArray [i] ["weather"];
							tempData._weat_daytime_id = resultArray [i] ["weat_daytime_id"];
							tempData._weat_nighttime_id = resultArray [i] ["weat_night_id"];
							tempData._week = resultArray [i] ["week"];
							tempData._wind_direction = resultArray [i] ["wind_direction"];
							tempData._wind_power = resultArray [i] ["wind_power"];
							data.Add (tempData);
						}

						Console.WriteLine ("Count = " + data.Count);
						_ServiceCallback (ServiceResult.ER_OK, data);
					}
					else {
						_ServiceCallback(ServiceResult.ER_JSON_PARSE_FAILED, null);
					}
				}
			}
		}

		#endregion
	}
}

