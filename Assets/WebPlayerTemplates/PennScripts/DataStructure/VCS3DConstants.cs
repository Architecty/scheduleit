using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VCS;

namespace VCS
{
    public static class VCS3DConstants
    {
        //SHL: added more const variables. 02-18-2010
        public const string dbFileName = "VCS3D-Global.accdb"; //"VCS3D-Global_March2012.accdb";//
        public const string dbProjectInfoImage = "Project_info_image";
        public const string dbTableName3DObjects = "3D_Objects";
        public const string db3DObjectsName = "Object_name";
        public const string db3DObjectsStatus = "Construction_status";
        public const string dbTable3DObjects_ObjectType = "Object_type";
        public const string dbTable3DObjects_ObjectMaterial = "Object_material";
        public const string dbTable3DObjects_ObjectDimensions = "Object_dimension";
        public const string dbTable3DObjects_ObjectFileName = "Model_name";
        
        public const string dbTableNameObjectQuantity = "Object_quantity";
        public const string dbTableNameWorkBreakDown = "Work_breakdown";

        //Global Variables
        public const string dbTableNameGlobalVariables = "GlobalVariables";
        public const string dbTableGlobalVariables_VarName = "VarName";
        public const string dbTableGlobalVariables_DefValue = "DefValue";
        public const string dbTableGlobalVariables_UseLearningCurve = "UseLearningCurve";
        public const string dbTableGlobalVariables_UseFatigue = "UseFatigue";
        public const string dbTableGlobalVariables_UseWeather = "UseWeather";
        public const string dbTableGlobalVariables_UseCongestion = "UseCongestion";
        public const string dbTableGlobalVariables_UseRandomEquipmentBreakdown = "UseRandomEquipmentBreakdown";
        public const string dbTableGlobalVariables_UseProjectExperience = "UseProjectExperience";
        public const string dbTableGlobalVariables_TotalBudget = "TotalBudget";

        //Camera Variables
        public const string dbTableNameCameraVariables = "CameraVariables";
        public const string dbTableCameraVariables_VarName = "VarName";
        public const string dbTableCameraVariables_DefValue = "DefValue";
        public const string dbTableCameraVariables_CameraInitX = "CameraInitX";
        public const string dbTableCameraVariables_CameraInitY = "CameraInitY";
        public const string dbTableCameraVariables_CameraInitZ = "CameraInitZ";
        public const string dbTableCameraVariables_CameraInitPitch = "CameraInitPitch";
        public const string dbTableCameraVariables_CameraInitYaw = "CameraInitYaw";
        public const string dbTableCameraVariables_CameraInitRoll = "CameraInitRoll";
        public const string dbTableCameraVariables_CameraSouthX = "CameraSouthX";
        public const string dbTableCameraVariables_CameraSouthY = "CameraSouthY";
        public const string dbTableCameraVariables_CameraSouthZ = "CameraSouthZ";
        public const string dbTableCameraVariables_CameraSouthPitch = "CameraSouthPitch";
        public const string dbTableCameraVariables_CameraSouthYaw = "CameraSouthYaw";
        public const string dbTableCameraVariables_CameraSouthRoll = "CameraSouthRoll";
        public const string dbTableCameraVariables_CameraSouthEastX = "CameraSouthEastX";
        public const string dbTableCameraVariables_CameraSouthEastY = "CameraSouthEastY";
        public const string dbTableCameraVariables_CameraSouthEastZ = "CameraSouthEastZ";
        public const string dbTableCameraVariables_CameraSouthEastPitch = "CameraSouthEastPitch";
        public const string dbTableCameraVariables_CameraSouthEastYaw = "CameraSouthEastYaw";
        public const string dbTableCameraVariables_CameraSouthEastRoll = "CameraSouthEastRoll";
        public const string dbTableCameraVariables_CameraSouthWestX = "CameraSouthWestX";
        public const string dbTableCameraVariables_CameraSouthWestY = "CameraSouthWestY";
        public const string dbTableCameraVariables_CameraSouthWestZ = "CameraSouthWestZ";
        public const string dbTableCameraVariables_CameraSouthWestPitch = "CameraSouthWestPitch";
        public const string dbTableCameraVariables_CameraSouthWestYaw = "CameraSouthWestYaw";
        public const string dbTableCameraVariables_CameraSouthWestRoll = "CameraSouthWestRoll";
        public const string dbTableCameraVariables_CameraNorthX = "CameraNorthX";
        public const string dbTableCameraVariables_CameraNorthY = "CameraNorthY";
        public const string dbTableCameraVariables_CameraNorthZ = "CameraNorthZ";
        public const string dbTableCameraVariables_CameraNorthPitch = "CameraNorthPitch";
        public const string dbTableCameraVariables_CameraNorthYaw = "CameraNorthYaw";
        public const string dbTableCameraVariables_CameraNorthRoll = "CameraNorthRoll";
        public const string dbTableCameraVariables_CameraNorthEastX = "CameraNorthEastX";
        public const string dbTableCameraVariables_CameraNorthEastY = "CameraNorthEastY";
        public const string dbTableCameraVariables_CameraNorthEastZ = "CameraNorthEastZ";
        public const string dbTableCameraVariables_CameraNorthEastPitch = "CameraNorthEastPitch";
        public const string dbTableCameraVariables_CameraNorthEastYaw = "CameraNorthEastYaw";
        public const string dbTableCameraVariables_CameraNorthEastRoll = "CameraNorthEastRoll";
        public const string dbTableCameraVariables_CameraNorthWestX = "CameraNorthWestX";
        public const string dbTableCameraVariables_CameraNorthWestY = "CameraNorthWestY";
        public const string dbTableCameraVariables_CameraNorthWestZ = "CameraNorthWestZ";
        public const string dbTableCameraVariables_CameraNorthWestPitch = "CameraNorthWestPitch";
        public const string dbTableCameraVariables_CameraNorthWestYaw = "CameraNorthWestYaw";
        public const string dbTableCameraVariables_CameraNorthWestRoll = "CameraNorthWestRoll";
        public const string dbTableCameraVariables_CameraEastX = "CameraEastX";
        public const string dbTableCameraVariables_CameraEastY = "CameraEastY";
        public const string dbTableCameraVariables_CameraEastZ = "CameraEastZ";
        public const string dbTableCameraVariables_CameraEastPitch = "CameraEastPitch";
        public const string dbTableCameraVariables_CameraEastYaw = "CameraEastYaw";
        public const string dbTableCameraVariables_CameraEastRoll = "CameraEastRoll";
        public const string dbTableCameraVariables_CameraWestX = "CameraWestX";
        public const string dbTableCameraVariables_CameraWestY = "CameraWestY";
        public const string dbTableCameraVariables_CameraWestZ = "CameraWestZ";
        public const string dbTableCameraVariables_CameraWestPitch = "CameraWestPitch";
        public const string dbTableCameraVariables_CameraWestYaw = "CameraWestYaw";
        public const string dbTableCameraVariables_CameraWestRoll = "CameraWestRoll";
        public const string dbTableCameraVariables_CameraTopX = "CameraTopX";
        public const string dbTableCameraVariables_CameraTopY = "CameraTopY";
        public const string dbTableCameraVariables_CameraTopZ = "CameraTopZ";
        public const string dbTableCameraVariables_CameraTopPitch = "CameraTopPitch";
        public const string dbTableCameraVariables_CameraTopYaw = "CameraTopYaw";
        public const string dbTableCameraVariables_CameraTopRoll = "CameraTopRoll";

        //Method table
        public const string dbTableNameMethods = "Methods";
        public const string dbTableMethodActivityName = "Activity";
        public const string dbTableMethodAssemblyImage = "Assembly_Image";
        public const string dbTableMethodMethodName = "Method";

        public const string dbTableNameCrew = "Crew";
        
        //const strings for Constraints table
        public const string dbTableNameConstraints = "Constraints";
        public const string dbConstraintsObjectName = "Object_name";
        public const string dbConstraintsConstraintsName = "Constraints";
        
        public const string dbTableNameActTime0 = "ActTime0";
        public const string dbTableNameRelationshipsTime0 = "RelationshipsTime0";
        public const string dbTableNameResourcesTime0 = "ResourcesTime0";
        public const string dbTableNameResourceExperienceX = "ResourceExperienceX";
        public const string dbTableNameActivitiesLatest = "ActivitiesLatest";
        public const string dbTableNameResourcesLatest = "ResourcesLatest";

        //Resource Pool Table and its Columns
        public const string dbTableNameResourcesPool = "ResourcesPool";
        public const string dbTableResourcesPool_ResourceType = "ResourceType";
        public const string dbTableResourcesPool_ResourceName = "ResourceName";
        public const string dbTableResourcesPool_IsWorking = "IsWorking";
        public const string dbTableResourcesPool_TimeOnProject = "TimeOnProject";
        public const string dbTableResourcesPool_Fatigue = "Fatigue";
        public const string dbTableResourcesPool_DailyCost = "DailyCost";


        //resource management window
        public const string resourceManagementWindowAssemblyName = "Assembly";
        public const string resourceManagementWindowCrewSizeName = "CrewSize";
        public const string resourceManagementWindowActivityName = "Activity";
        public const string resourceManagementWindowMethodName = "Method";
        public const string resourceManagementWindowCrewName = "Crew";

        //sequencing window
        public const string sequencingWindowActivityNumber = "ActivityNumber";
        public const string sequencingWindowActivityName = "ActivityName";
        public const string sequencingWindowActivityPredecessor = "ActivityPredecessors";
        public const string sequencingWindowActivityDuration = "durationColumn";
        public const string sequencingWindowActivityImage = "Assembly_Type";

        //simulation window datagridview
        public const string simulationBuildingElements = "BuildingElement";
        public const string simulationActivity = "ConstructionActivity";
        public const string simulationID = "ID";
        public const string simulationPlannedStart = "PlannedStart";
        public const string simulationStart = "Start";
        public const string simulationCompletePercent = "CompletePercent";

        //Weather Conditions
        public enum WeatherCondition {SUNNY, RAIN};

        //Building Element Construction Status
        public enum ObjectConstructionStatus { NOTSTARTED, INPROGRESS, COMPLETED };

        //Activity Status
        public enum ActivityStatus { NOTSTARTED, INPROGRESS, COMPLETED };
    }
}
