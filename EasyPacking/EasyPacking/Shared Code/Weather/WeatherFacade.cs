using System;
using System.Collections;
using OCTVision.Frame.Common;
using OCTVision.Frame.Weather;

namespace OCTVision.Yancheng
{
	public class WeatherFacade
	{
		#region private Fields

		private static ArrayList _WeatherList = new ArrayList();

		#endregion

		public WeatherFacade ()
		{
		}

		#region public Methods

		public static void AsyncTryRefresh (Callback_Simple callback)
		{
			_WeatherList.Clear ();

			WeatherAPI_K780 api = new WeatherAPI_K780 ();
			api.WeatherID = CustomConst.YANCHENG_WEATHER_ID;
			api.AppKey = CustomConst.YANCHENG_WEATHER_APPKEY;
			api.Sign = CustomConst.YANCHENG_WEATHER_SIGN;

			api.AsyncTryRefresh ((result, data) => {
				if(result == ServiceResult.ER_OK) {
					_WeatherList = data;
				}

				callback (result);
			});
		}

		public static WeatherData GetWeather(int day)
		{
			if (day < 0 || day >= _WeatherList.Count) {
				throw new IndexOutOfRangeException ();
			}

			return (WeatherData)(_WeatherList [day]);
		}

		public static WeatherData GetToday()
		{
			return GetWeather (0);
		}
	
		#endregion
	}
}

