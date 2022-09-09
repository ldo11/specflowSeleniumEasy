using BoDi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecflowSeleniumEasy.Hooks;
using System;
using System.Data;
using System.Data.SqlClient;
using TechTalk.SpecFlow;

namespace SpecflowSeleniumEasy.StepDefinitions
{
    [Binding]
    public class CoreSQLServerStepDefinitions
    {
        public ScenarioContext currentcontext;
        public Webelementsandvars elementvars;

        public CoreSQLServerStepDefinitions(ObjectContainer objectContainer, ScenarioContext currentcontext)
        {
            this.currentcontext = currentcontext;
            elementvars = new Webelementsandvars(currentcontext);
        }

        [When(@"Set up connection string ""([^""]*)""")]
        public void WhenSetUpConnectionString(string connectionstring)
        {
            currentcontext["LSQLSERVERCONNECTION"] = connectionstring;
        }

        [When(@"Execute SQL query ""([^""]*)"" and save result")]
        public void WhenExecuteSQLQueryAndSaveResult(string query)
        {
            try
            {
                if (currentcontext.ContainsKey(query))
                {
                    query = currentcontext[query].ToString();
                }
                DataTable dataTable = new DataTable();
                string connString = currentcontext["LSQLSERVERCONNECTION"].ToString();
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);
                conn.Close();
                da.Dispose();
                currentcontext["LSQLSERVERQUERIESRESULT"] = dataTable;
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message + ex.InnerException);
            }
        }

        [When(@"Save result at row ""([^""]*)"" and column ""([^""]*)"" to local variable ""([^""]*)""")]
        public void WhenSaveResultAtRowAndColumnToLocalVariable(string row, string col, string varname)
        {
            if (currentcontext.ContainsKey(row))
            {
                row = currentcontext[row].ToString();
            }
            if (currentcontext.ContainsKey(col))
            {
                col = currentcontext[col].ToString();
            }
            
            DataTable dataTable = (DataTable)currentcontext["LSQLSERVERQUERIESRESULT"];
            string value = dataTable.Rows[Int32.Parse(row)][Int32.Parse(col)].ToString();
            currentcontext[varname] = value;
        }

        [Then(@"Verify result at row ""([^""]*)"" and column ""([^""]*)"" contains ""([^""]*)""")]
        public void ThenVerifyResultAtRowAndColumnContains(string row, string col, string value)
        {
            if (currentcontext.ContainsKey(row))
            {
                row = currentcontext[row].ToString();
            }
            if (currentcontext.ContainsKey(col))
            {
                col = currentcontext[col].ToString();
            }
            if (currentcontext.ContainsKey(value))
            {
                value = currentcontext[value].ToString();
            }
            DataTable dataTable = (DataTable)currentcontext["LSQLSERVERQUERIESRESULT"];
            string dbvalue = dataTable.Rows[Int32.Parse(row)][Int32.Parse(col)].ToString();

            StringAssert.Contains(dbvalue, value, "Query result do not contains " + value);

        }
        [Then(@"Verify result contains list below")]
        public void ThenVerifyResultContainsListBelow(Table table)
        {
            DataTable dataTable = (DataTable)currentcontext["LSQLSERVERQUERIESRESULT"];
            foreach (TableRow row in table.Rows)
            {
                StringAssert.Contains(dataTable.Rows[0][row["columnName"]].ToString(), row["Value"].ToString());
            }
        }

    }
}
