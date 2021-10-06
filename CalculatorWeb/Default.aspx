<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="CalculatorWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .width-100{
            width: 100%;
            max-width: 100%;
        }

        .clsButton {
            color: white;
            font-size: 20px;
            font-family: helvetica;
            text-decoration: none;
            border: 2px solid white;
            border-radius: 20px;
            transition-duration: .2s;
            -webkit-transition-duration: .2s;
            -moz-transition-duration: .2s;
            background-color: black;
            padding: 4px 30px;
        }

        .clsButton:hover {
            color: black;
            background-color: white;
            transition-duration: .2s;
            -webkit-transition-duration: .2s;
            -moz-transition-duration: .2s;
            border-color:dodgerBlue;
        }

        input[type=text]{
            width:100%;
            border:2px solid #aaa;
            border-radius:4px;
            margin:8px 0;
            outline:none;
            padding: 4px 30px;
            box-sizing:border-box;
            transition:.3s;
        }
  
        .txt-box:focus{
            border-color:dodgerBlue;
            box-shadow:0 0 8px 0 dodgerBlue;
        }
        
        .msg-box{
            background-color: lightgray;
        }

        .msg-box:focus{
            border-color:dimgrey;
            box-shadow:0 0 8px 0 dimgrey;
        }
    </style>
    <div class="jumbotron">
        <h1>DEMO</h1>
        <p class="lead">Demo calculating given string.</p>
    </div>

    <div class="row">
        <div class="col-md-10">
            <asp:TextBox ID="txtInput" runat="server" CssClass="width-100 txt-box"></asp:TextBox>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnCalculate" runat="server" Text="Calculate" CssClass="width-100 clsButton"/>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <asp:TextBox ID="txtResult" runat="server" ReadOnly="true" CssClass="width-100 msg-box"></asp:TextBox>
        </div>
    </div>

</asp:Content>
