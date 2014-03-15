using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;
using System.Data.OleDb;


using System.IO;

using VCS;

namespace VCS
{
    static class VCS3DFunctions
    {
        //determines whether today's weather is sunny or rainny
        //temporarily we simply assume that the weather is rainny every four days
        public static VCS3DConstants.WeatherCondition calculateWeatherCondition(DateTime today)
        {
            if (today.Day % 4 == 0) return VCS3DConstants.WeatherCondition.RAIN;
            else return VCS3DConstants.WeatherCondition.SUNNY;
        }

        //create a connection to access database and return the connection
        //need full path to the database file
        public static OleDbConnection openAccessDatabase(string filename)
        {
            //string fullPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + filename;
            string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + filename;//fullPath;

            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection(connectionStatement);
                conn.Open();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("openAccessDatabase() failed: " + ex.Message);
                return conn;
            }

            return conn;
        }

        //close connection to access database
        public static bool closeAccessDatabase(OleDbConnection conn)
        {
            try
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError("VCS3DFunctions.closeAccessDatabase() failed: " + ex.Message);
                return false;
            }
        }


        public static bool executeSQLCommandToAccessDB(OleDbConnection conn, string commandStatement)
        {
            bool result = false;


            try
            {
                Debug.LogError("Not implemented yet");

            }
            catch (Exception ex)
            {
                Debug.LogError("ExecuteSQLCommandToAccessDB function failed: " + ex.Message);
                return false;
            }

            return result;
        }

        //Method to save a new table in Access database file
        //parameters: database file name and data table to be saved
        //SQL commands are used
        public static bool saveNewTableToAccess2(string filename, DataTable dataTable)
        {
			/* Could not find a way to save data in Access
            bool isSaved = true;
            string fullPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + filename;
            string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + fullPath;

            //create a string with column names and their times to create a new table
            string columnNamesAndTypes = "";
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                columnNamesAndTypes += dataTable.Columns[j].ColumnName + " " + "Char(80)";
                if (j != dataTable.Columns.Count - 1)
                    columnNamesAndTypes += ", ";
            }

            //create a new empty table 
            createNewTableInAccess(filename, dataTable.TableName, columnNamesAndTypes);

            //save data by creating a new row for each data set
            using (OleDbConnection conn = new OleDbConnection(connectionStatement))
            {
                try
                {
                    conn.Open();

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string insertStatement = createInsertCommandStatement(dataTable.Rows[i]);
                        OleDbCommand myCommand = new OleDbCommand(insertStatement, conn);
                        myCommand.ExecuteNonQuery();
                    }

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                    isSaved = false;
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }

            return isSaved;
            */
			return false;
        }

        //Method to save a new table in Access database file
        //parameters: OleDbConnection variable and data table to be saved
        //SQL commands are used
        public static bool saveNewTableToAccess2(OleDbConnection conn, DataTable dataTable)
        {
			/* Could not find a way to save data in Access
            bool isSaved = true;

            //create a string with column names and their times to create a new table
            string columnNamesAndTypes = "";
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                columnNamesAndTypes += dataTable.Columns[j].ColumnName + " " + "Char(80)";
                if (j != dataTable.Columns.Count - 1)
                    columnNamesAndTypes += ", ";
            }
            //create a new empty table 
            createNewTableInAccess(conn, dataTable.TableName, columnNamesAndTypes);

            //save data by creating a new row for each data set
            try
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string insertStatement = createInsertCommandStatement(dataTable.Rows[i]);
                    OleDbCommand myCommand = new OleDbCommand(insertStatement, conn);
                    myCommand.ExecuteNonQuery();
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("saveNewTableToAccess2() failed: " + ex.Message);
                isSaved = false;
            }

            return isSaved;
            */
			return false;
        }


        //Method to save a new table in Access database file
        //parameters: database file name and data table to be saved
        //SQL commands are used
        //A new (better) method (saveNewTableToAccess2) is implemented above. 
        public static bool saveNewTableToAccess(string filename, DataTable dataTable)
        {
			/* Could not find a way to save data in Access
            bool isDataSavedInTable = false;

            try
            {
                string columnNamesAndTypes = "";

                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    columnNamesAndTypes += dataTable.Columns[j].ColumnName + " " + "Char(80)";
                    if (j != dataTable.Columns.Count - 1)
                        columnNamesAndTypes += ", ";
                }

                createNewTableInAccess(filename, dataTable.TableName, columnNamesAndTypes);

                for (int j = 0; j < dataTable.Rows.Count; j++)
                {
                    saveNewDataRowInAccess(filename, dataTable.TableName, dataTable.Rows[j]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("VCS3DFunctions.saveNewTableToAccess() failed: " + ex.Message);
                isDataSavedInTable = false;
            }

            return isDataSavedInTable;
			 */
			return false;
        }

        //Method to save a new table in Access database file
        //parameters: database file name and DataSet, which can store multiple tables, to be saved
        //SQL commands are used
        public static bool saveNewTableToAccess(string filename, DataSet data)
        {
			/* Could not find a way to save data in Access
            bool isDataSavedInTable = false;

            try
            {
                //loop to save all the tables in the DataSet
                for (int i = 0; i < data.Tables.Count; i++)
                {
                    //save data in each table
                    string columnNamesAndTypes = "";

                    for (int j = 0; j < data.Tables[i].Columns.Count; j++)
                    {
                        columnNamesAndTypes += data.Tables[i].Columns[j].ColumnName + " " + "Char(80)";
                        if (j != data.Tables[i].Columns.Count - 1)
                            columnNamesAndTypes += ", ";
                    }


                    createNewTableInAccess(filename, data.Tables[i].TableName, columnNamesAndTypes);


                    for (int j = 0; j < data.Tables[i].Rows.Count; j++)
                    {
                        saveNewDataRowInAccess(filename, data.Tables[i].TableName, data.Tables[i].Rows[j]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("VCS3DFunctions.saveNewTableToAccess(string, DataSet) failed: " + ex.Message);
                isDataSavedInTable = false;
            }

            return isDataSavedInTable;
            */
			return false;
        }
        
        //Create Insert command statement
        private static string createInsertCommandStatement(DataRow dataRow)
        {
			/*
            string insertStatement = "";

            try
            {
                DataTable table = dataRow.Table;

                insertStatement = "INSERT INTO " + table.TableName + " ( ";

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    DataColumn crrColumn = table.Columns[i];
                    insertStatement += "[" + crrColumn.ColumnName + "]";
                    if (i == table.Columns.Count - 1)
                    {
                        insertStatement += ") ";
                    }
                    else
                    {
                        insertStatement += ", ";
                    }
                }

                insertStatement += "VALUES (";

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    insertStatement += "'" + dataRow[i].ToString().Replace("\"", "inch").Replace("'", "feet") + "'";
                    if (i == table.Columns.Count - 1)
                        insertStatement += ") ";
                    else
                        insertStatement += ", ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("VCS3DFunctions.createInsertCommandStatement() failed: " + ex.Message);
                insertStatement = "";
            }
          
            return insertStatement;
            */
			return "";
        }


        //Insert a new row to Access database
        public static bool saveNewDataRowInAccess(string filename, string tablename, DataRow dataRow)
        {
			/* Could not find a way to save data in Access yet
			 * 
            bool isSucceeded = false;
            string fullPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + filename;
            string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + fullPath;
            string insertStatement = createInsertCommandStatement(dataRow);

            using (OleDbConnection conn = new OleDbConnection(connectionStatement))
            {
                OleDbCommand myCommand = new OleDbCommand(insertStatement, conn);

                try
                {
                    conn.Open();
                    myCommand.ExecuteNonQuery();

                    isSucceeded = true;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("VCS3DFunctions.saveNewDataRowInAccess failed: " + ex.Message);
                    isSucceeded = false;
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            return isSucceeded;
			*/
			return false;
        }
        
        //Create new table in Access DB
        public static bool createNewTableInAccess(string filename, string tablename, string columnNamesandTypes)
        {
			/*
            bool isSucceeded = false;
            string fullPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + filename;
            string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + fullPath;
            string createTableStatement = "CREATE TABLE " + tablename + " (" + columnNamesandTypes + ")";

            using (OleDbConnection conn = new OleDbConnection(connectionStatement))
            {
                OleDbCommand myCommand = new OleDbCommand(createTableStatement, conn);

                try
                {
                    conn.Open();

                    //check if there is the table with the same name already
                    DataTable tableNames = conn.GetSchema("tables");
                    bool isTableExist = false;
                    for (int i = 0; i < tableNames.Rows.Count; i++)
                    {
                        if (tableNames.Rows[i]["TABLE_NAME"].ToString() == tablename)
                        {
                            isTableExist = true;
                            break;
                        }
                    }

                    //create a new table 
                    if (!isTableExist) //if table does not exist
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    else  //if table already exists. 
                    {
                        //Remove the existing table and create a new table with the data
                        OleDbCommand dropTableCommand = new OleDbCommand("DROP TABLE " + tablename, conn);
                        dropTableCommand.ExecuteNonQuery();
                        myCommand.ExecuteNonQuery();

                    }
                    isSucceeded = true;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                    isSucceeded = false;
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            return isSucceeded;
            */
			return false;
        }


        //create a new table in Access database file
        //parameters: OleDbConnection, table name, column names and their types
        public static bool createNewTableInAccess(OleDbConnection conn, string tablename, string columnNamesandTypes)
        {
			/*
            bool isSucceeded = false;
            string createTableStatement = "CREATE TABLE " + tablename + " (" + columnNamesandTypes + ")";

            try
            {
                //check if there is the table with the same name already
                DataTable tableNames = conn.GetSchema("tables");
                bool isTableExist = false;
                for (int i = 0; i < tableNames.Rows.Count; i++)
                {
                    if (tableNames.Rows[i]["TABLE_NAME"].ToString() == tablename)
                    {
                        isTableExist = true;
                        break;
                    }
                }

                //create a new table 
                if (!isTableExist) //if table does not exist
                {
                    OleDbCommand myCommand = new OleDbCommand(createTableStatement, conn);
                    myCommand.ExecuteNonQuery();
                }
                else  //if table already exists
                {
                    //delete the existing table
                    OleDbCommand dropTableCommand = new OleDbCommand("DROP TABLE " + tablename, conn);
                    dropTableCommand.ExecuteNonQuery();

                    //create a new table
                    OleDbCommand myCommand = new OleDbCommand(createTableStatement, conn);
                    myCommand.ExecuteNonQuery();

                }
                isSucceeded = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("VCS3DFunctions.createNewTableInAccess() failed: " + ex.Message);
                isSucceeded = false;
            }

            return isSucceeded;
            */
			return false;
        }
        
        //update database
        //parameter: filename = access database file name
        //parameter: tablename = target table name in the db file
        //parameter: targetcolumnname = target column name that stores the value to be updated
        //parameter: value = a new value
        //parameter: selectcolumnname = column name to find data name
        //parameter: dataname = data name of the value that needs to be updated
        public static bool updateAccessData(string filename, string tablename, string targetcolumnname, string value, string selectcolumnname, string dataname)
        {
			/*
            bool isSucceeded = false;
            string fullPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + filename;
            
            //form a statement to connect to the database file
            string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + fullPath;
            
            //form a update statement 
            string updateStatement = "UPDATE " + tablename + " SET [" + targetcolumnname + "] = @value" + " WHERE " + selectcolumnname + " = @selectname";

            //create a connection and update the data with the new value
            using (OleDbConnection conn = new OleDbConnection(connectionStatement))
            {
                OleDbCommand myCommand = new OleDbCommand(updateStatement, conn);
                myCommand.Parameters.AddWithValue("@value", value);
                myCommand.Parameters.AddWithValue("@selectname", dataname);

                try
                {
                    conn.Open();
                    myCommand.ExecuteNonQuery();

                    isSucceeded = true;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                    isSucceeded = false;
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }

            return isSucceeded;
            */
			return false;

        }

        //method to update value with connection variable
        public static bool updateAccessData(OleDbConnection conn, string tablename, string targetcolumnname, string value, string selectcolumnname, string dataname)
        {
            bool isSucceeded = false;
            
			/*
            //form a update statement
            string updateStatement = "UPDATE " + tablename + " SET [" + targetcolumnname + "] = @value" + " WHERE " + selectcolumnname + " = @selectname";


            try
            {
                //create a oledb command with the update statement
                OleDbCommand myCommand = new OleDbCommand(updateStatement, conn);

                //assign value to the data parameter
                myCommand.Parameters.AddWithValue("@value", value);
                myCommand.Parameters.AddWithValue("@selectname", dataname);
                //execute the command
                myCommand.ExecuteNonQuery();

                isSucceeded = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("updateAccessData function failed: " + ex.Message);
                isSucceeded = false;
            }
            finally
            {
            }
        */
            //return true if successfully update the value
            //return false if not
            return isSucceeded;

        }

        //retrieve data from AccessDatabase
        //parameter filename = access database file name
        //parameter tablename = table name to retrieve data from
        //parameter resultname = name for the result dataset 
        //parameter where = criteria to filter by
        public static DataSet retrieveAccessDataSet(string filename, string tablename, string resultname, string where)
        {
			/*
            DataSet resultDataSet = new DataSet(resultname);

            string fullPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + filename;

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + fullPath;
            string sql = "SELECT * FROM [" + tablename + "]";

            if (where != "") sql += " WHERE " + where;


            OleDbConnection conn = new OleDbConnection(connectionString);

            try
            {
                conn.Open();
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(sql, conn);
                myDataAdapter.Fill(resultDataSet, resultname);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("VCS3DFunctions.retrieveAccessDataSet() Null reference exception: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Retrieving Access Database. " + ex.Message);
                MessageBox.Show(sql);
                return null;
            }
            finally
            {
                conn.Close();
            }
            
            return resultDataSet;
            */
			return null;
        }

        //retrieve data from AccessDatabase
        //parameter conn = OleDbConnection variable to access the access database file
        //parameter tablename = table name to retrieve data from
        //parameter resultname = name for the result dataset 
        //parameter where = criteria to filter by
        public static DataSet retrieveAccessDataSet(OleDbConnection conn, string tablename, string resultname, string where)
        {
			/*
            DataSet resultDataSet = new DataSet(resultname);

            //select all the data (rows) from the table if the "where" condition meets
            string sql = "SELECT * FROM [" + tablename + "]";
            if (where != "") sql += " WHERE " + where;

            try
            {
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(sql, conn);
                myDataAdapter.Fill(resultDataSet, resultname);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Null reference exception");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("retrieveAccessDataSet() failed: " + ex.Message);
                //MessageBox.Show(sql);
                //conn.Close();
                return null;
            }
            finally
            {
                //conn.Close();
            }

            return resultDataSet;
            */
			return null;
        }



        //CPM foreward calculation for earliest starting time of each activity
        public static void CPMForewardCalc2(ref List<VCSConstructionActivity> activityList, bool isSimulation, List<int> activityIndexList)
        {
            try
            {
                //As the difference between task number and the list index is 1
                for (int i = 0; i < activityIndexList.Count; i++)
                    activityIndexList[i] -= 1;

                // SHL: first find the activities that has no predecessors
                for (int i = 0; i < activityIndexList.Count; i++)
                {
                    //MessageBox.Show(activityIndexList[i].ToString());
                    int index = activityIndexList[i];
                    List<int> predecessorList = activityList[index].predecessorList;

                    // SHL: if the activity has no predecessors
                    // SHL: set the earliest starting time to 0
                    if (predecessorList.Count == 0)
                    {
                        if (isSimulation)
                        {
                            activityList[index].AsBuiltEST = 0;
                            activityList[index].AsBuiltEET = activityList[index].AsBuiltEST + activityList[index].AsBuiltDuration;
                        }
                        else
                        {
                            activityList[index].AsPlannedEST = 0;
                            activityList[index].AsPlannedEET = activityList[index].AsPlannedEST + activityList[index].AsPlannedDuration;
                        }
                    }
                }

                //for activities that has predecessors
                for (int i = 0; i < activityIndexList.Count; i++)
                {
                    int index = activityIndexList[i];
                    List<int> predecessorList = new List<int>();
                    for (int j = 0; j < activityList[index].predecessorList.Count; j++)
                        predecessorList.Add(activityList[index].predecessorList[j]);
                    //MessageBox.Show("Activity Index: " + index.ToString() + " Predecessor Count: " + predecessorList.Count.ToString());

                    if (predecessorList.Count == 0) continue;
                    //MessageBox.Show("Predecessor: " + predecessorList[0].ToString());

                    CPMForewardCalc2(ref activityList, false, predecessorList);

                    for (int j = 0; j < predecessorList.Count; j++)
                    {
                        if (isSimulation)
                        {
                            if (activityList[index].AsBuiltEST < activityList[predecessorList[j]].AsBuiltEET)
                            {
                                activityList[index].AsBuiltEST = activityList[predecessorList[j]].AsBuiltEET;
                            }
                        }
                        else
                        {
                            if (activityList[index].AsPlannedEST < activityList[predecessorList[j]].AsPlannedEET)
                            {
                                activityList[index].AsPlannedEST = activityList[predecessorList[j]].AsPlannedEET;
                            }
                        }
                    }

                    if (isSimulation)
                        activityList[index].AsBuiltEET = activityList[index].AsBuiltEST + activityList[index].AsBuiltDuration;
                    else
                        activityList[index].AsPlannedEET = activityList[index].AsPlannedEST + activityList[index].AsPlannedDuration;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("VCS3DFunctions.CPMForewardCalc2() failed: " + ex.Message); 
            }

        }
    }
}
