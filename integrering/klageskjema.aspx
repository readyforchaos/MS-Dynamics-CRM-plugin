<%@ Page Language="C#" AutoEventWireup="true" CodeFile="klageskjema.aspx.cs" Inherits="klageskjema" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Konfekt AS</title>
    <link href="css/kontaktOss.css" rel="stylesheet" />
    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <style type="text/css">
        .auto-style1
        {
            width: 138px;
        }

        .auto-style2
        {
            width: 309px;
        }

        .auto-style3
        {
            width: 490px;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div id="container">

            <div id="content">
                <header>
                    <img src="img/logo.png" />
                    <nav>
                        <ul class="nav nav-pills">
                            <li>
                                <a href="default.aspx"><i class="icon-home"></i>Hjem</a>
                            </li>
                            <li class="active"><a href="klageskjema.aspx"><i class="icon-envelope"></i>Henvendelse</a></li>
                        </ul>
                    </nav>
                </header>

                
                    <asp:Literal  id="litSuccess" runat="server" />
                <div class="hero-unit main-unit">

                    <div class="span10">
                        <div class="well">
                            <div class="jumbotron">
                                <h4>Henvendelse</h4>
                                <table>
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="lblTittel" runat="server" Text="Tittel:"></asp:Label></td>
                                        <td class="auto-style3">
                                            <asp:TextBox ID="txtTittel" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvTittel" runat="server"
                                                ControlToValidate="txtTittel"
                                                ErrorMessage="Tittel må fylles ut"
                                                ForeColor="Red"> Tittel må fylles ut</asp:RequiredFieldValidator></td>
                                        <td class="auto-style2">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblNavn" runat="server" Text="Navn:"></asp:Label></td>
                                        <td class="auto-style3">
                                            <asp:TextBox ID="txtNavn" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvNavn" runat="server"
                                                ControlToValidate="txtNavn"
                                                ErrorMessage="Navn må fylles ut"
                                                ForeColor="Red"> Navn må fylles ut</asp:RequiredFieldValidator></td>
                                        <td class="auto-style2">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="lblSakstype" runat="server" Text="Sakstype:"></asp:Label></td>
                                        <td class="auto-style3">
                                            <asp:DropDownList ID="ddlSakstype" runat="server">
                                                <asp:ListItem>- : -</asp:ListItem>
                                                <asp:ListItem Value="1">Spørsmål</asp:ListItem>
                                                <asp:ListItem Value="2">Problem</asp:ListItem>
                                                <asp:ListItem Value="3">Forespørsel</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSak" runat="server"
                                                ControlToValidate="ddlSakstype"
                                                ErrorMessage=" Du må velge en sakstype"
                                                ForeColor="red"
                                                InitialValue="- : -"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="lblTilfredsstille" runat="server" Text="Tilfredsstillelse"></asp:Label>
                                        </td>
                                        <td class="auto-style3">
                                            <asp:DropDownList ID="ddlTilfredsstillelse" runat="server">
                                                <asp:ListItem>- : -</asp:ListItem>
                                                <asp:ListItem Value="1">Svært fornøyd</asp:ListItem>
                                                <asp:ListItem Value="2">Fornøyd</asp:ListItem>
                                                <asp:ListItem Value="3">Nøytral</asp:ListItem>
                                                <asp:ListItem Value="4">Misfornøyd</asp:ListItem>
                                                <asp:ListItem Value="5">Svært misfornøyd</asp:ListItem>
                                            </asp:DropDownList>


                                            <asp:RequiredFieldValidator ID="rfvTilfreds" runat="server"
                                                ControlToValidate="ddlTilfredsstillelse"
                                                ErrorMessage=" Du må velge et tilfredsnivå"
                                                ForeColor="red"
                                                InitialValue="- : -"></asp:RequiredFieldValidator>


                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="lblBeskrivelse" runat="server" Text="Beskrivelse:"></asp:Label>



                                            <asp:RequiredFieldValidator ID="rfvDesc" runat="server"
                                                ControlToValidate="txaBeskrivelse"
                                                ErrorMessage="*"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>



                                        </td>
                                        <td class="auto-style3">
                                            <asp:TextBox ID="txaBeskrivelse" runat="server" TextMode="MultiLine" Height="200px" Width="453px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button Text="Send Inn henvendelse" runat="server" ID="btnSendInnKlage" OnClick="btnSendInnKlage_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>
        <script src="http://code.jquery.com/jquery-latest.js"></script>
        <script src="js/bootstrap.min.js"></script>
    </form>
</body>
</html>

