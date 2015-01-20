<!--

FILE			: 	Default.aspx
PROJECT			: 	PROG2000 - Web Design and Development - Assignment 6 JSON & JQuery Text Editor
PROGRAMMER		: 	Linyan Becky Li
FIRST VERSION	: 	2014-12-05
DESCRIPTION		: 	Objectives: 
                        To learn and demonstrate the use of jQuery as a dynamic web technology
                    Requirements:
                        1. The client page must allow you to choose a text file from a list, open it, and populate an editable area on the page
                        a. The list of allowable files are to be populated from a directory called “MyFiles” that exists on the server
                        2. The user must be able to make simple text edits in the text edit area
                        3. The user must be able to save the text to the same file, or a new file on the server
                        a. Again the directory that the file is to be saved to is a directory called “MyFiles” on the server
                        b. You do NOT have to support multiple directories on the server.
                        4. Your text editor does NOT need to support any formatting requirements
                        a. It just captures text (like Notepad) – no need to allow bold, italics, etc.
                        b. You do NOT have to support cut/copy/paste programmatically within the editor
                        5. You must use jQuery to make an AJAX call to populate the file list on the client.
                        6. You must use jQuery to make an AJAX call to populate the editable text area on the web page.
                        7. You may use either JSP, ASP, PHP or ASP.NET as the server side technology.
                        8. The data interchange between the server and the client must be in JSON format.
                        a. This means that sending the file’s contents from the server to the client must be in JSON
                        b. This also means that sending the (potentially) updated file contents from the client to the server must also be in JSON
                        c. This also means that the list of editable files (sent from the server to the client) must be in JSON

    // this file contains the client side script
-->


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="A6_JSON_JQuery._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A6_JSON_JQuery Editor</title>
        <style type="text/css">
        .auto-style1 {
            width: 276px;
        }
        .auto-style2 {
            width: 33px;
        }
        #TextArea1 {
            height: 279px;
            width: 621px;
        }
        .auto-style5 {
            width: 743px;
        }
        .auto-style6 {
            width: 27px;
        }
        .auto-style7 {
            width: 26px;
        }
        #editorContents {
            height: 229px;
            width: 458px;
        }
    </style>

 <!--   jQuery source -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        var saveFileName = "";


        // ---------------------------------------------------------------------------------------
        // NAME:    $(document).ready()
        // PURPOSE: this is a jQuery call that calles updateFileList() when the page is loaded
        // ---------------------------------------------------------------------------------------
        // 
        $(document).ready(function () {
            updateFileList();
        });


        // ---------------------------------------------------------------------------------------
        // NAME:    updateFileList()
        // PURPOSE: this is a function that updates the file list
        // ---------------------------------------------------------------------------------------        
        function updateFileList()
        {
            var JSONObject = { "updateFileList": "true" };
            $.post("Default.aspx", JSONObject, updateCallback);
        }


        // ---------------------------------------------------------------------------------------
        // NAME:    updateCallback()
        // PURPOSE: this is a call back function that update the file list. it analyses the JSON object that server responded, extract the file name
        //          and put them to the select DOM object with the "fileList" ID
        // ---------------------------------------------------------------------------------------
        function updateCallback(data)
        {
            var jsonObj = data.fileList;
            var i = 0;

            // clean the select element
            document.getElementById("fileList").options.length = 0;

            // now walk through the JSON object and print out the key/value pairs
            for (i = 0; i < jsonObj.length; i++)
            {
                var option = document.createElement("option");
                option.text = jsonObj[i];
                document.getElementById("fileList").add(option);
            }
        }


        // ---------------------------------------------------------------------------------------
        // NAME:    jQueryOpenFile()
        // PURPOSE: this is a function that send open file request to the server in JSON using jQuery
        // ---------------------------------------------------------------------------------------
      function jQueryOpenFile()
      {
          document.getElementById("serverFeedback").innerHTML = "";
          var list = document.getElementById("fileList");
          var fileName = list.value;
          if (fileName != "") {

              saveFileName = fileName;

              // JSON
              var JSONObject = { "fileName": fileName };
              
              // jQuery, openCallback is the call back mothod when jQuery call is successful
              $.post("Default.aspx", JSONObject, openCallback);    //$.post() = $this.post()
          }
          else
          {
              // notify user if no file name has been selected from the file list
              document.getElementById("serverFeedback").innerHTML = "Please select a file first";
          }
      }


        // ---------------------------------------------------------------------------------------
        // NAME:    openCallback()
        // PURPOSE: this is a function that call back from jQueryOpenFile()
        // ---------------------------------------------------------------------------------------
      function openCallback(data)
      {
          $("#editorContents").html(data.fileContent);     //same as document.getElementById("editorContents").innerHTML...
      }


        // ---------------------------------------------------------------------------------------
        // NAME:    jQuerySaveFile()
        // PURPOSE: Will be called when the save button was pressed, send new contents asynchronous to server
        // ---------------------------------------------------------------------------------------
      function jQuerySaveFile()
      {
          // clear the message box for server's feedback
          document.getElementById("serverFeedback").innerHTML = "";

          if (saveFileName != "") {
              // save contents to a variable called "Contents"
              var Contents = document.getElementById("editorContents").innerHTML

              var JSONObject = { "saveContent": Contents, "SaveFileName" : saveFileName };
              $.post("Default.aspx", JSONObject, saveCallback);
          }
          else {
              // not sending new contents if no file name has been selected by user
              document.getElementById("serverFeedback").innerHTML = "Please select a file first";
          }
      }


        // ---------------------------------------------------------------------------------------
        // NAME:    saveCallBack()
        // PURPOSE: This is the function that will be automatically called when the asynchronous response comes back
        //   - it checks the JSON data that server returned back - it takes the response
        //     and uses it to update the message box for server's feedback
        // ---------------------------------------------------------------------------------------
      function saveCallback(data) {
            $("#serverFeedback").html(data.feedback);
      }


        // ---------------------------------------------------------------------------------------
        // NAME:    showSaveAs()
        // PURPOSE: Will be automatically called when the save as button was pressed, it makes a text box visible, within which
        //          user is able to enter the new file name for the contents
        // ---------------------------------------------------------------------------------------
      function showSaveAs() {
          // clear message box
          //document.getElementById("serverFeedback").innerHTML = "";
          $("#serverFeedback").html("");
          document.getElementById('saveAsInfo').style.display = 'block';
      }


        // ---------------------------------------------------------------------------------------
        // NAME:    jQuerySaveAs()
        // PURPOSE: Will be automatically called when the submit button for the new file name was pressed, send new contents 
        //            asynchronous to server
        // ---------------------------------------------------------------------------------------
      function jQuerySaveAs() {

          //var contents = document.getElementById("editorContents").innerHTML;
          var contents = $("#editorContents").html();
          //var saveAsName = document.getElementById("saveAsName").value;
          var saveAsName = $("#saveAsName").val();

          var JSONObject = { "saveAsName": saveAsName, "contents": contents};
          //$.post() = $this.post()
          $.post("Default.aspx", JSONObject, saveAsCallback);
      }


        // ---------------------------------------------------------------------------------------
        // NAME:    saveAsCallBack()
        // PURPOSE: This is the function that will be automatically called when the asynchronous response comes back
        //      - it takes the JSON response and uses it to update the message box for server's feedback
        //      - it makes the text for new file name invisible again
        //      - it updates the file list again
        // ---------------------------------------------------------------------------------------
      function saveAsCallback(data) {
          
          $("#serverFeedback").html(data.feedback);
          document.getElementById('saveAsInfo').style.display = 'none';
          updateFileList();
      }
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <table style="width:80%; height: 97px;">
        <tr>
            <td colspan="4" style="font-size: x-large; text-align: center">AJAX Text Editor</td>
        </tr>
        <tr>
            <td class="auto-style1" colspan="3">File List</td>
            <td class="auto-style5">Editor</td>
        </tr>
        <tr>
            <td class="auto-style1" colspan="3">
                
                <!-- <asp:ListBox ID="fileList" runat="server" Height="239px" Width="245px"></asp:ListBox> -->
                <select name="fileList" id="fileList" size="15" ></select>                 
            </td>
            <td class="auto-style5">         
                <textarea id="editorContents" rows="50" cols="50"></textarea>
            </td>
        </tr>
        <tr>
            <td class="auto-style7" >
                <input id="open" type="button" value="Open" onclick="jQueryOpenFile();"/>
            </td>
            <td class="auto-style2">
                <input id="save" type="button" value="Save" onclick="jQuerySaveFile();"/>
            </td>
            <td class="auto-style6">
                <input id="saveAs" type="button" value="Save as" onclick="showSaveAs();"/>
            </td>
            <td class="auto-style5"><div id ="serverFeedback"></div>
                <div id ="saveAsInfo" style="display: none;">
                        Save as:
                        <input id="saveAsName" type="text" />
                        <input id="saveAsButton" type="button" value="Submit" onclick="jQuerySaveAs();"/>
                </div></td>
        </tr>
    </table>
    </form>
</body>
</html>
