using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
	
	public const int MAXElement = 50;
	public const string GroupFile = "Groups.txt";

	public const string ObjectFile = "3D_Objects.csv";
	public const string ActivityFile = "Activities.txt";
	public const string AssemblyQuantityFile = "AssemblyQuantity.txt";
	public const string MethodFile = "Methods2.txt";
	public const string SequenceFile = "Sequence.txt";
	public const string ActivityScheduleFile = "ActivitySchedule.txt";
	public const string SelectedActivityMethodFile = "SelectedActivityMethods.txt";
	public const string ResourceFile = "Resource.txt";
	public const string LaborResourcesFile = "LaborResources.txt";
	public const string EquipmentResourcesFile = "EquipmentResources.txt";

	public const string SequenceFirstObject = "SequenceFirstObject";
	public const string StartToStart = "StartToStart";
	public const string FinishToStart = "FinishToStart";

	public enum ConstructionStatus : int {NotStarted, InProgress, Complete};
}
