<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManageTowers.aspx.cs" Inherits="Admin_ManageTowers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript" src="../Scripts/yahoo-dom-event.js"></script>
    <script type="text/javascript" src="../Scripts/connection-min.js"></script>
    <script type="text/javascript" src="../Scripts/json-min.js"></script>
    <script type="text/javascript" src="../Scripts/history-min.js"></script>
    <script type="text/javascript" src="../Scripts/element-min.js"></script>
    <script type="text/javascript" src="../Scripts/paginator-min.js"></script>
    <script type="text/javascript" src="../Scripts/datasource-min.js"></script>
    <script type="text/javascript" src="../Scripts/event-delegate-min.js"></script>
    <script type="text/javascript" src="../Scripts/datatable-min.js"></script>
    <script type="text/javascript" src="../Scripts/yuiloader.js"></script>
    <script type="text/javascript" src="../Scripts/button-min.js"></script>
    <script type="text/javascript" src="../Scripts/container-min.js"></script>
    <script type="text/javascript" src="../Scripts/container_core-min.js"></script>
    <script type="text/javascript" src="../Scripts/menu-min.js"></script>    
    <script type="text/javascript" src="../Scripts/autocomplete-min.js"></script>
    <script type="text/javascript" src="../Scripts/animation-min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.0.min.js"></script>

     
    <table style="float:left">
        <tr align="left">
            <td>Enter Site Id:</td>
            <td> <div id="divsiteid" class="autocomplete"><input id="siteidinput" type="text" /><div id="siteidcontainer" style ="text-align:left"></div></div> </td>           
            <td> <input id="btnfind" type="button" value="Search By Site Id" /></td>
            <td>Enter POP:</td>
            <td> <div id="divpop" class="autocomplete"><input id="popinput" type="text" /><div id="popcontainer" style ="text-align:left"></div></div> </td>           
            <td> <input id="btnpop" type="button" value="Search By POP" /></td>
        </tr>
        <tr align="left">
            <td>Enter Engineer Name:</td>
            <td> <div id="divengname" class="autocomplete"><input id="engnameinput" type="text" /><div id="engnamecontainer" style ="text-align:left"></div></div> </td>           
            <td> <input id="btnengname" type="button" value="Search By Engineer Engineer" /></td>
            <td>Enter District:</td>
            <td> <div id="dvidistrict" class="autocomplete"><input id="districtinput" type="text" /><div id="districtcontainer" style ="text-align:left"></div></div> </td>           
            <td> <input id="btndistrict" type="button" value="Search By District" /></td>
        </tr>
    </table>

    <br /> <br /> <br /> <br />

    <div id="dt-example">
        <div id="dt-options"><a id="dt-options-link" href="#">Table Options</a></div>
        <div id="columnshowhide"></div>
    </div>

    <div id="dt-dlg" class="inprogress">
        
        <span class="corner_tr"></span>
        <span class="corner_tl"></span>
        <span class="corner_br"></span>
        <span class="corner_bl"></span>
        <div class="hd" style="display:none;">
                Choose which columns you would like to see:
            </div>
        <div id="dt-dlg-picker" class="bd" style="overflow: auto;">
            
        </div>
        <div id="loading" class ="log"></div>
    </div>

    <div id="Edit_From" class="savefrm">
    </div>
    <script src="../Scripts/TowersImp.js" type="text/javascript"></script>

</asp:Content>

