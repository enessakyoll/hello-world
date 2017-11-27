<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hashing.aspx.cs" Inherits="Prolab1.Hashing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hashing</title>
    <link href="Style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_body">
            <div class="RandomStudent">
                <h3>1- Random Öğrenci Numarası ve Adı Oluşturma</h3>
                <asp:TextBox ID="TextBoxNumber" runat="server" Text="500" CssClass="textbox" Width="3%"></asp:TextBox>
                <asp:Button ID="ButtonRandomStudent" runat="server" Text="Create Random Student" CssClass="button" OnClick="ButtonRandomStudent_Click" />
                <asp:Label ID="LabelCreateResult1" runat="server" ForeColor="Red" CssClass="label"></asp:Label>
            </div>
            <div class="clc"></div>
            <div class="Create">
                <h3>2- Dosya Seçerek Bölen Kalan ve Kare Ortası Oluşturma</h3>
                <div class="algoritma">
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" />
                </div>
                <div class="clc"></div>
                <div class="algoritma">
                    <asp:Label ID="LabelAyirma" runat="server" Text="Txt File Ayirma Karakteri: " CssClass="label"></asp:Label>
                    <asp:TextBox ID="TextBoxAyirmaDegeri" runat="server" CssClass="textbox" Width="2%"></asp:TextBox>
                </div>
                <div class="clc"></div>
                <div class="algoritma">
                    <asp:Button ID="ButtonBolenKalan" runat="server" Text="Bölen Kalan & Kare Ortası Oluştur" CssClass="button" OnClick="ButtonBolenKalan_Click" />
                    <asp:Label ID="LabelCreateResult2" runat="server" ForeColor="Red" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="clc"></div>
            <div class="Calculate">
                <h3>3- Öğrenci Numarasına Göre Arama</h3>
                <asp:Label ID="LabelCalculate" runat="server" Text="Aranacak Numara: "></asp:Label>
                <asp:TextBox ID="TextBoxNumberValue" runat="server"></asp:TextBox>
                <asp:Button ID="ButtonCalc" runat="server" Text="Ara" OnClick="ButtonCalc_Click" />
            </div>
            <div class="clc"></div>
            <div class="CalculateResult">
                <asp:Label ID="LabelLineer" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label> <br />
                <asp:Label ID="LabelBolenKalan" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label> <br />
                <asp:Label ID="LabelKareOrtasi" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            </div>
            <div class="clc"></div>
            <div class="Result">
                <h3>4- Sonuç:</h3>
                <asp:Label ID="LabelAçiklama" runat="server" Text="En Hızlı ve En Yavaş Yöntem:" CssClass="label" ForeColor="Red" Font-Bold="true"></asp:Label>
                <asp:Label ID="LabelSiralama" runat="server"  CssClass="label" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
