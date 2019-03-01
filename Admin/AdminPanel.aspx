<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminPanel.aspx.cs" Inherits="Admin_AdminPanel" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <table>
       <tr>
        <td align ="left">
            <table>
             <tr valign ="top">
                    <th><h3>Manage Users/Roles:</h3></th>
                </tr>
                 <tr>
            <td>
                <asp:TextBox ID="txtrolename" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btncreate" runat="server" Text="Create Role" 
                    onclick="btncreate_Click" />
            </td>
        </tr>
        <tr>
            <td>
                Available Users
            </td>
            <td>
                Available Roles
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="lstusers" runat="server"></asp:ListBox> 
            </td>
                <td>
                <asp:ListBox ID="lstRoles" runat="server"></asp:ListBox> 
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnassign" runat="server" Text="Assign Role To User" 
                    onclick="btnassign_Click" />
            </td>
            <td>
                <asp:Button ID="btnremove" runat="server" Text="Remove User From User" 
                    onclick="btnremove_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btndelete" runat="server" Text="Delete Roles." 
                    onclick="btndelete_Click" />
            </td>
            <td>
                <asp:Button ID="btndeleteuser" runat="server" Text="Delete Users" OnClick="btndeleteuser_Click" />
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
            </table>
        </td>
        <td valign ="top" align="left">
            <table>
                <tr valign ="top">
                    <th><h3>Update DataBase:</h3></th>
                </tr>
                <tr>
                   <td>Choose The "Towers.xsl" <br /> File of 2003/07/10 versions.</td>
                    <td align ="left">
                        <asp:FileUpload ID="FileUpload1" runat="server" />  
                    </td>
                </tr>
                <tr>
                    <td colspan ="2" align ="right">
                        <asp:Button ID="btnupload" runat="server" Text="Upload" 
                            onclick="btnupload_Click" />
                    </td>
                </tr>
            </table>
        </td>
       </tr>      
    </table>
    <br />
    
    <table>
        <tr>
            <td colspan ="2">
                <asp:Label ID="Label1" runat="server" Text="" Font-Size ="Large" ForeColor ="BlueViolet"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

