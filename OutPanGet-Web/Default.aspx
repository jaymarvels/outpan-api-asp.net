<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OutPanGet_Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>OutPan Get Example</h1>
    </div>

    <div>
        <!-- API KEY -->
        <div class="row">
            <label class="col-md-4 control-label" for="txtApiValue">API Key</label>
            <div class="col-md-4">
                <input id="txtApiValue" name="txtApiValue" type="text" placeholder="" class="form-control input-md" runat="server"/>
            </div>
            <asp:Button id="btnSetCredentials" name="btnSetCredentials" class="btn btn-primary" value="Set" runat="server" Text="Set" OnClick="btnSetCredentials_Click"/>
        </div>
        <!-- EAN Number -->
        <div class="row">
            <label class="col-md-4 control-label" for="txtEANNumber">GTIN/EAN</label>
            <div class="col-md-4">
                <input id="txtEANNumber" name="txtEANNumber" type="text" placeholder="" class="form-control input-md" runat="server"/>
            </div>
        </div>
        <!-- Request Type -->
        <div class="row">
            <label class="col-md-4 control-label" for="cboRequestType">Request</label>
            <div class="col-md-4">               
                <asp:DropDownList ID="cboRequestType" runat="server" CssClass="form-control">
                    <asp:ListItem Text="All" Value=""></asp:ListItem>
                    <asp:ListItem Text="Attributes" Value="attributes"></asp:ListItem>
                    <asp:ListItem Text="Images" Value="images"></asp:ListItem>
                    <asp:ListItem Text="Name" Value="name"></asp:ListItem>
                    <asp:ListItem Text="Video" Value="video"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Button id="btnGet" name="btnGet" class="btn btn-primary" value="Retrive Data" runat="server" Text="Retrieve Data" OnClick="btnGet_Click"/>
        </div>
        <!-- Response -->
        <div class="row">
            <label class="col-md-4 control-label" for="rTxtOutput">Output</label>
            <div class="col-md-4">
                <textarea class="form-control" id="rTxtOutput" name="rTxtOutput" rows="15" runat="server"></textarea>
            </div>
        </div>
    </div>

</asp:Content>

