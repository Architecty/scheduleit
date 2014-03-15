/* COPY RIGHT 
 * Copyright © 2010 Sang Hoon Lee, Lorne Leonard, Dragana Nikolic, and John I. Messner
 * Computer-Integrated Construction Program
 * Department of Architectural Engineering
 * Pennsylvania State University
 *
 * Contact: 
 * Sang Hoon Lee: shlatpsu@gmail.com
 * Dragana Nikolic: dragana@psu.edu
 * John Messner: jmessner@engr.psu.edu
 *
 * This program is developed as a outcome of the research project, 
 * "Virtual Construction Simulator 3D: A Simulation Game for Construction Engineering Education"
 * supported by National Science Foundation. 
 * 
 * This program is distributed under the GNU General Public License (see below), 
 * provided that the following conditions are met: 
 * 1. A program that uses this source code must be open source and shared with other developers and
 * researchers for further development of construction simulation. 
 * 2. This source code must not be used to create commercial products.
 * 
 * The authors also want to receive any program and its source code that is built upon this source code 
 * so that this program can be updated with additional features
 * If you do not agree, do not download, install, use, modify this program.
 *
 *
 **** GNU General Public License ***
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *** End of GNU General Public License ***
 */

using UnityEngine;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;


using System.IO;

using VCS;

//VCS namespace for the object model used for the VCS3D 
namespace VCS
{
    //VCSDatabaseFunctions class
    //collection of database-related methods, including create/delete table, open/close connection, 
    //retrieve, update, save data
    static class VCSDatabaseFunctions
    {

        //update database
        //this method opens the access database file using its file name, update the given value and close the connection to the db file
        public static bool updateAccessData(string filename, string tablename, string targetcolumnname, string value, string selectcolumnname, string dataname)
        {
            bool isSucceeded = false;
			
			/*
            string fullPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + filename;
            
            //Connection statement to access MS Access database
            string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + fullPath;
            
            //SQL update statement
            string updateStatement = "UPDATE " + tablename + " SET [" + targetcolumnname + "] = @value" + " WHERE " + selectcolumnname + " = @selectname";

            //create connection variable
            using (OleDbConnection conn = new OleDbConnection(connectionStatement))
            {
                //set value to update
                OleDbCommand myCommand = new OleDbCommand(updateStatement, conn);
                myCommand.Parameters.AddWithValue("@value", value);
                myCommand.Parameters.AddWithValue("@selectname", dataname);

                //open the connection and execute the update command
                try
                {
                    conn.Open();
                    myCommand.ExecuteNonQuery();

                    isSucceeded = true;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("VCSDatabaseFunctions.updateAccessData() failed: " + ex.ToString());
                    isSucceeded = false;
                }
                finally
                {
                    //close the connection
                    if (conn != null) conn.Close();
                }
            }
			*/
            return isSucceeded;

        }

        //update access data using connection variable
        public static bool updateAccessData(OleDbConnection conn, string tablename, string targetcolumnname, string value, string selectcolumnname, string dataname)
        {
            bool isSucceeded = false;
			/*
            //set if connection is initially open or not
            bool isConnectionOpen = (conn.State == ConnectionState.Open) ? true : false;

            //create SQL update statement
            string updateStatement = "UPDATE " + tablename + " SET [" + targetcolumnname + "] = @value" + " WHERE " + selectcolumnname + " = @selectname";

            if (!isConnectionOpen) conn.Open();

            try
            {
                //create update command, set the value and execute the command
                OleDbCommand myCommand = new OleDbCommand(updateStatement, conn);
                myCommand.Parameters.AddWithValue("@value", value);
                myCommand.Parameters.AddWithValue("@selectname", dataname);
                myCommand.ExecuteNonQuery();

                isSucceeded = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("VCSDatabaseFunctions.updateAccessData() failed: " + ex.Message);
                isSucceeded = false;
            }
            finally
            {
                if (!isConnectionOpen) conn.Close();
            }
			*/
            return isSucceeded;

        }

        //Method to save a new table to the access database
        public static bool saveNewTableToAccess(OleDbConnection conn, DataTable dataTable)
        {
            bool isSaved = false;
			
			/*
            //store if the connection is initially open or not
            bool isInitConnectionOpen = (conn.State == ConnectionState.Open) ? true : false;

            //create a string with all the column names and their types to create columns in the table
            string columnNamesAndTypes = "";
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                columnNamesAndTypes += dataTable.Columns[j].ColumnName + " " + "Char(80)";
                if (j != dataTable.Columns.Count - 1)
                    columnNamesAndTypes += ", ";
            }

            //create a new table
            createNewTableInAccess(conn, dataTable.TableName, columnNamesAndTypes);

            try
            {
                //if connection is not open, open it
                if (!isInitConnectionOpen) conn.Open();

                //save data into the created new table
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string insertStatement = createInsertCommandStatement(dataTable.Rows[i]);
                    OleDbCommand myCommand = new OleDbCommand(insertStatement, conn);
                    myCommand.ExecuteNonQuery();
                }

                isSaved = true;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("VCSDatabaseFunctions.saveNewTableToAccess() failed: " + ex.ToString());
                isSaved = false;
            }
            finally
            {
                //if the connection was initially not open, close it 
                if (!isInitConnectionOpen) conn.Close();
            }

			 */
            return isSaved;
        }

        //method to save a new table into the access file with the filename
        public static bool saveNewTableToAccess(string filename, DataTable dataTable)
        {
            bool isSaved = false;
			
			/*
            //path to access the database file
            string fullPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + filename;
            //connection statement to connect the database
            string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + fullPath;

            //create a string of column names and their types to create a new table
            string columnNamesAndTypes = "";
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                columnNamesAndTypes += dataTable.Columns[j].ColumnName + " " + "Char(80)";
                if (j != dataTable.Columns.Count - 1)
                    columnNamesAndTypes += ", ";
            }

            //create a table
            createNewTableInAccess(filename, dataTable.TableName, columnNamesAndTypes);

            //create an actual connection
            using (OleDbConnection conn = new OleDbConnection(connectionStatement))
            {
                try
                {
                    //open the connection
                    conn.Open();

                    //store data into the new database table
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string insertStatement = createInsertCommandStatement(dataTable.Rows[i]);
                        OleDbCommand myCommand = new OleDbCommand(insertStatement, conn);
                        myCommand.ExecuteNonQuery();
                    }

                    isSaved = true;
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
			*/
            return isSaved;
        }

        //Create a new table in the Access database connected with OleDbConnection conn
        public static bool createNewTableInAccess(OleDbConnection conn, string tablename, string columnNamesandTypes)
        {
            bool isSucceeded = false;
            
			/*
			bool isInitConnectionOpen = (conn.State == ConnectionState.Open) ? true : false;
            string createTableStatement = "CREATE TABLE " + tablename + " (" + columnNamesandTypes + ")";


            OleDbCommand myCommand = new OleDbCommand(createTableStatement, conn);

            try
            {
                if (!isInitConnectionOpen)
                {
                    conn.Open();
                }

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
                else  //if table already exists
                {
                    OleDbCommand dropTableCommand = new OleDbCommand("DROP TABLE " + tablename, conn);
                    dropTableCommand.ExecuteNonQuery();
                    myCommand.ExecuteNonQuery();

                }
                isSucceeded = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("VCSDatabaseFunction.createNewTableInAccess() failed: " + ex.ToString());
                isSucceeded = false;
            }
            finally
            {
                //if the connection was not initially open, close it
                if (!isInitConnectionOpen) conn.Close();
            }
			*/
            return isSucceeded;
        }

        //open a new connection to an access database using the database file name
        public static OleDbConnection openAccessDatabase(string filename)
        {
            //create a connection statement
            
			/*
			string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + filename;
			
			OleDbConnection conn = null;
            try
            {
                //create and open a new connection
                conn = new OleDbConnection(connectionStatement);
                conn.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("VCSDatabaseFunctions.openAccessDatabase() failed: " + ex.Message);
                return conn;
            }

            return conn;
            */
			return null;
        }

        //delete a datatable from the access database
        public static bool deleteDataTable(OleDbConnection conn, string tablename)
        {
            bool isTableDeleted = false;
			/*
            bool isConnectionOpen = (conn.State == ConnectionState.Open) ? true : false;

            try
            {
                if (doesTableExistInDatabase(conn, tablename))
                {
                    //if connection is not open, open it
                    if (!isConnectionOpen)
                    {
                        conn.Open();
                    }

                    //create and execute a DELETE command
                    OleDbCommand dropTableCommand = new OleDbCommand("DROP TABLE " + tablename, conn);
                    dropTableCommand.ExecuteNonQuery();

                    isTableDeleted = true;
                }
                else
                {
                    MessageBox.Show("Table " + tablename + " does not exist in the database ");
                    if (!isConnectionOpen) conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("deleteDataTable() failed: " + ex.Message);
                isTableDeleted = false;
            }

            if (!isConnectionOpen) conn.Close();
			*/
			
            return isTableDeleted;
        }

        //delete the specified table from the access database 
        public static bool deleteDataTable(string dbFilename, string tablename)
        {
            bool isTableDeleted = false;
			
			/*
            //connection statement to connect to the "dbFilename" database
            string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + dbFilename;

            try
            {
                //if the table exist in the database, delete the table
                if (doesTableExistInDatabase(dbFilename, tablename))
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionStatement))
                    {
                        conn.Open();

                        OleDbCommand dropTableCommand = new OleDbCommand("DROP TABLE " + tablename, conn);
                        dropTableCommand.ExecuteNonQuery();

                    }
                    isTableDeleted = true;
                }
                else //if the table does not exist in the database, just display the following error message and return
                {
                    MessageBox.Show("Table " + tablename + " does not exist in " + dbFilename);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("VCSDatabaseFunctions.deleteDataTable() failed: " + ex.Message);
                isTableDeleted = false;
            }
			*/
            return isTableDeleted;
        }

        //retrieve data that meets criteria from the access database
        public static DataTable retrieveAccessDataTable(OleDbConnection conn, string tablename, string resultname, string where)
        {
			/*
            //data table to store the data that meets the criteria
            DataTable resultDataTable = new DataTable(resultname);
            bool isConnectionOpen = (conn.State == ConnectionState.Open) ? true : false;
            
            //create retrieval statement
            string sql = "SELECT * FROM [" + tablename + "]";
            //add criteria to the retrieve statement
            if (where != "") sql += " WHERE " + where;

            try
            {
                //if the connection is not open yet, open it
                if (!isConnectionOpen)
                {
                    conn.Open();
                }

                //retrieve
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(sql, conn);
                myDataAdapter.Fill(resultDataTable);
            }
            //when there is null reference 
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Null reference exception: " + ex.Message);
                if (!isConnectionOpen) conn.Close();

                return null;
            }
            //other exceptions
            catch (Exception ex)
            {
                MessageBox.Show("VCSDatabaseFunctions.retrieveAccessDataSet() failed: " + ex.Message);
                if (!isConnectionOpen) conn.Close();
                return null;
            }
            finally
            {
                //if the connection was not initially open, close it
                if (!isConnectionOpen) conn.Close();
            }

            return resultDataTable;
            */
			return null;
        }

        //check if the specified table exists in the database or not
        public static bool doesTableExistInDatabase(OleDbConnection conn, string tablename)
        {
			/*
            try
            {
                //if the connection is not open yet, open the connection to the database
                bool isConnectionOpen = (conn.State == ConnectionState.Open) ? true : false;
                if (!isConnectionOpen)
                {
                    conn.Open();
                }

                //check if the table with the table name exists in the database
                //if so, return true
                DataTable tableNames = conn.GetSchema("tables");
                for (int i = 0; i < tableNames.Rows.Count; i++)
                {
                    if (tableNames.Rows[i]["TABLE_NAME"].ToString().ToLower() == tablename.ToLower())
                    {
                        if (!isConnectionOpen) conn.Close();
                        return true;
                    }
                }

                //if the table was not found in the database, close the connection if the connection was not initially open
                //and return false
                if (!isConnectionOpen) conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("VCSDatabaseFunctions.doesTableExistInDatabase() failed: " + ex.Message);
                return false;
            }
            */
			return false;
        }

        //check if the table exists in the database
        public static bool doesTableExistInDatabase(string filename, string tablename)
        {
			/*
            //create a connection statement with the database filename
            string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + filename;

            try
            {
                //open connection
                OleDbConnection conn = new OleDbConnection(connectionStatement);
                conn.Open();

                //check if there is a table with the specified tablename
                //if exists, return true
                DataTable tableNames = conn.GetSchema("tables");
                for (int i = 0; i < tableNames.Rows.Count; i++)
                {
                    if (tableNames.Rows[i]["TABLE_NAME"].ToString().ToLower() == tablename.ToLower())
                    {
                        return true;
                    }
                }

                //if not, return false
                if (conn != null) conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("VCSDatabaseFunctions.doesTableExistInDatabase() failed: " + ex.Message);
                return false;
            }
			 
			 */
			return false;
        }

        //Add a new column to table
        public static bool addNewColumnToTable(string filename, string tablename, string columnname)
        {
            bool isSucceeded = false;
            
			/*
            //create connection statement with the database filename
            string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + filename;
            
            //create a statement to add a new column into the specified table
            string createColumnStatement = "ALTER TABLE " + tablename + " ADD " + columnname + " Char(80)";

            using (OleDbConnection conn = new OleDbConnection(connectionStatement))
            {
                //create a column addition command
                OleDbCommand addColumnCommand = new OleDbCommand(createColumnStatement, conn);
                try
                {
                    //execute the column addition command
                    conn.Open();
                    addColumnCommand.ExecuteNonQuery();
                    isSucceeded = true;
                }
                catch (Exception ex) //exception handling
                {
                    MessageBox.Show("addNewColumnToTable() failed: " + ex.Message);
                    isSucceeded = false;
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
			 
			*/
            return isSucceeded;
        }

        //add a new column into the specified table
        public static bool addNewColumnToTable(OleDbConnection conn, string tablename, string columnname)
        {
            bool isSucceeded = false;
            
			/*
			bool isConnectionOpen = (conn.State == ConnectionState.Open) ? true : false;

            //create a statement to add a new column into the specified table
            string createColumnStatement = "ALTER TABLE " + tablename + " ADD " + columnname + " Char(80)";

            OleDbCommand addColumnCommand = new OleDbCommand(createColumnStatement, conn);
            try
            {
                //if connection is not open yet, open it
                if (!isConnectionOpen)
                {
                    conn.Open();
                }

                //add a new column
                addColumnCommand.ExecuteNonQuery();
                isSucceeded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("addNewColumnToTable() failed: " + ex.Message);
                isSucceeded = false;
            }
            finally
            {
                //if the connection was not initially open, close it
                if (!isConnectionOpen) conn.Close();
            }
			*/
            return isSucceeded;
        }

        //Create new table in Access DB
        public static bool createNewTableInAccess(string filename, string tablename, string columnNamesandTypes)
        {
            bool isSucceeded = false;
			
			/*
            //create connection statement with the database filename
            string connectionStatement = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + filename;

            //a statement to add a new table to the specified database
            string createTableStatement = "CREATE TABLE " + tablename + " (" + columnNamesandTypes + ")";

            //create a new connection
            using (OleDbConnection conn = new OleDbConnection(connectionStatement))
            {
                //create a new command with the statement
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
                    else  //if table already exists
                    {
                        //delete the existing table first
                        OleDbCommand dropTableCommand = new OleDbCommand("DROP TABLE " + tablename, conn);
                        dropTableCommand.ExecuteNonQuery();
                        
                        //create a new table
                        myCommand.ExecuteNonQuery();

                    }
                    isSucceeded = true;
                }
                catch (System.Exception ex) //error handling
                {
                    MessageBox.Show("VCSDatabaseFunctions.createNewTableInAccess() failed: " + ex.ToString());
                    isSucceeded = false;
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            
            */
            return isSucceeded;
        }

        //internal method to create a insert command statement
        private static string createInsertCommandStatement(DataRow dataRow)
        {
            string insertStatement = "";
			
			/*
            try
            {

                //get the table instance (or reference? or just information?) that the data row belongs to
                DataTable table = dataRow.Table;

                //create insert statement
                //add table name
                insertStatement = "INSERT INTO " + table.TableName + " ( ";

                //add column names
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

                //add values for each column
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
            catch (System.Exception ex)
            {
                MessageBox.Show("VCSDatabaseFunctions.createInsertCommandStatement() failed: " + ex.Message);
                return "";
            }
			 
			*/
            //return the completed statement
            return insertStatement;
        }

    }
}
