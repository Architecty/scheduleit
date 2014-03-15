using UnityEngine;
using System.Collections;
using System;

public class Simulation : MonoBehaviour {
	enum WeatherCondition : int {Sunny, Rainny};
	WeatherCondition weatherToday;
	WeatherCondition weatherTomorrow;

	DateTime simulationDay;
	DateTime simulationStartDay;


	// Use this for initialization
	void Start () {
		simulationStartDay = DateTime.Now;
		simulationDay = DateTime.Now;
		weatherToday = calculateWeather(simulationDay);
		weatherTomorrow = calculateWeather(simulationDay.AddDays(1.0));

	}
	
	// Update is called once per frame
	void Update () {
		updateSimulationDay();
		updateTemperature();
		showWeatherCondition();
	}

	void updateSimulationDay()
	{
		GameObject simulationDayObject = GameObject.Find ("DayLabel");
		UILabel dayLabel = simulationDayObject.GetComponentInChildren<UILabel>();
		dayLabel.text = "Day " + ((simulationDay - simulationStartDay).Add(new TimeSpan(1,0,0,0,0))).Days.ToString();
	}

	void updateTemperature()
	{
		GameObject temperatureObject = GameObject.Find ("TemperatureLabel");
		UILabel temperatureLabel = temperatureObject.GetComponentInChildren<UILabel>();
		if (weatherToday == WeatherCondition.Sunny) temperatureLabel.text = "Current Temperature: 72 F";
		else temperatureLabel.text = "Current Temperature: 60 F";
	}

	void showWeatherCondition()
	{
		GameObject weatherTodayObject = GameObject.Find("WeatherSprite");
		UISprite weatherTodaySprite = weatherTodayObject.GetComponentInChildren<UISprite>();
		GameObject weatherTomorrowObject = GameObject.Find("ForecastSprite");
		UISprite weatherTomorrowSprite = weatherTomorrowObject.GetComponentInChildren<UISprite>();

		if (weatherToday == WeatherCondition.Sunny) weatherTodaySprite.spriteName = "sunny";
		else weatherTodaySprite.spriteName = "cloudy";

		if (weatherTomorrow == WeatherCondition.Sunny) weatherTomorrowSprite.spriteName = "sunny";
		else weatherTomorrowSprite.spriteName = "cloudy";
	}

	private WeatherCondition calculateWeather(DateTime today)
	{
		if (today.Day % 4 == 0) return WeatherCondition.Rainny;
		else return WeatherCondition.Sunny;
	}
}
