using UnityEngine;
using System.Collections;
using System;

public class VCSActivity {

	private string name;
	private string method;
	private string assemblyGroupType;
	private string assemblyGroupName;
	private string crew;
	private int asPlannedCrewSize;
	private int asBuiltCrewSize;
	private double unitDuration;
	private double currentDuration;
	private int unitCost;
	private int currentCost;
	private DateTime asPlannedStartDateTime;
	private DateTime asBuiltStartDateTime;
	Constants.ConstructionStatus currentConstructionStatus;

	public VCSActivity()
	{
		name = "";
		method = "";
		assemblyGroupType = "";
		assemblyGroupName = "";
		crew = "";
		asPlannedCrewSize = 0;
		asBuiltCrewSize = 0;
		unitDuration = 0;
		unitCost = 0;
		currentDuration = 0;
		currentCost = 0;
		asPlannedStartDateTime = new DateTime();
		asBuiltStartDateTime = new DateTime();
		currentConstructionStatus = Constants.ConstructionStatus.NotStarted;
	}

	public VCSActivity(string newName, string newMethod, 
	                   string newAssemblyGroupType, string newAssemblyGroupName,
	                   string newCrew, int newAsPlannedCrewSize, int newAsBuiltCrewSize,
	                   double newUnitDuration, double newCurrentDuration, 
	                   int newUnitCost, int newCurrentCost,
	                   DateTime newAsPlannedStartDateTime, DateTime newAsBuiltStartDateTime)
	{
		name = newName;
		method = newMethod;
		assemblyGroupType = newAssemblyGroupType;
		assemblyGroupName = newAssemblyGroupName;
		crew = newCrew;
		asPlannedCrewSize = newAsPlannedCrewSize;
		asBuiltCrewSize = newAsBuiltCrewSize;
		unitDuration = newUnitDuration;
		unitCost = newUnitCost;
		currentDuration = newCurrentDuration;
		currentCost = newCurrentCost;
		asPlannedStartDateTime = newAsPlannedStartDateTime;
		asBuiltStartDateTime = newAsBuiltStartDateTime;
		currentConstructionStatus = Constants.ConstructionStatus.NotStarted;
	}

	public string Name {get; set;}
	public string Method {get; set; }
	public string AssemblyGroupType {get; set; }
	public string AssemblyGroupName {get; set; }
	public string Crew {get; set; }
	public int AsPlannedCrewSize {get; set; }
	public int AsBuiltCrewSize {get; set; }
	public double UnitDuration {get; set; }
	public int UnitCost {get; set; }
	public double CurrentDuration {get; set; }
	public int CurrentCost {get; set; }
	public Constants.ConstructionStatus CurrentConstructionStatus {get; set; }
	public DateTime AsPlannedStartDateTime {get; set; }
	public DateTime AsBuiltStartDateTime { get; set; }
}
