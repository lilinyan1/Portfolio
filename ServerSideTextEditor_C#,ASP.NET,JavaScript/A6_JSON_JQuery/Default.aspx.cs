/*
FILE			: 	Default.aspx.cs
PROJECT			: 	PROG2000 - Web Design and Development - Assignment 6 JSON & JQuery Text Editor
PROGRAMMER		: 	Linyan Becky Li
FIRST VERSION	: 	2014-12-05
DESCRIPTION		: 	Contains the server's side code for Defult.aspx
*/

// include files
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.IO;

namespace A6_JSON_JQuery
{
    public partial class _Default : System.Web.UI.Page
    {
        string path;  //file path for the contents that this AJAX editor modifies

        // ---------------------------------------------------------------------------------------
        // NAME:    Page_Load()
        // PURPOSE: Event handler that occurs when this aspx was loaded, got triggered whenever a button was pressed
        // ---------------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            // path = Server.MapPath("/MyFiles/");   // Server.MapPath("/") returns the physical path to the root of the domain name, it doesn't work because it is trying to create folder outside of the wwwroot folder in IIS
            path = Server.MapPath("~/MyFiles/");    // returns the physical path to the root of the application, ref:http://stackoverflow.com/questions/275781/server-mappath-server-mappath-server-mappath-server-mappath
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // analysing the JSON object that client passed to server
            if (Request.Form["fileName"] != null)
            {
                // received open file request
                string fileName = Request.Form["fileName"];

                //byte[] bytes = System.IO.File.ReadAllBytes(path + fileName);          // another way to read the file
                //string fileContent = System.Text.Encoding.UTF8.GetString(bytes);
                string fileContent="";
                if (File.Exists(path + fileName))
                {
                    using (StreamReader reader = new StreamReader(path + fileName))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
                //fileContent.Replace("\\n", "\\\\n");             //wrong
                var newstr = fileContent.Replace("\n", "\\n");    //right, JSON doesn't work well with newline and carriage, must escape the \ (backslash)
                newstr = newstr.Replace("\r", "\\r");
                string json2 = "{\"fileContent\":\"" + newstr + "\"}";
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";     
                Response.Write(json2);
                Response.End();
            }
            else if (Request.Form["updateFileList"] != null)
            {
                // received request to update the file list
                SendFileList();
            }
            // run this when the JSON "save" request was received
            else if (Request.Form["saveContent"] != null)
            {
                string fileName = Request.Form["SaveFileName"];
                string contents = Request.Form["saveContent"];
                using (StreamWriter _testData = new StreamWriter(path + fileName, false))
                {
                    _testData.WriteLine(contents); // Write the file.
                }

                //send server's feedback
                string json = "{\"feedback\":\"Contents was saved to " + fileName + "\"}";
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(json);
                Response.End();
            }

            // run this when the JSON "save as" request was received
            //else if (Request.QueryString["saveAsName"] != null)     // wrong          
            else if (Request.Form["saveAsName"] != null)              //right
            {
                string fileName = Request.Form["saveAsName"];
                string contents = Request.Form["contents"];

                // client entered a non-existing file name, create this new file
                if (!File.Exists(path + fileName))
                {
                    // Create a file to write to. 
                    using (StreamWriter sw = File.CreateText(path + fileName))
                    {
                        sw.WriteLine(contents, false);
                        sw.Close();
                    }
                    //send server's feedback
                    string json = "{\"feedback\":\"Contents was saved as " + fileName + "\"}";
                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    Response.Write(json);
                    Response.End();
                }

                // client entered a existing file name, no new file is created, send server's feedback
                else
                {
                    //send server's feedback
                    string json = "{\"feedback\":\"File with name of " + fileName + " exist, please try again\"}";
                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    Response.Write(json);
                    Response.End();
                }
            }
            else if (Request.Form["updateFileList"] != null)         //right
            {
                SendFileList();
            }
        }


        // ---------------------------------------------------------------------------------------
        // NAME:    SendFileList()
        // PURPOSE: scan the content's folder, sond JSON object to reload the file list
        // ---------------------------------------------------------------------------------------
        public void SendFileList()
        {

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] fileInfo = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
            string json = "{ \"fileList\":[";

            foreach (FileInfo fileInfoItem in fileInfo)
            {
                json += "\""+ fileInfoItem.Name + "\",";
            }

            json = json.Remove(json.Length - 1);        // remove the ',' at the end
            json += "]}";                               // add "]}" to complete the JSON string
            Response.Clear();
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(json);
            Response.End();
        }
    }
}
